namespace Experiment.Matrix
{
    public class MatrixRotation
    {
        public static void Rotate(int[][] mx, int k)
        {
            if (mx == null)
            {
                return;
            }

            int nrows = mx.Length;
            int ncols = mx[0].Length;
            int ncells;
            if (nrows == 1)
            {
                ncells = ncols;
            }
            else if (ncols == 1)
            {
                ncells = nrows;
            }
            else
            {
                ncells = nrows * ncols - ((nrows - 2) * (ncols - 2));
            }

            k %= ncells;
            if (k == 0)
            {
                return;
            }

            if (ncells % 2 == 1)
            {
                Rotate(mx, k, nrows, ncols, ncells, 0, ncells);
            }
            else
            {
                Rotate(mx, k, nrows, ncols, ncells, 0, ncells / 2);
                Rotate(mx, k, nrows, ncols, ncells, 1, ncells / 2);
            }
        }

        private static void Rotate(int[][] mx, int k, int nrows, int ncols, int ncells, int startIndex, int count)
        {
            int cr = GetRow(startIndex, nrows, ncols, ncells);
            int cc = GetCol(startIndex, nrows, ncols, ncells);

            int[] temps = new int[2];
            int copyToIndex = 0;
            int copyFromIndex = 1;
            temps[copyFromIndex] = mx[cr][cc];

            int i = startIndex;
            for (int cnt = 0; cnt < count; cnt++)
            {
                i = (i + k) % ncells;

                int nr = GetRow(i, nrows, ncols, ncells);
                int nc = GetCol(i, nrows, ncols, ncells);
                temps[copyToIndex] = mx[nr][nc];
                mx[nr][nc] = temps[copyFromIndex];

                cr = nr;
                cc = nc;
                copyToIndex = (copyToIndex + 1) % 2;
                copyFromIndex = (copyFromIndex + 1) % 2;
            }
        }

        private static int GetRow(int i, int nrows, int ncols, int ncells)
        {
            if (ncols == 1) return i % nrows;

            int cellsInTopBottom = ncols - 1;
            int cellsInLeftRight = nrows - 1;

            if (i < cellsInTopBottom) return 0;

            i -= cellsInTopBottom;
            if (i < cellsInLeftRight) return i;

            i -= cellsInLeftRight;
            if (i < cellsInTopBottom) return cellsInLeftRight;

            i -= cellsInTopBottom;
            return cellsInLeftRight - i;
        }

        private static int GetCol(int i, int nrows, int ncols, int ncells)
        {
            if (nrows == 1) return i % ncols;

            int cellsInTopBottom = ncols - 1;
            int cellsInLeftRight = nrows - 1;

            if (i < cellsInTopBottom) return i;

            i -= cellsInTopBottom;
            if (i < cellsInLeftRight) return cellsInTopBottom;

            i -= cellsInLeftRight;
            if (i < cellsInTopBottom) return cellsInTopBottom - i;

            return 0;
        }
    }
}
