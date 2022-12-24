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

//8 fields | Last modification on: 24/12/2022 6:47:42 | Stack: 9

export class ClientApplicationModel {

    //Fields
    ClientApplicationId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	ClientId?: number;
	ApplicationId?: number;

    //Queries
    static Select1ByClientApplicationId(ClientApplicationId: number) {
        let URL = "/api/Requirement/ClientApplication/1/Select1ByClientApplicationIdToJSON/" + ClientApplicationId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/Requirement/ClientApplication/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(clientapplicationmodelQuery: clientapplicationmodelQuery) {
        let URL = "/api/Requirement/ClientApplication/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: clientapplicationmodelQuery.QueryString,
            ActualPageNumber: clientapplicationmodelQuery.ActualPageNumber,
            RowsPerPage: clientapplicationmodelQuery.RowsPerPage,
            SorterColumn: clientapplicationmodelQuery.SorterColumn,
            SortToggler: clientapplicationmodelQuery.SortToggler,
            RowCount: clientapplicationmodelQuery.TotalRows,
            TotalPages: clientapplicationmodelQuery.TotalPages,
            lstClientApplicationModel: clientapplicationmodelQuery.lstClientApplicationModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.put(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByClientApplicationId(ClientApplicationId: number | string | string[] | undefined) {
        let URL = "/api/Requirement/ClientApplication/1/DeleteByClientApplicationId/" + ClientApplicationId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/Requirement/ClientApplication/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByClientApplicationId(ClientApplicationId: string | number | string[] | undefined) {
        let URL = "/api/Requirement/ClientApplication/1/CopyByClientApplicationId/" + ClientApplicationId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/ClientApplicationing/ClientApplication/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}

export class clientapplicationmodelQuery {
    QueryString ?: string;
    ActualPageNumber?: number;
    RowsPerPage?: number;
    SorterColumn?: string;
    SortToggler?: boolean;
    TotalRows?: number;
    TotalPages?: number;
    lstClientApplicationModel?: ClientApplicationModel[] | undefined;
}