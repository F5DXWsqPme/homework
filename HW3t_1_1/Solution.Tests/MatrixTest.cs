using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Solution.Tests
{
    public class MatrixTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCaseSource(nameof(UseMultithreadingData))]
        [Test]
        public void MatrixShouldCreate(bool useMultithreading)
        {
            Matrix.UseMultithreading = useMultithreading;

            var matrix = new Matrix(5, 5);
        }

        [TestCaseSource(nameof(UseMultithreadingData))]
        [Test]
        public void MatrixShouldMultiplyByIdentity(bool useMultithreading)
        {
            Matrix.UseMultithreading = useMultithreading;

            var matrix = new Matrix(4, 4);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    matrix.Fields[i, j] = i + j;
                }
            }

            var matrixIdentity = this.IdentityMatrix(4);

            this.CheckMatrixEqual(matrix * matrixIdentity, matrix);
            this.CheckMatrixEqual(matrixIdentity * matrix, matrix);
        }

        [TestCaseSource(nameof(UseMultithreadingData))]
        [Test]
        public void MatrixShouldMultiplyByOtherMatrix(bool useMultithreading)
        {
            Matrix.UseMultithreading = useMultithreading;

            var firstMatrix = new Matrix(3, 2);

            firstMatrix.Fields[0, 0] = 1;
            firstMatrix.Fields[0, 1] = 2;
            firstMatrix.Fields[0, 2] = 3;
            firstMatrix.Fields[1, 0] = 2;
            firstMatrix.Fields[1, 1] = 4;
            firstMatrix.Fields[1, 2] = 3;

            var secondMatrix = new Matrix(1, 3);

            secondMatrix.Fields[0, 0] = 2;
            secondMatrix.Fields[1, 0] = 3;
            secondMatrix.Fields[2, 0] = 1;

            var result = new Matrix(1, 2);

            result.Fields[0, 0] = 11;
            result.Fields[1, 0] = 19;

            this.CheckMatrixEqual(firstMatrix * secondMatrix, result);
        }

        [TestCaseSource(nameof(UseMultithreadingData))]
        [Test]
        public void MatrixShouldThrowExceprtion(bool useMultithreading)
        {
            Matrix.UseMultithreading = useMultithreading;

            var firstMatrix = new Matrix(15, 7);
            var secondMatrix = new Matrix(7, 14);

            Assert.Throws<ArgumentException>(() =>
            {
                var result = firstMatrix * secondMatrix;
            });
        }

        private static IEnumerable<TestCaseData> UseMultithreadingData()
        {
            yield return new TestCaseData(false).SetCategory("Without multithreading");
            yield return new TestCaseData(true).SetCategory("With multithreading");
        }

        private void CheckMatrixEqual(Matrix firstMatrix, Matrix secondMatrix)
        {
            Assert.AreEqual(firstMatrix.Fields.GetLength(0), secondMatrix.Fields.GetLength(0));
            Assert.AreEqual(firstMatrix.Fields.GetLength(1), secondMatrix.Fields.GetLength(1));

            for (int row = 0; row < firstMatrix.Fields.GetLength(0); row++)
            {
                for (int column = 0; column < firstMatrix.Fields.GetLength(1); column++)
                {
                    Assert.AreEqual(firstMatrix.Fields[row, column], secondMatrix.Fields[row, column]);
                }
            }
        }

        private Matrix IdentityMatrix(int size)
        {
            var matrix = new Matrix(size, size);

            for (int i = 0; i < size; i++)
            {
                matrix.Fields[i, i] = 1;
            }

            return matrix;
        }
    }
}