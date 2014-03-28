using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Wallace.Nate.MetaDataExplorer;

namespace MetaDataExplorer.Tests
{
    /// <summary>
    /// Tests for the MetaDataExplorer assembly.
    /// </summary>
    public class Tests
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        /// <param name="args">Not used.</param>
        static void Main(string[] args)
        {
            TestGetClassesThatImplementInterface();
        }

        /// <summary>
        /// Test the GetClassesThatImplementInterface method.
        /// </summary>
        static void TestGetClassesThatImplementInterface()
        {
            Console.WriteLine("Classes that implement System.IDisposable:");

            using (AssemblyExplorer assembly = new AssemblyExplorer(@".\MetaDataExplorer.dll"))
                foreach (string c in assembly.GetClassesThatImplementInterface("System.IDisposable"))
                    Console.WriteLine(c);

            Console.WriteLine();
            Console.Write("press any key...");
            Console.ReadKey();
        }
    }
}
