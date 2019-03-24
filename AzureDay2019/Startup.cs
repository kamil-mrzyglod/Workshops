using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AzureDay2019;
using AzureDay2019.Triggers;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.WebJobs.Host.Protocols;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;

[assembly: WebJobsStartup(typeof(CustomStartup))]

namespace AzureDay2019
{
    public class CustomStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddExtension(typeof(QueryStringExtensionConfigProvider));
            builder.AddExtension(typeof(TwitterTriggerExtensionConfigProvider));

            builder.Services.TryAddSingleton<TwitterTriggerTriggerBindingProvider>();
        }
    }

    [Extension("QueryString", "AzureDay2019")]
    public class QueryStringExtensionConfigProvider : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            context.AddBindingRule<QueryStringBindingAttribute>()
                .Bind(new QueryStringBindingProvider());
        }
    }

    public class QueryStringBindingProvider : IBindingProvider
    {
        public Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            return Task.FromResult((IBinding) new QueryStringBinding());
        }
    }

    public class QueryStringBinding : IBinding
    {
        public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
        {
            throw new NotImplementedException();
        }

        public Task<IValueProvider> BindAsync(BindingContext context)
        {
            return Task.FromResult(
                (IValueProvider) new QueryStringValueProvider(
                    (IDictionary<string, string>) context.BindingData["Query"]));
        }

        public ParameterDescriptor ToParameterDescriptor()
        {
            return new ParameterDescriptor();
        }

        public bool FromAttribute => true;
    }

    public class QueryStringValueProvider : IValueProvider
    {
        public IDictionary<string, string> Parameters { get; }

        public QueryStringValueProvider(IDictionary<string, string> parameters)
        {
            Parameters = parameters;
        }

        public Task<object> GetValueAsync()
        {
            return Task.FromResult((object)Parameters);
        }

        public string ToInvokeString()
        {
            return "QueryStringBinding";
        }

        public Type Type { get; }
    }

    [Binding]
    public class QueryStringBindingAttribute : Attribute
    {
    }
}