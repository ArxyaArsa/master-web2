using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discounts.Web.Models.Report;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Discounts.Web.Helpers
{
    public static class ReportHelper
    {
        public const string ReportRelativePathTemplate = "App_Data/Reports/Report_{0}.xlsx";

        public static string Export(IEnumerable<ReportRecord> records, ReportFilterModel filter)
        {
            var file = Path.GetTempFileName();

            using (var document = SpreadsheetDocument.Create(file, SpreadsheetDocumentType.Workbook, true))
            {
                var workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet();
                var sharedStringTablePart = workbookPart.AddNewPart<SharedStringTablePart>();
                sharedStringTablePart.SharedStringTable = new SharedStringTable();
                var sheets = workbookPart.Workbook.AppendChild(new Sheets());
                var sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Report" };
                sheets.Append(sheet);
                workbookPart.Workbook.Save();
            }

            //using (var document = SpreadsheetDocument.Open(file, true))
            //{
            //    var worksheetPart = document.WorkbookPart.WorksheetParts.First();
            //    var sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());
            //
            //    int columnCount = 0;
            //
            //    var hRow = new Row();
            //    if (filter.GroupByPartnerType) { hRow.Append(new Cell { InlineString = new InlineString() { Text = new Text("Partner Type") } }); columnCount++; }
            //    if (filter.GroupByPartner) { hRow.Append(new Cell { InlineString = new InlineString() { Text = new Text("Partner") } }); columnCount++; }
            //    if (filter.GroupByUser) { hRow.Append(new Cell { InlineString = new InlineString() { Text = new Text("User") } }); columnCount++; }
            //    if (filter.GroupByAction) { hRow.Append(new Cell { InlineString = new InlineString() { Text = new Text("Action") } }); columnCount++; }
            //    hRow.Append(new Cell { InlineString = new InlineString() { Text = new Text("Original Value") } }); columnCount++;
            //    hRow.Append(new Cell { InlineString = new InlineString() { Text = new Text("Discount") } }); columnCount++;
            //    sheetData.AppendChild(hRow);
            //
            //    foreach (var r in records)
            //    {
            //        var row = new Row();
            //        if (filter.GroupByPartnerType) row.Append(new Cell { InlineString = new InlineString() { Text = new Text(r.PartnerTypeName ?? "") } });
            //        if (filter.GroupByPartner) row.Append(new Cell { InlineString = new InlineString() { Text = new Text(r.PartnerName ?? "") } });
            //        if (filter.GroupByUser) row.Append(new Cell { InlineString = new InlineString() { Text = new Text(r.UserName ?? "") } });
            //        if (filter.GroupByAction) row.Append(new Cell { InlineString = new InlineString() { Text = new Text(r.ActionName ?? "") } });
            //        row.Append(new Cell { CellValue = new CellValue(r.OriginalValue) });
            //        row.Append(new Cell { CellValue = new CellValue(r.ActionValue) });
            //        sheetData.AppendChild(row);
            //    }
            //}

            using (var document = SpreadsheetDocument.Open(file, true))
            {
                var worksheetPart = document.WorkbookPart.WorksheetParts.First();
                var sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());
                var sharedStringTable = document.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First().SharedStringTable;
            
                // future use
                int columnCount = 0;
            
                var hRow = new Row();
                if (filter.GroupByPartnerType) { AddStringCell("Partner Type", hRow, sharedStringTable); columnCount++; }
                if (filter.GroupByPartner) { AddStringCell("Partner", hRow, sharedStringTable); columnCount++; }
                if (filter.GroupByUser) { AddStringCell("User", hRow, sharedStringTable); columnCount++; }
                if (filter.GroupByAction) { AddStringCell("Action", hRow, sharedStringTable); columnCount++; }
                AddStringCell("Original Value", hRow, sharedStringTable); columnCount++;
                AddStringCell("Discount", hRow, sharedStringTable); columnCount++;
                sheetData.AppendChild(hRow);
            
                foreach (var r in records)
                {
                    var row = new Row();
                    if (filter.GroupByPartnerType) { AddStringCell(r.PartnerTypeName, row, sharedStringTable); }
                    if (filter.GroupByPartner) { AddStringCell(r.PartnerName, row, sharedStringTable); }
                    if (filter.GroupByUser) { AddStringCell(r.UserName, row, sharedStringTable); }
                    if (filter.GroupByAction) { AddStringCell(r.ActionName, row, sharedStringTable); }
                    row.Append(new Cell { CellValue = new CellValue(r.OriginalValue) });
                    row.Append(new Cell { CellValue = new CellValue(r.ActionValue) });
                    sheetData.AppendChild(row);
                }
            }

            var filePath = string.Format(ReportRelativePathTemplate, DateTime.Now.Ticks);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            File.Move(file, filePath);

            return filePath;
        }

        public static void AddStringCell(string str, Row row, SharedStringTable sharedStringTable)
        {
            var item = sharedStringTable.AppendChild(new SharedStringItem(new Text(str ?? "")));
            var cell = new Cell();
            cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
            cell.CellValue = new CellValue(item.ElementsBefore().Count().ToString());
            row.Append(cell);
        }
    }
}
