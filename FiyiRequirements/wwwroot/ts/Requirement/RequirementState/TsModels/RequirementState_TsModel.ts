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

//7 fields | Last modification on: 24/12/2022 6:47:04 | Stack: 9

export class RequirementStateModel {

    //Fields
    RequirementStateId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	Name?: string | string[] | number | undefined;

    //Queries
    static Select1ByRequirementStateId(RequirementStateId: number) {
        let URL = "/api/Requirement/RequirementState/1/Select1ByRequirementStateIdToJSON/" + RequirementStateId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/Requirement/RequirementState/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(requirementstatemodelQuery: requirementstatemodelQuery) {
        let URL = "/api/Requirement/RequirementState/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: requirementstatemodelQuery.QueryString,
            ActualPageNumber: requirementstatemodelQuery.ActualPageNumber,
            RowsPerPage: requirementstatemodelQuery.RowsPerPage,
            SorterColumn: requirementstatemodelQuery.SorterColumn,
            SortToggler: requirementstatemodelQuery.SortToggler,
            RowCount: requirementstatemodelQuery.TotalRows,
            TotalPages: requirementstatemodelQuery.TotalPages,
            lstRequirementStateModel: requirementstatemodelQuery.lstRequirementStateModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.put(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByRequirementStateId(RequirementStateId: number | string | string[] | undefined) {
        let URL = "/api/Requirement/RequirementState/1/DeleteByRequirementStateId/" + RequirementStateId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/Requirement/RequirementState/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByRequirementStateId(RequirementStateId: string | number | string[] | undefined) {
        let URL = "/api/Requirement/RequirementState/1/CopyByRequirementStateId/" + RequirementStateId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/RequirementStateing/RequirementState/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}

export class requirementstatemodelQuery {
    QueryString ?: string;
    ActualPageNumber?: number;
    RowsPerPage?: number;
    SorterColumn?: string;
    SortToggler?: boolean;
    TotalRows?: number;
    TotalPages?: number;
    lstRequirementStateModel?: RequirementStateModel[] | undefined;
}