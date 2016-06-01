'use strict';

angular.module('concentrator.controller.product').controller('productListCtrl', ['$q', '$scope', 'productResources', '$log', 'l10n', '$rootScope', 'uiGridConstants', 'i18nService', 'cfpLoadingBar', '$timeout', 'alertService', '$window', productListCtrl]);

function productListCtrl($q, $scope, productResources, $log, l10n, $rootScope, uiGridConstants, i18nService, cfpLoadingBar, $timeout, alertService, $window) {
    $scope.height = $window.innerHeight;
    $log.info($scope.height);
    var parameters = {
        'orderBy': null,
        'ascending': true,
        'min_Created': null,
        'max_Created': null,
        'CreatedBy': null,
        'ExternalProductIdentifier': null,
        'min_LastModified': null,
        'max_LastModified': null,
        'ParentProductID': null,
        'ProductTypeID': null,
        'SKU': null,
        'brand': null,
        'productTypeName': null,
        'productBrandName': null
    };

    var columnDefs = [
        {
            displayName: "SKU",
            width: 200,
            enableCellEdit: true,
            visible: true,
            field: "SKU"
        },
        {
            displayName: "Created",
            width: 150,
            enableCellEdit: false,
            visible: true,
            field: "Created",
            filters: [
                {
                    condition: uiGridConstants.filter.GREATER_THAN,
                    placeholder: 'greater than'
                },
                {
                    condition: uiGridConstants.filter.LESS_THAN,
                    placeholder: 'less than'
                }
            ]
        },
        {
            displayName: "CreatedBy",
            width: 150,
            enableCellEdit: false,
            visible: false,
            field: "CreatedBy"
        },
         {

             displayName: "LastModifiedBy",
             width: 150,
             enableCellEdit: false,
             visible: false,
             field: "LastModifiedBy"
         },
        {
            displayName: "LastModified",
            enableCellEdit: false,
            width: 150,
            visible: true,
            field: "LastModified",
            filters: [
                 {
                     condition: uiGridConstants.filter.GREATER_THAN,
                     placeholder: 'greater than'
                 },
                {
                    condition: uiGridConstants.filter.LESS_THAN,
                    placeholder: 'less than'
                }
            ]

        },
        {
            displayName: "ExternalID",
            width: 150,
            enableCellEdit: false,
            visible: true,
            field: "ExternalProductIdentifier"
        },
    ]


    //product resource class
    var Product = productResources.getClass();
    var query = Product.query(parameters);
    $scope.gridOptions = {
        shwoTreeExpandNoChildren: true,
        enableFiltering: true,
        enableGroupHeaderSelection: true,
        showGridFooter: true,
        hasHScrollbar: true,
        paginationPageSize: 25,
        paginationPageSizes: [10, 25, 50, 100, 500],
        useExternalPagination: true,
        useExternalSorting: true,
        useExternalFiltering: true,
        onRegisterApi: onRegister,
        columnDefs: columnDefs,
        data: [],
        totalItems: 0
    }

    //Register grid callbacks
    function onRegister(gridApi) {
        $scope.gridApi = gridApi;
        //Sorting
        $scope.gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
            if (sortColumns.length == 0) {
                parameters.sort = null;
            } else {
                parameters.orderBy = sortColumns[0].field;
                parameters.ascending = sortColumns[0].sort.direction === uiGridConstants.ASC ? true : false;
            }
            getPage();
        });
        //Pagination
        gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
            parameters.page = newPage - 1;
            parameters.results = pageSize;
            getPage();
        });
        //Filters
        gridApi.core.on.filterChanged($scope, function () {
            var grid = this.grid;
            function columnIterate(column) {
                function iterateFilters(filter) {
                    if (filter.term != undefined) {
                        parameters[column.field] = filter.term;
                        getPage();
                    }
                }
                _.forEach(column.filters, iterateFilters);
            }
            _.forEach(grid.columns, columnIterate)
        });
    };

    //Get product-array
    var waiting = false;
    var getPage = function getPageF() {
        if (waiting == false) {
            waiting = true;
            $timeout(aftertimeout(), 800);
            function aftertimeout() {
                cfpLoadingBar.start();
                $log.info(parameters);
                Product.query(parameters, function (success) {
                    $scope.gridOptions.data = success.data;
                    $scope.gridOptions.totalItems = success.results;
                    waiting = false;
                    cfpLoadingBar.complete();
                }, function (failure) {
                    $log.error(failure);
                    alertService.add({ msg: failure.statusText, type: 'danger' });
                    waiting = false;
                    cfpLoadingBar.complete();
                })
            }
        }
    }
    getPage();
    $scope.l10n = l10n;
};
