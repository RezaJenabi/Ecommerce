//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Microsoft.AspNetCore.Routing;
//using Microsoft.AspNetCore.Mvc.Infrastructure;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Routing;
//using Microsoft.AspNetCore.Hosting;
//using System.Reflection;
//using MediatR;

//namespace Infrastructure.Api.Configuration
//{
//    public abstract class RootStartup
//    {
//        protected abstract string Title { get; }
//        protected virtual int[] Versions => new[] { 1 };
//        protected IConfiguration Configuration { get; set; }
//        protected IHostingEnvironment Environment { get; }
//        private IServiceCollection ServiceCollection { get; set; }


//        public RootStartup(IConfiguration configuration,
//            IHostingEnvironment environment)
//        {
//            Configuration = configuration;
//            Environment = environment;

//        }

//        public void ConfigureServices(IServiceCollection services)
//        {
//            ServiceCollection = services;

//            services.AddMediatR(Assembly.GetExecutingAssembly());

//            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
//            services.AddScoped<IUrlHelper>(factory =>
//            {
//                var actionContext = factory.GetService<IActionContextAccessor>().ActionContext;
//                return new UrlHelper(actionContext);
//            });
//            services.AddControllers(options =>
//            {
//                options.Filters.Add(new ValidatorActionFilter());
//            }).ConfigureApiBehaviorOptions(options =>
//            {
//                options.SuppressConsumesConstraintForFormFileParameters = true;
//                options.SuppressModelStateInvalidFilter = true;
//            }).AddFluentValidation(fv =>
//            {
//                fv.RegisterValidatorsFromAssemblyContaining<CreateCustomer>();
//                fv.ImplicitlyValidateChildProperties = true;
//            });

//            ConfigureLoggingManager();

//            OnConfigureServices(services);

//            services.Configure<RouteOptions>(options =>
//            {
//                options.AppendTrailingSlash = true;
//                options.LowercaseUrls = true;
//            });

//        }

//        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory,
//            IApplicationLifetime applicationLifetime)
//        {


//            OnConfigure(app);

//        }

//        protected void ConfigureLoggingManager()
//        {

//        }


//        protected virtual void OnConfigure(IApplicationBuilder app)
//        {
//        }


//        protected virtual void OnConfigureServices(IServiceCollection services)
//        {
//        }

//        protected virtual void OnRegisterToBus()
//        {
//        }

//        protected virtual void OnApplicationStarting()
//        {
//        }

//        protected virtual void OnApplicationStopping()
//        {
//        }

//    }
//}
