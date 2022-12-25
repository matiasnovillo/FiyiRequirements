using FiyiRequirements.Areas.CMSCore.Models;
using FiyiRequirements.Library;
using System;
using System.Collections.Generic;

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

//Last modification on: 20/12/2022 21:44:06

namespace FiyiRequirements.Areas.CMSCore.Protocols
{
    /// <summary>
    /// Stack:             5<br/>
    /// Name:              C# Protocol/Interface. <br/>
    /// Function:          This protocol/interface allow you to standardize the C# service associated. 
    ///                    In other words, define the functions that has to implement the C# service. <br/>
    /// Note:              Raise exception in case of missing any function declared here but not in the service. <br/>
    /// Last modification: 20/12/2022 21:44:06
    /// </summary>
    public partial interface UserProtocol
    {
        #region Queries
        /// <summary>
        /// Note: Raise exception when the query find duplicated IDs
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        UserModel Select1ByUserIdToModel(int UserId);

        List<UserModel> SelectAllToList();

        userModelQuery SelectAllPagedToModel(userModelQuery userModelQuery);
        #endregion

        #region Non-Queries
        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <param name="User"></param>
        /// <returns>NewEnteredId: The ID of the last registry inserted in User table</returns>
        int Insert(UserModel User);

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull update in database
        /// </summary>
        /// <param name="User"></param>
        /// <returns>The number of rows updated in User table</returns>
        int UpdateByUserId(UserModel User);

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>The number of rows deleted in User table</returns>
        int DeleteByUserId(int UserId);

        void DeleteManyOrAll(Ajax Ajax, string DeleteType);

        int CopyByUserId(int UserId);

        int[] CopyManyOrAll(Ajax Ajax, string CopyType);

        UserModel Login(string UserFantasyNameOrEmail, string Password);

        string ChangePassword(int UserId, string ActualPassword, string NewPassword);

        string Register(string FantasyName, string Email, string Password);

        string RecoverPassword(string Email);
        #endregion

        #region Other actions
        DateTime ExportAsPDF(Ajax Ajax, string ExportationType);

        DateTime ExportAsExcel(Ajax Ajax, string ExportationType);

        DateTime ExportAsCSV(Ajax Ajax, string ExportationType);
        #endregion
    }
}