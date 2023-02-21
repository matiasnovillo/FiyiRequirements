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
    /// Fields:            9 <br/> 
    /// Dependencies:      0 models <br/>
    /// Last modification: 20/12/2022 19:56:32
    /// </summary>
    [Serializable]
    public partial class ParameterModel
    {
        [NotMapped]
        private string _ConnectionString = ConnectionStrings.ConnectionStrings.Development();

        #region Fields
        [Library.ModelAttributeValidator.Key("ParameterId")]
        public int ParameterId { get; set; }

        [Library.ModelAttributeValidator.String("Name", false, 1, 200, "")]
        public string Name { get; set; }

        [Library.ModelAttributeValidator.String("Value", false, 1, 8000, "")]
        public string Value { get; set; }

        public bool IsPrivate { get; set; }

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
        /// Function:     Create fastly this model with field ParameterId = 0 <br/>
        /// Note 1:       Generally used to have fast access to functions of object. <br/>
        /// Note 2:       Need construction with [new] reserved word, as all constructors. <br/>
        /// Fields:       9 <br/> 
        /// Dependencies: 0 models depend on this model <br/>
        /// </summary>
        public ParameterModel()
        {
            try { ParameterId = 0; }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with stored information in database using ParameterId <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       9 <br/> 
        /// Dependencies: 0 models depend on this model <br/>
        /// </summary>
        public ParameterModel(int ParameterId)
        {
            try
            {
                List<ParameterModel> lstParameterModel = new List<ParameterModel>();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("ParameterId", ParameterId, DbType.Int32, ParameterDirection.Input);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    //In case of not finding anything, Dapper return a List<ParameterModel>
                    lstParameterModel = (List<ParameterModel>)sqlConnection.Query<ParameterModel>("[dbo].[BasicCore.Parameter.Select1ByParameterId]", dp, commandType: CommandType.StoredProcedure);
                }

                if (lstParameterModel.Count > 1)
                {
                    throw new Exception("The stored procedure [dbo].[BasicCore.Parameter.Select1ByParameterId] returned more than one register/row");
                }
        
                foreach (ParameterModel parameter in lstParameterModel)
                {
                    this.ParameterId = parameter.ParameterId;
					this.Name = parameter.Name;
					this.Value = parameter.Value;
					this.IsPrivate = parameter.IsPrivate;
					this.Active = parameter.Active;
					this.UserCreationId = parameter.UserCreationId;
					this.UserLastModificationId = parameter.UserLastModificationId;
					this.DateTimeCreation = parameter.DateTimeCreation;
					this.DateTimeLastModification = parameter.DateTimeLastModification;
                }
            }
            catch (Exception ex) { throw ex; }
        }


        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with filled parameters <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       9 <br/> 
        /// Dependencies: 0 models depend on this model <br/>
        /// </summary>
        public ParameterModel(int ParameterId, string Name, string Value, bool IsPrivate, bool Active, int UserCreationId, int UserLastModificationId, DateTime DateTimeCreation, DateTime DateTimeLastModification)
        {
            try
            {
                this.ParameterId = ParameterId;
				this.Name = Name;
				this.Value = Value;
				this.IsPrivate = IsPrivate;
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
        /// Function:     Create this model (copy) using the given model (original), parameter, passed by parameter. <br/>
        /// Note:         This constructor is generally used to execute functions using the copied fields <br/>
        /// Fields:       9 <br/> 
        /// Dependencies: 0 models depend on this model <br/>
        /// </summary>
        public ParameterModel(ParameterModel parameter)
        {
            try
            {
                ParameterId = parameter.ParameterId;
				Name = parameter.Name;
				Value = parameter.Value;
				IsPrivate = parameter.IsPrivate;
				Active = parameter.Active;
				UserCreationId = parameter.UserCreationId;
				UserLastModificationId = parameter.UserLastModificationId;
				DateTimeCreation = parameter.DateTimeCreation;
				DateTimeLastModification = parameter.DateTimeLastModification;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The number of rows inside Parameter</returns>
        public int Count()
        {
            try
            {
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Parameter.Count]", commandType: CommandType.StoredProcedure, param: dp);
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
        public DataTable Select1ByParameterIdToDataTable(int ParameterId)
        {
            try
            {
                DataTable DataTable = new DataTable();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("ParameterId", ParameterId, DbType.Int32, ParameterDirection.Input);
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Parameter.Select1ByParameterId]", commandType: CommandType.StoredProcedure, param: dp);

                    DataTable.Load(dataReader);
                }

                if (DataTable.Rows.Count > 1)
                { throw new Exception("The stored procedure [dbo].[BasicCore.Parameter.Select1ByParameterId] returned more than one register/row"); }

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
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Parameter.SelectAll]", commandType: CommandType.StoredProcedure, param: dp);

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
        public ParameterModel Select1ByParameterIdToModel(int ParameterId)
        {
            try
            {
                ParameterModel ParameterModel = new ParameterModel();
                List<ParameterModel> lstParameterModel = new List<ParameterModel>();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("ParameterId", ParameterId, DbType.Int32, ParameterDirection.Input);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstParameterModel = (List<ParameterModel>)sqlConnection.Query<ParameterModel>("[dbo].[BasicCore.Parameter.Select1ByParameterId]", dp, commandType: CommandType.StoredProcedure);
                }
        
                if (lstParameterModel.Count > 1)
                { throw new Exception("The stored procedure [dbo].[BasicCore.Parameter.Select1ByParameterId] returned more than one register/row"); }

                foreach (ParameterModel parameter in lstParameterModel)
                {
                    ParameterModel.ParameterId = parameter.ParameterId;
					ParameterModel.Name = parameter.Name;
					ParameterModel.Value = parameter.Value;
					ParameterModel.IsPrivate = parameter.IsPrivate;
					ParameterModel.Active = parameter.Active;
					ParameterModel.UserCreationId = parameter.UserCreationId;
					ParameterModel.UserLastModificationId = parameter.UserLastModificationId;
					ParameterModel.DateTimeCreation = parameter.DateTimeCreation;
					ParameterModel.DateTimeLastModification = parameter.DateTimeLastModification;
                }

                return ParameterModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<ParameterModel> SelectAllToList()
        {
            try
            {
                List<ParameterModel> lstParameterModel = new List<ParameterModel>();
                DynamicParameters dp = new DynamicParameters();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstParameterModel = (List<ParameterModel>)sqlConnection.Query<ParameterModel>("[dbo].[BasicCore.Parameter.SelectAll]", dp, commandType: CommandType.StoredProcedure);
                }

                return lstParameterModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public parameterModelQuery SelectAllPagedToModel(parameterModelQuery parameterModelQuery)
        {
            try
            {
                parameterModelQuery.lstParameterModel = new List<ParameterModel>();
                DynamicParameters dp = new DynamicParameters();
                dp.Add("QueryString", parameterModelQuery.QueryString, DbType.String, ParameterDirection.Input);
                dp.Add("ActualPageNumber", parameterModelQuery.ActualPageNumber, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsPerPage", parameterModelQuery.RowsPerPage, DbType.Int32, ParameterDirection.Input);
                dp.Add("SorterColumn", parameterModelQuery.SorterColumn, DbType.String, ParameterDirection.Input);
                dp.Add("SortToggler", parameterModelQuery.SortToggler, DbType.Boolean, ParameterDirection.Input);
                dp.Add("TotalRows", parameterModelQuery.TotalRows, DbType.Int32, ParameterDirection.Output);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    parameterModelQuery.lstParameterModel = (List<ParameterModel>)sqlConnection.Query<ParameterModel>("[dbo].[BasicCore.Parameter.SelectAllPagedCustom]", dp, commandType: CommandType.StoredProcedure);
                    parameterModelQuery.TotalRows = dp.Get<int>("TotalRows");
                }

                parameterModelQuery.TotalPages = Library.Math.Divide(parameterModelQuery.TotalRows, parameterModelQuery.RowsPerPage, Library.Math.RoundType.RoundUp);

                return parameterModelQuery;
            }
            catch (Exception ex) { throw ex; }
        }

        public string Select1ByName(string Name)
        {
            try
            {
                List<ParameterModel> lstParameterModel = new List<ParameterModel>();
                DynamicParameters dp = new DynamicParameters();
                dp.Add("Name", Name, DbType.String, ParameterDirection.Input);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstParameterModel = (List<ParameterModel>)sqlConnection.Query<ParameterModel>("[dbo].[BasicCore.Parameter.Select1ByNameCustom]", dp, commandType: CommandType.StoredProcedure);
                }

                return lstParameterModel[0].Value;
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region Non-Queries
        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <returns>NewEnteredId: The ID of the last registry inserted in Parameter table</returns>
        public int Insert()
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
                
                dp.Add("Name", Name, DbType.String, ParameterDirection.Input);
				dp.Add("Value", Value, DbType.String, ParameterDirection.Input);
				dp.Add("IsPrivate", IsPrivate, DbType.Boolean, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Parameter.Insert]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The ID of the last registry inserted in Parameter table</returns>
        public int Insert(ParameterModel parameter)
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("Name", parameter.Name, DbType.String, ParameterDirection.Input);
				dp.Add("Value", parameter.Value, DbType.String, ParameterDirection.Input);
				dp.Add("IsPrivate", parameter.IsPrivate, DbType.Boolean, ParameterDirection.Input);
				dp.Add("Active", parameter.Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", parameter.UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", parameter.UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", parameter.DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", parameter.DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Parameter.Insert]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The ID of the last registry inserted in Parameter table</returns>
        public int Insert(string Name, string Value, bool IsPrivate, bool Active, int UserCreationId, int UserLastModificationId, DateTime DateTimeCreation, DateTime DateTimeLastModification)
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("Name", Name, DbType.String, ParameterDirection.Input);
				dp.Add("Value", Value, DbType.String, ParameterDirection.Input);
				dp.Add("IsPrivate", IsPrivate, DbType.Boolean, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Parameter.Insert]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows updated in Parameter table</returns>
        public int UpdateByParameterId()
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("ParameterId", ParameterId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Name", Name, DbType.String, ParameterDirection.Input);
				dp.Add("Value", Value, DbType.String, ParameterDirection.Input);
				dp.Add("IsPrivate", IsPrivate, DbType.Boolean, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Parameter.UpdateByParameterId]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows updated in Parameter table</returns>
        public int UpdateByParameterId(ParameterModel parameter)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("ParameterId", parameter.ParameterId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Name", parameter.Name, DbType.String, ParameterDirection.Input);
				dp.Add("Value", parameter.Value, DbType.String, ParameterDirection.Input);
				dp.Add("IsPrivate", parameter.IsPrivate, DbType.Boolean, ParameterDirection.Input);
				dp.Add("Active", parameter.Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", parameter.UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", parameter.UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", parameter.DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", parameter.DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Parameter.UpdateByParameterId]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows updated in Parameter table</returns>
        public int UpdateByParameterId(int ParameterId, string Name, string Value, bool IsPrivate, bool Active, int UserCreationId, int UserLastModificationId, DateTime DateTimeCreation, DateTime DateTimeLastModification)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("ParameterId", ParameterId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Name", Name, DbType.String, ParameterDirection.Input);
				dp.Add("Value", Value, DbType.String, ParameterDirection.Input);
				dp.Add("IsPrivate", IsPrivate, DbType.Boolean, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Parameter.UpdateByParameterId]", commandType: CommandType.StoredProcedure, param: dp);
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
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Parameter.DeleteAll]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <returns>The number of rows deleted in Parameter table</returns>
        public int DeleteByParameterId()
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
        
                dp.Add("ParameterId", ParameterId, DbType.Int32, ParameterDirection.Input);        
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Parameter.DeleteByParameterId]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows affected in Parameter table</returns>
        public int DeleteByParameterId(int ParameterId)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
        
                dp.Add("ParameterId", ParameterId, DbType.Int32, ParameterDirection.Input);        
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[BasicCore.Parameter.DeleteByParameterId]", commandType: CommandType.StoredProcedure, param: dp);
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
        public ParameterModel ByteArrayToParameterModel<T>(byte[] arrParameterModel)
        {
            try
            {
                if (arrParameterModel == null)
                { return new ParameterModel(); }
                BinaryFormatter BinaryFormatter = new BinaryFormatter();
                using MemoryStream MemoryStream = new MemoryStream(arrParameterModel);
                object Object = BinaryFormatter.Deserialize(MemoryStream);
                return (ParameterModel)Object;
            }
            catch (Exception ex)
            { throw ex; }
        }
        
        /// <summary>
        /// Function: Show all information (fields) inside the model during depuration mode.
        /// </summary>
        public override string ToString()
        {
            return $"ParameterId: {ParameterId}, " +
				$"Name: {Name}, " +
				$"Value: {Value}, " +
				$"IsPrivate: {IsPrivate}, " +
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{ParameterId}</span>
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Value}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{IsPrivate}</span>
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
    /// Virtual model used for [dbo].[BasicCore.Parameter.SelectAllPaged] stored procedure
    /// </summary>
    public partial class parameterModelQuery
    {
        public string QueryString { get; set; }
        public int ActualPageNumber { get; set; }
        public int RowsPerPage { get; set; }
        public string SorterColumn { get; set; }
        public bool SortToggler { get; set; }
        public int TotalRows { get; set; }
        public int TotalPages { get; set; }
        public List<ParameterModel> lstParameterModel { get; set; }
    }
}