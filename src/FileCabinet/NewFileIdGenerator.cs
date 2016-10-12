using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileCabinet
{
    /// <summary>
    /// Used to generate file ids for newly uploaded files
    /// </summary>
    /// <returns>The new file id</returns>
    public delegate string NewFileIdGenerator();
}
