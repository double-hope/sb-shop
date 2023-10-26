using FakeXrmEasy;
using Microsoft.Xrm.Sdk;

namespace Tests.Dynamics
{
    public abstract class DynamicsTestFake<T> : DynamicsTestBase<T> where T : IPlugin, new()

    {
        protected DynamicsTestFake()
        {
            Context = new XrmFakedContext();
            Service = Context.GetOrganizationService();
        }
    }
}