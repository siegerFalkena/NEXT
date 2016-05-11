'use strict';
angular.module('concentrator.controller.product', [
    'concentrator.model.product',
    'common.localization',
    'ui.grid',
    'ui.grid.pagination',
    'ui.grid.selection',
    'ui.grid.edit'
])
.controller('productListCtrl', ['$q', '$scope', 'productResources', '$log', 'l10n', '$rootScope', 'uiGridConstants', 'i18nService', '$http', '$httpParamSerializer', productListCtrl])
.controller('productDetailCtrl', ['$q', '$scope', 'productResources', '$log', 'l10n', '$rootScope', 'uiGridConstants', productDetailCtrl]);

function productListCtrl($q, $scope, productResources, $log, l10n, $rootScope, uiGridConstants, i18nService, $http, $httpParamSerializer) {


    $log.info(uiGridConstants);
    $log.info($scope.gridOptions);

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
        'page': 0,
        'results': 25
    }
    //product resource class
    var Product = productResources.getClass();
    var query = Product.query(parameters);
    var promise = query.$promise;
    var paginationOptions = {
        pageNumber: 1,
        pageSize: 25,
        sort: null
    };
    var queryParameters = {

    };
    $scope.gridOptions = {

        enableFiltering: true,
        enableGroupHeaderSelection: true,
        showGridFooter: true,
        hasHScrollbar: false,
        paginationPageSize: 25,
        paginationPageSizes: [5, 25, 50, 75],
        useExternalPagination: true,
        useExternalSorting: true,
        useExternalFiltering: true,
        columnDefs: [
            { name: 'SKU' },
            { name: 'ExternalProductIdentifier' },
            { name: 'Created' },
            { name: 'CreatedBy' },
            { name: 'LastModified' },
            { name: 'LastModifiedBy' },
            { name: 'BrandID' },
            { name: 'ParentProductID' },
            { name: 'ProductTypeID' }
        ],
        onRegisterApi: onRegister
    }

    function onRegister(gridApi) {
        $log.info(gridApi);
        $scope.gridApi = gridApi;
        $scope.gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
            $log.info('softChanged');
            if (sortColumns.length == 0) {
                paginationOptions.sort = null;
            } else {
                paginationOptions.sort = sortColumns[0].sort.direction;
            }
            getPage();
        });


        gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
            $log.info('pagination changed');
            paginationOptions.pageNumber = newPage;
            paginationOptions.pageSize = pageSize;
            getPage();
        });


        gridApi.core.on.filterChanged($scope, function () {
            var grid = this.grid;
            function columnIterate(column) {
                function iterateFilters(filter) {
                    if (filter.term != undefined) {
                        $log.info(parameters);
                        parameters[column.field] = filter.term;
                        getPage()
                    }
                }
                _.forEach(column.filters, iterateFilters);
            }
            _.forEach(grid.columns, columnIterate)
        });
    }


    var getPage = function getPageF() {
        var url = '/api/product?'+$httpParamSerializer(parameters);

        $http.get(url).success(function (data) {
            $scope.gridOptions.totalItems = data.meta;
            $scope.gridOptions.data = data.data;
        })
    }


    function removeSelection() {
    }

    function cb_success(resolvedValue) {
        $scope.gridOptions.data = resolvedValue.data;
        $log.info(resolvedValue);
    }
    function cb_failure(response) {
        $log.error(response);
    }
    function cb_notify(notification) {
        $log.error(notification);
    }
    promise.then(cb_success, cb_failure, cb_notify);

    $scope.l10n = l10n;
};

function productDetailCtrl($q, $scope, productResources, $log, l10n, $rootScope, uiGridConstants) {
    var stateParams = $rootScope.$stateParams;
    $scope.l10n = l10n;
    $scope.edit = false;

    var Product = productResources.getClass();

    $log.info(stateParams.id);
    if (stateParams.id == '') {
        $log.info('logo');
        $scope.edit = true;
    } else {
        $scope.edit = false;
        var productPromise = Product.get({ productId: stateParams.id }, function (object) {
            $log.info(object);
            $scope.product = object;

        }, function (errorEvent) {
            $log.error(errorEvent);
        });
    }

};
