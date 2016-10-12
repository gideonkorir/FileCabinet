using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace FileCabinet.AzureBlobStorage
{
    public class AzureBlobContainerDirectory : IDirectory
    {
        readonly CloudBlobContainer container;
        readonly NewFileIdGenerator fileIdGenerator;
        readonly Func<DateTime> utcClock;

        public string Name { get; }

        internal AzureBlobContainerDirectory(string name, CloudBlobContainer container, NewFileIdGenerator fileIdGenerator,
            Func<DateTime> utcClock)
        {
            Name = name;
            this.container = container;
            this.fileIdGenerator = fileIdGenerator;
            this.utcClock = utcClock;
        }

        public async Task<IFilePointer> FindAsync(string fileId, CancellationToken cancellationToken)
        {
            var path = GetFilePath(fileId);
            var block = container.GetBlockBlobReference(path);
            var metaData = await block.GetMetaDataAsync();
            var handle = new CloudBlobFilePointer(fileId, block, metaData);
            return handle;
        }

        public async Task<string> UploadAsync(IFileUpload fileUpload, CancellationToken cancellationToken)
        {
            var newId = fileIdGenerator();
            var path = GetFilePath(newId);
            var block = container.GetBlockBlobReference(path);
            block.SetMetaData(newId, fileUpload, utcClock);
            using (var reader = await fileUpload.GetReadStreamAsync().ConfigureAwait(false))
            {
                await block.UploadFromStreamAsync(reader).ConfigureAwait(false);
            }
            return newId;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        string GetFilePath(string fileId)
        {
            return string.IsNullOrWhiteSpace(Name) ? fileId : $"{Name}/{fileId}";
        }
    }
}
