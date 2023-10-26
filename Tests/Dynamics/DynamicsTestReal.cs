using FakeXrmEasy;
using Microsoft.Extensions.Configuration;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;

namespace Tests.Dynamics
{
    public abstract class DynamicsTestReal<T> : DynamicsTestBase<T> where T : IPlugin, new()
    {
        protected DynamicsTestReal()
        {
            Context = new XrmRealContext(new CrmServiceClient(Configuration.GetConnectionString("CrmConnectionString")));
        }
    }
}