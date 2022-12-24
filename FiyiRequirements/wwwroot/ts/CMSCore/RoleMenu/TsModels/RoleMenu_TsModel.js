"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.rolemenumodelQuery = exports.RoleMenuModel = void 0;
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
//8 fields | Last modification on: 21/12/2022 11:04:02 | Stack: 9
var RoleMenuModel = /** @class */ (function () {
    function RoleMenuModel() {
    }
    //Queries
    RoleMenuModel.Select1ByRoleMenuId = function (RoleMenuId) {
        var URL = "/api/CMSCore/RoleMenu/1/Select1ByRoleMenuIdToJSON/" + RoleMenuId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    RoleMenuModel.SelectAll = function () {
        var URL = "/api/CMSCore/RoleMenu/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    RoleMenuModel.SelectAllPaged = function (rolemenumodelQuery) {
        var URL = "/api/CMSCore/RoleMenu/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: rolemenumodelQuery.QueryString,
            ActualPageNumber: rolemenumodelQuery.ActualPageNumber,
            RowsPerPage: rolemenumodelQuery.RowsPerPage,
            SorterColumn: rolemenumodelQuery.SorterColumn,
            SortToggler: rolemenumodelQuery.SortToggler,
            RowCount: rolemenumodelQuery.TotalRows,
            TotalPages: rolemenumodelQuery.TotalPages,
            lstRoleMenuModel: rolemenumodelQuery.lstRoleMenuModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.put(URL, Body, Header));
    };
    //Non-Queries
    RoleMenuModel.DeleteByRoleMenuId = function (RoleMenuId) {
        var URL = "/api/CMSCore/RoleMenu/1/DeleteByRoleMenuId/" + RoleMenuId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    RoleMenuModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/CMSCore/RoleMenu/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    RoleMenuModel.CopyByRoleMenuId = function (RoleMenuId) {
        var URL = "/api/CMSCore/RoleMenu/1/CopyByRoleMenuId/" + RoleMenuId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    RoleMenuModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/RoleMenuing/RoleMenu/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return RoleMenuModel;
}());
exports.RoleMenuModel = RoleMenuModel;
var rolemenumodelQuery = /** @class */ (function () {
    function rolemenumodelQuery() {
    }
    return rolemenumodelQuery;
}());
exports.rolemenumodelQuery = rolemenumodelQuery;
//# sourceMappingURL=RoleMenu_TsModel.js.map