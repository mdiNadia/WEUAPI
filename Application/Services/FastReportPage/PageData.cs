using FastReport;
using FastReport.Data;
using FastReport.Utils;

namespace Application.Services.FastReportPage
{
    public static class PageData
    {
        public static void MakePage(this Report report, string ConnectionString, string Table)
        {
            RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
            var mssqlDataConnection = new MsSqlDataConnection();
            mssqlDataConnection.ConnectionString = ConnectionString;
            report.Dictionary.Connections.Add(mssqlDataConnection);

            //Add report page
            ReportPage page = new ReportPage();
            page.CreateUniqueName();
            page.TopMargin = 10.0f;
            page.LeftMargin = 10.0f;
            page.RightMargin = 10.0f;
            page.BottomMargin = 10.0f;
            report.Pages.Add(page);

            new Title(page);
            new Header(page, Table);
            new BandData(page, report, Table);
            new Footer(page);
        }



    }

}

