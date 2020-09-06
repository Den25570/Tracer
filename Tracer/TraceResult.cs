using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tracer
{
    [Serializable]
    [XmlRoot("trace_result")]
    public class TraceResult
    {
        [Serializable]
        public class Thread
        {
            [Serializable]
            public class Method
            {
                [XmlAttribute]
                public string Name { get; private set; }
                [XmlAttribute]
                public string Class { get; private set; }
                [XmlAttribute]
                public int Time;
                [XmlElement(ElementName = "method")]
                public Method[] Methods { get; private set; }

                public Method(){}

                public Method(MethodTrace currentMethodTrace)
                {
                    Name = currentMethodTrace.MethodName;
                    Class = currentMethodTrace.ClassName;
                    Time = currentMethodTrace.ExecutionTime;
                    Methods = formMethodsArray(currentMethodTrace.Methods.ToArray());
                }
                internal static Method[] formMethodsArray(MethodTrace[] methodTraceArray)
                {
                    Method[] methods = new Method[methodTraceArray.Length];

                    for (int i = 0; i < methods.Length; i++)
                    {
                        methods[i] = new Method(methodTraceArray[i]);
                    }
                    return methods;
                }
            }

            [XmlAttribute]
            public int Id { get; private set; }
            [XmlAttribute]
            public int Time { get; private set; }
            [XmlElement(ElementName = "method")]
            public Method[] Methods { get;  set; }

            public Thread() { }

            public Thread(ThreadTrace currentThreadTrace)
            {
                Id = currentThreadTrace.Id;
                Time = currentThreadTrace.TotalExecutionTime;
                Methods = Method.formMethodsArray(currentThreadTrace.Methods.ToArray());
            }
        }
        [XmlElement(ElementName = "thread")]
        public Thread[] threads { get; private set; }

        public TraceResult(){}

        public TraceResult(Trace trace)
        {
            threads = new Thread[trace.threads.Count];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(trace.threads.ElementAt(i).Value);            
            }
        }
    }
}
