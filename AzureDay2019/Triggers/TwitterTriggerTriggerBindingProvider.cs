using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Triggers;
using Microsoft.Extensions.Logging;

namespace AzureDay2019.Triggers
{
    public class TwitterTriggerTriggerBindingProvider : ITriggerBindingProvider
    {
        private readonly ILoggerFactory _loggerFactory;

        public TwitterTriggerTriggerBindingProvider(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public Task<ITriggerBinding> TryCreateAsync(TriggerBindingProviderContext context)
        {
            var parameter = context.Parameter;
            var attribute = parameter.GetCustomAttribute<TwitterTriggerAttribute>(inherit: false);
            if (attribute == null)
            {
                return Task.FromResult<ITriggerBinding>(null);
            }

            return Task.FromResult<ITriggerBinding>(new TwitterTriggerBinding(parameter, _loggerFactory));
        }
    }
}