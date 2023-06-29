using DinkToPdf;
using System.Text;

namespace Web.Command.Commands
{
    public class PdfFile<T>
    {
        public readonly List<T> _list;

        public string FileName => $"{typeof(T).Name}.pdf";
        public string FileType => "application/octet-stream";

        public MemoryStream Create()
        {
            var type = typeof(T);
            var sb = new StringBuilder();

            // Html Dokümanlarını PDF' e oluşturacağız.

            sb.Append($@"<html>
                            <head></head>
                            <body>
                              <div class='text-center'><h1>{type.Name} table</h1></div>
                                <table class='table table-stripped' align='center'>");
            sb.Append("<tr>");
            type.GetProperties().ToList().ForEach(x =>
            {
                sb.Append($"<th>{x.Name}</th>");
            });
            sb.Append("</tr>");

            // Satırları ekliyoruz.
            _list.ForEach(x =>
            {
                var values = type.GetProperties().Select(propertyInfo => propertyInfo.GetValue(x, null)).ToList();
                sb.Append("<tr>");
                values.ForEach(value =>
                {
                    sb.Append($"<td>{value}</td>");
                });

                sb.Append("</tr>");

            });


            sb.Append("</table></body></html>");

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                                    ColorMode = ColorMode.Color,
                                    Orientation = Orientation.Portrait,
                                    PaperSize = PaperKind.A4,
                                 },
                Objects = {
                        new ObjectSettings() {
                                                PagesCount = true,
                                                HtmlContent = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. In consectetur                                mauris eget ultrices  iaculis. Ut odio viverra, molestie lectus nec,                                   venenatis turpis.",
                                                WebSettings = { DefaultEncoding = "utf-8" ,UserStyleSheet=Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/lib/bootstrap/dist/css/bootstrap.css") },
                                                HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                                              }
                           }
            };

            return null;
        }
    }
}
