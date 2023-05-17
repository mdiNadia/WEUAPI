using FastReport;
using FastReport.Utils;
using System.Drawing;

namespace Application.Services.FastReportPage
{
    public class Title
    {
        public Title(ReportPage page)
        {
            page.ReportTitle = new ReportTitleBand();
            page.ReportTitle.CreateUniqueName();
            page.ReportTitle.Height = 4.0f * Units.Centimeters;
            //
            TextObject titleText = new TextObject();
            titleText.CreateUniqueName();
            titleText.Left = 1.0f * Units.Centimeters;
            titleText.Top = 1.0f * Units.Centimeters;
            titleText.Width = 17.0f * Units.Centimeters;
            titleText.Height = 2.0f * Units.Centimeters;
            titleText.HorzAlign = HorzAlign.Center;
            titleText.VertAlign = VertAlign.Center;
            titleText.Font = new Font("Arial", 32.0f, FontStyle.Bold);
            titleText.TextColor = Color.DarkGreen;
            titleText.FillColor = Color.DarkOrange;
            titleText.Border.Color = Color.DarkOrchid;
            titleText.Border.Lines = BorderLines.All;
            titleText.Border.Width = 4.0f;
            titleText.Text = "نتیجه گزارش";
            titleText.RightToLeft = true;
            page.ReportTitle.Objects.Add(titleText);
        }
    }
}
