using System.ServiceProcess;

namespace PatternsMediator
{
    static class Program
    {
        static void Main()
        {
            var mediator = new Mediator();
            mediator.RegisterHandler(new PingHandler());
            ServiceBase.Run(new MediatorService(mediator));
        }
    }
}
