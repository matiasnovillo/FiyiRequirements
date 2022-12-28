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

//9 fields | Sub-models: 0 models  | Last modification on: 28/12/2022 17:28:12 | Stack: 9

export class RequirementNoteModel {

    //Fields
    RequirementNoteId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	Title?: string | string[] | number | undefined;
	Body?: string | string[] | number | undefined;
	RequirementId?: number;
    UserCreationIdFantasyName?: string | string[] | number | undefined;
    UserLastModificationIdFantasyName?: string | string[] | number | undefined;

    //Queries
    static Select1ByRequirementNoteId(RequirementNoteId: number) {
        let URL = "/api/Requirement/RequirementNote/1/Select1ByRequirementNoteIdToJSON/" + RequirementNoteId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/Requirement/RequirementNote/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(requirementnotemodelQuery: requirementnotemodelQuery) {
        let URL = "/api/Requirement/RequirementNote/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: requirementnotemodelQuery.QueryString,
            ActualPageNumber: requirementnotemodelQuery.ActualPageNumber,
            RowsPerPage: requirementnotemodelQuery.RowsPerPage,
            SorterColumn: requirementnotemodelQuery.SorterColumn,
            SortToggler: requirementnotemodelQuery.SortToggler,
            RowCount: requirementnotemodelQuery.TotalRows,
            TotalPages: requirementnotemodelQuery.TotalPages,
            lstRequirementNoteModel: requirementnotemodelQuery.lstRequirementNoteModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.put(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByRequirementNoteId(RequirementNoteId: number | string | string[] | undefined) {
        let URL = "/api/Requirement/RequirementNote/1/DeleteByRequirementNoteId/" + RequirementNoteId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/Requirement/RequirementNote/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByRequirementNoteId(RequirementNoteId: string | number | string[] | undefined) {
        let URL = "/api/Requirement/RequirementNote/1/CopyByRequirementNoteId/" + RequirementNoteId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/RequirementNoteing/RequirementNote/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}

export class requirementnotemodelQuery {
    QueryString ?: string;
    ActualPageNumber?: number;
    RowsPerPage?: number;
    SorterColumn?: string;
    SortToggler?: boolean;
    TotalRows?: number;
    TotalPages?: number;
    lstRequirementNoteModel?: RequirementNoteModel[] | undefined;
}