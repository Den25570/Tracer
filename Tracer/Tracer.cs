using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private Trace trace;

        public Tracer()
        {
            trace = new Trace();
        }

        public void StartTrace()
        {
            //get method info using reflection
            StackTrace stackTrace = new StackTrace();
            MethodBase callingMethod = stackTrace.GetFrame(1).GetMethod();
            string methodName = callingMethod.Name;
            string className = callingMethod.ReflectedType.Name;

            MethodTrace currentMethodTrace = new MethodTrace(methodName, className);
            trace.StartMethodTrace(currentMethodTrace);
            currentMethodTrace.StartCount();
        }

        public void StopTrace()
        {
            trace.EndLastMethodTrace();
        }

        public TraceResult GetTraceResult()
        {
            return formTraceResult();
        }

        private TraceResult formTraceResult()
        {
            TraceResult traceResult = new TraceResult();

            traceResult.threads = new TraceResult.Thread[trace.threads.Count];
            for(int i = 0; i < traceResult.threads.Length; i++)
            {
                ThreadTrace currentThreadTrace = trace.threads.ElementAt(i).Value;

                traceResult.threads[i] = new TraceResult.Thread();
                traceResult.threads[i].Id = currentThreadTrace.Id;
                traceResult.threads[i].Time = currentThreadTrace.TotalExecutionTime;

                traceResult.threads[i].Methods = formMethodsArray(currentThreadTrace.Methods.ToArray());
            }

            return traceResult;
        }

        private TraceResult.Thread.Method[] formMethodsArray(MethodTrace[] methodTraceArray)
        {
            TraceResult.Thread.Method[] methods = new TraceResult.Thread.Method[methodTraceArray.Length];

            for (int i = 0; i < methods.Length; i++)
            {
                MethodTrace currentMethodTrace = methodTraceArray[i];

                methods[i] = new TraceResult.Thread.Method();
                methods[i].Name = currentMethodTrace.MethodName;
                methods[i].Class = currentMethodTrace.ClassName;
                methods[i].Time = currentMethodTrace.ExecutionTime;

                methods[i].Methods = formMethodsArray(currentMethodTrace.Methods.ToArray());
            }

            return methods;
        }
    }
}
