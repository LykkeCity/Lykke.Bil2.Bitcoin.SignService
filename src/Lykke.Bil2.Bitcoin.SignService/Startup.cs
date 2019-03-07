using System;
using JetBrains.Annotations;
using Lykke.Bil2.Bitcoin.SignService.Services;
using Lykke.Bil2.Bitcoin.SignService.Settings;
using Lykke.Bil2.Sdk.SignService;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NBitcoin;

namespace Lykke.Bil2.Bitcoin.SignService
{
    [UsedImplicitly]
    public class Startup
    {
        private const string IntegrationName = "Bitcoin";

        [UsedImplicitly]
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services.BuildBlockchainSignServiceProvider<AppSettings>(options =>
            {
                options.IntegrationName = IntegrationName;


                options.TransactionSignerFactory = ctx =>
                    new TransactionSigner
                    (
                       Network.GetNetwork(ctx.Settings.CurrentValue.NetworkType)
                    );

                options.AddressGeneratorFactory = ctx =>
                    new AddressGenerator
                    (
                        Network.GetNetwork(ctx.Settings.CurrentValue.NetworkType)
                    );
            });
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app)
        {
            app.UseBlockchainSignService(options =>
            {
                options.IntegrationName = IntegrationName;
            });
        }
    }
}
