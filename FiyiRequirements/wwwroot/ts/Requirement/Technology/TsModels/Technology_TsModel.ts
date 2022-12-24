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

//8 fields | Last modification on: 24/12/2022 6:47:20 | Stack: 9

export class TechnologyModel {

    //Fields
    TechnologyId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	Name?: string | string[] | number | undefined;
	Description?: string | string[] | number | undefined;

    //Queries
    static Select1ByTechnologyId(TechnologyId: number) {
        let URL = "/api/Requirement/Technology/1/Select1ByTechnologyIdToJSON/" + TechnologyId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/Requirement/Technology/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(technologymodelQuery: technologymodelQuery) {
        let URL = "/api/Requirement/Technology/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: technologymodelQuery.QueryString,
            ActualPageNumber: technologymodelQuery.ActualPageNumber,
            RowsPerPage: technologymodelQuery.RowsPerPage,
            SorterColumn: technologymodelQuery.SorterColumn,
            SortToggler: technologymodelQuery.SortToggler,
            RowCount: technologymodelQuery.TotalRows,
            TotalPages: technologymodelQuery.TotalPages,
            lstTechnologyModel: technologymodelQuery.lstTechnologyModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.put(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByTechnologyId(TechnologyId: number | string | string[] | undefined) {
        let URL = "/api/Requirement/Technology/1/DeleteByTechnologyId/" + TechnologyId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/Requirement/Technology/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByTechnologyId(TechnologyId: string | number | string[] | undefined) {
        let URL = "/api/Requirement/Technology/1/CopyByTechnologyId/" + TechnologyId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/Technologying/Technology/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}

export class technologymodelQuery {
    QueryString ?: string;
    ActualPageNumber?: number;
    RowsPerPage?: number;
    SorterColumn?: string;
    SortToggler?: boolean;
    TotalRows?: number;
    TotalPages?: number;
    lstTechnologyModel?: TechnologyModel[] | undefined;
}