namespace TestPlatform.Database.Seed.Seeders
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using TestPlatform.Database.Seed.Interfaces;

    internal abstract class BaseSeeder : ISeeder
    {
        protected BaseSeeder(IServiceProvider serviceProvider, ILogger logger, string jsonFileName)
        {
            this.ServiceProvider = serviceProvider;
            this.Logger = logger;
            this.JsonFileName = jsonFileName;
        }

        protected IServiceProvider ServiceProvider { get; private set; }

        protected ILogger Logger { get; private set; }

        protected string JsonFileName { get; private set; }

        public abstract Task SeedAsync();
    }
}
