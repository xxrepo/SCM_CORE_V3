﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QID.DataAccess;
using System.Data;

using System.IO;
using BAL;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Drawing;
using System.Text;

namespace ProjectSmartCargoManager
{
    public partial class rptComparative : System.Web.UI.Page
    {
        SQLServer da = new SQLServer(Global.GetConnectionString());
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtfrmdate.Text = Convert.ToDateTime(Session["IT"]).ToString("dd/MM/yyyy");
                    txtFromDate.Text = Convert.ToDateTime(Session["IT"]).ToString("dd/MM/yyyy");
                    DataSet ds = da.SelectRecords("spGetAirportCodes");
                    if (ds != null)
                    {
                        ddlStation.DataSource = ds;
                        ddlStation.DataMember = ds.Tables[0].TableName;
                        ddlStation.DataTextField = "Airport";
                        ddlStation.DataValueField = "AirportCode";
                        ddlStation.DataBind();
                        ddlStation.Items.Insert(0, new ListItem("Select", "Select"));
                        ddlStation.SelectedIndex = -1;
                    }
                }
                
            }
            catch (Exception ex)
            { }
        }

        protected void btnList_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            string ErrorLog = string.Empty;
            DataTable dt = new DataTable();
            DataTable dtt = new DataTable();
            DataTable dtt1 = new DataTable();
            DataTable dtt2 = new DataTable();
            DataSet ds = null;
            try
            {
                Session["dsComparative"] = null;
                ReportViewer1.Visible = false;
                if (Validate() == false)
                {
                    Session["dsComparative"] = null;
                    ReportViewer1.Visible = false;
                    return;
                }
                System.IO.MemoryStream Logo = null;

                string Agent, dest;
                DateTime dt1 = new DateTime();
                DateTime dt2 = new DateTime();

               
                
                dt1 = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", null); //DateTime.Parse(txtFromdate.Text);
                dt2 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null); //DateTime.Parse(txtToDate.Text);
                                
                object[] param = { dt1,rdbweekmonth.SelectedValue,ddlStation.SelectedValue,dt2 };
                string[] pname = { "Date", "Flag", "Station", "DateFrom" };
                SqlDbType[] QueryTypes = { SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.DateTime };

                ReportBAL objBal = new ReportBAL();
                string strResult = string.Empty;



                ds = da.SelectRecords("SPComparative", pname, param, QueryTypes);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ReportViewer1.Visible = true;
                        Session["dsComparative"] = ds.Tables[0];
                        
                        //Logo
                        try
                        {
                            Logo = CommonUtility.GetImageStream(Page.Server);
                            //System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                        }
                        catch (Exception ex)
                        {
                            Logo = new System.IO.MemoryStream();
                        }

                        dt = null;
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            dt = ds.Tables[0].Copy();
                            if (dt.Columns.Contains("Logo") == false)
                            {
                                DataColumn col1 = new DataColumn("Logo", System.Type.GetType("System.Byte[]"));
                                col1.DefaultValue = Logo.ToArray();
                                dt.Columns.Add(col1);
                            }
                        }
                        
                        //fromdate
                        dtt1 = null;
                        if (dtt.Rows.Count > 0)
                        {

                            dtt1 = dtt.Copy();
                            if (dtt1.Columns.Contains("") == false)
                            {
                                DataColumn col3 = new DataColumn("FromDate", System.Type.GetType("System.String"));
                                col3.DefaultValue = txtfrmdate.Text;
                                dtt1.Columns.Add(col3);
                            }
                        }
                       

                        ReportViewer1.ProcessingMode = ProcessingMode.Local;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Comparative.rdlc");
                        //Customers dsCustomers = GetData("select top 20 * from customers");
                        ReportDataSource datasource = new ReportDataSource("Comparative_dtComparative", dt);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(datasource);
                        //ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(ItemsSubreportProcessingEventHandlerForSUBrpt);

                        //btnExport.Visible = true;
                        SaveUserActivityLog("");
                    }
                    else
                    {
                        lblStatus.Text = "No records found.";
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                        ReportViewer1.Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLog = ex.Message;
                SaveUserActivityLog(ErrorLog);
            }
            finally
            {
                if (dt != null)
                    dt.Dispose();
                if (ds != null)
                    ds.Dispose();
                if (da != null)
                    da = null;
                if (dtt != null)
                    dtt.Dispose();
                if (dtt1 != null)
                    dtt1.Dispose();
                if (dtt2 != null)
                    dtt2.Dispose();
            }
        }

        private void SaveUserActivityLog(string ErrorLog)
        {
            ReportBAL objBAL = new ReportBAL();
            // taking all parameters as user selected in report in one variable - "Param"
            string Param = "FrmDt:" + txtfrmdate.Text.ToString();

            objBAL.SaveUserActivityLog(Convert.ToString(Session["IpAddress"]), Session["UserName"].ToString(), "Cargo Performance Report", Convert.ToDateTime(Session["IT"]), Param, ErrorLog, Session["Station"].ToString());
        }
        private bool Validate()
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            try
            {
                if (txtfrmdate.Text.Trim() != "")
                {
                    dt1 = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", null); //DateTime.Parse(txtFromdate.Text);
                    
                }
                if (txtfrmdate.Text.Trim() == "")
                {
                    lblStatus.ForeColor = Color.Red;
                    lblStatus.Text = "Please enter valid date";
                    txtfrmdate.Focus();
                    return false;
                }
                ReportBAL objrpt = new ReportBAL();
                string strResult = string.Empty;
                try
                {
                   // strResult = objrpt.GetReportInterval(DateTime.ParseExact(txtfrmdate.Text.Trim(), "dd/MM/yyyy", null), DateTime.ParseExact(txttodate.Text.Trim(), "dd/MM/yyyy", null));
                }
                catch
                {
                    strResult = "";
                }
                if (strResult != "")
                {
                    lblStatus.ForeColor = Color.Red;
                    lblStatus.Text = strResult;
                    txtfrmdate.Focus();
                    return false;
                }



            }
            catch (Exception ex)
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "Please enter valid date format ex:dd/MM/yyyy";
                txtfrmdate.Focus();
                return false;
            }

            return true;
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/rptComparative.aspx",false);
            }
            catch (Exception ex)
            { }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {

            DataSet dt = null;
            Session["dsComparative"] = null;
            lblStatus.Text = "";
            try
            {
                if ((DataSet)Session["dsComparative"] == null)
                    GetData();

                dt = (DataSet)Session["dsComparative"];
                if (rdbweekmonth.SelectedValue == "W")
                {
                    dt.Tables[0].Columns[1].ColumnName = "PrevWeekChargableWeight";
                    dt.Tables[0].Columns[2].ColumnName = "PrevWeekWeightCharged";
                    dt.Tables[0].Columns[3].ColumnName = "PrevWeekGrossRevenue";
                    dt.Tables[0].Columns[4].ColumnName = "PrevWeekGrossYeild";
                    dt.Tables[0].TableName = "Cargo Performance Details";
                }
                if (rdbweekmonth.SelectedValue == "M")
                {
                    try
                    {
                        dt.Tables[0].Columns[1].ColumnName = "PrevMonthChargableWeight";
                        dt.Tables[0].Columns[2].ColumnName = "PrevMonthWeightCharged";
                        dt.Tables[0].Columns[3].ColumnName = "PrevMonthGrossRevenue";
                        dt.Tables[0].Columns[4].ColumnName = "PrevMonthGrossYeild";
                        dt.Tables[0].Columns[5].ColumnName = "CurrentMonthChargableWeight";
                        dt.Tables[0].Columns[6].ColumnName = "CurrentMonthWeightCharged";
                        dt.Tables[0].Columns[7].ColumnName = "CurrentMonthGrossRevenue";
                        dt.Tables[0].Columns[8].ColumnName = "CurrentMonthGrossYeild";
                    }
                    catch (Exception ex)
                    { }
                    dt.Tables[0].TableName = "Cargo Performance Details";
                    dt.Tables[1].TableName = "Monthly Comparative (DOM)";
                    dt.Tables[2].TableName = "Monthly Comparative (INT)";
                }

                if (Session["dsComparative"] == null && dt == null)
                {
                    lblStatus.Text = "No records found";
                    lblStatus.ForeColor = Color.Red;
                    SaveUserActivityLog(lblStatus.Text);
                    ReportViewer1.Visible = false;
                    return;
                }
                if (rdbweekmonth.SelectedValue == "W")
                    ExcelHelper.ToExcel(dt, "CargoPerformanceReportWeekly" + txtFromDate.Text + "-" + txtfrmdate.Text + ".xls", Response);
                if (rdbweekmonth.SelectedValue == "M")
                    ExcelHelper.ToExcel(dt, "CargoPerformanceReportMonthly" + txtfrmdate.Text + ".xls", Response);
                Response.End();
                Response.Flush();
                Response.Close();
            }
            catch (Exception ex)
            { }
            finally
            {

                dt = null;
            }
        }

        protected void GetData()
        {
            lblStatus.Text = "";
            string ErrorLog = string.Empty;
            DataTable dt = new DataTable();
            DataTable dtt = new DataTable();
            DataTable dtt1 = new DataTable();
            DataTable dtt2 = new DataTable();
            DataSet ds = null;
            try
            {
                Session["dsComparative"] = null;
                ReportViewer1.Visible = false;
                if (Validate() == false)
                {
                    Session["dsComparative"] = null;
                    ReportViewer1.Visible = false;
                    return;
                }
                System.IO.MemoryStream Logo = null;

                string Agent, dest;
                DateTime dt1 = new DateTime();
                DateTime dt2 = new DateTime();



                dt1 = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", null); //DateTime.Parse(txtFromdate.Text);
                
                object[] param = { dt1, rdbweekmonth.SelectedValue, ddlStation.SelectedValue, dt2 };
                string[] pname = { "Date", "Flag", "Station", "DateFrom" };
                SqlDbType[] QueryTypes = { SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.DateTime };

                ReportBAL objBal = new ReportBAL();
                string strResult = string.Empty;



                ds = da.SelectRecords("SPComparative", pname, param, QueryTypes);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ReportViewer1.Visible = true;
                        Session["dsComparative"] = ds;//.Tables[0];

                        
                        SaveUserActivityLog("");
                    }
                    else
                    {
                        lblStatus.Text = "No records found.";
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                        ReportViewer1.Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLog = ex.Message;
                SaveUserActivityLog(ErrorLog);
            }
            finally
            {
                if (dt != null)
                    dt.Dispose();
                if (ds != null)
                    ds.Dispose();
                if (da != null)
                    da = null;
                if (dtt != null)
                    dtt.Dispose();
                if (dtt1 != null)
                    dtt1.Dispose();
                if (dtt2 != null)
                    dtt2.Dispose();
            }
        }
        //ExcelHelper.cs

        public class ExcelHelper
        {
            //Row limits older excel verion per sheet, the row limit for excel 2003 is 65536
            const int rowLimit = 65000;

            private static string getWorkbookTemplate()
            {
                var sb = new StringBuilder(818);
                sb.AppendFormat(@"<?xml version=""1.0""?>{0}", Environment.NewLine);
                sb.AppendFormat(@"<?mso-application progid=""Excel.Sheet""?>{0}", Environment.NewLine);
                sb.AppendFormat(@"<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet""{0}", Environment.NewLine);
                sb.AppendFormat(@" xmlns:o=""urn:schemas-microsoft-com:office:office""{0}", Environment.NewLine);
                sb.AppendFormat(@" xmlns:x=""urn:schemas-microsoft-com:office:excel""{0}", Environment.NewLine);
                sb.AppendFormat(@" xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet""{0}", Environment.NewLine);
                sb.AppendFormat(@" xmlns:html=""http://www.w3.org/TR/REC-html40"">{0}", Environment.NewLine);
                sb.AppendFormat(@" <Styles>{0}", Environment.NewLine);
                sb.AppendFormat(@"  <Style ss:ID=""Default"" ss:Name=""Normal"">{0}", Environment.NewLine);
                sb.AppendFormat(@"   <Alignment ss:Vertical=""Bottom""/>{0}", Environment.NewLine);
                sb.AppendFormat(@"   <Borders/>{0}", Environment.NewLine);
                sb.AppendFormat(@"   <Font ss:FontName=""Calibri"" x:Family=""Swiss"" ss:Size=""11"" ss:Color=""#000000""/>{0}", Environment.NewLine);
                sb.AppendFormat(@"   <Interior/>{0}", Environment.NewLine);
                sb.AppendFormat(@"   <NumberFormat/>{0}", Environment.NewLine);
                sb.AppendFormat(@"   <Protection/>{0}", Environment.NewLine);
                sb.AppendFormat(@"  </Style>{0}", Environment.NewLine);
                sb.AppendFormat(@"  <Style ss:ID=""s62"">{0}", Environment.NewLine);
                sb.AppendFormat(@"   <Font ss:FontName=""Calibri"" x:Family=""Swiss"" ss:Size=""11"" ss:Color=""#000000""{0}", Environment.NewLine);
                sb.AppendFormat(@"    ss:Bold=""1""/>{0}", Environment.NewLine);
                sb.AppendFormat(@"  </Style>{0}", Environment.NewLine);
                sb.AppendFormat(@"  <Style ss:ID=""s63"">{0}", Environment.NewLine);
                sb.AppendFormat(@"   <NumberFormat ss:Format=""Short Date""/>{0}", Environment.NewLine);
                sb.AppendFormat(@"  </Style>{0}", Environment.NewLine);
                sb.AppendFormat(@" </Styles>{0}", Environment.NewLine);
                sb.Append(@"{0}\r\n</Workbook>");
                return sb.ToString();
            }

            private static string replaceXmlChar(string input)
            {
                input = input.Replace("&", "&amp");
                input = input.Replace("<", "&lt;");
                input = input.Replace(">", "&gt;");
                input = input.Replace("\"", "&quot;");
                input = input.Replace("'", "&apos;");
                return input;
            }

            private static string getCell(Type type, object cellData)
            {
                var data = (cellData is DBNull) ? "" : cellData;
                if (type.Name.Contains("Int") || type.Name.Contains("Double") || type.Name.Contains("Decimal")) return string.Format("<Cell><Data ss:Type=\"Number\">{0}</Data></Cell>", data);
                if (type.Name.Contains("Date") && data.ToString() != string.Empty)
                {
                    return string.Format("<Cell ss:StyleID=\"s63\"><Data ss:Type=\"DateTime\">{0}</Data></Cell>", Convert.ToDateTime(data).ToString("yyyy-MM-dd"));
                }
                return string.Format("<Cell><Data ss:Type=\"String\">{0}</Data></Cell>", replaceXmlChar(data.ToString()));
            }
            private static string getWorksheets(DataSet source)
            {
                var sw = new StringWriter();
                if (source == null || source.Tables.Count == 0)
                {
                    sw.Write("<Worksheet ss:Name=\"Sheet1\">\r\n<Table>\r\n<Row><Cell><Data ss:Type=\"String\"></Data></Cell></Row>\r\n</Table>\r\n</Worksheet>");
                    return sw.ToString();
                }
                foreach (DataTable dt in source.Tables)
                {
                    if (dt.Rows.Count == 0)
                        sw.Write("<Worksheet ss:Name=\"" + replaceXmlChar(dt.TableName) + "\">\r\n<Table>\r\n<Row><Cell  ss:StyleID=\"s62\"><Data ss:Type=\"String\"></Data></Cell></Row>\r\n</Table>\r\n</Worksheet>");
                    else
                    {
                        //write each row data                
                        var sheetCount = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if ((i % rowLimit) == 0)
                            {
                                //add close tags for previous sheet of the same data table
                                if ((i / rowLimit) > sheetCount)
                                {
                                    sw.Write("\r\n</Table>\r\n</Worksheet>");
                                    sheetCount = (i / rowLimit);
                                }
                                sw.Write("\r\n<Worksheet ss:Name=\"" + replaceXmlChar(dt.TableName) +
                                         (((i / rowLimit) == 0) ? "" : Convert.ToString(i / rowLimit)) + "\">\r\n<Table>");
                                //write column name row
                                sw.Write("\r\n<Row>");
                                foreach (DataColumn dc in dt.Columns)
                                    sw.Write(string.Format("<Cell ss:StyleID=\"s62\"><Data ss:Type=\"String\">{0}</Data></Cell>", replaceXmlChar(dc.ColumnName)));
                                sw.Write("</Row>");
                            }
                            sw.Write("\r\n<Row>");
                            foreach (DataColumn dc in dt.Columns)
                                sw.Write(getCell(dc.DataType, dt.Rows[i][dc.ColumnName]));
                            sw.Write("</Row>");
                        }
                        sw.Write("\r\n</Table>\r\n</Worksheet>");
                    }
                }

                return sw.ToString();
            }
            public static string GetExcelXml(DataTable dtInput, string filename)
            {
                var excelTemplate = getWorkbookTemplate();
                var ds = new DataSet();
                ds.Tables.Add(dtInput.Copy());
                var worksheets = getWorksheets(ds);
                var excelXml = string.Format(excelTemplate, worksheets);
                return excelXml;
            }

            public static string GetExcelXml(DataSet dsInput, string filename)
            {
                var excelTemplate = getWorkbookTemplate();
                var worksheets = getWorksheets(dsInput);
                var excelXml = string.Format(excelTemplate, worksheets);
                return excelXml;
            }

            public static void ToExcel(DataSet dsInput, string filename, HttpResponse response)
            {
                var excelXml = GetExcelXml(dsInput, filename);
                response.Clear();
                response.AppendHeader("Content-Type", "application/vnd.ms-excel");
                response.AppendHeader("Content-disposition", "attachment; filename=" + filename);
                response.Write(excelXml);
                response.Flush();
                response.End();
            }

            public static void ToExcel(DataTable dtInput, string filename, HttpResponse response)
            {
                var ds = new DataSet();
                ds.Tables.Add(dtInput.Copy());
                ToExcel(ds, filename, response);
            }
        }
    }
}