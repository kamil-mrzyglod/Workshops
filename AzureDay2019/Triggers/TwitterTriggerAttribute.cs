using System;
using Microsoft.Azure.WebJobs.Description;

namespace AzureDay2019.Triggers
{
    [AttributeUsage(AttributeTargets.Parameter)]
    [Binding]
    public class TwitterTriggerAttribute : Attribute
    {
    }
}