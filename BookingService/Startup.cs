using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookingService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //var connection = @"Server=sql.data;Initial Catalog=BookingService;User=sa;Password=Pass@word;";
            //add db context with options
            //services.AddDbContext<BookingServiceContext>(options =>
            //{
            //    options.UseSqlServer(connection,
            //    sqlServerOptionsAction: sqlOptions =>
            //    {
            //        sqlOptions.
            //    MigrationsAssembly(
            //        typeof(Startup).
            //         GetTypeInfo().
            //          Assembly.
            //           GetName().Name);
                      
            //    //Configuring Connection Resiliency:
            //    sqlOptions.
            //        EnableRetryOnFailure(maxRetryCount: 5,
            //        maxRetryDelay: TimeSpan.FromSeconds(30),
            //        errorNumbersToAdd: null);

            //    });

            //    // Changing default behavior when client evaluation occurs to throw.
            //    // Default in EFCore would be to log warning when client evaluation is done.
            //    options.ConfigureWarnings(warnings => warnings.Throw(
            //        RelationalEventId.QueryClientEvaluationWarning));
            //});
             
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Booking - HTTP API",
                    Version = "v1",
                    Description = "The Booking Microservice HTTP API. This is a Data-Driven/CRUD microservice sample",
                    TermsOfService = "Terms Of Service"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Migrate
            //using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetRequiredService<BookingServiceContext>();
            //    context.Database.Migrate();
            //}


            app.UseMvc();
             
            app.UseSwagger()
           .UseSwaggerUI(c =>
           {
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
           });
        }
    }
}
