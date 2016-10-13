using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FileCabinet.AzureBlobStorage.Tests
{
    [Collection(BlobClientFixtureCollection.Name)]
    public class DirectoryManagerTests
    {
        BlobClientFixture fixture;

        public DirectoryManagerTests(BlobClientFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Manager_Returns_Default_Directory_If_Name_IsNullOrWhitespace(string name)
        {
            var manager = new AzureBlobStorageDirectoryManager(ContainerBuilders.CreateVirtualDirectoryBuilder(fixture.Client, "myfolder"),
                Delegates.UtcClock, Delegates.FromGuid);
            var dir = await manager.GetDirectoryAsync(name, default(CancellationToken));
            Assert.Equal(AzureBlobStorageDirectoryManager.defaultDirectoryName, dir.Name);

            manager = new AzureBlobStorageDirectoryManager(ContainerBuilders.CreateContainerPerDirectoryBuilder(fixture.Client),
                Delegates.UtcClock, Delegates.FromGuid);
            dir = await manager.GetDirectoryAsync(name, default(CancellationToken));
            Assert.Equal(AzureBlobStorageDirectoryManager.defaultDirectoryName, dir.Name);

        }

    }
}
