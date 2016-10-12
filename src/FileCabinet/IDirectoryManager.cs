using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileCabinet
{
    public interface IDirectoryManager
    {
        string Type { get; }
        IDirectory GetDirectory(string directoryName);
        Task<IDirectory> GetDirectoryAsync(string directoryName, CancellationToken cancellation);
    }
}
