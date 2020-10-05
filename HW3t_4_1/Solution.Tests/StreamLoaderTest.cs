namespace Solution.Tests
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;

    internal class StreamLoaderTest
    {
        [Test]
        public async Task StreamLoaderShouldReadEmptyStream()
        {
            var stream = new MemoryStream();
            var loader = new StreamLoader(new StreamReader(stream));

            Assert.AreEqual(string.Empty, await loader.LoadUntilDelimeterAsync(' '));
        }

        [Test]
        public async Task StreamLoaderShouldReadFullStream()
        {
            var buffer = Encoding.ASCII.GetBytes("buffer");
            var stream = new MemoryStream(buffer);
            var loader = new StreamLoader(new StreamReader(stream));

            Assert.AreEqual("buffer", await loader.LoadUntilDelimeterAsync(' ', '\n'));
        }

        [Test]
        public async Task StreamLoaderShouldReadUntilDelimeterStream()
        {
            var buffer = Encoding.ASCII.GetBytes("buffer buffer");
            var stream = new MemoryStream(buffer);
            var loader = new StreamLoader(new StreamReader(stream));

            Assert.AreEqual("buffer", await loader.LoadUntilDelimeterAsync(' '));
        }

        [Test]
        public async Task StreamLoaderShouldReadUntilFirstDelimeterStream()
        {
            var buffer = Encoding.ASCII.GetBytes("buffer buffer\nbuffer");
            var stream = new MemoryStream(buffer);
            var loader = new StreamLoader(new StreamReader(stream));

            Assert.AreEqual("buffer", await loader.LoadUntilDelimeterAsync('\n', ' '));
        }

        [Test]
        public async Task StreamLoaderShouldReadAfterDelimeterStream()
        {
            var buffer = Encoding.ASCII.GetBytes(" \n\n  buffer buffer\nbuffer");
            var stream = new MemoryStream(buffer);
            var loader = new StreamLoader(new StreamReader(stream));

            Assert.AreEqual("buffer", await loader.LoadUntilDelimeterAsync('\n', ' '));
        }
    }
}
