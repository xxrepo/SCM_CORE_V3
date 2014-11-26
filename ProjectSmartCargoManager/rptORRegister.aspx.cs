﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QID.DataAccess;
using System.Data;

using System.IO;
using Microsoft.Reporting.WebForms;
using BAL;
using System.Drawing;

namespace ProjectSmartCargoManager
{
    public partial class rptORRegister : System.Web.UI.Page
    {
        SQLServer da = new SQLServer(Global.GetConnectionString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    txtfrmdate.Text = Convert.ToDateTime(Session["IT"]).ToString("dd/MM/yyyy");
                    txttodate.Text = Convert.ToDateTime(Session["IT"]).ToString("dd/MM/yyyy");
                }
                
            }
            
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
                Session["dsORRegister"] = null;
                ReportViewer1.Visible = false;
                if (Validate() == false)
                {
                    Session["dsORRegister"] = null;
                    ReportViewer1.Visible = false;
                    return;
                }
                System.IO.MemoryStream Logo = null;

                string Agent, dest;
                DateTime dt1 = new DateTime();
                DateTime dt2 = new DateTime();
                
                dt1 = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", null); //DateTime.Parse(txtFromdate.Text);
                dt2 = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", null); //DateTime.Parse(txtToDate.Text);
                
                object[] param = { dt1, dt2 };
                string[] pname = { "FromDate", "ToDate" };
                SqlDbType[] QueryTypes = { SqlDbType.DateTime, SqlDbType.DateTime };

                ReportBAL objBal = new ReportBAL();
                string strResult = string.Empty;

                try
                {
                    strResult = objBal.GetReportInterval(DateTime.ParseExact(txtfrmdate.Text.Trim(), "dd/MM/yyyy", null), DateTime.ParseExact(txttodate.Text.Trim(), "dd/MM/yyyy", null));
                }
                catch
                {
                    strResult = "";
                }
                finally
                {
                    objBal = null;
                }

                if (strResult != "")
                {
                    lblStatus.ForeColor = Color.Red;
                    lblStatus.Text = strResult;
                    ReportViewer1.Visible = false;
                    txtfrmdate.Focus();
                    return;
                }

                ds = da.SelectRecords("ORRegister", pname, param, QueryTypes);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ReportViewer1.Visible = true;
                        Session["dsORRegister"] = ds.Tables[0];
                       
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
                        if (dt.Rows.Count > 0)
                        {

                            dtt1 = dt.Copy();
                            if (dtt1.Columns.Contains("") == false)
                            {
                                DataColumn col3 = new DataColumn("FromDate", System.Type.GetType("System.String"));
                                col3.DefaultValue = txtfrmdate.Text;
                                dtt1.Columns.Add(col3);
                            }
                        }
                        //todate
                        dtt2 = null;
                        if (dtt1.Rows.Count > 0)
                        {

                            dtt2 = dtt1.Copy();
                            if (dtt2.Columns.Contains("") == false)
                            {
                                DataColumn col4 = new DataColumn("ToDate", System.Type.GetType("System.String"));
                                col4.DefaultValue = txttodate.Text;
                                dtt2.Columns.Add(col4);
                            }
                        }

                        ReportViewer1.ProcessingMode = ProcessingMode.Local;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ORRegister.rdlc");
                        //Customers dsCustomers = GetData("select top 20 * from customers");
                        ReportDataSource datasource = new ReportDataSource("ORRegister_dtORRegister", dtt2);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(datasource);
                        //ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(ItemsSubreportProcessingEventHandlerForSUBrpt);

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
            string Param = "FrmDt:" + txtfrmdate.Text.ToString() + ", ToDt:" + txttodate.Text.ToString();

            objBAL.SaveUserActivityLog(Convert.ToString(Session["IpAddress"]), Session["UserName"].ToString(), "ORRegisterReport", Convert.ToDateTime(Session["IT"]), Param, ErrorLog, Session["Station"].ToString());
        }
        private bool Validate()
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            try
            {
                if (txtfrmdate.Text.Trim() != "" || txttodate.Text.Trim() != "")
                {
                    dt1 = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", null); //DateTime.Parse(txtFromdate.Text);
                    dt2 = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", null); //DateTime.Parse(txtToDate.Text);

                    int chk = DateTime.Compare(dt1, dt2);
                    if (chk > 0)
                    {
                        lblStatus.ForeColor = Color.Red;
                        lblStatus.Text = "Please enter valid To date";
                        txtfrmdate.Focus();
                        return false;
                    }

                }
                ReportBAL objrpt = new ReportBAL();
                string strResult = string.Empty;
                try
                {
                    strResult = objrpt.GetReportInterval(DateTime.ParseExact(txtfrmdate.Text.Trim(), "dd/MM/yyyy", null), DateTime.ParseExact(txttodate.Text.Trim(), "dd/MM/yyyy", null));
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
                Response.Redirect("~/rptORRegister.aspx",false);
            }
            catch (Exception ex)
            { }
        }
    }
}
