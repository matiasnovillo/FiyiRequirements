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

//13 fields | Last modification on: 24/12/2022 6:48:02 | Stack: 9

export class RequirementModel {

    //Fields
    RequirementId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	ClientId?: number;
	Title?: string | string[] | number | undefined;
	Body?: string | string[] | number | undefined;
	RequirementStateId?: number;
	RequirementTypeId?: number;
	RequirementPriorityId?: number;
	UserProgrammerId?: number;

    //Queries
    static Select1ByRequirementId(RequirementId: number) {
        let URL = "/api/Requirement/Requirement/1/Select1ByRequirementIdToJSON/" + RequirementId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/Requirement/Requirement/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(requirementmodelQuery: requirementmodelQuery) {
        let URL = "/api/Requirement/Requirement/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: requirementmodelQuery.QueryString,
            ActualPageNumber: requirementmodelQuery.ActualPageNumber,
            RowsPerPage: requirementmodelQuery.RowsPerPage,
            SorterColumn: requirementmodelQuery.SorterColumn,
            SortToggler: requirementmodelQuery.SortToggler,
            RowCount: requirementmodelQuery.TotalRows,
            TotalPages: requirementmodelQuery.TotalPages,
            lstRequirementModel: requirementmodelQuery.lstRequirementModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.put(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByRequirementId(RequirementId: number | string | string[] | undefined) {
        let URL = "/api/Requirement/Requirement/1/DeleteByRequirementId/" + RequirementId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/Requirement/Requirement/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByRequirementId(RequirementId: string | number | string[] | undefined) {
        let URL = "/api/Requirement/Requirement/1/CopyByRequirementId/" + RequirementId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/Requirementing/Requirement/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}

export class requirementmodelQuery {
    QueryString ?: string;
    ActualPageNumber?: number;
    RowsPerPage?: number;
    SorterColumn?: string;
    SortToggler?: boolean;
    TotalRows?: number;
    TotalPages?: number;
    lstRequirementModel?: RequirementModel[] | undefined;
}