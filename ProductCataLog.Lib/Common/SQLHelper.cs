﻿using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;

namespace ProductCataLog.Lib.Common
{
    public enum ContentTypePurpose
    {
        Utility,
        Product
    }
    public enum PageNames
    {
        Login,
        ChangePassword,
        Content,
        News,
        ProductContent,
        ProductInquiry,
        ProductInquiryReport,
        ContactUsReport,
        Dashboard,
        Category,
        Products,
        Config,
        RelatedProduct
    }
    public enum StoredProcedures
    {
        USP_Check_Login,
        USP_Insert_Modules_Error_Log,
        USP_InsertUpdate_Login_Master,
        USP_Select_CategoryList,
        USP_InsertUpdate_Category_Master,
        USP_Select_LoginMasterList,
        USP_Select_ProductList,
        USP_InsertUpdate_ProductPrice_Master,
        USP_InsertUpdate_Product_Master,
        USP_Select_ConfigMasterList,
        USP_Select_ConfigDetailsList,
        USP_InsertUpdate_Configuration_Details,
        USP_Select_ContactUsReport,
        USP_Select_LookUpTypeList,
        USP_Select_ProductInquiryList,
        USP_Select_ContentTypeMasterList,
        USP_Select_GalleryMappingList,
        USP_InsertUpdate_GalleryMapping,
        USP_InsertUpdate_LoginDetails,
        USP_Select_ProductInquiryListUpdate,
        USP_Update_InquiryDetails,
        USP_Select_RelatedProductList,
        USP_InsertUpdate_Related_Product_Master,
        USP_Select_DashboardSummary,
        USP_Select_MenuMasterList,
        USP_Update_ChangePassword,
        USP_Select_ProductDetails
    }
    public enum ContentType
    {
        Document,
        Gallery,
        Video,
        Blogs,
        News,
        Banner,
        Services,
        Clients
    }
    public class Common_Messages
    {
        public const string Save_Failed_Message = "Error in saving {0} data.";
        public const string Save_Success_Message = "Data saved successfully.";
    }
    public class SQLHelper
    {
        public static IConfiguration Configuration { get; private set; }
        public static void InitializeConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static string Connectionstring
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["DefaultConnection"] == null)
                {
                    return Configuration["ConnectionStrings:DefaultConnection"].ToString();
                }
                else

                {
                    return System.Configuration.ConfigurationManager.AppSettings["DefaultConnection"];
                }
            }
        }
        public static DataSet GetData(StoredProcedures eStoredProcedure, object[] ParamName = null, object[] ParamVal = null)
        {
            DataSet dsTable = new DataSet();
            SqlConnection sqlconne = null;
            SqlDataAdapter sqladp = null;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (sqlconne = new SqlConnection(Connectionstring))
                    {
                        using (sqladp = new SqlDataAdapter(eStoredProcedure.ToString(), sqlconne))
                        {
                            //sqlconne.Open();
                            sqladp.SelectCommand.CommandType = CommandType.StoredProcedure;
                            sqladp.SelectCommand.CommandTimeout = 0;
                            if (ParamName != null && ParamName.Length > 0)
                            {
                                for (int i = 0; i < ParamName.Length; i++)
                                {
                                    sqladp.SelectCommand.Parameters.AddWithValue(ParamName[i].ToString(), ParamVal[i]);
                                }
                            }
                            sqladp.Fill(dsTable);

                        }
                    }
                    scope.Complete();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                sqlconne.Close();
                sqladp.Dispose();
            }


            return dsTable;
        }
        public static int ExecuteQuery(StoredProcedures eStoredProcedure, object[] ParamName = null, object[] ParamVal = null)
        {
            int iRetVal = 0;
            SqlConnection sqlconne = null;
            SqlCommand sqlCommand = null;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (sqlconne = new SqlConnection(Connectionstring))
                    {
                        using (SqlDataAdapter sqladp = new SqlDataAdapter(eStoredProcedure.ToString(), sqlconne))
                        {
                            sqlCommand = new SqlCommand(eStoredProcedure.ToString(), sqlconne);
                            if (sqlconne.State != ConnectionState.Open)
                                sqlconne.Open();

                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            sqladp.InsertCommand = sqlCommand;
                            if (ParamName != null && ParamName.Length > 0)
                            {
                                for (int i = 0; i < ParamName.Length; i++)
                                {
                                    sqladp.InsertCommand.Parameters.AddWithValue(ParamName[i].ToString(), ParamVal[i]);
                                }
                            }
                            iRetVal = sqladp.InsertCommand.ExecuteNonQuery();

                        }
                        sqlconne.Close();
                        sqlCommand.Dispose();
                    }
                    scope.Complete();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                sqlconne.Close();
                sqlCommand.Dispose();
            }
            return iRetVal;
        }
        public static DataSet executeQuery(string sqlQry)
        {
            DataSet ds = new DataSet();
            SqlConnection sqlconne = null;
            SqlCommand sqlCommand = null;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (sqlconne = new SqlConnection(Connectionstring))
                    {
                        sqlCommand = new SqlCommand(sqlQry.ToString(), sqlconne);
                        SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);

                        if (sqlconne.State != ConnectionState.Open)
                            sqlconne.Open();

                        int tableNameIndex = sqlQry.ToUpper().IndexOf("FROM");
                        String tableName = sqlQry.Substring(tableNameIndex + 4).Trim();
                        tableNameIndex = tableName.IndexOf(" ");
                        if (tableNameIndex == -1)
                            tableNameIndex = tableName.Length;
                        tableName = tableName.Substring(0, tableNameIndex);
                        tableNameIndex = tableName.IndexOf(",");
                        if (tableNameIndex == -1)
                            tableNameIndex = tableName.Length;
                        tableName = tableName.Substring(0, tableNameIndex).Trim();

                        if (tableName.Length == 0)
                            tableName = "DATASOURCE";

                        sqlAdapter.Fill(ds, tableName);

                        sqlconne.Close();
                        sqlCommand.Dispose();
                    }
                    scope.Complete();
                }
                return ds;
            }
            catch
            {
                throw;
            }
        }
        public static void writeException(Exception ex)
        {
            string str = Environment.CurrentDirectory + "\\log\\";
            string strpath = DateTime.Now.Ticks.ToString() + ".txt";
            string strFinal = str + strpath;

            if (!System.IO.Directory.Exists(str))
            {
                System.IO.Directory.CreateDirectory(str);
            }

            using (Stream stream = File.Create(strFinal))
            {
                TextWriter tw = new StreamWriter(stream); /* this is where the problem was */
                tw.WriteLine(ex.Message + " - " + Environment.NewLine + ex.StackTrace);
                tw.Close();
            }
        }
    }
}
