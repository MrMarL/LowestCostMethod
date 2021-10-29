using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MrMarL Inc.");
            LowestCostMethod lcm = new LowestCostMethod();

            string patch = "";
            foreach (string a in args)patch+=a;

            patch = patch == "" ? "C:\\Users\\Пользователь\\Desktop\\0.xlsx" : patch;

            Excel.Application excelApp = new Excel.Application();

            if (excelApp == null)
            {
                Console.WriteLine("Excel is not installed!!");
                return;
            }
            try
            {
                Excel.Workbook excelBook = excelApp.Workbooks.Open(patch);
                Excel.Worksheet excelSheet = excelBook.Sheets[1];
                Excel.Range excelRange = excelSheet.UsedRange;

                int rows = excelRange.Rows.Count;
                int cols = excelRange.Columns.Count;

                List<List<int>> matrix = new List<List<int>>();

                for (int i = 1; i <= rows; i++)
                {
                    List<int> matr_ = new List<int>();
                    for (int j = 1; j <= cols; j++)
                        if (excelRange.Cells[i, j] != null && ((Excel.Range)excelRange.Cells[i, j]).Value2 != null)
                            matr_.Add((int)((Excel.Range)excelRange.Cells[i, j]).Value2);
                    matrix.Add(matr_);
                }

                int cost = lcm.Calculate(matrix);

                excelSheet.Cells[1, "A"] = cost;
            } catch { Console.WriteLine("Ошибка открытия файла "+patch);}
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

            Console.WriteLine("\nНажмите любую клавишу для выхода.");
            Console.Read();
        }
    }
}
