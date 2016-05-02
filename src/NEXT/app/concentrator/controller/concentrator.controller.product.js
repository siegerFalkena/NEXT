'use strict';
angular.module('concentrator.controller.product', [
    'concentrator.model.product',
    'common.localization',
    'ui.grid',
    'ui.grid.pagination',
    'ui.grid.selection',
    'ui.grid.edit'
])
.controller('productListCtrl', ['$q', '$scope', 'productResources', '$log', 'l10n', '$rootScope', 'uiGridConstants', 'i18nService', productListCtrl])
.controller('productDetailCtrl', ['$q', '$scope', 'productResources', '$log', 'l10n', '$rootScope', 'uiGridConstants', productDetailCtrl]);

function productListCtrl($q, $scope, productResources, $log, l10n, $rootScope, uiGridConstants, i18nService) {


    $log.info(uiGridConstants);

    //product resource class
    var Product = productResources.getClass();
    var query = Product.query();
    var promise = query.$promise;
    $scope.gridOptions = {

        enableFiltering: true,
        enableGroupHeaderSelection: true,
        enableSelectAll: true,
        enableRowSelection: true,
        selectionRowHeaderWidth: 35,
        showGridFooter: true,
        hasHScrollbar: false,
        paginationPageSize: 25,
        paginationPageSizes: [25, 50, 75],
        useExternalPagination: true,
        useExternalSorting: false,
        columnDefs: [
            { name: "ID", enableCellEdit: false, width: '20%' },
            { name: "name", width: '20%' },
            { name: "description", editableCellTemplat: 'ui-grid/dropdownEditor', width: '20%' },
            { name: "categories", width: '20%' },
            { name: "price", width: '20%' },



        ]
    };


    function removeSelection() {
    }

    function cb_success(resolvedValue) {
        $scope.gridOptions.data = resolvedValue;
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
