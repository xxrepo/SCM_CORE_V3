﻿using System;using System.Collections.Generic;using System.Linq;using System.Web;using System.Web.UI;using System.Web.UI.WebControls;using System.Data;using QID.DataAccess;using System.Drawing;using System.Data.SqlClient;using System.Security.Cryptography;using System.Text;using System.IO;using System.Text.RegularExpressions;using BAL;//using DataDynamics.Reports.Rendering.Pdf;//using DataDynamics.Reports.Rendering.IO;//using DataDynamics.Reports.Rendering.Excel;using Microsoft.Reporting.WebForms;namespace ProjectSmartCargoManager{    public partial class ClaimsDownload : System.Web.UI.Page    {        protected void Page_Load(object sender, EventArgs e)        {            try            {                if (!IsPostBack)                {                    if (Request.QueryString["ClaimID"] != null && Request.QueryString["DocType"] != null)                    {                        string ClaimID = Request.QueryString["ClaimID"].ToString();                        string DocType = Request.QueryString["DocType"].ToString();                        SQLServer db = new SQLServer(Global.GetConnectionString());                        string[] QueryName = { "ClaimID", "DocType" };                        object[] QueryValue = { ClaimID, DocType };                        SqlDbType[] QueryTypes = { SqlDbType.VarChar, SqlDbType.VarChar };                        DataSet ds = db.SelectRecords("spGetClaimDocumentPerDocType", QueryName, QueryValue, QueryTypes);                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)                        {                            Byte[] Document = (Byte[])ds.Tables[0].Rows[0]["Document"];                            if (Document != null && Document.Length > 0)                            {                                Response.Clear();                                Response.AddHeader("content-disposition", "attachment; filename=" + ds.Tables[0].Rows[0]["DocName"]);                                Response.BinaryWrite(Document);                            }                        }                    }                }            }            catch (Exception ex)            { }        }    }}