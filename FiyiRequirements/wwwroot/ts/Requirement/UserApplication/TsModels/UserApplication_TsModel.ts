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

//8 fields | Sub-models: 0 models  | Last modification on: 27/12/2022 16:32:18 | Stack: 9

export class UserApplicationModel {

    //Fields
    UserApplicationId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	ApplicationId?: number;
	UserId?: number;
    

    //Queries
    static Select1ByUserApplicationId(UserApplicationId: number) {
        let URL = "/api/Requirement/UserApplication/1/Select1ByUserApplicationIdToJSON/" + UserApplicationId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/Requirement/UserApplication/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(userapplicationmodelQuery: userapplicationmodelQuery) {
        let URL = "/api/Requirement/UserApplication/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: userapplicationmodelQuery.QueryString,
            ActualPageNumber: userapplicationmodelQuery.ActualPageNumber,
            RowsPerPage: userapplicationmodelQuery.RowsPerPage,
            SorterColumn: userapplicationmodelQuery.SorterColumn,
            SortToggler: userapplicationmodelQuery.SortToggler,
            RowCount: userapplicationmodelQuery.TotalRows,
            TotalPages: userapplicationmodelQuery.TotalPages,
            lstUserApplicationModel: userapplicationmodelQuery.lstUserApplicationModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.put(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByUserApplicationId(UserApplicationId: number | string | string[] | undefined) {
        let URL = "/api/Requirement/UserApplication/1/DeleteByUserApplicationId/" + UserApplicationId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/Requirement/UserApplication/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByUserApplicationId(UserApplicationId: string | number | string[] | undefined) {
        let URL = "/api/Requirement/UserApplication/1/CopyByUserApplicationId/" + UserApplicationId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/UserApplicationing/UserApplication/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}

export class userapplicationmodelQuery {
    QueryString ?: string;
    ActualPageNumber?: number;
    RowsPerPage?: number;
    SorterColumn?: string;
    SortToggler?: boolean;
    TotalRows?: number;
    TotalPages?: number;
    lstUserApplicationModel?: UserApplicationModel[] | undefined;
}