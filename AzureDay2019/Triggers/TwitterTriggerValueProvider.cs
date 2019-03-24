using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;

namespace AzureDay2019.Triggers
{
    public class TwitterTriggerValueProvider : IValueProvider
    {
        private readonly object _value;

        public TwitterTriggerValueProvider(ParameterInfo parameter, object value)
        {
            _value = value;
            Type = parameter.ParameterType;
        }

        public Task<object> GetValueAsync()
        {
            return Task.FromResult(_value);
        }

        public string ToInvokeString()
        {
            return _value.ToString();
        }

        public Type Type { get; }
    }
}