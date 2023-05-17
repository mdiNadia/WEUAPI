using FastReport;
using FastReport.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Drawing;

namespace Application.Services.FastReportPage
{
    public class BandData
    {
        private readonly IConfiguration _configuration;

        public BandData(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public BandData(ReportPage page, Report report, string table)
        {

            DataBand band = new DataBand();
            page.Bands.Add(band);
            band.CreateUniqueName();
            band.CanGrow = true;
            band.DataSource = report.GetDataSource(table);
            band.Height = 0.5f * Units.Centimeters;
            float index = 0;
            JToken jAppSettings = JToken.Parse(
              File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "appsettings.json"))
            );
            var mapping = jAppSettings[table];
            JObject dataObject = JObject.Parse(mapping.ToString());

            foreach (var item in dataObject)
            {
                TextObject obj = new TextObject();
                obj.CreateUniqueName();
                obj.HorzAlign = HorzAlign.Center;
                obj.Bounds = new RectangleF(
                    Units.Centimeters * 1 * index,
                    0,
                    Units.Centimeters * 2.5f,
                    Units.Centimeters);
                obj.Border.Lines = BorderLines.All;
                obj.Text = $"[{table}.{item.Key}]";
                obj.RightToLeft = true;
                band.AddChild(obj);
                index += (float)2.5;
            }


        }



    }
}



