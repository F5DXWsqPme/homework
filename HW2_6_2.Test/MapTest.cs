using System;
using NUnit.Framework;

namespace HW2_6_2.Test
{
    public class MapTest
    {
        [Test]
        public void MapShouldThrowExceptionWhenFileDontExist()
        {
            Assert.Throws<ArgumentException>(() => new Map("DontExistingFile.DontExist"));
        }

        [Test]
        public void MapShouldThrowExceptionWhenFileWrong()
        {
            string path = "MapTest.txt";
            using (System.IO.StreamWriter writer = System.IO.File.CreateText(path))
            {
                writer.WriteLine(" W  W W ");
            }

            Assert.Throws<ArgumentException>(() => new Map(path));
        }

        [Test]
        public void MapShouldThrowExceptionWhenWrongWidth()
        {
            string path = "MapTest.txt";
            using (System.IO.StreamWriter writer = System.IO.File.CreateText(path))
            {
                writer.WriteLine(3);
                writer.WriteLine(2);
                writer.WriteLine(" W ");
                writer.Write("W W ");
            }

            Assert.Throws<ArgumentException>(() => new Map(path));
        }

        [Test]
        public void MapShouldThrowExceptionWhenWrongHeigth()
        {
            string path = "MapTest.txt";
            using (System.IO.StreamWriter writer = System.IO.File.CreateText(path))
            {
                writer.WriteLine(3);
                writer.WriteLine(2);
                writer.WriteLine(" W ");
                writer.WriteLine("W W");
                writer.Write(" W ");
            }

            Assert.Throws<ArgumentException>(() => new Map(path));
        }

        [Test]
        public void MapShouldThrowExceptionWhenWrongLetter()
        {
            string path = "MapTest.txt";
            using (System.IO.StreamWriter writer = System.IO.File.CreateText(path))
            {
                writer.WriteLine(3);
                writer.WriteLine(2);
                writer.WriteLine(" W ");
                writer.WriteLine("W?W");
            }

            Assert.Throws<ArgumentException>(() => new Map(path));
        }

        [Test]
        public void MapShouldThrowExceptionWhenInFirstCellWall()
        {
            string path = "MapTest.txt";
            using (System.IO.StreamWriter writer = System.IO.File.CreateText(path))
            {
                writer.WriteLine(3);
                writer.WriteLine(2);
                writer.WriteLine("WW ");
                writer.WriteLine("W?W");
            }

            Assert.Throws<ArgumentException>(() => new Map(path));
        }

        [Test]
        public void MapShouldGetWidthAndHeigth()
        {
            string path = "MapTest.txt";
            using (System.IO.StreamWriter writer = System.IO.File.CreateText(path))
            {
                writer.WriteLine(3);
                writer.WriteLine(2);
                writer.WriteLine(" W ");
                writer.Write("W W");
            }

            var map = new Map(path);
            map.GetWidthAndHegth(out int width, out int heigth);
            Assert.AreEqual(3, width);
            Assert.AreEqual(2, heigth);
        }

        [Test]
        public void MapShouldGetWalls()
        {
            string path = "MapTest.txt";
            using (System.IO.StreamWriter writer = System.IO.File.CreateText(path))
            {
                writer.WriteLine(3);
                writer.WriteLine(2);
                writer.WriteLine("   ");
                writer.Write("  W");
            }

            var map = new Map(path);
            Assert.AreEqual(true, map.IsWall(2, 1));
            Assert.AreEqual(false, map.IsWall(1, 1));
        }
    }
}
