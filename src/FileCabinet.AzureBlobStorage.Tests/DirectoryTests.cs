using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FileCabinet.AzureBlobStorage.Tests
{
    [Collection(BlobClientFixtureCollection.Name)]
    public class DirectoryTests
    {
        BlobClientFixture fixture;

        public DirectoryTests(BlobClientFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task Uploaded_Tasks_Can_Be_Found_Using_Virtual_Directories()
        {
            var utcNow = DateTime.UtcNow;
            var manager = new AzureBlobStorageDirectoryManager(ContainerBuilders.CreateVirtualDirectoryBuilder(fixture.Client, "tests"),
                () => utcNow, Delegates.FromGuid);
            var dir = await manager.GetDirectoryAsync(null, default(CancellationToken));

            var text = "awesome upload test";
            var upload = new FileUpload(CreateTextStream(text), "text/plain", "awesome.txt", "test", "my awesome tests");
            var id = await dir.UploadAsync(upload, default(CancellationToken));
            var handle = await dir.FindAsync(id, default(CancellationToken));

            Assert.Equal(id, handle.FileId);
            Assert.Equal(upload.ContentType, handle.MetaData.ContentType);
            Assert.Equal(upload.FileName, handle.MetaData.OriginalName);
            Assert.Equal(upload.Description, handle.MetaData.Description);
            Assert.Equal(upload.UploaderId, handle.MetaData.UploaderId);
            Assert.True(utcNow.Subtract(handle.MetaData.DateCreatedUtc) < TimeSpan.FromSeconds(1));

            var read = await GetTextFromFileHandle(handle);
            Assert.Equal(text, read);

        }

        [Fact]
        public async Task Uploaded_Tasks_Can_Be_Found_Using_Container_Directories()
        {
            var utcNow = DateTime.UtcNow;
            var manager = new AzureBlobStorageDirectoryManager(ContainerBuilders.CreateContainerPerDirectoryBuilder(fixture.Client),
                () => utcNow, Delegates.FromGuid);
            var dir = await manager.GetDirectoryAsync("contdir", default(CancellationToken));

            var text = "awesome upload test";
            var upload = new FileUpload(CreateTextStream(text), "text/plain", "awesome.txt", "test", "my awesome tests");
            var id = await dir.UploadAsync(upload, default(CancellationToken));
            var handle = await dir.FindAsync(id, default(CancellationToken));

            Assert.Equal(id, handle.FileId);
            Assert.Equal(upload.ContentType, handle.MetaData.ContentType);
            Assert.Equal(upload.FileName, handle.MetaData.OriginalName);
            Assert.Equal(upload.Description, handle.MetaData.Description);
            Assert.Equal(upload.UploaderId, handle.MetaData.UploaderId);
            Assert.True(utcNow.Subtract(handle.MetaData.DateCreatedUtc) < TimeSpan.FromSeconds(1));

            var read = await GetTextFromFileHandle(handle);
            Assert.Equal(text, read);

        }
        public static Stream CreateTextStream(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            return new MemoryStream(bytes);
        }

        public static async Task<string> GetTextFromFileHandle(IFilePointer pointer)
        {
            using (var stream = new MemoryStream())
            {
                await pointer.CopyAsync(stream, default(CancellationToken));
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}
