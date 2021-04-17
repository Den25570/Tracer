# Tracer
Multi thread utility to track method invokes including nested invokes

## Usage:

Declare Tracer class instance:
```
ITracer tracer = new Tracer.Tracer();
```
Use **StartTrace** and **EndTace** methods of class **Trace** to track method invokes
```
public void MyMethod()
{
    _tracer.StartTrace();

    ...

    _tracer.StopTrace();
}
```
To get the result use **GetTraceResult** method and **ISerializer** interface to transorm data to **xml** or **json**
```
var result = tracer.GetTraceResult()
var json = serializerJSON.Serialize(result);
```
Example:
```
// Declare tracer
ITracer tracer = new Tracer.Tracer();
ISerializer serializerJSON = new SerializerJSON();

//Process
var foo = new Foo(tracer);
foo.MyMethod();
Thread thread = new Thread(test.MyMethod);
thread.Start();
thread.Join();

//Serialize
result = serializerXML.Serialize(tracer.GetTraceResult());
```

```
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Tracer;

namespace TracerConsoleApp
{
    public class Foo
    {
        private Bar _bar;
        private ITracer _tracer;

        internal Foo(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(_tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace();

            _bar.InnerMethod();

            _tracer.StopTrace();
        }
    }

    public class Bar
    {
        private ITracer _tracer;

        internal Bar(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod2()
        {
            _tracer.StartTrace();

            Thread.Sleep(50);

            _tracer.StopTrace();
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();

            InnerMethod2();
            Thread thread = new Thread(InnerMethod2);
            thread.Start();
            thread.Join();

            _tracer.StopTrace();
        }
    }
}

```
XML result:
```
<?xml version="1.0" encoding="utf-16"?>
<root>
  <thread id="1" time="10011">
    <method name="MyMethod" class="Foo" time="10011">
      <method name="InnerMethod" class="Bar" time="10010">
        <method name="InnerMethod2" class="Bar" time="5001" />
      </method>
    </method>
  </thread>
  <thread id="5" time="5000">
    <method name="InnerMethod2" class="Bar" time="5000" />
  </thread>
  <thread id="6" time="10008">
    <method name="MyMethod" class="Foo" time="10008">
      <method name="InnerMethod" class="Bar" time="10008">
        <method name="InnerMethod2" class="Bar" time="5000" />
      </method>
    </method>
  </thread>
  <thread id="9" time="5000">
    <method name="InnerMethod2" class="Bar" time="5000" />
  </thread>
</root>
```
