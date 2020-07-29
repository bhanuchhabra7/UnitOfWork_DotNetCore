using DbUp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace UnitOfWorkExample_WebProject.Filters
{
    public class DbSetupStartupFilter : IStartupFilter
    {
        private readonly IConfiguration configuration;

        public DbSetupStartupFilter(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            var connectionString = configuration.GetConnectionString("ExampleConn");

            EnsureDatabase.For.SqlDatabase(connectionString);

            var dbUpEngine = DeployChanges.
                To.SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(typeof(Program).Assembly)
                .WithTransactionPerScript().LogToAutodetectedLog().Build();

            if (dbUpEngine.IsUpgradeRequired())
            {
                Console.WriteLine("Upgrade required");
                var result = dbUpEngine.PerformUpgrade();

                if (result.Successful)
                {
                    Console.WriteLine("upgrade done");
                }
                else
                {
                    Console.WriteLine("upgrade fail");
                }
            }

            return next;
        }
    }
}
