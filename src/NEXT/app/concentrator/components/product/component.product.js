angular.module('concentrator.component')
    .controller('productPartialController', ['$scope', 'l10n', '$log', 'productResources', 'cfpLoadingBar', productPartialController]);

function productPartialController($scope, l10n, $log, productResources, loadingBar) {
    var productSelection = productResources.getClass();
    $scope.product = $scope.$parent.$parent.it.data;
    $scope.l10n = l10n;
    $scope.isRoot = ($scope.product.ParentProduct != undefined);
    $scope.selectorItems = [];
    $scope.edit = false;
    $scope.select = false;
    $scope.toggleSelect = function () {
        $scope.select = !$scope.select;
    }

    $scope.dropdownSelector = function (nameInput) {
        loadingBar.start();
        //productSelection.query({ Name: nameInput, results: 25, page: 0 }, function success() {
        //    $log.info('concentrator.component.product.js');
        //    loadingBar.complete();
        //}, function fail() {
        //    $log.info('notImplemented');
        //    loadingBar.complete();
        //});
    };
    $scope.logscope = function () {
        $log.info($scope);
    };


}