using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HangfireContainer.Server
{
    public class BackgroundServer : BackgroundService
    {
        private readonly ILogger<BackgroundServer> _logger;
        private readonly IConfiguration config;

        public BackgroundJobServer Server { get; private set; }

        public BackgroundServer(
            ILogger<BackgroundServer> logger,
            IConfiguration config
        )
        {
            _logger = logger;
            this.config = config;
            GlobalConfiguration.Configuration.UseSqlServerStorage(config.GetConnectionString("DefaultConnection"));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background Job Server Started");
            Server = new BackgroundJobServer(new BackgroundJobServerOptions()
            {
                WorkerCount = Environment.ProcessorCount
            });
        }

        public override void Dispose()
        {
            Server.Dispose();
            base.Dispose();
        }
    }
}
