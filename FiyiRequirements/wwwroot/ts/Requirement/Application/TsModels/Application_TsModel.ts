import * as Rx from "rxjs";
import { ajax } from "rxjs/ajax";
import { Ajax } from "../../../Library/Ajax";
import { UserApplicationModel } from "../../UserApplication/TsModels/UserApplication_TsModel";

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

//9 fields | Sub-models: 1 models  | Last modification on: 25/12/2022 12:07:25 | Stack: 9

export class ApplicationModel {

    //Fields
    ApplicationId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	Name?: string | string[] | number | undefined;
	Description?: string | string[] | number | undefined;
	TechnologyId?: number;
    lstUserApplicationModel?: UserApplicationModel[] | undefined;
    UserCreationIdFantasyName?: string | string[] | number | undefined;
    UserLastModificationIdFantasyName?: string | string[] | number | undefined;
    TechnologyIdName?: string | string[] | number | undefined;
    

    //Queries
    static Select1ByApplicationId(ApplicationId: number) {
        let URL = "/api/Requirement/Application/1/Select1ByApplicationIdToJSON/" + ApplicationId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/Requirement/Application/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(applicationmodelQuery: applicationmodelQuery) {
        let URL = "/api/Requirement/Application/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: applicationmodelQuery.QueryString,
            ActualPageNumber: applicationmodelQuery.ActualPageNumber,
            RowsPerPage: applicationmodelQuery.RowsPerPage,
            SorterColumn: applicationmodelQuery.SorterColumn,
            SortToggler: applicationmodelQuery.SortToggler,
            RowCount: applicationmodelQuery.TotalRows,
            TotalPages: applicationmodelQuery.TotalPages,
            lstApplicationModel: applicationmodelQuery.lstApplicationModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.put(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByApplicationId(ApplicationId: number | string | string[] | undefined) {
        let URL = "/api/Requirement/Application/1/DeleteByApplicationId/" + ApplicationId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/Requirement/Application/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByApplicationId(ApplicationId: string | number | string[] | undefined) {
        let URL = "/api/Requirement/Application/1/CopyByApplicationId/" + ApplicationId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/Applicationing/Application/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}

export class applicationmodelQuery {
    QueryString ?: string;
    ActualPageNumber?: number;
    RowsPerPage?: number;
    SorterColumn?: string;
    SortToggler?: boolean;
    TotalRows?: number;
    TotalPages?: number;
    lstApplicationModel?: ApplicationModel[] | undefined;
}