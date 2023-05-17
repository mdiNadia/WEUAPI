using Application.Interfaces;
using Application.Services.FastReportPage;
using FastReport.Export.Html;
using FastReport.Export.OoXML;
using FastReport.Export.Pdf;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Application.Features.ReportData.Queries
{
    public class GetUsersLoginHistory : IRequest<byte[]>
    {
        public string format { get; set; }
        public class GetUsersLoginHistoryHandler : IRequestHandler<GetUsersLoginHistory, byte[]>
        {
            private readonly IConfiguration _configuration;
            private readonly IUnitOfWork _unitOfWork;

            public GetUsersLoginHistoryHandler(IConfiguration configuration, IUnitOfWork unitOfWork)
            {
                this._configuration = configuration;
                this._unitOfWork = unitOfWork;
            }
            public async Task<byte[]> Handle(GetUsersLoginHistory query, CancellationToken cancellationToken)
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                DataSet dataSet = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter("select * from UsersLoginHistory", connectionString);
                da.Fill(dataSet, "UsersLoginHistory");
                var report = new FastReport.Report();
                ///////////////////////////////////////////
                using (MemoryStream ms = new MemoryStream()) // Create a stream for the report
                {
                    try
                    {
                        report.RegisterData(dataSet);
                        //Enable data table
                        report.GetDataSource("UsersLoginHistory").Enabled = true;
                        report.MakePage(connectionString, "UsersLoginHistory");
                        report.Prepare();
                        if (query.format == "pdf")
                        {
                            PDFExport export = new PDFExport();
                            export.Export(report, ms);
                        }
                        else if (query.format == "xls" || query.format == "xlsx")
                        {
                            Excel2007Export export = new Excel2007Export();
                            export.Export(report, ms);
                        }
                        else if (query.format == "html")
                        {
                            // Export in HTML
                            HTMLExport html = new HTMLExport();
                            html.SinglePage = true;
                            html.Navigator = false;
                            html.EmbedPictures = true;
                            report.Export(html, ms);
                        }

                        return ms.ToArray();
                    }
                    catch (Exception err)
                    {

                        throw;
                    }
                    finally
                    {
                        ms.Dispose();
                    }
                }
            }
        }
    }
}
