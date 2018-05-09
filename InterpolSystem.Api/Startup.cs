namespace InterpolSystem.Api
{
    using AutoMapper;
    using Common.Mapping;
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Services.Blog;
    using Services.Blog.Implementations;
    using Services.Implementations;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder();

            builder.AddUserSecrets<Startup>();

            Configuration = builder.Build();

            foreach (var item in configuration.AsEnumerable())
            {
                Configuration[item.Key] = item.Value;
            }
        }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<InterpolDbContext>(options => options
                    .UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddTransient<IMissingPeopleService, MissingPeopleService>();
            services.AddTransient<IWantedPeopleService, WantedPeopleService>();
            services.AddTransient<IArticleService, ArticleService>();

            services.AddRouting(routing => routing.LowercaseUrls = true);

            services.AddMvc();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
