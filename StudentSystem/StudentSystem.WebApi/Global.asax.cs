namespace StudentSystem.WebApi
{
    using System.Data.Entity;
    using System.Web;
    using System.Web.Http;
    using Data.Migrations;
    using Data;

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentSystemDbContext, Configuration>());

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}