using NUnit.Framework;
using MM26.IO;
//using UnityEngine;
//using UnityEngine.TestTools;

namespace Tests
{
    public class BufferTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestAppend()
        {
            // Use the Assert class to test conditions
            Buffer buffer = new Buffer(2);

            buffer.Content[0] = 0;
            buffer.Content[1] = 1;

            buffer.Append(2);

            Assert.Greater(buffer.Content.Length, 2);
            Assert.AreEqual(buffer.Content[0], 0);
            Assert.AreEqual(buffer.Content[1], 1);
        }
    }
}
