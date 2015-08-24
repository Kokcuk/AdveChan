namespace AdveChan.App_Start
{
    using System.Web.Mvc;
    using Common;
    using Entities;
    using SimpleInjector;
    using SimpleInjector.Integration.Web.Mvc;

    public static class IocContainer
    {
        public static void Configure()
        {
            var container = new Container();

            container.RegisterPerWebRequest<ChanContext>();

            var cryptoProvider = new DefaultCryptoProvider("RnObFfS6tQKWfHJmwCC1yGLtl/1T3oP+FGMZOyr77us=", "JOgQUaVye+6qoThNrqZDjA==");
            container.RegisterSingleton(typeof(ICryptoProvider), () => cryptoProvider);

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}