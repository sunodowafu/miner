using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class Matrix
    {
        public int[,] mines;
        private int size;
        public List<int[]> sqList;
        private bool[,] open;

        //create a matrix with numbers
        public Matrix(int x, int y, int n, int count)
        {
            int w, v;
            int[,] a = new int[n + 2, n + 2];
            this.NullMatrix(a, n + 2);
            mines = new int[n, n];
            size = n;
            sqList = new List<int[]>();
            open = new bool[n, n];
            this.NullMatrix(mines, n);
            Random r = new Random();
            int i = 0;
            while (i < count)
            {
                w = r.Next(n);
                v = r.Next(n);
                if (a[w, v] != 1 && w-1 != x && v-1 != y)
                {
                    a[w, v] = 1;
                    i++;
                }
            }
            for (i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (a[i + 1, j + 1] == 1) mines[i, j] = 9;
                    else mines[i, j] = (a[i + 1 - 1, j + 1 - 1] + a[i + 1, j + 1 - 1] + a[i + 1 + 1, j + 1 - 1]) + (a[i + 1 - 1, j + 1] + a[i + 1, j + 1] + a[i + 1 + 1, j + 1]) + (a[i + 1 - 1, j + 1 + 1] + a[i + 1, j + 1 + 1] + a[i + 1 + 1, j + 1 + 1]);
                    open[i, j] = false;
                }
        }

        public int Ex(int x, int y)
        {
            if (mines[x, y] == 9) return 1;
            else return 0;
        }

        //create null matrix
        private void NullMatrix(int[,] a, int n)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    a[i, j] = 0;                                   
        }

        //empty cell
        private bool empty(int i, int j)
        {
            if ((i >= 0) && (i < size))
            {
                if ((j >= 0) && (j < size))
                {
                    if (mines[i,j] == 0) return true;
                }
            }
            return false;
        }

        //opens empty cells
        public void Square(int x, int y)
        {
            int[] sq = new int[2];
            if ((x >= 0) && (x < size) && (y >= 0) && (y < size) && (mines[x, y] != 9))
            {
                if (!open[x, y])
                {
                    open[x, y] = true;
                    sq[0] = x;
                    sq[1] = y;
                    sqList.Add(sq);
                    if (mines[x, y] == 0)
                    {
                        Square(x - 1, y - 1);
                        Square(x - 1, y);
                        Square(x - 1, y + 1);
                        Square(x, y - 1);
                        Square(x, y + 1);
                        Square(x + 1, y - 1);
                        Square(x + 1, y);
                        Square(x + 1, y + 1);
                    }
                    else
                    {
                        if (empty(x - 1, y - 1)) Square(x - 1, y - 1);
                        if (empty(x - 1, y)) Square(x - 1, y);
                        if (empty(x - 1, y + 1)) Square(x - 1, y + 1);
                        if (empty(x, y - 1)) Square(x, y - 1);
                        if (empty(x, y + 1)) Square(x, y + 1);
                        if (empty(x + 1, y - 1)) Square(x + 1, y - 1);
                        if (empty(x + 1, y)) Square(x + 1, y);
                        if (empty(x + 1, y + 1)) Square(x + 1, y + 1);
                    }
                }
            }
        }
    }
}
