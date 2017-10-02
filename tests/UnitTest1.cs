using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MemoryManager;
using System.Diagnostics;

namespace tests
{   

    [TestClass]
    public class UnitTest1
    {
        public MemoryCollection memory;

        private Process getTestProcess()
        {
            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                if (process.ProcessName == "Project64")
                {
                    return process;
                }
            }
            return null;
        }

        [TestMethod]
        public void TestMethod1()
        {
            Process p = getTestProcess();
            Assert.IsNotNull(p);

            memory = new MemoryCollection(p);

            foreach (IntPtr pointer in memory)
            {

            }

        }
    }
}
