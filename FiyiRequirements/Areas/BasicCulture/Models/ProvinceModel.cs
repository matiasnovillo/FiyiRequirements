using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2022
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

namespace FiyiRequirements.Areas.BasicCulture.Models
{
    /// <summary>
    /// Stack:             3 <br/>
    /// Name:              C# Model with stored procedure calls saved on database. <br/>
    /// Function:          Allow you to manipulate information from database using stored procedures.
    ///                    Also, let you make other related actions with the model in question or
    ///                    make temporal copies with random data. <br/>
    /// Fields:            10 <br/> 
    /// Dependencies:      1 models <br/>
    /// Last modification: 20/12/2022 20:14:59
    /// </summary>
    [Serializable]
    public partial class ProvinceModel
    {
        [NotMapped]
        private string _ConnectionString = ConnectionStrings.ConnectionStrings.Development();

        #region Fields
        [Library.ModelAttributeValidator.Key("ProvinceId")]
        public int ProvinceId { get; set; }

        [Library.ModelAttributeValidator.String("Name", false, 1, 500, "")]
        public string Name { get; set; }

        [Library.ModelAttributeValidator.String("GeographicalCoordinates", false, 1, 200, "")]
        public string GeographicalCoordinates { get; set; }

        [Library.ModelAttributeValidator.String("Code", false, 1, 100, "")]
        public string Code { get; set; }

        [Library.ModelAttributeValidator.Key("CountryId")]
        public int CountryId { get; set; }

        public bool Active { get; set; }

        [Library.ModelAttributeValidator.Int("UserCreationId", false, 1, 2147483647)]
        public int UserCreationId { get; set; }

        [Library.ModelAttributeValidator.Int("UserLastModificationId", false, 1, 2147483647)]
        public int UserLastModificationId { get; set; }

        [Library.ModelAttributeValidator.DateTime("DateTimeCreation", false, "01/01/1753 0:00:00.001", "30/12/9998 23:59:59.999")]
        public DateTime DateTimeCreation { get; set; }

        [Library.ModelAttributeValidator.DateTime("DateTimeLastModification", false, "01/01/1753 0:00:00.001", "30/12/9998 23:59:59.999")]
        public DateTime DateTimeLastModification { get; set; }

        public string UserCreationIdFantasyName { get; set; }

        public string UserLastModificationIdFantasyName { get; set; }

        public string CountryIdName { get; set; }
        #endregion

        #region Models that depend on this model
        public virtual List<CityModel> lstCityModel { get; set; } //Foreign Key name: ProvinceId 
        #endregion

        #region Constructors
        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create fastly this model with field ProvinceId = 0 <br/>
        /// Note 1:       Generally used to have fast access to functions of object. <br/>
        /// Note 2:       Need construction with [new] reserved word, as all constructors. <br/>
        /// Fields:       10 <br/> 
        /// Dependencies: 1 models depend on this model <br/>
        /// </summary>
        public ProvinceModel()
        {
            try { ProvinceId = 0; }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with stored information in database using ProvinceId <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       10 <br/> 
        /// Dependencies: 1 models depend on this model <br/>
        /// </summary>
        public ProvinceModel(int ProvinceId)
        {
            try
            {
                List<ProvinceModel> lstProvinceModel = new List<ProvinceModel>();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("ProvinceId", ProvinceId, DbType.Int32, ParameterDirection.Input);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    //In case of not finding anything, Dapper return a List<ProvinceModel>
                    lstProvinceModel = (List<ProvinceModel>)sqlConnection.Query<ProvinceModel>("[dbo].[BasicCulture.Province.Select1ByProvinceId]", dp, commandType: CommandType.StoredProcedure);
                }

                if (lstProvinceModel.Count > 1)
                {
                    throw new Exception("The stored procedure [dbo].[BasicCulture.Province.Select1ByProvinceId] returned more than one register/row");
                }
        
                foreach (ProvinceModel province in lstProvinceModel)
                {
                    this.ProvinceId = province.ProvinceId;
					this.Name = province.Name;
					this.GeographicalCoordinates = province.GeographicalCoordinates;
					this.Code = province.Code;
					this.CountryId = province.CountryId;
					this.Active = province.Active;
					this.UserCreationId = province.UserCreationId;
					this.UserLastModificationId = province.UserLastModificationId;
					this.DateTimeCreation = province.DateTimeCreation;
					this.DateTimeLastModification = province.DateTimeLastModification;
                }
            }
            catch (Exception ex) { throw ex; }
        }


        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with filled parameters <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       10 <br/> 
        /// Dependencies: 1 models depend on this model <br/>
        /// </summary>
        public ProvinceModel(int ProvinceId, string Name, string GeographicalCoordinates, string Code, int CountryId, bool Active, int UserCreationId, int UserLastModificationId, DateTime DateTimeCreation, DateTime DateTimeLastModification)
        {
            try
            {
                this.ProvinceId = ProvinceId;
				this.Name = Name;
				this.GeographicalCoordinates = GeographicalCoordinates;
				this.Code = Code;
				this.CountryId = CountryId;
				this.Active = Active;
				this.UserCreationId = UserCreationId;
				this.UserLastModificationId = UserLastModificationId;
				this.DateTimeCreation = DateTimeCreation;
				this.DateTimeLastModification = DateTimeLastModification;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model (copy) using the given model (original), province, passed by parameter. <br/>
        /// Note:         This constructor is generally used to execute functions using the copied fields <br/>
        /// Fields:       10 <br/> 
        /// Dependencies: 1 models depend on this model <br/>
        /// </summary>
        public ProvinceModel(ProvinceModel province)
        {
            try
            {
                ProvinceId = province.ProvinceId;
				Name = province.Name;
				GeographicalCoordinates = province.GeographicalCoordinates;
				Code = province.Code;
				CountryId = province.CountryId;
				Active = province.Active;
				UserCreationId = province.UserCreationId;
				UserLastModificationId = province.UserLastModificationId;
				DateTimeCreation = province.DateTimeCreation;
				DateTimeLastModification = province.DateTimeLastModification;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The number of rows inside Province</returns>
        public int Count()
        {
            try
            {
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCulture.Province.Count]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                }

                int RowsCounter = Convert.ToInt32(DataTable.Rows[0][0].ToString());

                return RowsCounter;
            }
            catch (Exception ex) { throw ex; }
        }

        #region Queries to DataTable
        /// <summary>
        /// Note: Raise exception when the query find duplicated IDs
        /// </summary>
        public DataTable Select1ByProvinceIdToDataTable(int ProvinceId)
        {
            try
            {
                DataTable DataTable = new DataTable();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("ProvinceId", ProvinceId, DbType.Int32, ParameterDirection.Input);
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCulture.Province.Select1ByProvinceId]", commandType: CommandType.StoredProcedure, param: dp);

                    DataTable.Load(dataReader);
                }

                if (DataTable.Rows.Count > 1)
                { throw new Exception("The stored procedure [dbo].[BasicCulture.Province.Select1ByProvinceId] returned more than one register/row"); }

                return DataTable;
            }
            catch (Exception ex) { throw ex; }
        }

        public DataTable SelectAllToDataTable()
        {
            try
            {
                DataTable DataTable = new DataTable();
                DynamicParameters dp = new DynamicParameters();
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCulture.Province.SelectAll]", commandType: CommandType.StoredProcedure, param: dp);

                    DataTable.Load(dataReader);
                }

                return DataTable;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region Queries to Models
        /// <summary>
        /// Note: Raise exception when the query find duplicated IDs
        /// </summary>
        public ProvinceModel Select1ByProvinceIdToModel(int ProvinceId)
        {
            try
            {
                ProvinceModel ProvinceModel = new ProvinceModel();
                List<ProvinceModel> lstProvinceModel = new List<ProvinceModel>();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("ProvinceId", ProvinceId, DbType.Int32, ParameterDirection.Input);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstProvinceModel = (List<ProvinceModel>)sqlConnection.Query<ProvinceModel>("[dbo].[BasicCulture.Province.Select1ByProvinceId]", dp, commandType: CommandType.StoredProcedure);
                }
        
                if (lstProvinceModel.Count > 1)
                { throw new Exception("The stored procedure [dbo].[BasicCulture.Province.Select1ByProvinceId] returned more than one register/row"); }

                foreach (ProvinceModel province in lstProvinceModel)
                {
                    ProvinceModel.ProvinceId = province.ProvinceId;
					ProvinceModel.Name = province.Name;
					ProvinceModel.GeographicalCoordinates = province.GeographicalCoordinates;
					ProvinceModel.Code = province.Code;
					ProvinceModel.CountryId = province.CountryId;
					ProvinceModel.Active = province.Active;
					ProvinceModel.UserCreationId = province.UserCreationId;
					ProvinceModel.UserLastModificationId = province.UserLastModificationId;
					ProvinceModel.DateTimeCreation = province.DateTimeCreation;
					ProvinceModel.DateTimeLastModification = province.DateTimeLastModification;
                }

                return ProvinceModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<ProvinceModel> SelectAllToList()
        {
            try
            {
                List<ProvinceModel> lstProvinceModel = new List<ProvinceModel>();
                DynamicParameters dp = new DynamicParameters();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstProvinceModel = (List<ProvinceModel>)sqlConnection.Query<ProvinceModel>("[dbo].[BasicCulture.Province.SelectAll]", dp, commandType: CommandType.StoredProcedure);
                }

                return lstProvinceModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public provinceModelQuery SelectAllPagedToModel(provinceModelQuery provinceModelQuery)
        {
            try
            {
                provinceModelQuery.lstProvinceModel = new List<ProvinceModel>();
                DynamicParameters dp = new DynamicParameters();
                dp.Add("QueryString", provinceModelQuery.QueryString, DbType.String, ParameterDirection.Input);
                dp.Add("ActualPageNumber", provinceModelQuery.ActualPageNumber, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsPerPage", provinceModelQuery.RowsPerPage, DbType.Int32, ParameterDirection.Input);
                dp.Add("SorterColumn", provinceModelQuery.SorterColumn, DbType.String, ParameterDirection.Input);
                dp.Add("SortToggler", provinceModelQuery.SortToggler, DbType.Boolean, ParameterDirection.Input);
                dp.Add("TotalRows", provinceModelQuery.TotalRows, DbType.Int32, ParameterDirection.Output);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    provinceModelQuery.lstProvinceModel = (List<ProvinceModel>)sqlConnection.Query<ProvinceModel>("[dbo].[BasicCulture.Province.SelectAllPagedCustom]", dp, commandType: CommandType.StoredProcedure);
                    provinceModelQuery.TotalRows = dp.Get<int>("TotalRows");
                }

                provinceModelQuery.TotalPages = Library.Math.Divide(provinceModelQuery.TotalRows, provinceModelQuery.RowsPerPage, Library.Math.RoundType.RoundUp);

                return provinceModelQuery;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region Non-Queries
        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <returns>NewEnteredId: The ID of the last registry inserted in Province table</returns>
        public int Insert()
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
                
                dp.Add("Name", Name, DbType.String, ParameterDirection.Input);
				dp.Add("GeographicalCoordinates", GeographicalCoordinates, DbType.String, ParameterDirection.Input);
				dp.Add("Code", Code, DbType.String, ParameterDirection.Input);
				dp.Add("CountryId", CountryId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCulture.Province.Insert]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    NewEnteredId = dp.Get<int>("NewEnteredId");
                }
                                
                if (NewEnteredId == 0) { throw new Exception("NewEnteredId with no value"); }

                return NewEnteredId;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <returns>The ID of the last registry inserted in Province table</returns>
        public int Insert(ProvinceModel province)
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("Name", province.Name, DbType.String, ParameterDirection.Input);
				dp.Add("GeographicalCoordinates", province.GeographicalCoordinates, DbType.String, ParameterDirection.Input);
				dp.Add("Code", province.Code, DbType.String, ParameterDirection.Input);
				dp.Add("CountryId", province.CountryId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", province.Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", province.UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", province.UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", province.DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", province.DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCulture.Province.Insert]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    NewEnteredId = dp.Get<int>("NewEnteredId");
                }
                                
                if (NewEnteredId == 0) { throw new Exception("NewEnteredId with no value"); }

                return NewEnteredId;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <returns>The ID of the last registry inserted in Province table</returns>
        public int Insert(string Name, string GeographicalCoordinates, string Code, int CountryId, bool Active, int UserCreationId, int UserLastModificationId, DateTime DateTimeCreation, DateTime DateTimeLastModification)
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("Name", Name, DbType.String, ParameterDirection.Input);
				dp.Add("GeographicalCoordinates", GeographicalCoordinates, DbType.String, ParameterDirection.Input);
				dp.Add("Code", Code, DbType.String, ParameterDirection.Input);
				dp.Add("CountryId", CountryId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCulture.Province.Insert]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    NewEnteredId = dp.Get<int>("NewEnteredId");
                }
                                
                if (NewEnteredId == 0) { throw new Exception("NewEnteredId with no value"); }

                return NewEnteredId;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull update in database
        /// </summary>
        /// <returns>The number of rows updated in Province table</returns>
        public int UpdateByProvinceId()
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("ProvinceId", ProvinceId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Name", Name, DbType.String, ParameterDirection.Input);
				dp.Add("GeographicalCoordinates", GeographicalCoordinates, DbType.String, ParameterDirection.Input);
				dp.Add("Code", Code, DbType.String, ParameterDirection.Input);
				dp.Add("CountryId", CountryId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCulture.Province.UpdateByProvinceId]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    RowsAffected = dp.Get<int>("RowsAffected");
                }
                                
                if (RowsAffected == 0) { throw new Exception("RowsAffected with no value"); }

                return RowsAffected;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull update in database
        /// </summary>
        /// <returns>The number of rows updated in Province table</returns>
        public int UpdateByProvinceId(ProvinceModel province)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("ProvinceId", province.ProvinceId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Name", province.Name, DbType.String, ParameterDirection.Input);
				dp.Add("GeographicalCoordinates", province.GeographicalCoordinates, DbType.String, ParameterDirection.Input);
				dp.Add("Code", province.Code, DbType.String, ParameterDirection.Input);
				dp.Add("CountryId", province.CountryId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", province.Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", province.UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", province.UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", province.DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", province.DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCulture.Province.UpdateByProvinceId]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    RowsAffected = dp.Get<int>("RowsAffected");
                }
                                
                if (RowsAffected == 0) { throw new Exception("RowsAffected with no value"); }

                return RowsAffected;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull update in database
        /// </summary>
        /// <returns>The number of rows updated in Province table</returns>
        public int UpdateByProvinceId(int ProvinceId, string Name, string GeographicalCoordinates, string Code, int CountryId, bool Active, int UserCreationId, int UserLastModificationId, DateTime DateTimeCreation, DateTime DateTimeLastModification)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("ProvinceId", ProvinceId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Name", Name, DbType.String, ParameterDirection.Input);
				dp.Add("GeographicalCoordinates", GeographicalCoordinates, DbType.String, ParameterDirection.Input);
				dp.Add("Code", Code, DbType.String, ParameterDirection.Input);
				dp.Add("CountryId", CountryId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCulture.Province.UpdateByProvinceId]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    RowsAffected = dp.Get<int>("RowsAffected");
                }
                                
                if (RowsAffected == 0) { throw new Exception("RowsAffected with no value"); }

                return RowsAffected;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        ///
        public void DeleteAll()
        {
            try
            {
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCulture.Province.DeleteAll]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <returns>The number of rows deleted in Province table</returns>
        public int DeleteByProvinceId()
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
        
                dp.Add("ProvinceId", ProvinceId, DbType.Int32, ParameterDirection.Input);        
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCulture.Province.DeleteByProvinceId]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    RowsAffected = dp.Get<int>("RowsAffected");
                }
                                
                if (RowsAffected == 0) { throw new Exception("RowsAffected with no value"); }

                return RowsAffected;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <returns>The number of rows affected in Province table</returns>
        public int DeleteByProvinceId(int ProvinceId)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
        
                dp.Add("ProvinceId", ProvinceId, DbType.Int32, ParameterDirection.Input);        
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCulture.Province.DeleteByProvinceId]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    RowsAffected = dp.Get<int>("RowsAffected");
                }
                                
                if (RowsAffected == 0) { throw new Exception("RowsAffected with no value"); }

                return RowsAffected;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        /// <summary>
        /// Function: Take the model stored in the given byte array to return the model. <br/>
        /// Note 1:   Similar to a decryptor function. <br/>
        /// Note 2:   The model need the [Serializable] decorator in model definition. <br/>
        /// </summary>
        public ProvinceModel ByteArrayToProvinceModel<T>(byte[] arrProvinceModel)
        {
            try
            {
                if (arrProvinceModel == null)
                { return new ProvinceModel(); }
                BinaryFormatter BinaryFormatter = new BinaryFormatter();
                using MemoryStream MemoryStream = new MemoryStream(arrProvinceModel);
                object Object = BinaryFormatter.Deserialize(MemoryStream);
                return (ProvinceModel)Object;
            }
            catch (Exception ex)
            { throw ex; }
        }
        
        /// <summary>
        /// Function: Show all information (fields) inside the model during depuration mode.
        /// </summary>
        public override string ToString()
        {
            return $"ProvinceId: {ProvinceId}, " +
				$"Name: {Name}, " +
				$"GeographicalCoordinates: {GeographicalCoordinates}, " +
				$"Code: {Code}, " +
				$"CountryId: {CountryId}, " +
				$"Active: {Active}, " +
				$"UserCreationId: {UserCreationId}, " +
				$"UserLastModificationId: {UserLastModificationId}, " +
				$"DateTimeCreation: {DateTimeCreation}, " +
				$"DateTimeLastModification: {DateTimeLastModification}";
        }

        public string ToStringOnlyValuesForHTML()
        {
            return $@"<tr>
                <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{ProvinceId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Name}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{GeographicalCoordinates}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Code}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{CountryId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Active}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{UserCreationId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{UserLastModificationId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{DateTimeCreation}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{DateTimeLastModification}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
                </tr>";
        }
    }

    /// <summary>
    /// Virtual model used for [dbo].[BasicCulture.Province.SelectAllPaged] stored procedure
    /// </summary>
    public partial class provinceModelQuery 
    {
        public string QueryString { get; set; }
        public int ActualPageNumber { get; set; }
        public int RowsPerPage { get; set; }
        public string SorterColumn { get; set; }
        public bool SortToggler { get; set; }
        public int TotalRows { get; set; }
        public int TotalPages { get; set; }
        public List<ProvinceModel> lstProvinceModel { get; set; }
    }
}