using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileCabinet
{
    public static class Directory
    {
        public static Task<IFilePointer[]> FetchAsync(this IDirectory directory, string[] fileIds,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var tasks = new Task<IFilePointer>[fileIds.Length];
            for (int i = 0; i < tasks.Length; i++)
                tasks[i] = directory.FindAsync(fileIds[i], cancellationToken);
            return Task.WhenAll(tasks);
        }

        public static Task<string[]> UploadAsync(this IDirectory directory, IEnumerable<IFileUpload> uploads,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var tasks = uploads.Select(c => directory.UploadAsync(c, cancellationToken));
            return Task.WhenAll(tasks);
        }
    }
}
