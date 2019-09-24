using System.IO.Abstractions;
using Apache.Log.AccessLog;
using Apache.Log.Configuration;
using Apache.Log.Data;
using Apache.Log.Repository;
using Apache.Log.Resource;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Apache.Log.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
