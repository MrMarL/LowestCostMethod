using System;
using System.Collections.Generic;

namespace Lab3
{
    public class LowestCostMethod
    {
        protected Min min = new Min();
        public int Calculate(List<List<int>> matrix)
        {
            int cost = 0;

            while (matrix.Count > 1)
            {
                Print(matrix);
                GetMin(matrix);

                int oldcost = cost;

                int warehouse = matrix[0][min.y];
                int consumer = matrix[min.x][0];
                if (warehouse - consumer < 0)
                {
                    cost += warehouse * min.value;
                    matrix[min.x][0] = consumer - warehouse;
                    for (int r = 0; matrix.Count > r; r++)
                        matrix[r].RemoveAt(min.y);
                }
                else {
                    cost += consumer * min.value;
                    matrix[0][min.y] = warehouse - consumer;
                    matrix.RemoveAt(min.x);
                    if (warehouse - consumer == 0)
                        for (int r = 0; matrix.Count > r; r++)
                            matrix[r].RemoveAt(min.y);
                }
                Console.WriteLine("Привезено товара:" + (cost - oldcost) / min.value);
                Console.WriteLine("Cost = " + cost);
            }
            return cost;
        }
        protected Min GetMin(List<List<int>> matrix)
        {
            min.Def(matrix[1][1]);

            for (int x = 1; x < matrix.Count; x++)
                for (int y = 1; y < matrix[0].Count; y++)
                    if (matrix[x][y] < min.value)
                    {
                        min.value = matrix[x][y];
                        min.x = x; min.y = y;
                    }
            return min;
        }

        protected void Print(List<List<int>> matrix)
        {
            for (int x = 0; x < matrix.Count; x++)
            {
                Console.WriteLine();
                for (int y = 0; y < matrix[0].Count; y++)
                     Console.Write(matrix[x][y] + "\t");
            }
            Console.WriteLine();
        }
    }
}
