angular.module('concentrator.component')
    .controller('vendorPartialController', ['$scope', 'l10n', '$log', 'productResources', 'cfpLoadingBar', vendorPartialController]);

function vendorPartialController($scope, l10n, $log, productResources, loadingBar) {
    var vendorSelection = productResources.getClass();
    $scope.vendor = $scope.$parent.$parent.it.data;
    $scope.l10n = l10n;

    $scope.edit = false;
    $scope.select = false;
    $scope.remove = false;


    $scope.toggleSelect = function () {
        $scope.select = !$scope.select;
        $scope.savebtn = !$scope.savebtn;
    };


    $scope.toggleEdit = function () {
        $scope.edit = !$scope.select;
        $scope.savebtn = !$scope.savebtn;
    };


    $scope.toggleRemove = function () {
        $scope.remove = !$scope.remove;
        $scope.savebtn = !$scope.savebtn;
    };

    $scope.selectorItems = [];
    $scope.dropdownSelector = function (nameInput) {
        loadingBar.start();
        //vendorSelection.query({ Name: nameInput, results: 25, page: 0 }, function success() {
        //    $log.info('concentrator.component.vendor.js');
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