'use strict';
angular.module('concentrator.controller.product', [
    'concentrator.model.product',
    'common.localization',
    'ngAnimate',
    'ngTouch',
    'ui.grid',
    'ui.grid.pagination',
    'ui.grid.selection',
    'ui.grid.edit',
    'ui.grid.treeView',
    'ui.grid.resizeColumns',
    'ui.grid.autoResize',
    'ui.grid.moveColumns',
    'cfp.loadingBar',
    'ui.grid.pinning'
])
.controller('productListCtrl', ['$q', '$scope', 'productResources', '$log', 'l10n', '$rootScope', 'uiGridConstants', 'i18nService', '$http', '$httpParamSerializer', 'cfpLoadingBar', productListCtrl])
.controller('productDetailCtrl', ['$q', '$scope', 'productResources', '$log', 'l10n', '$rootScope', 'uiGridConstants', productDetailCtrl]);

function productListCtrl($q, $scope, productResources, $log, l10n, $rootScope, uiGridConstants, i18nService, $http, $httpParamSerializer, cfpLoadingBar) {
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
        'SKU': null
    }
    //product resource class
    var Product = productResources.getClass();
    var query = Product.query(parameters);
    var promise = query.$promise;
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
        columnDefs: [
            { name: 'SKU' },
            { name: 'ExternalProductIdentifier' },
            {
                name: 'Created', type: 'date', enableCellEdit: false, filters: [
                {
                    condition: uiGridConstants.filter.GREATER_THAN,
                    placeholder: 'greater than'
                },
                {
                    condition: uiGridConstants.filter.LESS_THAN,
                    placeholder: 'less than'
                }
                ],
            },
            //move these to sub object / subfield
            //{ name: 'CreatedBy' },
            {
                name: 'LastModified', type: 'date', enableCellEdit: false, cellFilter: 'date:\'yyyy-MM-dd\'', filters: [
                {
                    condition: uiGridConstants.filter.GREATER_THAN,
                    placeholder: 'greater than'

                },
                {
                    condition: uiGridConstants.filter.LESS_THAN,
                    placeholder: 'less than'
                }
                ],
            }
            //{ name: 'LastModifiedBy' }
            //{ name: 'BrandID' },
            //{ name: 'ParentProductID' },
            //{ name: 'ProductTypeID' }
        ],
        onRegisterApi: onRegister
    }
    var opts = {
          lines: 17 // The number of lines to draw
        , length: 28 // The length of each line
        , width: 16 // The line thickness
        , radius: 42 // The radius of the inner circle
        , scale: 10 // Scales overall size of the spinner
        , corners: 0 // Corner roundness (0..1)
        , color: '#000' // #rgb or #rrggbb or array of colors
        , opacity: 0.25 // Opacity of the lines
        , rotate: 90 // The rotation offset
        , direction: 1 // 1: clockwise, -1: counterclockwise
        , speed: 1 // Rounds per second
        , trail: 10 // Afterglow percentage
        , fps: 20 // Frames per second when using setTimeout() as a fallback for CSS
        , zIndex: 2e9 // The z-index (defaults to 2000000000)
        , className: 'spinner' // The CSS class to assign to the spinner
        , top: '50%' // Top position relative to parent
        , left: '50%' // Left position relative to parent
        , shadow: false // Whether to render a shadow
        , hwaccel: true // Whether to use hardware acceleration
        , position: 'absolute' // Element positioning
    }

    function onRegister(gridApi) {
        $log.info(gridApi);
        $scope.gridApi = gridApi;

        //Sorting
        $scope.gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
            if (sortColumns.length == 0) {
                parameters.sort = null;
            } else {
                parameters.orderBy = sortColumns[0].field;
                parameters.ascending = sortColumns[0].sort.direction === uiGridConstants.ASC ? true : false;
                $log.info(sortColumns);
            }
            getPage();
        });

        //Pagination
        gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
            $log.info('pagination changed');
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
                        $log.info(parameters);
                        parameters[column.field] = filter.term;
                        getPage();
                    }
                }
                _.forEach(column.filters, iterateFilters);
            }
            _.forEach(grid.columns, columnIterate)
        });
    };


    var getPage = function getPageF() {
        cfpLoadingBar.start();
        var url = '/api/product?' + $httpParamSerializer(parameters);
        $http.get(url).success(function (data) {
            cfpLoadingBar.complete();
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
