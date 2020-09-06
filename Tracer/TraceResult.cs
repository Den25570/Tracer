using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    [Serializable]
    public class TraceResult
    {
        [Serializable]
        public struct Thread
        {
            [Serializable]
            public struct Method
            {
                public string Name;
                public string Class;
                public int Time;
                public Method[] Methods;
            }

            public int Id;
            public int Time;
            public Method[] Methods;
        }

        public Thread[] threads;
    }
}
