using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileCabinet
{
    public static class Delegates
    {
        public static Func<DateTime> UtcClock = () => DateTime.UtcNow;

        public static NewFileIdGenerator FromGuid = () => Guid.NewGuid().ToString();
    }
}
