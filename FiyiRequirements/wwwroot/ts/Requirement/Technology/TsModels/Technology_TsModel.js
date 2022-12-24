"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.technologymodelQuery = exports.TechnologyModel = void 0;
var Rx = require("rxjs");
var ajax_1 = require("rxjs/ajax");
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
var TechnologyModel = /** @class */ (function () {
    function TechnologyModel() {
    }
    //Queries
    TechnologyModel.Select1ByTechnologyId = function (TechnologyId) {
        var URL = "/api/Requirement/Technology/1/Select1ByTechnologyIdToJSON/" + TechnologyId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    TechnologyModel.SelectAll = function () {
        var URL = "/api/Requirement/Technology/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    TechnologyModel.SelectAllPaged = function (technologymodelQuery) {
        var URL = "/api/Requirement/Technology/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: technologymodelQuery.QueryString,
            ActualPageNumber: technologymodelQuery.ActualPageNumber,
            RowsPerPage: technologymodelQuery.RowsPerPage,
            SorterColumn: technologymodelQuery.SorterColumn,
            SortToggler: technologymodelQuery.SortToggler,
            RowCount: technologymodelQuery.TotalRows,
            TotalPages: technologymodelQuery.TotalPages,
            lstTechnologyModel: technologymodelQuery.lstTechnologyModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.put(URL, Body, Header));
    };
    //Non-Queries
    TechnologyModel.DeleteByTechnologyId = function (TechnologyId) {
        var URL = "/api/Requirement/Technology/1/DeleteByTechnologyId/" + TechnologyId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    TechnologyModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/Requirement/Technology/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    TechnologyModel.CopyByTechnologyId = function (TechnologyId) {
        var URL = "/api/Requirement/Technology/1/CopyByTechnologyId/" + TechnologyId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    TechnologyModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/Technologying/Technology/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return TechnologyModel;
}());
exports.TechnologyModel = TechnologyModel;
var technologymodelQuery = /** @class */ (function () {
    function technologymodelQuery() {
    }
    return technologymodelQuery;
}());
exports.technologymodelQuery = technologymodelQuery;
//# sourceMappingURL=Technology_TsModel.js.map