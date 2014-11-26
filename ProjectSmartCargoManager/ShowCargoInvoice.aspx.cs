﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.IO;
using BAL;
using System.Net;
using System.Text;

namespace ProjectSmartCargoManager
{
    public partial class ShowCargoInvoice : System.Web.UI.Page
    {
        ReportDataSource rds1 = new ReportDataSource();
        ReportDataSource rds2 = new ReportDataSource();
        DataTable dtTable1 = new DataTable();
        DataTable dtTable2 = new DataTable();
        DataSet dsResult;
        BAL.BALInvoiceListing objBAL = new BAL.BALInvoiceListing();
        string format = "";
        

        protected void Page_Load(object sender, EventArgs e)
        {
            string ConfigRepName = "";
            string invNo = Request.QueryString["INVNO"];
            try
            {
                #region Prepare Parameters
                object[] RateCardInfo = new object[1];
                int irow = 0;

                RateCardInfo.SetValue(invNo, irow);

                #endregion Prepare Parameters

                GenerateAgentInvoice(RateCardInfo);


                dtTable1 = (DataTable)Session["ShowExcel"];
                dtTable2 = (DataTable)Session["AWBData"];

                ReportViewer1.Visible = true;

                ReportViewer1.Reset();

                ReportViewer1.ProcessingMode = ProcessingMode.Local;

                LocalReport rep1 = ReportViewer1.LocalReport;

                //ConfigRepName = objBAL.getConfiguredInvoiceReportName();

                //if(ConfigRepName == "")
                rep1.ReportPath = Server.MapPath("/Reports/CargoInvoiceClient.rdlc");
                //else
                //    rep1.ReportPath = Server.MapPath("/Reports/" + ConfigRepName);

                //rep1.ReportPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"] + "CargoInvoice.rdlc";
                rds1.Name = "dsCargoInvoice_DataTable1";
                rds1.Value = dtTable1;
                rep1.DataSources.Add(rds1);

                ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(ItemsSubreportProcessingEventHandler);

                //Code to Save the generated Excel file
                format = Request.QueryString["Format"];

                if (format == "Excel")
                {
                    try
                    {
                        string reportType = "Excel";
                        string mimeType;
                        string encoding;
                        string fileNameExtension;
                        string deviceInfo =

                        "<DeviceInfo>" +

                        "  <OutputFormat>Excel</OutputFormat>" +

                        "</DeviceInfo>";

                        Warning[] warnings;
                        string[] streams;
                        byte[] renderedBytes;

                        //Render the report

                        renderedBytes = rep1.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                        Response.Clear();

                        Response.ContentType = mimeType;

                        Response.AddHeader("content-disposition", "attachment; filename=" + Session["CurrentInvoiceNo"].ToString() + "." + fileNameExtension);

                        Response.BinaryWrite(renderedBytes);


                        //Response.End();
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    try
                    {
                        string reportType = "PDF";
                        string mimeType;
                        string encoding;
                        string fileNameExtension;
                        //string deviceInfo =

                        //"<DeviceInfo>" +

                        //"  <OutputFormat>PDF</OutputFormat>" +

                        //"</DeviceInfo>";

                        string deviceInfo = "<DeviceInfo><PageHeight>35cm</PageHeight><PageWidth>40cm</PageWidth></DeviceInfo>";

                        Warning[] warnings;
                        string[] streams;
                        byte[] renderedBytes;

                        //Render the report

                        renderedBytes = rep1.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                        Response.Clear();

                        Response.ContentType = mimeType;

                        Response.AddHeader("content-disposition", "attachment; filename=" + Session["CurrentInvoiceNo"].ToString() + "." + fileNameExtension);

                        Response.BinaryWrite(renderedBytes);


                        //Response.End();
                    }
                    catch (Exception ex)
                    {

                    }
                }

                //Closing current tab
                //Response.Write("<script type='text/javascript' language='javascript'>window.close();</script>");

            }
            catch (Exception ex)
            {

            }
        }


        protected void GenerateAgentInvoice(object[] InvoiceNum)
        {
            //if(Request.QueryString["Type"] == "Regular")
            //    dsResult = objBAL.GetInvoiceDataImport(InvoiceNum);
            //else //Proforma
            //    dsResult = objBAL.GetProformaInvoiceDataImport(InvoiceNum);


            //Invoice printing for GoAir(Go Air format)
            if (Request.QueryString["Type"] == "Regular")
                dsResult = objBAL.GetInvoiceDataImport(InvoiceNum);
            else if (Request.QueryString["Type"] == "Proforma")
                dsResult = objBAL.GetProformaInvoiceDataImport(InvoiceNum);
            else if (Request.QueryString["Type"] == "WalkIn")
                dsResult = objBAL.GetWalkInAgentInvoiceData(InvoiceNum);
            else //Dest
                dsResult = objBAL.GetDestAgentInvoiceData(InvoiceNum);

            //img for report
            System.IO.MemoryStream Logo = null;
            System.IO.MemoryStream Partnerlogo = null;
            try
            {

                Logo = CommonUtility.GetImageStream(Page.Server);
            }
            catch (Exception ex)
            {
                Logo = new System.IO.MemoryStream();
            }

            try
            {
                Partnerlogo = CommonUtility.getPartnerImage(Page.Server);
            }
            catch (Exception ex)
            {
                Partnerlogo = new System.IO.MemoryStream();
            }
            //end

            if (dsResult != null)
            {
                if (dsResult.Tables != null)
                {
                    if (dsResult.Tables.Count > 0)
                    {
                        if (dsResult.Tables[0].Rows.Count > 0)
                        {
                            try
                            {
                                Session["CurrentInvoiceNo"] = dsResult.Tables[1].Rows[0]["InvoiceNumber"].ToString();

                                Session["ShowExcel"] = "";

                                DataTable DTExport = new DataTable();

                                DTExport.Columns.Add("ClientName");
                                DTExport.Columns.Add("ClientAddress");
                                DTExport.Columns.Add("EmailID");
                                DTExport.Columns.Add("PhoneNum");
                                DTExport.Columns.Add("FaxNum");
                                DTExport.Columns.Add("AgentName");
                                DTExport.Columns.Add("AgentAddress");
                                DTExport.Columns.Add("InvoiceNumber");
                                DTExport.Columns.Add("InvoiceDate");
                                DTExport.Columns.Add("AgentCode");
                                DTExport.Columns.Add("TotalChargesDueAirline");
                                DTExport.Columns.Add("TotalBaseAmtForST");
                                DTExport.Columns.Add("TotalSTDueAirline");
                                DTExport.Columns.Add("TDSOnCommission");
                                DTExport.Columns.Add("CommissionableSales");
                                DTExport.Columns.Add("AgentsCommission");
                                DTExport.Columns.Add("OtherChargesDueAgent");
                                DTExport.Columns.Add("STOnCommission");
                                DTExport.Columns.Add("TotalDeductions");
                                DTExport.Columns.Add("NetDueAirlinesAgentINR");
                                DTExport.Columns.Add("WordsINR");
                                DTExport.Columns.Add("ServiceTaxNumber");
                                DTExport.Columns.Add("PanCardNumber");
                                DTExport.Columns.Add("TanNumber");
                                DTExport.Columns.Add("InvoiceType");
                                DTExport.Columns.Add("RegOfficeAddress");
                                DTExport.Columns.Add("RegOfficePhoneNum");
                                DTExport.Columns.Add("Logo", System.Type.GetType("System.Byte[]"));

                                DTExport.Columns.Add("RateLineCurrency");
                                DTExport.Columns.Add("InvoiceDueDate");
                                DTExport.Columns.Add("ContactPerson");
                                DTExport.Columns.Add("Logo1", System.Type.GetType("System.Byte[]"));
                                //DTExport.Columns.Add("TotalChargableWeight");
                                //DTExport.Columns.Add("TotalPPFreight");
                                //DTExport.Columns.Add("TotalPPOCDC");
                                //DTExport.Columns.Add("TotalPPOCDA");
                                //DTExport.Columns.Add("TotalCCFreight");
                                //DTExport.Columns.Add("TotalCCOCDC");
                                //DTExport.Columns.Add("TotalCCOCDA");
                                //DTExport.Columns.Add("TotalServiceTax");
                                //DTExport.Columns.Add("SubTotalAWBCharges");
                                //DTExport.Columns.Add("TotalSpotFreight");

                                DTExport.Rows.Add(
                                dsResult.Tables[0].Rows[0]["ClientName"].ToString(),
                                dsResult.Tables[0].Rows[0]["ClientAddress"].ToString(),
                                dsResult.Tables[0].Rows[0]["EmailID"].ToString(),
                                dsResult.Tables[0].Rows[0]["PhoneNum"].ToString(),
                                dsResult.Tables[0].Rows[0]["FaxNum"].ToString(),
                                dsResult.Tables[1].Rows[0]["AgentName"].ToString(),
                                dsResult.Tables[1].Rows[0]["AgentAddress"].ToString(),
                                dsResult.Tables[1].Rows[0]["InvoiceNumber"].ToString(),
                                dsResult.Tables[1].Rows[0]["InvoiceDate"].ToString(),
                                dsResult.Tables[1].Rows[0]["AgentCode"].ToString(),
                                dsResult.Tables[3].Rows[0]["TotalChargesDueAirline"].ToString(),
                                dsResult.Tables[3].Rows[0]["TotalBaseAmtForST"].ToString(),
                                dsResult.Tables[3].Rows[0]["TotalSTDueAirline"].ToString(),
                                dsResult.Tables[3].Rows[0]["TDSOnCommission"].ToString(),
                                dsResult.Tables[3].Rows[0]["CommissionableSales"].ToString(),
                                dsResult.Tables[3].Rows[0]["AgentsCommission"].ToString(),
                                dsResult.Tables[3].Rows[0]["OtherChargesDueAgent"].ToString(),
                                dsResult.Tables[3].Rows[0]["STOnCommission"].ToString(),
                                dsResult.Tables[3].Rows[0]["TotalDeductions"].ToString(),
                                //dsResult.Tables[3].Rows[0]["NetDueAirlinesAgentINR"].ToString(),
                                dsResult.Tables[4].Rows[0]["NetDueAirlinesAgentINR"].ToString(),
                                dsResult.Tables[3].Rows[0]["WordsINR"].ToString(),
                                dsResult.Tables[0].Rows[0]["ServiceTaxNumber"].ToString(),
                                dsResult.Tables[0].Rows[0]["PanCardNumber"].ToString(),
                                dsResult.Tables[0].Rows[0]["TanNumber"].ToString(),
                                dsResult.Tables[0].Rows[0]["InvoiceType"].ToString(),
                                dsResult.Tables[0].Rows[0]["RegOfficeAddress"].ToString(),
                                dsResult.Tables[0].Rows[0]["RegOfficePhoneNum"].ToString(),
                                Logo.ToArray(),

                                dsResult.Tables[1].Rows[0]["RateLineCurrency"].ToString(),
                                dsResult.Tables[1].Rows[0]["InvoiceDueDate"].ToString(),
                                dsResult.Tables[1].Rows[0]["ContactPerson"].ToString(),Partnerlogo.ToArray());

                                //dsResult.Tables[2].Rows[0]["TotalChargableWeight"].ToString(),
                                //dsResult.Tables[2].Rows[0]["TotalPPFreight"].ToString(),
                                //dsResult.Tables[2].Rows[0]["TotalPPOCDC"].ToString(),
                                //dsResult.Tables[2].Rows[0]["TotalPPOCDA"].ToString(),
                                //dsResult.Tables[2].Rows[0]["TotalCCFreight"].ToString(),
                                //dsResult.Tables[2].Rows[0]["TotalCCOCDC"].ToString(),
                                //dsResult.Tables[2].Rows[0]["TotalCCOCDA"].ToString(),
                                //dsResult.Tables[2].Rows[0]["TotalServiceTax"].ToString(),
                                //dsResult.Tables[2].Rows[0]["SubTotalAWBCharges"].ToString(),
                                //dsResult.Tables[2].Rows[0]["TotalSpotFreight"].ToString()


                                Session["ShowExcel"] = DTExport;

                                Session["AWBData"] = "";
                                DataTable DTAWBData = new DataTable();
                                DTAWBData = dsResult.Tables[2];
                                Session["AWBData"] = DTAWBData;

                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else
                        {
                            //ClientScript.RegisterStartupScript(this.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('No records found.');</SCRIPT>");
                            //lblStatus.Text = "No records found";
                            //lblStatus.ForeColor = Color.Blue;
                            return;
                        }

                    }
                }
            }
        }

        //Contains code to "Save" Agent Invoice xls file, send it as attachment and "Delete" Agent Invoice xls file from disk
        //protected void SaveAgentInvoice()
        //{
        //    //Code to check if folder SCMAgentInvoices exists or not. If not, create.
        //    //string path = @"C:\SCMAgentInvoices";
        //    string path = System.Configuration.ConfigurationManager.AppSettings["InvoicePath"];
        //    try
        //    {
        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }

        //        //Code to Save xls file
        //        Microsoft.Reporting.WebForms.Warning[] warnings;
        //        string[] streamids;
        //        string mimeType;
        //        string encoding;
        //        string extension;
        //        Byte[] bytes;
        //        bytes = ReportViewer1.LocalReport.Render("Excel", string.Empty, out mimeType, out encoding,
        //                    out extension, out streamids, out warnings);
        //        //FileStream fs = new FileStream(@"c:\SCMAgentInvoices\" + Session["CurrentInvoiceNo"].ToString() + ".xls", FileMode.Create);
        //        FileStream fs = new FileStream(System.Configuration.ConfigurationManager.AppSettings["InvoicePath"] + "\\\\" + Session["CurrentInvoiceNo"].ToString() + ".xls", FileMode.Create, FileAccess.ReadWrite);
        //        fs.Write(bytes, 0, bytes.Length);
        //        fs.Close();

        //        //Code to open save dialog box
        //        String FileName = Session["CurrentInvoiceNo"].ToString() + ".xls";
        //        //String FilePath = @"c:\SCMAgentInvoices\" + Session["CurrentInvoiceNo"].ToString() + ".xls"; //Replace this
        //        String FilePath = System.Configuration.ConfigurationManager.AppSettings["InvoicePath"] + "\\\\" + Session["CurrentInvoiceNo"].ToString() + ".xls";
        //        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        //        response.ClearContent();
        //        response.Clear();
        //        response.ContentType = "text/plain";
        //        response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
        //        response.TransmitFile(FilePath);
        //        response.Flush();
        //        response.End();


        //        ////Code to send it as attachment in mail
        //        //SendMail();

        //        //Code to Delete Agent Invoice xls file
        //        //string filePath = @"c:\SCMAgentInvoices\" + Session["CurrentInvoiceNo"].ToString() + ".xls";
        //        //if (System.IO.File.Exists(filePath))
        //        //{
        //        //    System.IO.File.Delete(filePath);
        //        //}

        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //}

        protected void SaveAgentInvoiceFTP()
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["InvoicePath"];
            try
            {
                FtpWebRequest myFtpWebRequest;
                FtpWebResponse myFtpWebResponse;

                string UserName = "qidtech";
                string Password = "QID#tech#2012";
                string FTPPath = "ftp://72.167.41.153/Vijay";

                myFtpWebRequest = (FtpWebRequest)WebRequest.Create(FTPPath + "/" + Session["CurrentInvoiceNo"].ToString() + ".xls");

                if (UserName != "")
                {
                    myFtpWebRequest.Credentials = new NetworkCredential(UserName, Password);
                }

                myFtpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;
                myFtpWebRequest.UseBinary = true;

                //Code to Save xls file
                Microsoft.Reporting.WebForms.Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;
                Byte[] bytes;
                bytes = ReportViewer1.LocalReport.Render("Excel", string.Empty, out mimeType, out encoding,
                            out extension, out streamids, out warnings);
                
                Stream myStream = myFtpWebRequest.GetRequestStream();
                myStream.Write(bytes, 0, bytes.Length);
                myStream.Close();

                Download(FTPPath, Session["CurrentInvoiceNo"].ToString() + ".xls");

            }
            catch (Exception ex)
            {
            }

        }

        private void Download(string filePath, string fileName)
        {
            string UserName = "qidtech";
            string Password = "QID#tech#2012";
            string FTPPath = "ftp://72.167.41.153/Vijay";

            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTPPath + "/" + Session["CurrentInvoiceNo"].ToString() + ".xls");
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(UserName, Password);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            
            // note: since you are writing directly to client, I removed the `file` stream in your original code since we don't need to store the file locally... or so I am assuming
            Response.AddHeader("content-disposition", "attachment;filename=" + Session["CurrentInvoiceNo"].ToString() + ".xls");
            Stream responseStream = response.GetResponseStream();

            byte[] buffer = new byte[2 * 1024];
            int read;
            while ((read = responseStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                Response.OutputStream.Write(buffer, 0, read);
            }

            responseStream.Close();
            response.Close();
        }

        protected void SaveAgentInvoiceFTPPDF()
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["InvoicePath"];
            try
            {
                FtpWebRequest myFtpWebRequest;
                FtpWebResponse myFtpWebResponse;

                string UserName = "qidtech";
                string Password = "QID#tech#2012";
                string FTPPath = "ftp://72.167.41.153/Vijay";

                myFtpWebRequest = (FtpWebRequest)WebRequest.Create(FTPPath + "/" + Session["CurrentInvoiceNo"].ToString() + ".pdf");

                if (UserName != "")
                {
                    myFtpWebRequest.Credentials = new NetworkCredential(UserName, Password);
                }

                myFtpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;
                myFtpWebRequest.UseBinary = true;

                //Code to Save xls file
                Microsoft.Reporting.WebForms.Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;
                Byte[] bytes;
                bytes = ReportViewer1.LocalReport.Render("PDF", string.Empty, out mimeType, out encoding,
                            out extension, out streamids, out warnings);

                Stream myStream = myFtpWebRequest.GetRequestStream();
                myStream.Write(bytes, 0, bytes.Length);
                myStream.Close();

                DownloadPDF(FTPPath, Session["CurrentInvoiceNo"].ToString() + ".pdf");

            }
            catch (Exception ex)
            {
            }

        }

        private void DownloadPDF(string filePath, string fileName)
        {
            string UserName = "qidtech";
            string Password = "QID#tech#2012";
            string FTPPath = "ftp://72.167.41.153/Vijay";

            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTPPath + "/" + Session["CurrentInvoiceNo"].ToString() + ".pdf");
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(UserName, Password);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            // note: since you are writing directly to client, I removed the `file` stream in your original code since we don't need to store the file locally... or so I am assuming
            Response.AddHeader("content-disposition", "attachment;filename=" + Session["CurrentInvoiceNo"].ToString() + ".pdf");
            Stream responseStream = response.GetResponseStream();

            byte[] buffer = new byte[2 * 1024];
            int read;
            while ((read = responseStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                Response.OutputStream.Write(buffer, 0, read);
            }

            responseStream.Close();
            response.Close();
        }

        //protected void SaveAgentInvoicePDF()
        //{
        //    //Code to check if folder SCMAgentInvoices exists or not. If not, create.
        //    //string path = @"C:\SCMAgentInvoices";
        //    string path = System.Configuration.ConfigurationManager.AppSettings["InvoicePath"];
        //    try
        //    {
        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }

        //        //Code to Save xls file
        //        Microsoft.Reporting.WebForms.Warning[] warnings;
        //        string[] streamids;
        //        string mimeType;
        //        string encoding;
        //        string extension;
        //        Byte[] bytes;
        //        bytes = ReportViewer1.LocalReport.Render("PDF", string.Empty, out mimeType, out encoding,
        //                    out extension, out streamids, out warnings);
        //        //FileStream fs = new FileStream(@"c:\SCMAgentInvoices\" + Session["CurrentInvoiceNo"].ToString() + ".pdf", FileMode.Create);
        //        FileStream fs = new FileStream(System.Configuration.ConfigurationManager.AppSettings["InvoicePath"] + "\\\\" + Session["CurrentInvoiceNo"].ToString() + ".pdf", FileMode.Create);
        //        fs.Write(bytes, 0, bytes.Length);
        //        fs.Close();

        //        //Code to open save dialog box
        //        String FileName = Session["CurrentInvoiceNo"].ToString() + ".pdf";
        //        //String FilePath = @"c:\SCMAgentInvoices\" + Session["CurrentInvoiceNo"].ToString() + ".pdf"; //Replace this
        //        String FilePath = System.Configuration.ConfigurationManager.AppSettings["InvoicePath"] + "\\\\" + Session["CurrentInvoiceNo"].ToString() + ".pdf";
        //        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        //        response.ClearContent();
        //        response.Clear();
        //        response.ContentType = "text/plain";
        //        response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
        //        response.TransmitFile(FilePath);
        //        response.Flush();
        //        response.End();


        //        ////Code to send it as attachment in mail
        //        //SendMail();

        //        //Code to Delete Agent Invoice xls file
        //        //string filePath = @"c:\SCMAgentInvoices\" + Session["CurrentInvoiceNo"].ToString() + ".xls";
        //        //if (System.IO.File.Exists(filePath))
        //        //{
        //        //    System.IO.File.Delete(filePath);
        //        //}

        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //}

        public void ItemsSubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("dsCargoInvoice_DataTable2", dtTable2));
        }
    }
}
