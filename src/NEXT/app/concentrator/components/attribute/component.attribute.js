angular.module('concentrator.component')
    .controller('attributePartialController', ['$scope', 'l10n', '$log', 'productResources', 'cfpLoadingBar', attributePartialController]);

function attributePartialController($scope, l10n, $log, productResources, loadingBar) {
    $scope.attribute = $scope.$parent.$parent.it.data;
    $log.info($scope.attribute);
    var vendorSelection = productResources.ProductAttributes();
    $scope.selectorItems = [];
    $scope.edit = false;
    $scope.select = false;
    $scope.savebtn = false;

    $scope.toggleSelect = function () {
        $scope.select = !$scope.select;
        $scope.savebtn = !$scope.savebtn;
    }

    $scope.toggleEdit = function () {
        $scope.edit = !$scope.edit;
        $scope.savebtn = !$scope.savebtn;
    }


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