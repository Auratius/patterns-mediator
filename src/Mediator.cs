using System;
using System.Collections.Generic;

namespace PatternsMediator
{
    /// <summary>
    /// Concrete implementation of the Mediator pattern.
    /// This class manages the registration and invocation of request handlers,
    /// providing a central point for request/response communication.
    /// </summary>
    public class Mediator : IMediator
    {
        /// <summary>
        /// Dictionary that maps request types to their corresponding handlers.
        /// Key: Type of the request (e.g., typeof(PingRequest))
        /// Value: The handler instance that can process that request type
        /// </summary>
        private readonly Dictionary<Type, object> _handlers = new();

        /// <summary>
        /// Registers a handler for a specific request type.
        /// This method ensures type safety by enforcing the generic constraints.
        /// </summary>
        /// <typeparam name="TRequest">The type of request the handler can process.</typeparam>
        /// <typeparam name="TResponse">The type of response the handler produces.</typeparam>
        /// <param name="handler">The handler instance to register.</param>
        /// <exception cref="ArgumentNullException">Thrown when the handler parameter is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when a handler is already registered for the request type.</exception>
        public void RegisterHandler<TRequest, TResponse>(IRequestHandler<TRequest, TResponse> handler)
            where TRequest : IRequest<TResponse>
        {
            // Validate input parameters
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler), "Handler cannot be null");
            }

            var requestType = typeof(TRequest);
            
            // Check if handler is already registered to prevent accidental overwrites
            if (_handlers.ContainsKey(requestType))
            {
                throw new InvalidOperationException($"Handler for request type {requestType.Name} is already registered");
            }

            // Store the handler with the request type as key
            _handlers[requestType] = handler;
        }

        /// <summary>
        /// Sends a request to the appropriate handler and returns the response.
        /// This method uses improved type safety and comprehensive error handling.
        /// </summary>
        /// <typeparam name="TResponse">The type of response expected from the handler.</typeparam>
        /// <param name="request">The request object to process.</param>
        /// <returns>The response from the handler.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request parameter is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when no handler is registered for the request type.</exception>
        /// <exception cref="InvalidCastException">Thrown when the handler cannot be cast to the expected type.</exception>
        public TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            // Validate input parameters
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Request cannot be null");
            }

            var requestType = request.GetType();
            
            // Check if handler exists for this request type
            if (!_handlers.TryGetValue(requestType, out var handlerObj))
            {
                throw new InvalidOperationException($"No handler registered for request type {requestType.Name}");
            }

            // Use dynamic dispatch to safely invoke the handler
            // This approach maintains type safety while avoiding unsafe casting
            try
            {
                // Create the handler interface type for safe casting
                var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));
                
                // Safely cast the handler object
                if (!handlerType.IsAssignableFrom(handlerObj.GetType()))
                {
                    throw new InvalidCastException($"Handler for request type {requestType.Name} cannot be cast to expected handler type");
                }

                // Use reflection to invoke the Handle method safely
                var handleMethod = handlerType.GetMethod("Handle");
                if (handleMethod == null)
                {
                    throw new InvalidOperationException($"Handle method not found on handler for request type {requestType.Name}");
                }

                // Invoke the handler and return the result
                var result = handleMethod.Invoke(handlerObj, new object[] { request });
                if (result is TResponse response)
                {
                    return response;
                }
                else
                {
                    throw new InvalidOperationException($"Handler returned unexpected result type for request {requestType.Name}");
                }
            }
            catch (Exception ex) when (!(ex is ArgumentNullException || ex is InvalidOperationException))
            {
                // Wrap any unexpected exceptions with context
                throw new InvalidOperationException($"Error occurred while processing request of type {requestType.Name}", ex);
            }
        }

        /// <summary>
        /// Checks if a handler is registered for the specified request type.
        /// This method is useful for validation before sending requests.
        /// </summary>
        /// <typeparam name="TRequest">The type of request to check.</typeparam>
        /// <returns>True if a handler is registered, false otherwise.</returns>
        public bool IsHandlerRegistered<TRequest>()
        {
            return _handlers.ContainsKey(typeof(TRequest));
        }

        /// <summary>
        /// Gets the number of registered handlers.
        /// This method is useful for diagnostics and testing.
        /// </summary>
        /// <returns>The number of registered handlers.</returns>
        public int GetHandlerCount()
        {
            return _handlers.Count;
        }
    }
}
