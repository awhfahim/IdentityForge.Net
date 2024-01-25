using Autofac;
using IdentityAuth.Models;

namespace IdentityAuth
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegisterModel>().AsSelf();
        }
    }
}
