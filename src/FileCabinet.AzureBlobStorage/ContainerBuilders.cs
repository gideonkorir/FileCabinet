using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileCabinet.AzureBlobStorage
{
    public static class ContainerBuilders
    {
        public static Func<string, Task<CloudBlobContainer>> CreateContainerPerDirectoryBuilder(CloudBlobClient client)
        {
            return async (name) =>
            {
                var container = client.GetContainerReference(name);
                await container.CreateIfNotExistsAsync().ConfigureAwait(false);
                return container;
            };
        }

        public static Func<string, Task<CloudBlobContainer>> CreateVirtualDirectoryBuilder(CloudBlobClient client, string containerName)
        {
            if (string.IsNullOrWhiteSpace(containerName))
                throw new ArgumentNullException(nameof(containerName));

            return async (name) =>
            {
                var container = client.GetContainerReference(containerName);
                await container.CreateIfNotExistsAsync().ConfigureAwait(false);
                return container;
            };
        }
    }
}
