using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileCabinet
{
    public interface IFileUpload
    {
        string ContentType { get; }
        string FileName { get; }
        string Description { get; }
        string UploaderId { get; }

        void CopyTo(Stream steam);

        Task CopyToAsync(Stream stream, CancellationToken cancellationToken);
    }
}
