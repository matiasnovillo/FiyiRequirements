import * as Rx from "rxjs";
import { ajax } from "rxjs/ajax";
import { Ajax } from "../../../Library/Ajax";

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

//9 fields | Last modification on: 24/12/2022 6:48:12 | Stack: 9

export class RequirementChangehistoryModel {

    //Fields
    RequirementChangehistoryId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	RequirementId?: number;
	RequirementStateId?: number;
	RequirementPriorityId?: number;

    //Queries
    static Select1ByRequirementChangehistoryId(RequirementChangehistoryId: number) {
        let URL = "/api/Requirement/RequirementChangehistory/1/Select1ByRequirementChangehistoryIdToJSON/" + RequirementChangehistoryId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/Requirement/RequirementChangehistory/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(requirementchangehistorymodelQuery: requirementchangehistorymodelQuery) {
        let URL = "/api/Requirement/RequirementChangehistory/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: requirementchangehistorymodelQuery.QueryString,
            ActualPageNumber: requirementchangehistorymodelQuery.ActualPageNumber,
            RowsPerPage: requirementchangehistorymodelQuery.RowsPerPage,
            SorterColumn: requirementchangehistorymodelQuery.SorterColumn,
            SortToggler: requirementchangehistorymodelQuery.SortToggler,
            RowCount: requirementchangehistorymodelQuery.TotalRows,
            TotalPages: requirementchangehistorymodelQuery.TotalPages,
            lstRequirementChangehistoryModel: requirementchangehistorymodelQuery.lstRequirementChangehistoryModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.put(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByRequirementChangehistoryId(RequirementChangehistoryId: number | string | string[] | undefined) {
        let URL = "/api/Requirement/RequirementChangehistory/1/DeleteByRequirementChangehistoryId/" + RequirementChangehistoryId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/Requirement/RequirementChangehistory/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByRequirementChangehistoryId(RequirementChangehistoryId: string | number | string[] | undefined) {
        let URL = "/api/Requirement/RequirementChangehistory/1/CopyByRequirementChangehistoryId/" + RequirementChangehistoryId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/RequirementChangehistorying/RequirementChangehistory/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}

export class requirementchangehistorymodelQuery {
    QueryString ?: string;
    ActualPageNumber?: number;
    RowsPerPage?: number;
    SorterColumn?: string;
    SortToggler?: boolean;
    TotalRows?: number;
    TotalPages?: number;
    lstRequirementChangehistoryModel?: RequirementChangehistoryModel[] | undefined;
}