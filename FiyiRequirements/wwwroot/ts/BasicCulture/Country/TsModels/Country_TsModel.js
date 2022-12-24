"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.countrymodelQuery = exports.CountryModel = void 0;
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
//10 fields | Last modification on: 21/12/2022 10:30:11 | Stack: 9
var CountryModel = /** @class */ (function () {
    function CountryModel() {
    }
    //Queries
    CountryModel.Select1ByCountryId = function (CountryId) {
        var URL = "/api/BasicCulture/Country/1/Select1ByCountryIdToJSON/" + CountryId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    CountryModel.SelectAll = function () {
        var URL = "/api/BasicCulture/Country/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    CountryModel.SelectAllPaged = function (countrymodelQuery) {
        var URL = "/api/BasicCulture/Country/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: countrymodelQuery.QueryString,
            ActualPageNumber: countrymodelQuery.ActualPageNumber,
            RowsPerPage: countrymodelQuery.RowsPerPage,
            SorterColumn: countrymodelQuery.SorterColumn,
            SortToggler: countrymodelQuery.SortToggler,
            RowCount: countrymodelQuery.TotalRows,
            TotalPages: countrymodelQuery.TotalPages,
            lstCountryModel: countrymodelQuery.lstCountryModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.put(URL, Body, Header));
    };
    //Non-Queries
    CountryModel.DeleteByCountryId = function (CountryId) {
        var URL = "/api/BasicCulture/Country/1/DeleteByCountryId/" + CountryId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    CountryModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/BasicCulture/Country/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    CountryModel.CopyByCountryId = function (CountryId) {
        var URL = "/api/BasicCulture/Country/1/CopyByCountryId/" + CountryId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    CountryModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/Countrying/Country/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return CountryModel;
}());
exports.CountryModel = CountryModel;
var countrymodelQuery = /** @class */ (function () {
    function countrymodelQuery() {
    }
    return countrymodelQuery;
}());
exports.countrymodelQuery = countrymodelQuery;
//# sourceMappingURL=Country_TsModel.js.map