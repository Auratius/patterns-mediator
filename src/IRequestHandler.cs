namespace PatternsMediator
{
    /// <summary>
    /// Defines the contract for request handlers in the Mediator pattern.
    /// Each handler is responsible for processing a specific type of request
    /// and returning the appropriate response.
    /// </summary>
    /// <typeparam name="TRequest">The type of request this handler can process.</typeparam>
    /// <typeparam name="TResponse">The type of response this handler produces.</typeparam>
    public interface IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Processes the specified request and returns the response.
        /// </summary>
        /// <param name="request">The request to process.</param>
        /// <returns>The response produced by processing the request.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request parameter is null.</exception>
        TResponse Handle(TRequest request);
    }
}
