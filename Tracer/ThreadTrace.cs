using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Tracer
{
    public class ThreadTrace
    {
        public int Id;
        public int TotalExecutionTime = 0;
        public ConcurrentBag<MethodTrace> Methods;

        private ConcurrentStack<MethodTrace> currentStackTrace;

        public ThreadTrace(int id)
        {
            this.currentStackTrace = new ConcurrentStack<MethodTrace>();
            this.TotalExecutionTime = 0;

            this.Id = id;
        }

        public void StartMethodTrace(MethodTrace method)
        {
            if (currentStackTrace.IsEmpty)
            {
                Methods.Add(method);
            }
            else
            {
                MethodTrace currentMethod = currentStackTrace.Last();
                currentMethod.Methods.Add(method);
            }

            currentStackTrace.Push(method);
        }

        public void StopMethodTrace()
        {
            MethodTrace currentMethod;
            if (currentStackTrace.TryPop(out currentMethod))
            {
                currentMethod.StopCount();
                TotalExecutionTime += currentMethod.ExecutionTime;
            }
            else
            {
                throw new Exception("Error occured while trying to stop tracing method. There no tracing methods in current thread");
            }
        }
    }
}
