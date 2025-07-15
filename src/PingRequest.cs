using System;

namespace PatternsMediator
{
    /// <summary>
    /// Example request implementation for the Mediator pattern.
    /// This request represents a simple ping operation that expects a string response.
    /// It demonstrates how to implement the IRequest interface for specific use cases.
    /// </summary>
    public class PingRequest : IRequest<string> 
    { 
        // This class is intentionally empty as it's a simple marker request
        // In more complex scenarios, you might include properties for request data
        // For example: public string Message { get; set; }
    }

    /// <summary>
    /// Handler for PingRequest that processes ping operations.
    /// This handler demonstrates the implementation of IRequestHandler interface
    /// and provides a simple response to ping requests.
    /// </summary>
    public class PingHandler : IRequestHandler<PingRequest, string>
    {
        /// <summary>
        /// Processes a ping request and returns a pong response.
        /// This method demonstrates basic request handling in the Mediator pattern.
        /// </summary>
        /// <param name="request">The ping request to process.</param>
        /// <returns>A string response containing "Pong".</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request parameter is null.</exception>
        public string Handle(PingRequest request)
        {
            // Validate input parameters - defensive programming practice
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "PingRequest cannot be null");
            }

            // In a real-world scenario, you might perform additional operations here:
            // - Logging the request
            // - Validating request data
            // - Calling external services
            // - Performing business logic
            
            // Return the standard pong response
            return "Pong";
        }
    }
}
