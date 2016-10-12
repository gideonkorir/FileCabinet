using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FileCabinet.AzureBlobStorage.Tests
{
    public class BlobClientFixture
    {
        public CloudBlobClient Client { get; }

        public BlobClientFixture()
        {
            var account = CloudStorageAccount.DevelopmentStorageAccount;
            Client = account.CreateCloudBlobClient();
        }
    }

    [CollectionDefinition(Name)]
    public class BlobClientFixtureCollection : ICollectionFixture<BlobClientFixture>
    {
        public const string Name = "BlobClientCollection";
    }
}
