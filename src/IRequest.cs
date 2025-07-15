namespace PatternsMediator
{
    /// <summary>
    /// Marker interface for request objects in the Mediator pattern.
    /// This interface establishes the contract that all requests must implement,
    /// specifying the type of response they expect.
    /// </summary>
    /// <typeparam name="TResponse">The type of response this request expects to receive.</typeparam>
    public interface IRequest<TResponse> 
    { 
        // This is intentionally empty - it's a marker interface that serves as a contract
        // to identify request types and their expected response types for the mediator pattern
    }
}
