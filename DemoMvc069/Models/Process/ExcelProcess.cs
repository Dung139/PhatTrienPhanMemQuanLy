using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using OfficeOpenXml;

namespace DemoMvc069.Models.Process
{
    public class ExcelProcess
    {

        /// <summary>
        /// Reads Excel file and returns DataTable
        /// </summary>
        public DataTable ExcelToDataTable(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found: {filePath}");

            DataTable dt = new DataTable();
            try
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    if (package.Workbook.Worksheets.Count == 0)
                        throw new Exception("Excel file contains no worksheets");

                    var worksheet = package.Workbook.Worksheets[0]; // Get first sheet
                    
                    if (worksheet.Dimension == null)
                        return dt; // Return empty DataTable if sheet is empty

                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    // Create columns from first row (header)
                    for (int col = 1; col <= colCount; col++)
                    {
                        var headerValue = worksheet.Cells[1, col].Value;
                        string columnName = headerValue?.ToString() ?? $"Column{col}";
                        dt.Columns.Add(columnName);
                    }

                    // Add data rows starting from row 2
                    for (int row = 2; row <= rowCount; row++)
                    {
                        DataRow dr = dt.NewRow();
                        for (int col = 1; col <= colCount; col++)
                        {
                            var cellValue = worksheet.Cells[row, col].Value;
                            dr[col - 1] = cellValue ?? DBNull.Value;
                        }
                        dt.Rows.Add(dr);
                    }

                    dt.TableName = "ExcelData";
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading Excel file: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Exports DataTable to Excel file
        /// </summary>
        public static void ExportToExcel(DataTable dt, string filePath)
        {
            if (dt == null || dt.Rows.Count == 0)
                throw new ArgumentException("DataTable is empty");

            try
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Data");
                    
                    // Write headers
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        worksheet.Cells[1, col + 1].Value = dt.Columns[col].ColumnName;
                    }

                    // Write data
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        for (int col = 0; col < dt.Columns.Count; col++)
                        {
                            worksheet.Cells[row + 2, col + 1].Value = dt.Rows[row][col];
                        }
                    }

                    // Auto fit columns
                    worksheet.Cells.AutoFitColumns();

                    // Save file
                    package.SaveAs(new FileInfo(filePath));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error exporting to Excel: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Validates Excel file format
        /// </summary>
        public static bool IsValidExcelFile(string filePath)
        {
            if (!File.Exists(filePath))
                return false;

            string extension = Path.GetExtension(filePath).ToLower();
            return extension == ".xlsx" || extension == ".xls";
        }
    }
}