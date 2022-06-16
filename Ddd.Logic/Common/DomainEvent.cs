using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ddd.Logic.Common
{
    public class DomainEvent
    {
        public static List<Type> _handler;

        public DomainEvent()
        {
            _handler = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IHandler<>)))
                .ToList();
        }

        public static void Dispatch(IDomainEvent domainEvent)
        {
            foreach (var handlerType in _handler)
            {
                bool canHandleEvent = handlerType.GetInterfaces().
                    Any(x => x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(IHandler<>) &&
                    x.GenericTypeArguments[0] == domainEvent.GetType());

                if (canHandleEvent)
                {
                    dynamic handler = Activator.CreateInstance(handlerType);
                    handler.Handle((dynamic)domainEvent);
                }
            }
        }
    }
}
