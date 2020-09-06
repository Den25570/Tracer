﻿using System;
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
                    formatter.Serialize(textWriter, traceResult);
                    output = textWriter.ToString();
                }

                return output;
            }

        }
    }
}

