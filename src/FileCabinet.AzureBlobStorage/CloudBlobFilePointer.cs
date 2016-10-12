using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileCabinet.AzureBlobStorage
{
    public class CloudBlobFilePointer : IFilePointer
    {
        readonly CloudBlockBlob blob;

        public string FileId { get; }

        public FileMetaData MetaData { get; }

        internal CloudBlobFilePointer(string fileId, CloudBlockBlob blob,
            FileMetaData metaData)
        {
            FileId = fileId;
            this.blob = blob;
            MetaData = metaData;
        }

        public Task CopyAsync(Stream destination, CancellationToken cancellation)
        {
            return blob.DownloadToStreamAsync(destination);
        }
    }
}
