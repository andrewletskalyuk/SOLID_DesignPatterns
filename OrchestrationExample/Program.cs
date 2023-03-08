using Microsoft.Extensions.DependencyInjection;
using OrchestrationExample.Services;
using WorkflowCore.Interface;
using static System.Console;

namespace OrchestrationExample
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            IServiceProvider serviceProvider = ConfigureServices();
            var host = serviceProvider.GetService<IWorkflowHost>();
            if (host!=null)
            {
                host.RegisterWorkflow<SampleWorkFlow>();
                host.Start();
                host.StartWorkflow("Sample");
                ReadLine();
                host.Stop();
            }
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddWorkflow();
            services.AddTransient<LastStep>();
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}