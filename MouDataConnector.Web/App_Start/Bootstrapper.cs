using System.Reflection;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Mozu.Api.WebToolKit;

namespace MozuDataConnector.Web
{
    public class Bootstrapper : AbstractWebApiBootstrapper
    {
        public override void InitializeContainer(ContainerBuilder containerBuilder)
        {
            base.InitializeContainer(containerBuilder);
            containerBuilder.RegisterControllers(Assembly.GetExecutingAssembly());
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            Domain.Bootstrapper.Register(containerBuilder);


        }


    }
}