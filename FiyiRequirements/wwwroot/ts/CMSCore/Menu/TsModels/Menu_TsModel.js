"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.menumodelQuery = exports.MenuModel = void 0;
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
//11 fields | Last modification on: 21/12/2022 10:58:18 | Stack: 9
var MenuModel = /** @class */ (function () {
    function MenuModel() {
    }
    //Queries
    MenuModel.Select1ByMenuId = function (MenuId) {
        var URL = "/api/CMSCore/Menu/1/Select1ByMenuIdToJSON/" + MenuId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    MenuModel.SelectAll = function () {
        var URL = "/api/CMSCore/Menu/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    MenuModel.SelectAllPaged = function (menumodelQuery) {
        var URL = "/api/CMSCore/Menu/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: menumodelQuery.QueryString,
            ActualPageNumber: menumodelQuery.ActualPageNumber,
            RowsPerPage: menumodelQuery.RowsPerPage,
            SorterColumn: menumodelQuery.SorterColumn,
            SortToggler: menumodelQuery.SortToggler,
            RowCount: menumodelQuery.TotalRows,
            TotalPages: menumodelQuery.TotalPages,
            lstMenuModel: menumodelQuery.lstMenuModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.put(URL, Body, Header));
    };
    //Non-Queries
    MenuModel.DeleteByMenuId = function (MenuId) {
        var URL = "/api/CMSCore/Menu/1/DeleteByMenuId/" + MenuId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    MenuModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/CMSCore/Menu/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    MenuModel.CopyByMenuId = function (MenuId) {
        var URL = "/api/CMSCore/Menu/1/CopyByMenuId/" + MenuId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    MenuModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/Menuing/Menu/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return MenuModel;
}());
exports.MenuModel = MenuModel;
var menumodelQuery = /** @class */ (function () {
    function menumodelQuery() {
    }
    return menumodelQuery;
}());
exports.menumodelQuery = menumodelQuery;
//# sourceMappingURL=Menu_TsModel.js.map