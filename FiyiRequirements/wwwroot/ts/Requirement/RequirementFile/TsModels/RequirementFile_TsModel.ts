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

//9 fields | Sub-models: 0 models  | Last modification on: 25/12/2022 18:05:38 | Stack: 9

export class RequirementFileModel {

    //Fields
    RequirementFileId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	RequirementId?: number;
	FileName?: string | string[] | number | undefined;
	FilePath?: string | string[] | number | undefined;
    

    //Queries
    static Select1ByRequirementFileId(RequirementFileId: number) {
        let URL = "/api/Requirement/RequirementFile/1/Select1ByRequirementFileIdToJSON/" + RequirementFileId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/Requirement/RequirementFile/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(requirementfilemodelQuery: requirementfilemodelQuery) {
        let URL = "/api/Requirement/RequirementFile/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: requirementfilemodelQuery.QueryString,
            ActualPageNumber: requirementfilemodelQuery.ActualPageNumber,
            RowsPerPage: requirementfilemodelQuery.RowsPerPage,
            SorterColumn: requirementfilemodelQuery.SorterColumn,
            SortToggler: requirementfilemodelQuery.SortToggler,
            RowCount: requirementfilemodelQuery.TotalRows,
            TotalPages: requirementfilemodelQuery.TotalPages,
            lstRequirementFileModel: requirementfilemodelQuery.lstRequirementFileModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.put(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByRequirementFileId(RequirementFileId: number | string | string[] | undefined) {
        let URL = "/api/Requirement/RequirementFile/1/DeleteByRequirementFileId/" + RequirementFileId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/Requirement/RequirementFile/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByRequirementFileId(RequirementFileId: string | number | string[] | undefined) {
        let URL = "/api/Requirement/RequirementFile/1/CopyByRequirementFileId/" + RequirementFileId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/RequirementFileing/RequirementFile/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}

export class requirementfilemodelQuery {
    QueryString ?: string;
    ActualPageNumber?: number;
    RowsPerPage?: number;
    SorterColumn?: string;
    SortToggler?: boolean;
    TotalRows?: number;
    TotalPages?: number;
    lstRequirementFileModel?: RequirementFileModel[] | undefined;
}