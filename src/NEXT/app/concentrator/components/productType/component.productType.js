angular.module('concentrator.component')
    .controller('productTypePartialController', ['$scope', 'l10n', '$log', 'productResources', 'cfpLoadingBar', productTypePartialController]);

function productTypePartialController($scope, l10n, $log, productResources, loadingBar) {
    $scope.productType = $scope.$parent.$parent.it.data;
    var productTypeSelection = productResources.ProductType();
    $scope.selectorItems = [];

    $scope.edit = false;

    $scope.select = false;

    $scope.savebtn = false;
    $scope.toggleSelect = function () {
        $scope.select = !$scope.select;
        $scope.savebtn = !$scope.savebtn;
    }

    $scope.dropdownSelector = function (nameInput) {
        loadingBar.start();
        //productTypeSelection.query({ Name: nameInput, results: 25, page: 0 }, function success() {
        //    $log.info('concentrator.component.productType.js');
        //    loadingBar.complete();
        //}, function fail() {
        //    $log.info('notImplemented');
        //    loadingBar.complete();
        //});
    };
    $scope.logscope = function () {
        $log.info($scope);
    }


}