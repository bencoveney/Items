using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Bootstrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            Index index = new Index();
            File.WriteAllText("index.html", index.TransformText());
            System.Diagnostics.Process.Start("index.html");
        }
    }
}
