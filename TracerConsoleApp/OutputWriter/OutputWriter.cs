using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TracerConsoleApp
{
    namespace OutputWriter {
        class OutputWriter : IOutputWriter
        {
            public void Write(string toWrite, StreamWriter writer = null)
            {
                if (writer != null)
                {
                    Console.SetOut(writer);
                }
                Console.WriteLine(toWrite);
            }
        }
    }
}
