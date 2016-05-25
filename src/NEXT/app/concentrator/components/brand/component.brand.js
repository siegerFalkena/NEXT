angular.module('concentrator.component')
    .controller('brandPartialController', ['$scope', 'l10n', '$log', 'brandResources', 'cfpLoadingBar', brandPartialController]);

function brandPartialController($scope, l10n, $log, brandResources, loadingBar) {
    var brandSelection = brandResources.Brand();
    $scope.brand = $scope.$parent.$parent.it.data;
    $scope.l10n = l10n;

    $scope.selectorItems = [];
    $scope.edit = false;
    $scope.select = false;
    $scope.remove = false;
    $scope.savebtn = false;

    $scope.toggleSelect = function () {
        $scope.select = !$scope.select;
        $scope.savebtn = !$scope.savebtn;
    };

    $scope.toggleEdit = function () {
        $scope.edit = !$scope.edit;
        $scope.savebtn = !$scope.savebtn;
    };

    $scope.dropdownSelector = function (nameInput) {
        loadingBar.start();
        brandSelection.query({ Name: nameInput, results: 25, page: 0 }, function success() {
            $log.info('concentrator.component.brand.js');
            loadingBar.complete();
        }, function fail() {
            $log.info('notImplemented');
            loadingBar.complete();
        });
    };

    $scope.logscope = function () {
        $log.info($scope);
    };
}