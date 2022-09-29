using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.SqlServer.Server;
using OdeToFood.Data.Models;
using OdeToFood.Data.Services;
using OdoToFood.Web;
using OdoToFood.Web.Models;
using System.DirectoryServices.AccountManagement;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Web
{
    public class ContainerConfig
    {
        internal static void RegisterContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<SqlRestaurantData>()
                .As<IRestaurantData>()
                .InstancePerRequest();
            builder.RegisterType<SqlMenuData>()
                .As<IMenuData>()
                .InstancePerRequest();

            builder.RegisterType<SqlMenuItemData>()
                    .As<IMenuItemData>()
                    .InstancePerRequest();

            builder.RegisterType<SqlRestaurantReviewData>()
                    .As<IRestaurantReviewData>()
                    .InstancePerRequest();

            builder.RegisterType<OdeToFoodDbContext>().InstancePerRequest();


            builder.RegisterType<ApplicationUser>()
                .As<IdentityUser>().InstancePerRequest();

            builder.RegisterType<Principal>();


            builder.RegisterType<ApplicationDbContext>().InstancePerRequest();


            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}