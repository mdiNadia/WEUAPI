using FastReport;
using FastReport.Utils;
using System.Drawing;

namespace Application.Services.FastReportPage
{
    public class Footer
    {
        public Footer(ReportPage page)
        {
            page.PageFooter = new PageFooterBand();
            page.PageFooter.CreateUniqueName();
            page.PageFooter.Height = 0.5f * Units.Centimeters;
            TextObject footerText = new TextObject();
            footerText.CreateUniqueName();
            footerText.HorzAlign = HorzAlign.Right;
            footerText.VertAlign = VertAlign.Center;
            footerText.Bounds = new RectangleF(0.0f, 3 * Units.Centimeters,
            19.0f * Units.Centimeters, 0.5f * Units.Centimeters);
            footerText.TextColor = Color.Teal;
            footerText.FillColor = Color.YellowGreen;
            footerText.Border.Lines = BorderLines.All;
            footerText.Border.TopLine.Color = Color.Indigo;
            footerText.Border.LeftLine.Color = Color.Gold;
            footerText.Border.RightLine.Color = Color.Gold;
            footerText.Border.BottomLine.Color = Color.Indigo;
            footerText.Border.TopLine.Width = 3.0f;
            footerText.Border.LeftLine.Width = 2.0f;
            footerText.Border.RightLine.Width = 2.0f;
            footerText.Border.BottomLine.Width = 3.0f;
            footerText.Text = "Page [Page]";
            page.PageFooter.Objects.Add(footerText);
            //
            page.ReportSummary = new ReportSummaryBand();
            page.ReportSummary.CreateUniqueName();
            page.ReportSummary.Height = 4.0f * Units.Centimeters;
            footerText = new TextObject();
            footerText.CreateUniqueName();
            footerText.Bounds = new RectangleF(0.0f, 0.5f * Units.Centimeters,
            19.0f * Units.Centimeters, 1.0f * Units.Centimeters);
            footerText.HorzAlign = HorzAlign.Center;
            footerText.VertAlign = VertAlign.Center;
            footerText.Top = 1.5f * Units.Centimeters;
            footerText.Font = new Font("Arial", 16.0f,
            FontStyle.Bold | FontStyle.Italic);
            footerText.TextColor = Color.Teal;
            footerText.FillColor = Color.YellowGreen;
            footerText.Border.Lines = BorderLines.All;
            footerText.Border.TopLine.Color = Color.Indigo;
            footerText.Border.LeftLine.Color = Color.Gold;
            footerText.Border.RightLine.Color = Color.Gold;
            footerText.Border.BottomLine.Color = Color.Indigo;
            footerText.Border.TopLine.Width = 3.0f;
            footerText.Border.LeftLine.Width = 2.0f;
            footerText.Border.RightLine.Width = 2.0f;
            footerText.Border.BottomLine.Width = 3.0f;

            footerText.Text = "Totla Page [TotalPages#]";
            page.ReportSummary.Objects.Add(footerText);
        }
    }
}
