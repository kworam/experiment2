using System;
using Experiment.Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.Matrix
{
    [TestClass]
    public class MatrixRotationUnitTest
    {
        [TestCategory("MatrixRotation"), TestMethod]
        public void RotateFiveByOneKisEven()
        {
            int[][] matrix = new int[1][];
            matrix[0] = new int[] { 0, 1, 2, 3, 4 };

            DoRotation(matrix, 2);
        }

        [TestCategory("MatrixRotation"), TestMethod]
        public void RotateFiveByOneKisOdd()
        {
            int[][] matrix = new int[1][];
            matrix[0] = new int[] { 0, 1, 2, 3, 4 };

            DoRotation(matrix, 3);
        }

        [TestCategory("MatrixRotation"), TestMethod]
        public void RotateOneByFiveKisEven()
        {
            int[][] matrix = new int[5][];
            matrix[0] = new int[] { 0 };
            matrix[1] = new int[] { 1 };
            matrix[2] = new int[] { 2 };
            matrix[3] = new int[] { 3 };
            matrix[4] = new int[] { 4 };

            DoRotation(matrix, 2);
        }

        [TestCategory("MatrixRotation"), TestMethod]
        public void RotateOneByFiveKisOdd()
        {
            int[][] matrix = new int[5][];
            matrix[0] = new int[] { 0 };
            matrix[1] = new int[] { 1 };
            matrix[2] = new int[] { 2 };
            matrix[3] = new int[] { 3 };
            matrix[4] = new int[] { 4 };

            DoRotation(matrix, 3);
        }

        [TestCategory("MatrixRotation"), TestMethod]
        public void RotateFourByOne()
        {
            int[][] matrix = new int[1][];
            matrix[0] = new int[] { 0, 1, 2, 3 };

            DoRotation(matrix, 2);
        }

        [TestCategory("MatrixRotation"), TestMethod]
        public void RotateFourByTwo()
        {
            int[][] matrix = new int[2][];
            matrix[0] = new int[] { 0, 1, 2, 3 };
            matrix[1] = new int[] { 7, 6, 5, 4 };
            DoRotation(matrix, 2);
        }

        [TestCategory("MatrixRotation"), TestMethod]
        public void RotateFourByThree()
        {
            int[][] matrix = new int[3][];
            matrix[0] = new int[] { 0, 1, 2, 3 };
            matrix[1] = new int[] { 9, 0, 0, 4 };
            matrix[2] = new int[] { 8, 7, 6, 5 };

            DoRotation(matrix, 4);
        }

        private void DoRotation(int[][] matrix, int k)
        {
            PrintMatrix(matrix, string.Format("BEFORE rotate by {0}", k));
            MatrixRotation.Rotate(matrix, k);
            PrintMatrix(matrix, "AFTER");
        }

        private void PrintMatrix(int[][] matrix, string title)
        {
            Console.WriteLine(title);
            foreach(int[] row in matrix)
            {
                Console.WriteLine(string.Join(" ", row));
            }
            Console.WriteLine();
        }
    }
}
