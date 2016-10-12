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
        Task<string> UploadAsync(IFileUpload fileUpload, CancellationToken cancellationToken);
        Task<IFilePointer> FindAsync(string fileId, CancellationToken cancellationToken);
    }
}
