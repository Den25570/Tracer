using System;
using System.IO;
using System.Threading;
using Tracer;
using TracerConsoleApp.OutputWriter;
using TracerConsoleApp.Serializer;

namespace TracerConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ITracer tracer = new Tracer.Tracer();
            ISerializer serializerJSON = new SerializerJSON();
            ISerializer serializerXML = new SerializerXML();
            IOutputWriter writer = new OutputWriter.OutputWriter();
            
            //Process
            var test = new Foo(tracer);
            test.MyMethod();
            Thread thread = new Thread(test.MyMethod);
            thread.Start();
            thread.Join();

            //Serialize
            string result = serializerXML.Serialize(tracer.GetTraceResult());
            StreamWriter fs = new StreamWriter("people.xml");

            //Write
            writer.Write(result);
            writer.Write(result, fs);

            fs.Close();
        }
    }
}
