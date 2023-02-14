import * as Rx from "rxjs";
import { ajax } from "rxjs/ajax";
import { Ajax } from "../../../Library/Ajax";


/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2023
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
*/

//23 fields | Sub-models: 0 models  | Last modification on: 31/01/2023 7:54:01 | Stack: 9

export class ExampleModel {

    //Fields
    ExampleId?: number;
	Active?: boolean;
	DateTimeCreation?: string | string[] | number | undefined;
	DateTimeLastModification?: string | string[] | number | undefined;
	UserCreationId?: number;
	UserLastModificationId?: number;
	Boolean?: boolean;
	DateTime?: string | string[] | number | undefined;
	Decimal?: number;
	ForeignKeyDropDown?: number;
	ForeignKeyOptions?: number;
	Integer?: number;
	TextBasic?: string | string[] | number | undefined;
	TextEmail?: string | string[] | number | undefined;
	TextFile?: string | string[] | number | undefined;
	TextHexColour?: string | string[] | number | undefined;
	TextPassword?: string | string[] | number | undefined;
	TextPhoneNumber?: string | string[] | number | undefined;
	TextTag?: string | string[] | number | undefined;
	TextTextArea?: string | string[] | number | undefined;
	TextTextEditor?: string | string[] | number | undefined;
	TextURL?: string | string[] | number | undefined;
	Time?: string;
    

    //Queries
    static Select1ByExampleId(ExampleId: number) {
        let URL = "/api/Examples/Example/1/Select1ByExampleIdToJSON/" + ExampleId;
        return Rx.from(ajax(URL));
    }

    static SelectAll() {
        let URL = "/api/Examples/Example/1/SelectAllToJSON"
        return Rx.from(ajax(URL));
    }
    
    static SelectAllPaged(examplemodelQuery: examplemodelQuery) {
        let URL = "/api/Examples/Example/1/SelectAllPagedToJSON";
        let Body = {
            QueryString: examplemodelQuery.QueryString,
            ActualPageNumber: examplemodelQuery.ActualPageNumber,
            RowsPerPage: examplemodelQuery.RowsPerPage,
            SorterColumn: examplemodelQuery.SorterColumn,
            SortToggler: examplemodelQuery.SortToggler,
            RowCount: examplemodelQuery.TotalRows,
            TotalPages: examplemodelQuery.TotalPages,
            lstExampleModel: examplemodelQuery.lstExampleModel
        };
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.put(URL, Body, Header));
    }

    //Non-Queries
    static DeleteByExampleId(ExampleId: number | string | string[] | undefined) {
        let URL = "/api/Examples/Example/1/DeleteByExampleId/" + ExampleId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.delete(URL, Header));
    }

    static DeleteManyOrAll(DeleteType: string, Body: Ajax) {
        let URL = "/api/Examples/Example/1/DeleteManyOrAll/" + DeleteType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
    
    static CopyByExampleId(ExampleId: string | number | string[] | undefined) {
        let URL = "/api/Examples/Example/1/CopyByExampleId/" + ExampleId;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        let Body: any = {};
        return Rx.from(ajax.post(URL, Body, Header));
    }

    static CopyManyOrAll(CopyType: string, Body: Ajax) {
        let URL = "/api/Exampleing/Example/1/CopyManyOrAll/" + CopyType;
        let Header: any = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax.post(URL, Body, Header));
    }
}

export class examplemodelQuery {
    QueryString ?: string;
    ActualPageNumber?: number;
    RowsPerPage?: number;
    SorterColumn?: string;
    SortToggler?: boolean;
    TotalRows?: number;
    TotalPages?: number;
    lstExampleModel?: ExampleModel[] | undefined;
}