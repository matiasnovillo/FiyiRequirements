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

//11 fields | Last modification on: 24/12/2022 6:47:32 | Stack: 9

export class ClientModel {

    //Fields
    ClientId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	FirstName?: string | string[] | number | undefined;
	LastName?: string | string[] | number | undefined;
	BusinessName?: string | string[] | number | undefined;
	PhoneNumber?: string | string[] | number | undefined;
	Email?: string | string[] | number | undefined;

    //Queries
    static Select1ByClientId(ClientId: number) {
        let URL = "/api/Requirement/Client/1/Select1ByClientIdToJSON/" + ClientId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/Requirement/Client/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(clientmodelQuery: clientmodelQuery) {
        let URL = "/api/Requirement/Client/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: clientmodelQuery.QueryString,
            ActualPageNumber: clientmodelQuery.ActualPageNumber,
            RowsPerPage: clientmodelQuery.RowsPerPage,
            SorterColumn: clientmodelQuery.SorterColumn,
            SortToggler: clientmodelQuery.SortToggler,
            RowCount: clientmodelQuery.TotalRows,
            TotalPages: clientmodelQuery.TotalPages,
            lstClientModel: clientmodelQuery.lstClientModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.put(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByClientId(ClientId: number | string | string[] | undefined) {
        let URL = "/api/Requirement/Client/1/DeleteByClientId/" + ClientId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/Requirement/Client/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByClientId(ClientId: string | number | string[] | undefined) {
        let URL = "/api/Requirement/Client/1/CopyByClientId/" + ClientId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/Clienting/Client/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}

export class clientmodelQuery {
    QueryString ?: string;
    ActualPageNumber?: number;
    RowsPerPage?: number;
    SorterColumn?: string;
    SortToggler?: boolean;
    TotalRows?: number;
    TotalPages?: number;
    lstClientModel?: ClientModel[] | undefined;
}