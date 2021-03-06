﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileCabinet
{
    public interface IFilePointer
    {
        string FileId { get; }

        FileMetaData MetaData { get; }

        Task CopyAsync(Stream destination, CancellationToken cancellation);
    }
}
