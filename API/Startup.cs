using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

namespace API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action = RouteParameter.Optional, id = RouteParameter.Optional }
            );            

            ConfigureCors(app);

            AtivandoAcessTokens(app);

            app.UseWebApi(config);
        }

        private void ConfigureCors(IAppBuilder app)
        {
            //var politica = new CorsPolicy();
            //politica.AllowAnyHeader = true;
            //politica.Origins.Add("http://localhost:40874");
            //politica.Origins.Add("http://localhost:40880");
            //politica.Methods.Add("GET");
            //politica.Methods.Add("POST");
            //var corsOptions = new CorsOptions
            //{
            //    PolicyProvider = new CorsPolicyProvider
            //    {
            //        PolicyResolver = context => Task.FromResult(politica)
            //    }
            //};
            //app.UseCors(corsOptions);
            app.UseCors(CorsOptions.AllowAll);
        }

        private void AtivandoAcessTokens(IAppBuilder app)
        {
            var opcoesConfiguracaoToken = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new ProviderDeTokensDeAcesso()
            };

            app.UseOAuthAuthorizationServer(opcoesConfiguracaoToken);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
