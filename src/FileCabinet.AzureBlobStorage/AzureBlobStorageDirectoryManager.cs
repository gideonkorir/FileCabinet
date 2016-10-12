using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileCabinet.AzureBlobStorage
{
    public class AzureBlobStorageDirectoryManager : IDirectoryManager
    {
        const string defaultDirectoryName = "Documents";

        readonly Func<string, Task<CloudBlobContainer>> containerBuilder;
        readonly Func<DateTime> utcClock;
        readonly NewFileIdGenerator fileIdGenerator;
        
        public string Type { get; } = "Azure Blob Storage";

        public AzureBlobStorageDirectoryManager(Func<string, Task<CloudBlobContainer>> containerBuilder,
            Func<DateTime> utcClock, 
            NewFileIdGenerator fileIdGenerator)
        {
            if (containerBuilder == null)
                throw new ArgumentNullException(nameof(containerBuilder));
            if (utcClock == null)
                throw new ArgumentNullException(nameof(utcClock));
            if (fileIdGenerator == null)
                throw new ArgumentNullException(nameof(fileIdGenerator));
            this.containerBuilder = containerBuilder;
            this.utcClock = utcClock;
            this.fileIdGenerator = fileIdGenerator;
        }

        public async Task<IDirectory> GetDirectoryAsync(string directoryName, CancellationToken cancellation)
        {
            var name = string.IsNullOrWhiteSpace(directoryName) ? defaultDirectoryName : directoryName;
            var container = await containerBuilder(name);
            return new AzureBlobContainerDirectory(name, container,
                fileIdGenerator, utcClock);
        }

        string GetDirectoryNameOrDefault(string directoryName)
        {
            return string.IsNullOrWhiteSpace(directoryName) ? defaultDirectoryName : directoryName;
        }
    }
}
