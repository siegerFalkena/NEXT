angular.module('concentrator.component')
    .controller('brandPartialController', ['$scope', 'l10n', '$log', 'brandResources', 'cfpLoadingBar', '$timeout', '$http', brandPartialController]);

function brandPartialController($scope, l10n, $log, brandResources, loadingBar, $timeout, $http) {
    var brandSelection = brandResources.Brand();
    $scope.brand = $scope.$parent.$parent.it.data;
    $scope.l10n = l10n;

    $scope.selectorItems = [];
    $scope.edit = false;
    $scope.select = false;
    $scope.remove = false;
    $scope.savebtn = false;
    $scope.typeaheadloading = false;

    $scope.toggleSelect = function () {
        $scope.select = !$scope.select;
        $scope.savebtn = !$scope.savebtn;
    };

    $scope.toggleEdit = function () {
        $scope.edit = !$scope.edit;
        $scope.savebtn = !$scope.savebtn;
    };

    var timeout = false;
    $scope.dropdownSelector = function (nameInput) {
        function getSelection() {
            loadingBar.start();
            //$http.get('/api/brand/query', {
            //    params: {
            //        Name: nameInput
            //    }
            //}).then(function (response) {
            //    $log.info(response);
            //    $scope.selectorItems = response.data;
            //    loadingBar.complete();
            //});
            brandSelection.query({ Name: nameInput, results: 25, page: 0 },
                function (brandArray) {
                    $scope.selectorItems = brandArray;
                    $log.info(brandArray);
                    loadingBar.complete();
                }, function fail(status) {
                    $log.error(status);
                    loadingBar.complete();
                });
        }
        if (timeout) {
            return;
        }
        timeout = true;
        $timeout(function () {
            getSelection();
            timeout = false;
        }, 400);
    };

    $scope.logscope = function () {
        $log.info($scope);
    };
}