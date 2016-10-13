using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileCabinet
{
    public class FileUpload : IFileUpload
    {
        readonly Stream stream;

        public string ContentType { get; }

        public string Description { get; }

        public string FileName { get; }

        public string UploaderId { get; }

        public FileUpload(Stream stream, string contentType, string fileName,
            string uploaderId, string description)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            this.stream = stream;
            ContentType = contentType;
            FileName = fileName;
            UploaderId = uploaderId;
            Description = description;
        }

        public void CopyTo(Stream steam)
        {
            this.stream.CopyTo(stream);
        }

        public Task CopyToAsync(Stream stream, CancellationToken cancellationToken)
        {
            return this.stream.CopyToAsync(stream, 81920, cancellationToken); //default buffer size is 81920 per docs
        }

        public Task<Stream> GetReadStreamAsync()
        {
            return Task.FromResult(stream);
        }
    }
}
