using System;
using System.ServiceProcess;
using System.Runtime.InteropServices;

namespace PatternsMediator
{
    /// <summary>
    /// Entry point for the Mediator pattern demonstration application.
    /// This class handles application initialization, mediator setup,
    /// and service registration with comprehensive error handling.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Main entry point for the application.
        /// Initializes the mediator, registers handlers, and starts the service
        /// with proper error handling and platform considerations.
        /// </summary>
        static void Main()
        {
            try
            {
                // Initialize and configure the mediator
                var mediator = InitializeMediator();
                
                // Check if running on Windows before attempting to start as Windows Service
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    // Run as Windows Service
                    RunAsWindowsService(mediator);
                }
                else
                {
                    // Run as console application on non-Windows platforms
                    RunAsConsoleApplication(mediator);
                }
            }
            catch (Exception ex)
            {
                // Handle any critical startup errors
                Console.WriteLine($"Critical error during application startup: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Initializes the mediator and registers all necessary handlers.
        /// This method demonstrates proper mediator setup with error handling.
        /// </summary>
        /// <returns>A configured mediator instance.</returns>
        /// <exception cref="InvalidOperationException">Thrown when handler registration fails.</exception>
        private static IMediator InitializeMediator()
        {
            try
            {
                // Create a new mediator instance
                var mediator = new Mediator();
                
                // Register handlers for different request types
                // In a real application, you might use dependency injection
                // and automatic handler discovery/registration
                RegisterHandlers(mediator);
                
                // Validate that essential handlers are registered
                ValidateHandlerRegistration(mediator);
                
                return mediator;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to initialize mediator", ex);
            }
        }

        /// <summary>
        /// Registers all request handlers with the mediator.
        /// This method demonstrates proper handler registration with error handling.
        /// </summary>
        /// <param name="mediator">The mediator instance to register handlers with.</param>
        private static void RegisterHandlers(Mediator mediator)
        {
            try
            {
                // Register the ping handler
                mediator.RegisterHandler<PingRequest, string>(new PingHandler());
                
                // TODO: Register additional handlers as needed
                // Example: mediator.RegisterHandler<UserCreateRequest, User>(new UserCreateHandler());
                // Example: mediator.RegisterHandler<OrderProcessRequest, OrderResult>(new OrderProcessHandler());
                
                Console.WriteLine("All handlers registered successfully");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to register handlers", ex);
            }
        }

        /// <summary>
        /// Validates that essential handlers are properly registered.
        /// This method ensures the application has all required handlers before starting.
        /// </summary>
        /// <param name="mediator">The mediator instance to validate.</param>
        private static void ValidateHandlerRegistration(Mediator mediator)
        {
            // Validate that essential handlers are registered
            if (!mediator.IsHandlerRegistered<PingRequest>())
            {
                throw new InvalidOperationException("PingHandler is not registered");
            }
            
            // Check that at least one handler is registered
            if (mediator.GetHandlerCount() == 0)
            {
                throw new InvalidOperationException("No handlers are registered");
            }
            
            Console.WriteLine($"Validated {mediator.GetHandlerCount()} registered handlers");
        }

        /// <summary>
        /// Runs the application as a Windows Service.
        /// This method is only called on Windows platforms.
        /// </summary>
        /// <param name="mediator">The configured mediator instance.</param>
        private static void RunAsWindowsService(IMediator mediator)
        {
            try
            {
                Console.WriteLine("Starting as Windows Service...");
                
                // Create and start the service
                var service = new MediatorService(mediator);
                ServiceBase.Run(service);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error running as Windows Service: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Runs the application as a console application.
        /// This method is used on non-Windows platforms or for testing.
        /// </summary>
        /// <param name="mediator">The configured mediator instance.</param>
        private static void RunAsConsoleApplication(IMediator mediator)
        {
            try
            {
                Console.WriteLine("Running as console application...");
                Console.WriteLine("Platform: " + RuntimeInformation.OSDescription);
                
                // Demonstrate mediator usage
                DemonstrateMediator(mediator);
                
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error running as console application: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Demonstrates the mediator functionality with sample requests.
        /// This method shows how to use the mediator pattern in practice.
        /// </summary>
        /// <param name="mediator">The configured mediator instance.</param>
        private static void DemonstrateMediator(IMediator mediator)
        {
            try
            {
                // Run validation tests first
                Console.WriteLine("--- Running Validation Tests ---");
                MediatorTests.RunAllTests();
                Console.WriteLine("--- Tests Complete ---\n");
                
                Console.WriteLine("--- Mediator Demonstration ---");
                
                // Test the ping request
                var pingRequest = new PingRequest();
                var pingResponse = mediator.Send(pingRequest);
                Console.WriteLine($"Ping Request -> Response: {pingResponse}");
                
                // TODO: Add more demonstration examples
                // Example: var userRequest = new GetUserRequest { Id = 1 };
                // Example: var user = mediator.Send(userRequest);
                // Example: Console.WriteLine($"User: {user.Name}");
                
                Console.WriteLine("--- Demonstration Complete ---\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during mediator demonstration: {ex.Message}");
                throw;
            }
        }
    }
}
