using FastReport;
using FastReport.Utils;
using System.Drawing;

namespace Application.Services.FastReportPage
{
    public class Header
    {
        public Header(ReportPage page, string table)
        {
            page.PageHeader = new PageHeaderBand();
            page.PageHeader.CreateUniqueName();
            page.PageHeader.Height = 0.5f * Units.Centimeters;
            TextObject headerText = new TextObject();
            headerText.CreateUniqueName();
            headerText.Bounds = new RectangleF(0.0f, -0.6f * Units.Centimeters,
            19.0f * Units.Centimeters, 1.0f * Units.Centimeters);
            headerText.HorzAlign = HorzAlign.Center;
            headerText.VertAlign = VertAlign.Center;
            headerText.Font = new Font("Arial", 16.0f,
            FontStyle.Bold | FontStyle.Italic);
            headerText.TextColor = Color.Teal;
            headerText.FillColor = Color.YellowGreen;
            headerText.Border.Lines = BorderLines.All;
            headerText.Border.TopLine.Color = Color.Indigo;
            headerText.Border.LeftLine.Color = Color.Gold;
            headerText.Border.RightLine.Color = Color.Gold;
            headerText.Border.BottomLine.Color = Color.Indigo;
            headerText.Border.TopLine.Width = 3.0f;
            headerText.Border.LeftLine.Width = 2.0f;
            headerText.Border.RightLine.Width = 2.0f;
            headerText.Border.BottomLine.Width = 3.0f;
            headerText.Text = table + "Table";

            page.PageHeader.Objects.Add(headerText);
        }
    }
}
