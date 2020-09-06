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
        public struct Thread
        {
            [Serializable]
            public struct Method
            {
                [XmlAttribute]
                public string Name;
                [XmlAttribute]
                public string Class;
                [XmlAttribute]
                public int Time;
                [XmlElement(ElementName = "method")]
                public Method[] Methods;
            }
            [XmlAttribute]
            public int Id;
            [XmlAttribute]
            public int Time;
            [XmlElement(ElementName = "method")]
            public Method[] Methods;
        }
        [XmlElement(ElementName = "thread")]
        public Thread[] threads;
    }
}
