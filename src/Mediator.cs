using System;
using System.Collections.Generic;

namespace PatternsMediator
{
    public class Mediator : IMediator
    {
        private readonly Dictionary<Type, object> _handlers = new();

        public void RegisterHandler<TRequest, TResponse>(IRequestHandler<TRequest, TResponse> handler)
            where TRequest : IRequest<TResponse>
        {
            _handlers[typeof(TRequest)] = handler;
        }

        public TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            var handler = (IRequestHandler<IRequest<TResponse>, TResponse>)_handlers[request.GetType()];
            return handler.Handle(request);
        }
    }
}
