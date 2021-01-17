using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;

namespace RealityCS.SharedMethods
{
    //https://forums.asp.net/t/2067741.aspx?HOw+reading+null+cell+value+when+using+open+xml+in+asp+net 
//https://stackoverflow.com/questions/36670768/openxml-cell-datetype-is-null	
    public class ExcelReader:IDisposable    
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="dt"></param>
        /// <param name="SheetName"></param>
        public void CreateSpreadsheetWorkbook(string filepath, DataTable dt, String SheetName)
        {
            //Ref- http://www.totaldotnet.com/sub/how-to-create-excel-file-using-open-xml-ooxml-in-c
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            FileInfo f = new FileInfo(filepath);
            if (f.Exists)
                f.Delete();

            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Template);

            // Add a WorkbookPart to the document.
            WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            // Add Sheets to the Workbook.
            Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = SheetName };
            sheets.Append(sheet);
            string cl = "";
            uint row = 2;
            int index;
            Cell cell;
            foreach (DataRow dr in dt.Rows)
            {
                for (int idx = 0; idx < dt.Columns.Count; idx++)
                {
                    if (idx >= 26)
                        cl = "A" + Convert.ToString(Convert.ToChar(65 + idx - 26));
                    else
                        cl = Convert.ToString(Convert.ToChar(65 + idx));
                    SharedStringTablePart shareStringPart;
                    if (spreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().Count() > 0)
                    {
                        shareStringPart = spreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First();
                    }
                    else
                    {
                        shareStringPart = spreadsheetDocument.WorkbookPart.AddNewPart<SharedStringTablePart>();
                    }
                    if (row == 2)
                    {
                        index = InsertSharedStringItem(dt.Columns[idx].ColumnName, shareStringPart);
                        cell = InsertCellInWorksheet(cl, row - 1, worksheetPart);
                        cell.CellValue = new CellValue(index.ToString());
                        cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
                    }

                    // Insert the text into the SharedStringTablePart.
                    index = InsertSharedStringItem(Convert.ToString(dr[idx]), shareStringPart);
                    cell = InsertCellInWorksheet(cl, row, worksheetPart);
                    cell.CellValue = new CellValue(index.ToString());
                    cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
                }
                row++;
            }

            //InsertCellInWorksheet("A", 1, worksheetPart);
            workbookpart.Workbook.Save();

            // Close the document.
            spreadsheetDocument.Close();
            //InsertText(@"c:MyXL3.xlx", "Hello");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xlsxFilePath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public DataTable ExtractExcelSheetValuesToDataTable(string xlsxFilePath, string sheetName)
        {

            //using (SpreadsheetDocument document = SpreadsheetDocument.Open(xlsxFilePath, true))
            //try
            //{
            //FileStream fileStream = new FileStream(xlsxFilePath, FileMode.Open, FileAccess.Read);
            
            DataTable dt = new DataTable();
            try
            {
                using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(xlsxFilePath, false))
                {


                    WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                    IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                    string relationshipId = sheets.First().Id.Value;
                    WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
                    Worksheet workSheet = worksheetPart.Worksheet;
                    SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                    IEnumerable<Row> rows = sheetData.Descendants<Row>();
                    foreach (Cell cell in rows.ElementAt(0))
                    {
                        dt.Columns.Add(GetCellValue(spreadSheetDocument, cell));
                    }
                    foreach (Row row in rows) //this will also include your header row...
                    {
                        DataRow tempRow = dt.NewRow();
                        int columnIndex = 0;
                        var iii = row.Descendants<Cell>().Count();
                        //for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
                        //{
                        //    //tempRow[i] = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(i-1));
                        //    tempRow[i] = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(i));
                        //}

                        foreach (Cell cell in row.Descendants<Cell>())
                        {
                            // Gets the column index of the cell with data
                            int cellColumnIndex = (int)GetColumnIndexFromName(GetColumnName(cell.CellReference));
                            cellColumnIndex--; //zero based index
                            if (columnIndex < cellColumnIndex)
                            {
                                do
                                {
                                    tempRow[columnIndex] = ""; //Insert blank data here;
                                    columnIndex++;
                                }
                                while (columnIndex < cellColumnIndex);
                            }
                            tempRow[columnIndex] = GetCellValue(spreadSheetDocument, cell);

                            columnIndex++;
                        }


                        dt.Rows.Add(tempRow);
                    }
                }
                dt.Rows.RemoveAt(0); //...so i'm taking it out here.
            }
            catch(Exception ex)
            {

            }
            return dt;
            //}
            //catch (Exception ex)
            //{

            //}

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static int? GetColumnIndexFromName(string columnName)
        {
            //return columnIndex;
            string name = columnName;
            int number = 0;
            int pow = 1;
            for (int i = name.Length - 1; i >= 0; i--)
            {
                number += (name[i] - 'A' + 1) * pow;
                pow *= 26;
            }
            return number;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellReference"></param>
        /// <returns></returns>
        public static string GetColumnName(string cellReference)
        {
            // Create a regular expression to match the column name portion of the cell name.
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellReference);

            return match.Value;
        }

        private enum Formats
        {
            General = 0,
            Number = 1,
            Decimal = 2,
            Currency = 164,
            Accounting = 44,
            DateShort = 14,
            DateLong = 165,
            Time = 166,
            Percentage = 10,
            Fraction = 12,
            Scientific = 11,
            Text = 49
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        private string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            if (cell.CellValue == null)
            {
                return "";
            }
            string value = cell.CellValue.InnerXml;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
           else if (cell.DataType == null) // number & dates
            {
                if (cell.StyleIndex != null)
                {
                    int styleIndex = (int)cell.StyleIndex.Value;
                    CellFormat cellFormat = (CellFormat)document.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats.ElementAt(styleIndex);
                    uint formatId = cellFormat.NumberFormatId.Value;

                    if (formatId == (uint)Formats.DateShort || formatId == (uint)Formats.DateLong)
                    {
                        double oaDate;
                        if (double.TryParse(cell.InnerText, out oaDate))
                        {
                            return DateTime.FromOADate(oaDate).ToShortDateString();
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else
                    {
                        return cell.InnerText;
                    }
                }
                else
                {
                    return value;
                }
            }

            else
            {
                return value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="shareStringPart"></param>
        /// <returns></returns>
        private int InsertSharedStringItem(string text, SharedStringTablePart shareStringPart)
        {

            // If the part does not contain a SharedStringTable, create one.
            if (shareStringPart.SharedStringTable == null)
            {
                shareStringPart.SharedStringTable = new SharedStringTable();
            }
            int i = 0;

            // Iterate through all the items in the SharedStringTable. If the text already exists, return its index.
            foreach (SharedStringItem item in shareStringPart.SharedStringTable.Elements<SharedStringItem>())
            {
                if (item.InnerText == text)
                {
                    return i;
                }
                i++;
            }

            // The text does not exist in the part. Create the SharedStringItem and return its index.
            shareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new DocumentFormat.OpenXml.Spreadsheet.Text(text)));
            shareStringPart.SharedStringTable.Save();
            return i;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="rowIndex"></param>
        /// <param name="worksheetPart"></param>
        /// <returns></returns>
        private Cell InsertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)
        {
            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            string cellReference = columnName + rowIndex;

            // If the worksheet does not contain a row with the specified row index, insert one.
            Row row;
            if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0)
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = rowIndex };
                sheetData.Append(row);
            }

            // If there is not a cell with the specified column name, insert one. 
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0)
            {
                return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
            }
            else
            {

                // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
                Cell refCell = null;
                //foreach (Cell cell in row.Elements<cell>())
                //{
                // if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                // {
                // refCell = cell;
                // break;
                // }
                //}
                Cell newCell = new Cell() { CellReference = cellReference };
                row.InsertBefore(newCell, refCell);
                worksheet.Save();
                return newCell;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
