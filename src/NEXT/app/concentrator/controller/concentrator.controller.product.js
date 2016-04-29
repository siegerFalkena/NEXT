'use strict';
angular.module('concentrator.controller.product', [
    'concentrator.model.product',
    'common.localization',
    'ui.grid',
    'ui.grid.pagination'
])
.controller('productListCtrl', ['$q', '$scope', 'productResources', '$log', 'l10n', '$rootScope', 'uiGridConstants', productListCtrl])
.controller('productDetailCtrl', ['$q', '$scope', 'productResources', '$log', 'l10n', '$rootScope', 'uiGridConstants', productDetailCtrl]);

function productListCtrl($q, $scope ,productResources , $log ,l10n, $rootScope, uiGridConstants) {


    $log.info(uiGridConstants);
    uiGridConstants.scrollbars.ALWAYS= 0;
    uiGridConstants.scrollbars.NEVER = 1;

    //product resource class
    var Product = productResources.getClass();
    var query = Product.query();
    var promise = query.$promise;
    $scope.gridOptions = {
        enableFiltering: true,
        showGridFooter: true,
        hasHScrollbar: false,
        paginationPageSize: 25,
        paginationPageSizes: [25, 50, 75],
        useExternalPagination: true,
        useExternalSorting: false
    };

    function cb_success(resolvedValue) {
        $scope.gridOptions.data = resolvedValue;
        $log.info(resolvedValue);
    }
    function cb_failure(response){
        $log.error(response);
    }
    function cb_notify(notification){
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
    var productPromise = Product.get({ productId: stateParams.id }, function (object) {
        $log.info(object);
        $scope.product = object;
    }, function (errorEvent) {
        $log.error(errorEvent);
    });

};
