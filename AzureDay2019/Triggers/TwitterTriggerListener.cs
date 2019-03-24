using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinqToTwitter;
using Microsoft.Azure.WebJobs.Host.Executors;
using Microsoft.Azure.WebJobs.Host.Listeners;
using Microsoft.Azure.WebJobs.Logging;
using Microsoft.Extensions.Logging;

namespace AzureDay2019.Triggers
{
    public class TwitterTriggerListener : IListener
    {
        private readonly ITriggeredFunctionExecutor _executor;
        private readonly ILoggerFactory _loggerFactory;
        private readonly TwitterContext _ctx;
        private readonly IDictionary<ulong, DateTime> _cache;
        private Task _runner;

        public TwitterTriggerListener(ITriggeredFunctionExecutor executor, ILoggerFactory loggerFactory)
        {
            _executor = executor;
            _loggerFactory = loggerFactory;
            var authorizer = new ApplicationOnlyAuthorizer
            {
                CredentialStore = new InMemoryCredentialStore
                {
                    ConsumerKey = "",
                    ConsumerSecret = ""
                }
            };

            authorizer.AuthorizeAsync().GetAwaiter().GetResult();

            _ctx = new TwitterContext(authorizer);
            _cache = new Dictionary<ulong, DateTime>();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _runner = Task.Run(Process, cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Cancel()
        {
        }

        private async Task Process()
        {
            while (true)
            {
                var logger = _loggerFactory.CreateLogger(LogCategories.CreateTriggerCategory("TwitterIngesting"));

                var results =
                    await
                        (from search in _ctx.Search
                            where search.Type == SearchType.Search &&
                                  search.Query == "azureday" && 
                                  search.ResultType == ResultType.Recent
                            orderby search.Until
                            select search)
                        .SingleOrDefaultAsync();

                logger.LogInformation("Fetched Twitter data: {0} tweets", results.Statuses.Count);

                var newTweets = new List<Status>();
                results.Statuses.ForEach(status =>
                {
                    if (_cache.ContainsKey(status.StatusID) == false)
                    {
                        newTweets.Add(status);
                        _cache.Add(status.StatusID, status.CreatedAt);
                    }
                });

                if (newTweets.Any())
                {
                    var data = new TriggeredFunctionData {TriggerValue = newTweets};
                    var result = await _executor.TryExecuteAsync(data, CancellationToken.None);

                    if (result.Succeeded)
                    {
                        logger.LogTrace("Function executed correctly");
                    }
                    else
                    {
                        logger.LogError(result.Exception, "Error during a function execution");
                    }
                }
                
                
                await Task.Delay(5000);
            }
        }
    }
}