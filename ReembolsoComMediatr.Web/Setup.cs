using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReembolsoComMediatr.Web.Domain.Despesas.Repository;
using ReembolsoComMediatr.Web.Repository.Despesas;
using System.Reflection;

namespace ReembolsoComMediatr.Web
{
    public static class Setup
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
                    .AddAzureAD(options => Configuration.Bind("AzureAd", options));
        }

        public static void ConfigureCookie(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        public static void ConfigureMVC(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public static void ConfigureMediatR(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(Pipelines.MeasureTime<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(Pipelines.ValidateCommand<,>));

            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(ISolicitacaoWrite), typeof(SolicitacaoRepository));
            services.AddTransient(typeof(ISolicitacaoRead), typeof(SolicitacaoRepository));
            services.AddTransient(typeof(IFuncionarioRead), typeof(FuncionarioRepository));
        }
    }
}
