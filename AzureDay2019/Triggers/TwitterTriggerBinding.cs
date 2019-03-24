using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;
using LinqToTwitter;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Listeners;
using Microsoft.Azure.WebJobs.Host.Protocols;
using Microsoft.Azure.WebJobs.Host.Triggers;
using Microsoft.Extensions.Logging;

namespace AzureDay2019.Triggers
{
    public class TwitterTriggerBinding : ITriggerBinding
    {
        private readonly ParameterInfo _parameter;
        private readonly ILoggerFactory _loggerFactory;

        public TwitterTriggerBinding(ParameterInfo parameter, ILoggerFactory loggerFactory)
        {
            _parameter = parameter;
            _loggerFactory = loggerFactory;
        }

        public Task<ITriggerData> BindAsync(object value, ValueBindingContext context)
        {
            return Task.FromResult<ITriggerData>(new TwitterTriggerData((List<Status>)value, _parameter));
        }

        public Task<IListener> CreateListenerAsync(ListenerFactoryContext context)
        {
            return Task.FromResult<IListener>(new TwitterTriggerListener(context.Executor, _loggerFactory));
        }

        public ParameterDescriptor ToParameterDescriptor()
        {
            return new ParameterDescriptor();
        }

        public Type TriggerValueType => typeof(object);
        public IReadOnlyDictionary<string, Type> BindingDataContract => new ReadOnlyDictionary<string, Type>(new Dictionary<string, Type>()
        {
            {"object", typeof(object)}
        });
    }
}