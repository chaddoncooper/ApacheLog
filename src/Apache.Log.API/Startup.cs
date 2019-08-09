using Apache.Log.AccessLog;
using Apache.Log.Configuration;
using Apache.Log.Data;
using Apache.Log.Repository;
using Apache.Log.Resource;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Abstractions;

namespace Apache.Log.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json",
                             optional: false,
                             reloadOnChange: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }
            Configuration = builder.Build();

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient<IFileSystem, FileSystem>();
            services.AddTransient<IFinder, Finder>();
            services.AddTransient<IParser, Parser>();
            services.AddTransient<IAnalyser, Analyser>();
            services.AddTransient<IAccessLogService, AccessLogService>();
            services.AddTransient<IWhitelist, Whitelist>();
            services.AddTransient<IBlacklist, Blacklist>();
            services.AddTransient<IBlacklistedResourceRepository, BlacklistedResourceRepository>();
            services.AddTransient<IWhitelistedResourceRepository, WhitelistedResourceRepository>();

            services.AddDbContext<ApacheLogContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ApacheLogContext")));
            var accessLogParserConfig = new AccessLogParserConfig();
            Configuration.GetSection("Sites:AccessLogParserConfig").Bind(accessLogParserConfig);
            services.AddSingleton(accessLogParserConfig);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod());
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
