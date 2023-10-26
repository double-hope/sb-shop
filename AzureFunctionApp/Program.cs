using Azure.Identity;
using Azure.Messaging.ServiceBus.Administration;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;

List<string> queueNames = await GetQueueNames();

//IHost host = Host.CreateDefaultBuilder(args)
//    .ConfigureServices((hostContext, services) =>
//    {
//        services.AddHostedService<Worker>();
//        services.AddAzureClients(clientBuilder =>
//        {
//            clientBuilder.AddSecretClient(
//                hostContext.Configuration.GetSection("KeyVault"));

//            clientBuilder.AddBlobServiceClient(
//                hostContext.Configuration.GetSection("Storage"));

//            clientBuilder.AddServiceBusClientWithNamespace(
//                hostContext.Configuration["ServiceBus:Namespace"]);

//            clientBuilder.UseCredential(new DefaultAzureCredential());

//            clientBuilder.ConfigureDefaults(
//                hostContext.Configuration.GetSection("AzureDefaults"));
//        });
//    })
//    .Build();

//await host.RunAsync();

async Task<List<string>> GetQueueNames()
{
    var adminClient = new ServiceBusAdministrationClient
        ("<your_namespace>.servicebus.windows.net", new DefaultAzureCredential());
    var queueNames = new List<string>();

    await foreach (QueueProperties queue in adminClient.GetQueuesAsync())
    {
        queueNames.Add(queue.Name);
    }

    return queueNames;
}