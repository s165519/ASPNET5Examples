﻿using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using DAL;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using BLL;

namespace ImageProcessorTest
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940


        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }
            builder.AddEnvironmentVariables();
            

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ImageTestContext>(options => options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));
            

            //var pathToDoc = Configuration["Swagger:Path"];
            //services.ConfigureSwaggerDocument(options =>
            //{
            //    options.SingleApiVersion(new Info
            //    {
            //        Version = "v1",
            //        Title = "Picture service API",
            //        Description = "Add, resize and serves pictures"
            //    });
            //    options.OperationFilter(new Swashbuckle.SwaggerGen.XmlComments.ApplyXmlActionComments(pathToDoc));
            //});

            //services.ConfigureSwaggerSchema(options =>
            //{
            //    options.DescribeAllEnumsAsStrings = true;
            //    options.ModelFilter(new Swashbuckle.SwaggerGen.XmlComments.ApplyXmlTypeComments(pathToDoc));
            //});

            services.AddSwaggerGen();

            services.AddMvc();
            services.AddScoped<IPictureBLL, PictureBLL>();
            services.AddScoped<IDbPictures, DbPicture>();
            services.AddScoped<FileHandler, FileHandler>();
            services.AddScoped(repo =>
            {
                var conf = Configuration["Data:AzureWebJobsStorage:StorageConnectionString"];
                return CloudStorageAccount.Parse(conf);
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
            }

            app.UseIISPlatformHandler();

            app.UseStaticFiles();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name:"default",
                    template: "{controller=Picture}/{action=Get}/{id?}");
            });

            app.UseSwaggerGen();
            app.UseSwaggerUi();

        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
