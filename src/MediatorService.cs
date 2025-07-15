using System;
using System.ServiceProcess;

namespace PatternsMediator
{
    /// <summary>
    /// Windows Service implementation that demonstrates the use of the Mediator pattern.
    /// This service provides a foundation for building long-running applications
    /// that use the Mediator pattern for decoupled communication.
    /// 
    /// Note: This service is Windows-specific due to the ServiceBase inheritance.
    /// For cross-platform applications, consider using alternatives like
    /// Microsoft.Extensions.Hosting.BackgroundService or similar patterns.
    /// </summary>
    public class MediatorService : ServiceBase
    {
        /// <summary>
        /// The mediator instance used for request/response communication.
        /// This field is readonly to ensure the mediator reference doesn't change
        /// after service initialization.
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the MediatorService class.
        /// </summary>
        /// <param name="mediator">The mediator instance to use for request processing.</param>
        /// <exception cref="ArgumentNullException">Thrown when the mediator parameter is null.</exception>
        public MediatorService(IMediator mediator)
        {
            // Validate input parameters - defensive programming practice
            if (mediator == null)
            {
                throw new ArgumentNullException(nameof(mediator), "Mediator cannot be null");
            }

            // Set the service name for Windows Service Manager
            ServiceName = "MediatorService";
            
            // Store the mediator instance
            _mediator = mediator;
        }

        /// <summary>
        /// Executes when the service starts.
        /// This method includes comprehensive error handling and logging
        /// to ensure service stability and troubleshooting capabilities.
        /// </summary>
        /// <param name="args">Command line arguments passed to the service.</param>
        protected override void OnStart(string[] args)
        {
            try
            {
                // Log service startup (in a real application, use proper logging framework)
                // Example: _logger.LogInformation("MediatorService starting...");
                
                // Example usage of mediator with error handling
                var request = new PingRequest();
                var response = _mediator.Send(request);
                
                // In a real-world scenario, you might:
                // - Initialize background workers
                // - Set up periodic tasks
                // - Configure event handlers
                // - Start listening for external requests
                
                // Log successful startup
                // Example: _logger.LogInformation("MediatorService started successfully. Response: {Response}", response);
            }
            catch (InvalidOperationException ex)
            {
                // Handle mediator-specific errors
                // Example: _logger.LogError(ex, "Mediator operation failed during service startup");
                Console.WriteLine($"Mediator error: {ex.Message}");
                
                // Stop the service gracefully if initialization fails
                Stop();
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors during startup
                // Example: _logger.LogError(ex, "Unexpected error occurred during service startup");
                Console.WriteLine($"Startup error: {ex.Message}");
                
                // Stop the service gracefully if initialization fails
                Stop();
            }
        }

        /// <summary>
        /// Executes when the service stops.
        /// This method includes cleanup logic and error handling
        /// to ensure graceful shutdown.
        /// </summary>
        protected override void OnStop()
        {
            try
            {
                // Log service shutdown
                // Example: _logger.LogInformation("MediatorService stopping...");
                
                // Cleanup logic should be implemented here:
                // - Dispose of resources
                // - Cancel background tasks
                // - Close connections
                // - Flush logs
                
                // Example cleanup operations:
                // await _backgroundTaskCancellation.CancelAsync();
                // _httpClient?.Dispose();
                // _database?.Dispose();
                
                // Log successful shutdown
                // Example: _logger.LogInformation("MediatorService stopped successfully");
            }
            catch (Exception ex)
            {
                // Log any errors during shutdown, but don't re-throw
                // as this is the cleanup phase
                // Example: _logger.LogError(ex, "Error occurred during service shutdown");
                Console.WriteLine($"Shutdown error: {ex.Message}");
            }
        }

        /// <summary>
        /// Executes when the service is paused.
        /// Override this method if your service supports pausing.
        /// </summary>
        protected override void OnPause()
        {
            try
            {
                // Implement pause logic here
                // Example: _backgroundWorker.Pause();
                base.OnPause();
            }
            catch (Exception ex)
            {
                // Log pause errors
                // Example: _logger.LogError(ex, "Error occurred during service pause");
                Console.WriteLine($"Pause error: {ex.Message}");
            }
        }

        /// <summary>
        /// Executes when the service is resumed after being paused.
        /// Override this method if your service supports resuming.
        /// </summary>
        protected override void OnContinue()
        {
            try
            {
                // Implement resume logic here
                // Example: _backgroundWorker.Resume();
                base.OnContinue();
            }
            catch (Exception ex)
            {
                // Log resume errors
                // Example: _logger.LogError(ex, "Error occurred during service resume");
                Console.WriteLine($"Resume error: {ex.Message}");
            }
        }
    }
}
