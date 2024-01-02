namespace MathProject
{
    public class Matrix
    {
        private Complex[,] X;
        private int[] columnWidths;

        public int M { get; }
        public int N { get; }

        public Matrix(Complex[,] X)
        {
            this.X = X;

            this.M = X.GetLength(0);
            this.N = X.GetLength(1);

            this.columnWidths = new int[this.N];

            for (int j = 0; j < N; j++)
            {
                int max = 0;

                for (int i = 0; i < M; i++)
                {
                    max = Math.Max(max, X[i, j].ToString().Length);
                }

                this.columnWidths[j] = max;
            }
        }

        public Complex Get(int i, int j) => X[i, j];

        public string GetSize() => string.Format("{1}x{2}", M, N);

        public Matrix GetRow(int row)
        {
            if (row < 0 || M < row) throw new Exception();

            var r = new Complex[1, N];

            for (int j = 0; j < N; j++)
            {
                r[0, j] = X[row, j];
            }

            return new Matrix(r);
        }

        public Matrix GetColumn(int column)
        {
            if (column < 0 || N < column) throw new Exception();

            var r = new Complex[M, 1];

            for (int i = 0; i < N; i++)
            {
                r[i, 0] = X[i, column];
            }

            return new Matrix(r);
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            int M = a.M;
            int N = a.N;

            if (M != b.M || N != b.N) throw new Exception();

            var s = new Complex[M, N];

            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    s[i, j] = a.X[i, j] + b.X[i, j];
                }
            }

            return new Matrix(s);
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            return a + (-1 * b);
        }

        public static Matrix operator *(Complex number, Matrix matrix)
        {
            var result = new Complex[matrix.M, matrix.N];

            for (int i = 0; i < matrix.M; i++)
            {
                for (int j = 0; j < matrix.N; j++)
                {
                    result[i, j] = matrix.X[i, j] * number;
                }
            }

            return new Matrix(result);
        }

        public static Matrix operator *(Matrix matrix, Complex number)
        {
            return number * matrix;
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.N != m2.M) throw new Exception();

            int M = m1.M;
            int N = m2.N;
            int K = m1.N;

            var r = new Complex[M, N];

            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    r[i, j] = 0;

                    for (int k = 0; k < K; k++)
                    {
                        r[i, j] += m1.X[i, k] * m2.X[k, j];
                    }
                }
            }

            return new Matrix(r);
        }

        public Matrix RemoveRowAndColumn(int r, int c)
        {
            int resultM = M - (0 <= r && r < M ? 1 : 0);
            int resultN = N - (0 <= c && c < N ? 1 : 0);

            var result = new Complex[resultM, resultN];

            int seenR = 0;

            for (int i = 0; i < M; i++)
            {
                if (i == r)
                {
                    seenR = 1; 
                    continue;
                }

                int seenC = 0;

                for (int j = 0; j < N; j++)
                {
                    if (j == c)
                    {
                        seenC = 1;
                        continue;
                    }

                    result[i - seenR, j - seenC] = X[i, j];
                }
            }

            return new Matrix(result);
        }

        public Matrix RemoveRow(int r) => RemoveRowAndColumn(r, -1);

        public Matrix RemoveColumn(int c) => RemoveRowAndColumn(-1, c);

        public Matrix InsertColumn(Matrix m, int c)
        {
            if (!(m.N == 1 && M == m.M)) throw new Exception();

            if (c == N) return MergeHorizontally(this, m);

            if (!(0 <= c && c < N)) throw new Exception();

            var result = new Complex[M, N + 1];

            int seen = 0;

            for (int j = 0; j < N; j++)
            {
                if (j == c)
                {
                    seen = 1;

                    for (int i = 0; i < M; i++)
                    {
                        result[i, j] = m.Get(i, 0);
                    }
                }

                for (int i = 0; i < M; i++)
                {
                    result[i, j + seen] = X[i, j];
                }
            }

            return new Matrix(result);
        }

        public Matrix ReplaceColumn(Matrix m, int c) => RemoveColumn(c).InsertColumn(m, c);

        public static Matrix MergeHorizontally(Matrix m1, Matrix m2)
        {
            if (m1.M != m2.M) throw new Exception();

            int M = m1.M;
            int N1 = m1.N;
            int N = m1.N + m2.N;

            var merged = new Complex[M, N];

            for (int i = 0; i < M; i++)
            {
                int p1 = 0;
                int p2 = 0;

                while (p1 + p2 < N)
                {
                    merged[i, p1 + p2] = (p1 < N1) ? m1.Get(i, p1++) : m2.Get(i, p2++);
                }
            }

            return new Matrix(merged);
        }

        public static Matrix MergeVertically(Matrix m1, Matrix m2)
        {
            if (m1.N != m2.N) throw new Exception();

            int M1 = m1.M;
            int M = m1.M + m2.M;
            int N = m1.N;

            var merged = new Complex[M, N];

            int p1 = 0;
            int p2 = 0;
            while (p1 + p2 < M)
            {
                for (int j = 0; j < N; j++)
                {
                    merged[p1 + p2, j] = (p1 < M1) ? m1.Get(p1, j) : m2.Get(p2, j);
                }

                if (p1 < M1) p1++;
                else p2++;
            }

            return new Matrix(merged);
        }

        public Matrix Transposed()
        {
            var result = new Complex[N, M];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    result[i, j] = X[j, i];
                }
            }

            return new Matrix(result);
        }

        public Complex Determinant()
        {
            if (N != M) throw new Exception();

            if (M == 1) return X[0, 0];
            if (M == 2) return X[1, 1] * X[0, 0] - X[1, 0] * X[0, 1];

            Complex result = 0;

            for (int p = 0; p < M; p++)
            {
                var newM = new Complex[M - 1, M - 1];

                for (int i = 0; i < M - 1; i++)
                {
                    var seen = false;
                    for (int j = 0; j < M; j++)
                    {
                        if (j == p)
                        {
                            seen = true;
                            continue;
                        }

                        newM[i, j - (seen ? 1 : 0)] = X[i + 1, j];
                    }
                }

                result += Math.Pow(-1, p) * X[0, p] * (new Matrix(newM)).Determinant();
            }

            return result;
        }

        public override string ToString()
        {
            var result = string.Format("{0}x{1} :", M, N);
            var shift = result.Length;

            for (int i = 0; i < M; i++)
            {
                result += (i == 0 ? "" : new string(' ', shift)) + " | ";

                for (int j = 0; j < N; j++)
                {
                    var n = X[i, j];
                    result += new string(' ', columnWidths[j] - n.ToString().Length) + n + " ";
                }

                result += "|\n";
            }

            return result;
        }
    }
}
