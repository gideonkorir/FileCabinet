using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileCabinet.AzureBlobStorage
{
    public static class ContainerBuilders
    {
        public static Func<string, CloudBlobContainer> CreateContainerPerDirectoryBuilder(CloudBlobClient client)
        {
            return (name) =>
            {
                return client.GetContainerReference(name);
            };
        }

        public static Func<string, CloudBlobContainer> CreateVirtualDirectoryBuilder(CloudBlobClient client, string containerName)
        {
            if (string.IsNullOrWhiteSpace(containerName))
                throw new ArgumentNullException(nameof(containerName));

            return (name) =>
            {
                return client.GetContainerReference(containerName);
            };
        }
    }
}
