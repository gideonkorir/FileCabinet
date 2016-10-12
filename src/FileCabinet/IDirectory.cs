using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileCabinet
{
    public interface IDirectory
    {
        string Name { get; }
        string Upload(IFileUpload fileUplad);
        Task<string> UploadAsync(IFileUpload fileUpload, CancellationToken cancellationToken);
        string Find(string fileId);
        Task<string> FindAsync(string fileId, CancellationToken cancellationToken);
    }
}
