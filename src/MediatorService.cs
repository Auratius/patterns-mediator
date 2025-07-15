using System.ServiceProcess;

namespace PatternsMediator
{
    public class MediatorService : ServiceBase
    {
        private readonly IMediator _mediator;

        public MediatorService(IMediator mediator)
        {
            ServiceName = "MediatorService";
            _mediator = mediator;
        }

        protected override void OnStart(string[] args)
        {
            // Example usage of mediator
            var request = new PingRequest();
            var response = _mediator.Send(request);
        }

        protected override void OnStop()
        {
            // Cleanup logic
        }
    }
}
