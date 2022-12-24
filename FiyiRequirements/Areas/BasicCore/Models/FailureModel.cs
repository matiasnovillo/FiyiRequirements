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

namespace FiyiRequirements.Areas.BasicCore.Models
{
    /// <summary>
    /// Stack:             3 <br/>
    /// Name:              C# Model with stored procedure calls saved on database. <br/>
    /// Function:          Allow you to manipulate information from database using stored procedures.
    ///                    Also, let you make other related actions with the model in question or
    ///                    make temporal copies with random data. <br/>
    /// Fields:            12 <br/> 
    /// Dependencies:      0 models <br/>
    /// Last modification: 20/12/2022 19:54:13
    /// </summary>
    [Serializable]
    public partial class FailureModel
    {
        [NotMapped]
        private string _ConnectionString = "data source =.; initial catalog = fiyistack_FiyiRequirements; Integrated Security = SSPI; MultipleActiveResultSets=True;Pooling=false;Persist Security Info=True;App=EntityFramework;TrustServerCertificate=True";

        #region Fields
        [Library.ModelAttributeValidator.Key("FailureId")]
        public int FailureId { get; set; }

        [Library.ModelAttributeValidator.Int("HTTPCode", false, 1, 2147483647)]
        public int HTTPCode { get; set; }

        [Library.ModelAttributeValidator.Int("EmergencyLevel", false, 1, 2147483647)]
        public int EmergencyLevel { get; set; }

        [Library.ModelAttributeValidator.String("Message", false, 0, 8000, "")]
        public string Message { get; set; }

        [Library.ModelAttributeValidator.String("StackTrace", false, 0, 8000, "")]
        public string StackTrace { get; set; }

        [Library.ModelAttributeValidator.String("Source", false, 0, 8000, "")]
        public string Source { get; set; }

        [Library.ModelAttributeValidator.String("Comment", false, 0, 8000, "")]
        public string Comment { get; set; }

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
        #endregion

        #region Models that depend on this model

        #endregion

        #region Constructors
        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create fastly this model with field FailureId = 0 <br/>
        /// Note 1:       Generally used to have fast access to functions of object. <br/>
        /// Note 2:       Need construction with [new] reserved word, as all constructors. <br/>
        /// Fields:       12 <br/> 
        /// Dependencies: 0 models depend on this model <br/>
        /// </summary>
        public FailureModel()
        {
            try { FailureId = 0; }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with stored information in database using FailureId <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       12 <br/> 
        /// Dependencies: 0 models depend on this model <br/>
        /// </summary>
        public FailureModel(int FailureId)
        {
            try
            {
                List<FailureModel> lstFailureModel = new List<FailureModel>();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("FailureId", FailureId, DbType.Int32, ParameterDirection.Input);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    //In case of not finding anything, Dapper return a List<FailureModel>
                    lstFailureModel = (List<FailureModel>)sqlConnection.Query<FailureModel>("[dbo].[BasicCore.Failure.Select1ByFailureId]", dp, commandType: CommandType.StoredProcedure);
                }

                if (lstFailureModel.Count > 1)
                {
                    throw new Exception("The stored procedure [dbo].[BasicCore.Failure.Select1ByFailureId] returned more than one register/row");
                }
        
                foreach (FailureModel failure in lstFailureModel)
                {
                    this.FailureId = failure.FailureId;
					this.HTTPCode = failure.HTTPCode;
					this.EmergencyLevel = failure.EmergencyLevel;
					this.Message = failure.Message;
					this.StackTrace = failure.StackTrace;
					this.Source = failure.Source;
					this.Comment = failure.Comment;
					this.Active = failure.Active;
					this.UserCreationId = failure.UserCreationId;
					this.UserLastModificationId = failure.UserLastModificationId;
					this.DateTimeCreation = failure.DateTimeCreation;
					this.DateTimeLastModification = failure.DateTimeLastModification;
                }
            }
            catch (Exception ex) { throw ex; }
        }


        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with filled parameters <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       12 <br/> 
        /// Dependencies: 0 models depend on this model <br/>
        /// </summary>
        public FailureModel(int FailureId, int HTTPCode, int EmergencyLevel, string Message, string StackTrace, string Source, string Comment, bool Active, int UserCreationId, int UserLastModificationId, DateTime DateTimeCreation, DateTime DateTimeLastModification)
        {
            try
            {
                this.FailureId = FailureId;
				this.HTTPCode = HTTPCode;
				this.EmergencyLevel = EmergencyLevel;
				this.Message = Message;
				this.StackTrace = StackTrace;
				this.Source = Source;
				this.Comment = Comment;
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
        /// Function:     Create this model (copy) using the given model (original), failure, passed by parameter. <br/>
        /// Note:         This constructor is generally used to execute functions using the copied fields <br/>
        /// Fields:       12 <br/> 
        /// Dependencies: 0 models depend on this model <br/>
        /// </summary>
        public FailureModel(FailureModel failure)
        {
            try
            {
                FailureId = failure.FailureId;
				HTTPCode = failure.HTTPCode;
				EmergencyLevel = failure.EmergencyLevel;
				Message = failure.Message;
				StackTrace = failure.StackTrace;
				Source = failure.Source;
				Comment = failure.Comment;
				Active = failure.Active;
				UserCreationId = failure.UserCreationId;
				UserLastModificationId = failure.UserLastModificationId;
				DateTimeCreation = failure.DateTimeCreation;
				DateTimeLastModification = failure.DateTimeLastModification;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The number of rows inside Failure</returns>
        public int Count()
        {
            try
            {
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Failure.Count]", commandType: CommandType.StoredProcedure, param: dp);
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
        public DataTable Select1ByFailureIdToDataTable(int FailureId)
        {
            try
            {
                DataTable DataTable = new DataTable();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("FailureId", FailureId, DbType.Int32, ParameterDirection.Input);
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Failure.Select1ByFailureId]", commandType: CommandType.StoredProcedure, param: dp);

                    DataTable.Load(dataReader);
                }

                if (DataTable.Rows.Count > 1)
                { throw new Exception("The stored procedure [dbo].[BasicCore.Failure.Select1ByFailureId] returned more than one register/row"); }

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
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Failure.SelectAll]", commandType: CommandType.StoredProcedure, param: dp);

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
        public FailureModel Select1ByFailureIdToModel(int FailureId)
        {
            try
            {
                FailureModel FailureModel = new FailureModel();
                List<FailureModel> lstFailureModel = new List<FailureModel>();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("FailureId", FailureId, DbType.Int32, ParameterDirection.Input);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstFailureModel = (List<FailureModel>)sqlConnection.Query<FailureModel>("[dbo].[BasicCore.Failure.Select1ByFailureId]", dp, commandType: CommandType.StoredProcedure);
                }
        
                if (lstFailureModel.Count > 1)
                { throw new Exception("The stored procedure [dbo].[BasicCore.Failure.Select1ByFailureId] returned more than one register/row"); }

                foreach (FailureModel failure in lstFailureModel)
                {
                    FailureModel.FailureId = failure.FailureId;
					FailureModel.HTTPCode = failure.HTTPCode;
					FailureModel.EmergencyLevel = failure.EmergencyLevel;
					FailureModel.Message = failure.Message;
					FailureModel.StackTrace = failure.StackTrace;
					FailureModel.Source = failure.Source;
					FailureModel.Comment = failure.Comment;
					FailureModel.Active = failure.Active;
					FailureModel.UserCreationId = failure.UserCreationId;
					FailureModel.UserLastModificationId = failure.UserLastModificationId;
					FailureModel.DateTimeCreation = failure.DateTimeCreation;
					FailureModel.DateTimeLastModification = failure.DateTimeLastModification;
                }

                return FailureModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<FailureModel> SelectAllToList()
        {
            try
            {
                List<FailureModel> lstFailureModel = new List<FailureModel>();
                DynamicParameters dp = new DynamicParameters();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstFailureModel = (List<FailureModel>)sqlConnection.Query<FailureModel>("[dbo].[BasicCore.Failure.SelectAll]", dp, commandType: CommandType.StoredProcedure);
                }

                return lstFailureModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public failureModelQuery SelectAllPagedToModel(failureModelQuery failureModelQuery)
        {
            try
            {
                failureModelQuery.lstFailureModel = new List<FailureModel>();
                DynamicParameters dp = new DynamicParameters();
                dp.Add("QueryString", failureModelQuery.QueryString, DbType.String, ParameterDirection.Input);
                dp.Add("ActualPageNumber", failureModelQuery.ActualPageNumber, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsPerPage", failureModelQuery.RowsPerPage, DbType.Int32, ParameterDirection.Input);
                dp.Add("SorterColumn", failureModelQuery.SorterColumn, DbType.String, ParameterDirection.Input);
                dp.Add("SortToggler", failureModelQuery.SortToggler, DbType.Boolean, ParameterDirection.Input);
                dp.Add("TotalRows", failureModelQuery.TotalRows, DbType.Int32, ParameterDirection.Output);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    failureModelQuery.lstFailureModel = (List<FailureModel>)sqlConnection.Query<FailureModel>("[dbo].[BasicCore.Failure.SelectAllPagedCustom]", dp, commandType: CommandType.StoredProcedure);
                    failureModelQuery.TotalRows = dp.Get<int>("TotalRows");
                }

                failureModelQuery.TotalPages = Library.Math.Divide(failureModelQuery.TotalRows, failureModelQuery.RowsPerPage, Library.Math.RoundType.RoundUp);

                return failureModelQuery;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region Non-Queries
        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <returns>NewEnteredId: The ID of the last registry inserted in Failure table</returns>
        public int Insert()
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
                
                dp.Add("HTTPCode", HTTPCode, DbType.Int32, ParameterDirection.Input);
				dp.Add("EmergencyLevel", EmergencyLevel, DbType.Int32, ParameterDirection.Input);
				dp.Add("Message", Message, DbType.String, ParameterDirection.Input);
				dp.Add("StackTrace", StackTrace, DbType.String, ParameterDirection.Input);
				dp.Add("Source", Source, DbType.String, ParameterDirection.Input);
				dp.Add("Comment", Comment, DbType.String, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Failure.Insert]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The ID of the last registry inserted in Failure table</returns>
        public int Insert(FailureModel failure)
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("HTTPCode", failure.HTTPCode, DbType.Int32, ParameterDirection.Input);
				dp.Add("EmergencyLevel", failure.EmergencyLevel, DbType.Int32, ParameterDirection.Input);
				dp.Add("Message", failure.Message, DbType.String, ParameterDirection.Input);
				dp.Add("StackTrace", failure.StackTrace, DbType.String, ParameterDirection.Input);
				dp.Add("Source", failure.Source, DbType.String, ParameterDirection.Input);
				dp.Add("Comment", failure.Comment, DbType.String, ParameterDirection.Input);
				dp.Add("Active", failure.Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", failure.UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", failure.UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", failure.DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", failure.DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Failure.Insert]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The ID of the last registry inserted in Failure table</returns>
        public int Insert(int HTTPCode, int EmergencyLevel, string Message, string StackTrace, string Source, string Comment, bool Active, int UserCreationId, int UserLastModificationId, DateTime DateTimeCreation, DateTime DateTimeLastModification)
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("HTTPCode", HTTPCode, DbType.Int32, ParameterDirection.Input);
				dp.Add("EmergencyLevel", EmergencyLevel, DbType.Int32, ParameterDirection.Input);
				dp.Add("Message", Message, DbType.String, ParameterDirection.Input);
				dp.Add("StackTrace", StackTrace, DbType.String, ParameterDirection.Input);
				dp.Add("Source", Source, DbType.String, ParameterDirection.Input);
				dp.Add("Comment", Comment, DbType.String, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Failure.Insert]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows updated in Failure table</returns>
        public int UpdateByFailureId()
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("FailureId", FailureId, DbType.Int32, ParameterDirection.Input);
				dp.Add("HTTPCode", HTTPCode, DbType.Int32, ParameterDirection.Input);
				dp.Add("EmergencyLevel", EmergencyLevel, DbType.Int32, ParameterDirection.Input);
				dp.Add("Message", Message, DbType.String, ParameterDirection.Input);
				dp.Add("StackTrace", StackTrace, DbType.String, ParameterDirection.Input);
				dp.Add("Source", Source, DbType.String, ParameterDirection.Input);
				dp.Add("Comment", Comment, DbType.String, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Failure.UpdateByFailureId]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows updated in Failure table</returns>
        public int UpdateByFailureId(FailureModel failure)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("FailureId", failure.FailureId, DbType.Int32, ParameterDirection.Input);
				dp.Add("HTTPCode", failure.HTTPCode, DbType.Int32, ParameterDirection.Input);
				dp.Add("EmergencyLevel", failure.EmergencyLevel, DbType.Int32, ParameterDirection.Input);
				dp.Add("Message", failure.Message, DbType.String, ParameterDirection.Input);
				dp.Add("StackTrace", failure.StackTrace, DbType.String, ParameterDirection.Input);
				dp.Add("Source", failure.Source, DbType.String, ParameterDirection.Input);
				dp.Add("Comment", failure.Comment, DbType.String, ParameterDirection.Input);
				dp.Add("Active", failure.Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", failure.UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", failure.UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", failure.DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", failure.DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Failure.UpdateByFailureId]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows updated in Failure table</returns>
        public int UpdateByFailureId(int FailureId, int HTTPCode, int EmergencyLevel, string Message, string StackTrace, string Source, string Comment, bool Active, int UserCreationId, int UserLastModificationId, DateTime DateTimeCreation, DateTime DateTimeLastModification)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("FailureId", FailureId, DbType.Int32, ParameterDirection.Input);
				dp.Add("HTTPCode", HTTPCode, DbType.Int32, ParameterDirection.Input);
				dp.Add("EmergencyLevel", EmergencyLevel, DbType.Int32, ParameterDirection.Input);
				dp.Add("Message", Message, DbType.String, ParameterDirection.Input);
				dp.Add("StackTrace", StackTrace, DbType.String, ParameterDirection.Input);
				dp.Add("Source", Source, DbType.String, ParameterDirection.Input);
				dp.Add("Comment", Comment, DbType.String, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Failure.UpdateByFailureId]", commandType: CommandType.StoredProcedure, param: dp);
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
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Failure.DeleteAll]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <returns>The number of rows deleted in Failure table</returns>
        public int DeleteByFailureId()
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
        
                dp.Add("FailureId", FailureId, DbType.Int32, ParameterDirection.Input);        
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Failure.DeleteByFailureId]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows affected in Failure table</returns>
        public int DeleteByFailureId(int FailureId)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
        
                dp.Add("FailureId", FailureId, DbType.Int32, ParameterDirection.Input);        
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Failure.DeleteByFailureId]", commandType: CommandType.StoredProcedure, param: dp);
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
        public FailureModel ByteArrayToFailureModel<T>(byte[] arrFailureModel)
        {
            try
            {
                if (arrFailureModel == null)
                { return new FailureModel(); }
                BinaryFormatter BinaryFormatter = new BinaryFormatter();
                using MemoryStream MemoryStream = new MemoryStream(arrFailureModel);
                object Object = BinaryFormatter.Deserialize(MemoryStream);
                return (FailureModel)Object;
            }
            catch (Exception ex)
            { throw ex; }
        }
        
        /// <summary>
        /// Function: Show all information (fields) inside the model during depuration mode.
        /// </summary>
        public override string ToString()
        {
            return $"FailureId: {FailureId}, " +
				$"HTTPCode: {HTTPCode}, " +
				$"EmergencyLevel: {EmergencyLevel}, " +
				$"Message: {Message}, " +
				$"StackTrace: {StackTrace}, " +
				$"Source: {Source}, " +
				$"Comment: {Comment}, " +
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{FailureId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{HTTPCode}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{EmergencyLevel}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Message}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{StackTrace}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Source}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Comment}</span>
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
    /// Virtual model used for [dbo].[BasicCore.Failure.SelectAllPaged] stored procedure
    /// </summary>
    public partial class failureModelQuery 
    {
        public string QueryString { get; set; }
        public int ActualPageNumber { get; set; }
        public int RowsPerPage { get; set; }
        public string SorterColumn { get; set; }
        public bool SortToggler { get; set; }
        public int TotalRows { get; set; }
        public int TotalPages { get; set; }
        public List<FailureModel> lstFailureModel { get; set; }
    }
}