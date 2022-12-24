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

//8 fields | Last modification on: 24/12/2022 6:47:16 | Stack: 9

export class RequirementTypeModel {

    //Fields
    RequirementTypeId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	Name?: string | string[] | number | undefined;
	Description?: string | string[] | number | undefined;

    //Queries
    static Select1ByRequirementTypeId(RequirementTypeId: number) {
        let URL = "/api/Requirement/RequirementType/1/Select1ByRequirementTypeIdToJSON/" + RequirementTypeId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/Requirement/RequirementType/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(requirementtypemodelQuery: requirementtypemodelQuery) {
        let URL = "/api/Requirement/RequirementType/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: requirementtypemodelQuery.QueryString,
            ActualPageNumber: requirementtypemodelQuery.ActualPageNumber,
            RowsPerPage: requirementtypemodelQuery.RowsPerPage,
            SorterColumn: requirementtypemodelQuery.SorterColumn,
            SortToggler: requirementtypemodelQuery.SortToggler,
            RowCount: requirementtypemodelQuery.TotalRows,
            TotalPages: requirementtypemodelQuery.TotalPages,
            lstRequirementTypeModel: requirementtypemodelQuery.lstRequirementTypeModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.put(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByRequirementTypeId(RequirementTypeId: number | string | string[] | undefined) {
        let URL = "/api/Requirement/RequirementType/1/DeleteByRequirementTypeId/" + RequirementTypeId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/Requirement/RequirementType/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByRequirementTypeId(RequirementTypeId: string | number | string[] | undefined) {
        let URL = "/api/Requirement/RequirementType/1/CopyByRequirementTypeId/" + RequirementTypeId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/RequirementTypeing/RequirementType/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}

export class requirementtypemodelQuery {
    QueryString ?: string;
    ActualPageNumber?: number;
    RowsPerPage?: number;
    SorterColumn?: string;
    SortToggler?: boolean;
    TotalRows?: number;
    TotalPages?: number;
    lstRequirementTypeModel?: RequirementTypeModel[] | undefined;
}