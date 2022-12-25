using FiyiRequirements.Areas.Requirement.Models;
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

//Last modification on: 25/12/2022 18:10:07

namespace FiyiRequirements.Areas.Requirement.Protocols
{
    /// <summary>
    /// Stack:             5<br/>
    /// Name:              C# Protocol/Interface. <br/>
    /// Function:          This protocol/interface allow you to standardize the C# service associated. 
    ///                    In other words, define the functions that has to implement the C# service. <br/>
    /// Note:              Raise exception in case of missing any function declared here but not in the service. <br/>
    /// Last modification: 25/12/2022 18:10:07
    /// </summary>
    public partial interface RequirementNoteProtocol
    {
        #region Queries
        /// <summary>
        /// Note: Raise exception when the query find duplicated IDs
        /// </summary>
        /// <param name="RequirementNoteId"></param>
        /// <returns></returns>
        RequirementNoteModel Select1ByRequirementNoteIdToModel(int RequirementNoteId);

        List<RequirementNoteModel> SelectAllToList();

        requirementnoteModelQuery SelectAllPagedToModel(requirementnoteModelQuery requirementnoteModelQuery);
        #endregion

        #region Non-Queries
        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <param name="RequirementNote"></param>
        /// <returns>NewEnteredId: The ID of the last registry inserted in RequirementNote table</returns>
        int Insert(RequirementNoteModel RequirementNote);

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull update in database
        /// </summary>
        /// <param name="RequirementNote"></param>
        /// <returns>The number of rows updated in RequirementNote table</returns>
        int UpdateByRequirementNoteId(RequirementNoteModel RequirementNote);

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <param name="RequirementNoteId"></param>
        /// <returns>The number of rows deleted in RequirementNote table</returns>
        int DeleteByRequirementNoteId(int RequirementNoteId);

        void DeleteManyOrAll(Ajax Ajax, string DeleteType);

        int CopyByRequirementNoteId(int RequirementNoteId);

        int[] CopyManyOrAll(Ajax Ajax, string CopyType);
        #endregion

        #region Other actions
        DateTime ExportAsPDF(Ajax Ajax, string ExportationType);

        DateTime ExportAsExcel(Ajax Ajax, string ExportationType);

        DateTime ExportAsCSV(Ajax Ajax, string ExportationType);
        #endregion
    }
}