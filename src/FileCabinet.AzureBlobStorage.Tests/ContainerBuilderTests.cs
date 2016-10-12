using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FileCabinet.AzureBlobStorage.Tests
{
    [Collection(BlobClientFixtureCollection.Name)]
    public class ContainerBuilderTests
    {
        BlobClientFixture fixture;
        public ContainerBuilderTests(BlobClientFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [InlineData("containera")]
        [InlineData("containerb")]
        [InlineData("containerc")]
        public async Task Virtual_Container_Builder_Returns_Reference_To_Same_Container(string containerName)
        {
            var del = ContainerBuilders.CreateVirtualDirectoryBuilder(fixture.Client, containerName);
            var container = await del(Guid.NewGuid().ToString());
            Assert.Equal(containerName, container.Name);
        }

        [Theory]
        [InlineData("containere")]
        [InlineData("containerf")]
        public async Task Container_Per_Dir_Returns_Reference_To_Specified_Container(string containerName)
        {
            var del = ContainerBuilders.CreateContainerPerDirectoryBuilder(fixture.Client);
            var container = await del(containerName);
            Assert.Equal(containerName, container.Name);
        }
    }
}
