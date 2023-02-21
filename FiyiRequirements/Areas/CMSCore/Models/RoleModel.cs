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

namespace FiyiRequirements.Areas.CMSCore.Models
{
    /// <summary>
    /// Stack:             3 <br/>
    /// Name:              C# Model with stored procedure calls saved on database. <br/>
    /// Function:          Allow you to manipulate information from database using stored procedures.
    ///                    Also, let you make other related actions with the model in question or
    ///                    make temporal copies with random data. <br/>
    /// Fields:            7 <br/> 
    /// Dependencies:      2 models <br/>
    /// Last modification: 20/12/2022 20:47:32
    /// </summary>
    [Serializable]
    public partial class RoleModel
    {
        [NotMapped]
        private string _ConnectionString = ConnectionStrings.ConnectionStrings.Development();

        #region Fields
        [Library.ModelAttributeValidator.Key("RoleId")]
        public int RoleId { get; set; }

        [Library.ModelAttributeValidator.String("Name", false, 1, 200, "")]
        public string Name { get; set; }

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
        public virtual List<UserModel> lstUserModel { get; set; } //Foreign Key name: RoleId 
		public virtual List<RoleMenuModel> lstRoleMenuModel { get; set; } //Foreign Key name: RoleId 
        #endregion

        #region Constructors
        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create fastly this model with field RoleId = 0 <br/>
        /// Note 1:       Generally used to have fast access to functions of object. <br/>
        /// Note 2:       Need construction with [new] reserved word, as all constructors. <br/>
        /// Fields:       7 <br/> 
        /// Dependencies: 2 models depend on this model <br/>
        /// </summary>
        public RoleModel()
        {
            try { RoleId = 0; }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with stored information in database using RoleId <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       7 <br/> 
        /// Dependencies: 2 models depend on this model <br/>
        /// </summary>
        public RoleModel(int RoleId)
        {
            try
            {
                List<RoleModel> lstRoleModel = new List<RoleModel>();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("RoleId", RoleId, DbType.Int32, ParameterDirection.Input);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    //In case of not finding anything, Dapper return a List<RoleModel>
                    lstRoleModel = (List<RoleModel>)sqlConnection.Query<RoleModel>("[dbo].[CMSCore.Role.Select1ByRoleId]", dp, commandType: CommandType.StoredProcedure);
                }

                if (lstRoleModel.Count > 1)
                {
                    throw new Exception("The stored procedure [dbo].[CMSCore.Role.Select1ByRoleId] returned more than one register/row");
                }
        
                foreach (RoleModel role in lstRoleModel)
                {
                    this.RoleId = role.RoleId;
					this.Name = role.Name;
					this.Active = role.Active;
					this.UserCreationId = role.UserCreationId;
					this.UserLastModificationId = role.UserLastModificationId;
					this.DateTimeCreation = role.DateTimeCreation;
					this.DateTimeLastModification = role.DateTimeLastModification;
                }
            }
            catch (Exception ex) { throw ex; }
        }


        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with filled parameters <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       7 <br/> 
        /// Dependencies: 2 models depend on this model <br/>
        /// </summary>
        public RoleModel(int RoleId, string Name, bool Active, int UserCreationId, int UserLastModificationId, DateTime DateTimeCreation, DateTime DateTimeLastModification)
        {
            try
            {
                this.RoleId = RoleId;
				this.Name = Name;
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
        /// Function:     Create this model (copy) using the given model (original), role, passed by parameter. <br/>
        /// Note:         This constructor is generally used to execute functions using the copied fields <br/>
        /// Fields:       7 <br/> 
        /// Dependencies: 2 models depend on this model <br/>
        /// </summary>
        public RoleModel(RoleModel role)
        {
            try
            {
                RoleId = role.RoleId;
				Name = role.Name;
				Active = role.Active;
				UserCreationId = role.UserCreationId;
				UserLastModificationId = role.UserLastModificationId;
				DateTimeCreation = role.DateTimeCreation;
				DateTimeLastModification = role.DateTimeLastModification;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The number of rows inside Role</returns>
        public int Count()
        {
            try
            {
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[CMSCore.Role.Count]", commandType: CommandType.StoredProcedure, param: dp);
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
        public DataTable Select1ByRoleIdToDataTable(int RoleId)
        {
            try
            {
                DataTable DataTable = new DataTable();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("RoleId", RoleId, DbType.Int32, ParameterDirection.Input);
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[CMSCore.Role.Select1ByRoleId]", commandType: CommandType.StoredProcedure, param: dp);

                    DataTable.Load(dataReader);
                }

                if (DataTable.Rows.Count > 1)
                { throw new Exception("The stored procedure [dbo].[CMSCore.Role.Select1ByRoleId] returned more than one register/row"); }

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
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[CMSCore.Role.SelectAll]", commandType: CommandType.StoredProcedure, param: dp);

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
        public RoleModel Select1ByRoleIdToModel(int RoleId)
        {
            try
            {
                RoleModel RoleModel = new RoleModel();
                List<RoleModel> lstRoleModel = new List<RoleModel>();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("RoleId", RoleId, DbType.Int32, ParameterDirection.Input);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstRoleModel = (List<RoleModel>)sqlConnection.Query<RoleModel>("[dbo].[CMSCore.Role.Select1ByRoleId]", dp, commandType: CommandType.StoredProcedure);
                }
        
                if (lstRoleModel.Count > 1)
                { throw new Exception("The stored procedure [dbo].[CMSCore.Role.Select1ByRoleId] returned more than one register/row"); }

                foreach (RoleModel role in lstRoleModel)
                {
                    RoleModel.RoleId = role.RoleId;
					RoleModel.Name = role.Name;
					RoleModel.Active = role.Active;
					RoleModel.UserCreationId = role.UserCreationId;
					RoleModel.UserLastModificationId = role.UserLastModificationId;
					RoleModel.DateTimeCreation = role.DateTimeCreation;
					RoleModel.DateTimeLastModification = role.DateTimeLastModification;
                }

                return RoleModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<RoleModel> SelectAllToList()
        {
            try
            {
                List<RoleModel> lstRoleModel = new List<RoleModel>();
                DynamicParameters dp = new DynamicParameters();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstRoleModel = (List<RoleModel>)sqlConnection.Query<RoleModel>("[dbo].[CMSCore.Role.SelectAll]", dp, commandType: CommandType.StoredProcedure);
                }

                return lstRoleModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public roleModelQuery SelectAllPagedToModel(roleModelQuery roleModelQuery)
        {
            try
            {
                roleModelQuery.lstRoleModel = new List<RoleModel>();
                DynamicParameters dp = new DynamicParameters();
                dp.Add("QueryString", roleModelQuery.QueryString, DbType.String, ParameterDirection.Input);
                dp.Add("ActualPageNumber", roleModelQuery.ActualPageNumber, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsPerPage", roleModelQuery.RowsPerPage, DbType.Int32, ParameterDirection.Input);
                dp.Add("SorterColumn", roleModelQuery.SorterColumn, DbType.String, ParameterDirection.Input);
                dp.Add("SortToggler", roleModelQuery.SortToggler, DbType.Boolean, ParameterDirection.Input);
                dp.Add("TotalRows", roleModelQuery.TotalRows, DbType.Int32, ParameterDirection.Output);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    roleModelQuery.lstRoleModel = (List<RoleModel>)sqlConnection.Query<RoleModel>("[dbo].[CMSCore.Role.SelectAllPagedCustom]", dp, commandType: CommandType.StoredProcedure);
                    roleModelQuery.TotalRows = dp.Get<int>("TotalRows");
                }

                roleModelQuery.TotalPages = Library.Math.Divide(roleModelQuery.TotalRows, roleModelQuery.RowsPerPage, Library.Math.RoundType.RoundUp);

                return roleModelQuery;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region Non-Queries
        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <returns>NewEnteredId: The ID of the last registry inserted in Role table</returns>
        public int Insert()
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
                
                dp.Add("Name", Name, DbType.String, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[CMSCore.Role.Insert]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The ID of the last registry inserted in Role table</returns>
        public int Insert(RoleModel role)
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("Name", role.Name, DbType.String, ParameterDirection.Input);
				dp.Add("Active", role.Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", role.UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", role.UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", role.DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", role.DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[CMSCore.Role.Insert]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The ID of the last registry inserted in Role table</returns>
        public int Insert(string Name, bool Active, int UserCreationId, int UserLastModificationId, DateTime DateTimeCreation, DateTime DateTimeLastModification)
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("Name", Name, DbType.String, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[CMSCore.Role.Insert]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows updated in Role table</returns>
        public int UpdateByRoleId()
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("RoleId", RoleId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Name", Name, DbType.String, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[CMSCore.Role.UpdateByRoleId]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows updated in Role table</returns>
        public int UpdateByRoleId(RoleModel role)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("RoleId", role.RoleId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Name", role.Name, DbType.String, ParameterDirection.Input);
				dp.Add("Active", role.Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", role.UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", role.UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", role.DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", role.DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[CMSCore.Role.UpdateByRoleId]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows updated in Role table</returns>
        public int UpdateByRoleId(int RoleId, string Name, bool Active, int UserCreationId, int UserLastModificationId, DateTime DateTimeCreation, DateTime DateTimeLastModification)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("RoleId", RoleId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Name", Name, DbType.String, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[CMSCore.Role.UpdateByRoleId]", commandType: CommandType.StoredProcedure, param: dp);
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
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[CMSCore.Role.DeleteAll]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <returns>The number of rows deleted in Role table</returns>
        public int DeleteByRoleId()
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
        
                dp.Add("RoleId", RoleId, DbType.Int32, ParameterDirection.Input);        
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[CMSCore.Role.DeleteByRoleId]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows affected in Role table</returns>
        public int DeleteByRoleId(int RoleId)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
        
                dp.Add("RoleId", RoleId, DbType.Int32, ParameterDirection.Input);        
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[CMSCore.Role.DeleteByRoleId]", commandType: CommandType.StoredProcedure, param: dp);
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
        public RoleModel ByteArrayToRoleModel<T>(byte[] arrRoleModel)
        {
            try
            {
                if (arrRoleModel == null)
                { return new RoleModel(); }
                BinaryFormatter BinaryFormatter = new BinaryFormatter();
                using MemoryStream MemoryStream = new MemoryStream(arrRoleModel);
                object Object = BinaryFormatter.Deserialize(MemoryStream);
                return (RoleModel)Object;
            }
            catch (Exception ex)
            { throw ex; }
        }
        
        /// <summary>
        /// Function: Show all information (fields) inside the model during depuration mode.
        /// </summary>
        public override string ToString()
        {
            return $"RoleId: {RoleId}, " +
				$"Name: {Name}, " +
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{RoleId}</span>
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
    /// Virtual model used for [dbo].[CMSCore.Role.SelectAllPaged] stored procedure
    /// </summary>
    public partial class roleModelQuery 
    {
        public string QueryString { get; set; }
        public int ActualPageNumber { get; set; }
        public int RowsPerPage { get; set; }
        public string SorterColumn { get; set; }
        public bool SortToggler { get; set; }
        public int TotalRows { get; set; }
        public int TotalPages { get; set; }
        public List<RoleModel> lstRoleModel { get; set; }
    }
}