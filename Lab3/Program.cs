using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

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

            try
            {
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(patch, true))
                {
                    Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                    Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;
                    IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();

                    List<List<int>> matrix = new List<List<int>>();

                    foreach (Row row in rows)
                    {
                        List<int> matr_ = new List<int>();
                        foreach (Cell c in row)
                            matr_.Add(int.Parse(c.CellValue.Text));
                        matrix.Add(matr_);
                    }

                    int cost = lcm.Calculate(matrix);

                    rows.First().GetFirstChild<Cell>().CellValue = new CellValue(cost);
                    worksheet.Save();
                }
            } catch { Console.WriteLine("Ошибка открытия файла "+patch);}

            Console.WriteLine("\nНажмите любую клавишу для выхода.");
            Console.Read();
        }
    }
}