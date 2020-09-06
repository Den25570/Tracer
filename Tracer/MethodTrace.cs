using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Tracer
{
    public class MethodTrace
    {
        public string MethodName;
        public string ClassName;
        public UInt32 ExecutionTime;

        private DateTime startTime;

        public ConcurrentBag<MethodTrace> Methods;

        public MethodTrace(string methodName, string className)
        {
            this.MethodName = methodName;
            this.ClassName = className;
        }

        public void StartCount()
        {
            startTime = DateTime.UtcNow;
        }

        public void StopCount()
        {
            DateTime stopTime = DateTime.UtcNow;
            TimeSpan span = stopTime - startTime;
            ExecutionTime = (UInt32)span.TotalMilliseconds;
        }
    }
}
