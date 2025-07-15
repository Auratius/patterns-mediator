namespace PatternsMediator
{
    /// <summary>
    /// Defines the contract for the Mediator pattern implementation.
    /// The Mediator acts as a central communication hub, decoupling the sender from the receiver.
    /// It routes requests to appropriate handlers without the sender needing to know about the handler.
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Sends a request to the appropriate handler and returns the response.
        /// </summary>
        /// <typeparam name="TResponse">The type of response expected from the handler.</typeparam>
        /// <param name="request">The request object that implements IRequest&lt;TResponse&gt;.</param>
        /// <returns>The response from the handler.</returns>
        /// <exception cref="InvalidOperationException">Thrown when no handler is registered for the request type.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the request parameter is null.</exception>
        TResponse Send<TResponse>(IRequest<TResponse> request);
    }
}
