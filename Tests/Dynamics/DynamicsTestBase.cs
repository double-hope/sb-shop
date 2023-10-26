using FakeXrmEasy;
using Microsoft.Extensions.Configuration;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;

namespace Tests.Dynamics
{
    public abstract class DynamicsTestBase<T> where T : IPlugin, new()
    {
        protected readonly IConfiguration Configuration;
        protected IXrmContext Context;
        protected IOrganizationService Service;

        protected DynamicsTestBase()
        {
            Configuration = new ConfigurationBuilder()
                .AddUserSecrets("user_secret_guid")
                .Build();

            Garbage = new Dictionary<int, EntityReference>();
        }

        public Dictionary<int, EntityReference> Garbage { get; set; }

        public abstract void Execute();

        protected IXrmContext ExecutePlugin(Action<XrmFakedPluginExecutionContext> action)
        {
            var executionContext = Context.GetDefaultPluginContext();
            action(executionContext);
            Context.ExecutePluginWith<T>(executionContext);

            return Context;
        }
    }
}