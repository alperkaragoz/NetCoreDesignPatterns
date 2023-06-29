using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using System.Data;
using DataTable = System.Data.DataTable;

namespace Web.Command.Commands
{
    public class ExcelFile<T>
    {
        // Readonly yapmamızın sebebi costructorda veya tanımlandığı yerde initialize edilsin ve sonrasında değiştirilmesin.
        public readonly List<T> _list;
        public string FileName => $"{typeof(T).Name}.xlsx";
        public string FileType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";


        public ExcelFile(List<T> list)
        {
            _list = list;
        }

        // Excel dosyasını memory'de bir byte array olarak tutuyoruz.
        public Task<MemoryStream> Create()
        {
            var wb = new XLWorkbook();

            // DataSet, Datatableları tutan bir db olarak düşünebiliriz.
            var ds = new DataSet();
            ds.Tables.Add(GetTable());

            wb.Worksheets.Add(ds);

            // MemoryStream, memory'de byte arrayları tutmamızı sağlar.
            var excelMemory = new MemoryStream();

            wb.SaveAs(excelMemory);

            return Task.FromResult(excelMemory);
        }

        private DataTable GetTable()
        {
            var table = new System.Data.DataTable();

            var type = typeof(T);

            type.GetProperties().ToList().ForEach(x => table.Columns.Add(x.Name, x.PropertyType));

            _list.ForEach(x =>
            {
                var values = type.GetProperties().Select(propertyInfo => propertyInfo.GetValue(x, null)).ToArray();
                table.Rows.Add(values);
            });
            return table;
        }
    }
}
