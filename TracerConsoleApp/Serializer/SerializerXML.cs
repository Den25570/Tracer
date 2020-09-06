using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using Tracer;
using TracerConsoleApp.Serializer;
using System.Xml.Serialization;
using System.Xml;

namespace TracerConsoleApp
{
    namespace Serializer
    {
        public class SerializerXML : ISerializer
        {
            public string Serialize(TraceResult traceResult)
            {
                string output;
                XmlSerializer formatter = new XmlSerializer(typeof(TraceResult));

                using (StringWriter textWriter = new StringWriter())
                {
                    var xmlwriter = new XmlTextWriter(Console.Out);
                    xmlwriter.Formatting = System.Xml.Formatting.Indented;
                    xmlwriter.Indentation = 4;

                    formatter.Serialize(xmlwriter, traceResult);
                    output = textWriter.ToString();
                }

                return output;
            }

        }
    }
}

