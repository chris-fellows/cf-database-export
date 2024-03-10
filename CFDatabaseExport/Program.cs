using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using CFDatabaseExport.Forms;
using CFDatabaseExport.QueryHandlers;
using CFUtilities.Databases;
using CFUtilities.Logging;

namespace CFDatabaseExport
{
    static class Program
    {
        ///// <summary>
        ///// The main entry point for the application.
        ///// </summary>
        //[STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new MainForm());
        //}

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(ServiceProvider.GetRequiredService<MainForm>());
        }

        public static IServiceProvider ServiceProvider { get; private set; }
        /// <summary>
        /// Create a host builder to build the service provider
        /// </summary>
        /// <returns></returns>
        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    services.AddSingleton<ApplicationObject>((scope) =>
                    {
                        //return new ApplicationObject(Environment.CurrentDirectory);
                        return new ApplicationObject("D:\\Data\\Temp\\CFDatabaseExportData");
                    });
                    services.AddTransient<ILogWriter>((scope) =>
                    {
                        return new ConsoleLog();                        
                    });                   
                    services.AddTransient<IDatabaseInfoService>((scope) =>
                    {                        
                        return new XmlDatabaseInfoService(scope.GetRequiredService<ApplicationObject>().DatabaseInfoFolder);
                    });
                    services.AddTransient<IDatabaseTypeService>((scope) =>
                    {                        
                        return new XmlDatabaseTypeService(scope.GetRequiredService<ApplicationObject>().DatabaseTypeFolder);
                    });
                    services.AddTransient<IQueryFunctionService>((scope) =>
                    {                        
                        return new XmlQueryFunctionService(scope.GetRequiredService<ApplicationObject>().QueryFunctionFolder);
                    });
                    services.AddTransient<IQueryService>((scope) =>
                    {                        
                        return new XmlQueryService(scope.GetRequiredService<ApplicationObject>().QueryFolder);
                    });
                    services.RegisterAllTypes<IQueryHandler>(new[] { Assembly.GetExecutingAssembly() });
                    services.RegisterAllTypes<ISQLGenerator>(new[] { Assembly.GetExecutingAssembly() });
                    services.AddTransient<MainForm>();
                });
        }

        /// <summary>
        /// Registers all types implementing interface
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <param name="lifetime"></param>
        private static void RegisterAllTypes<T>(this IServiceCollection services, IEnumerable<Assembly> assemblies, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));
            foreach (var type in typesFromAssemblies)
            {                
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
            }
        }
    }
}
