using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsMediator
{
    /// <summary>
    /// Simple test class to validate the improved Mediator implementation.
    /// This demonstrates the enhanced type safety and error handling.
    /// Note: In a production environment, use a proper testing framework like xUnit, NUnit, or MSTest.
    /// </summary>
    public static class MediatorTests
    {
        /// <summary>
        /// Runs all tests to validate the mediator functionality.
        /// </summary>
        public static void RunAllTests()
        {
            Console.WriteLine("Running Mediator Tests...");
            
            TestBasicFunctionality();
            TestErrorHandling();
            TestTypeValidation();
            TestHandlerRegistration();
            
            Console.WriteLine("All tests completed successfully!");
        }
        
        /// <summary>
        /// Tests basic mediator functionality with the ping request.
        /// </summary>
        private static void TestBasicFunctionality()
        {
            Console.WriteLine("Testing basic functionality...");
            
            var mediator = new Mediator();
            mediator.RegisterHandler<PingRequest, string>(new PingHandler());
            
            var request = new PingRequest();
            var response = mediator.Send(request);
            
            if (response != "Pong")
            {
                throw new InvalidOperationException($"Expected 'Pong', got '{response}'");
            }
            
            Console.WriteLine("✓ Basic functionality test passed");
        }
        
        /// <summary>
        /// Tests error handling for null requests and missing handlers.
        /// </summary>
        private static void TestErrorHandling()
        {
            Console.WriteLine("Testing error handling...");
            
            var mediator = new Mediator();
            
            // Test null request
            try
            {
                mediator.Send<string>(null!);
                throw new InvalidOperationException("Should have thrown ArgumentNullException");
            }
            catch (ArgumentNullException)
            {
                // Expected
            }
            
            // Test missing handler
            try
            {
                var request = new PingRequest();
                mediator.Send(request);
                throw new InvalidOperationException("Should have thrown InvalidOperationException");
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("No handler registered"))
            {
                // Expected
            }
            
            Console.WriteLine("✓ Error handling test passed");
        }
        
        /// <summary>
        /// Tests type validation and handler registration.
        /// </summary>
        private static void TestTypeValidation()
        {
            Console.WriteLine("Testing type validation...");
            
            var mediator = new Mediator();
            
            // Test null handler registration
            try
            {
                mediator.RegisterHandler<PingRequest, string>(null!);
                throw new InvalidOperationException("Should have thrown ArgumentNullException");
            }
            catch (ArgumentNullException)
            {
                // Expected
            }
            
            Console.WriteLine("✓ Type validation test passed");
        }
        
        /// <summary>
        /// Tests handler registration functionality.
        /// </summary>
        private static void TestHandlerRegistration()
        {
            Console.WriteLine("Testing handler registration...");
            
            var mediator = new Mediator();
            
            // Test initial state
            if (mediator.GetHandlerCount() != 0)
            {
                throw new InvalidOperationException("Expected 0 handlers initially");
            }
            
            if (mediator.IsHandlerRegistered<PingRequest>())
            {
                throw new InvalidOperationException("Expected PingRequest handler to not be registered");
            }
            
            // Register handler
            mediator.RegisterHandler<PingRequest, string>(new PingHandler());
            
            // Test after registration
            if (mediator.GetHandlerCount() != 1)
            {
                throw new InvalidOperationException("Expected 1 handler after registration");
            }
            
            if (!mediator.IsHandlerRegistered<PingRequest>())
            {
                throw new InvalidOperationException("Expected PingRequest handler to be registered");
            }
            
            // Test duplicate registration
            try
            {
                mediator.RegisterHandler<PingRequest, string>(new PingHandler());
                throw new InvalidOperationException("Should have thrown InvalidOperationException for duplicate registration");
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("already registered"))
            {
                // Expected
            }
            
            Console.WriteLine("✓ Handler registration test passed");
        }
    }
}