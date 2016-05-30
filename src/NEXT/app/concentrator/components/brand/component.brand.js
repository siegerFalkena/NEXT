angular.module('concentrator.component')
    .controller('brandPartialController', ['$scope', 'l10n', '$log', 'brandResources', 'cfpLoadingBar', '$timeout', '$http', 'alertService', 'productResources', brandPartialController]);

function brandPartialController($scope, l10n, $log, brandResources, loadingBar, $timeout, $http, alerts, productResources) {
    var state = $scope.$parent.$parent.it.state;
    
    var Product = productResources.getClass();
    $scope.brand = $scope.$parent.$parent.it.data;

    $scope.ID = $scope.$parent.$parent.it.productID;
    var store = _.clone($scope.brand);
    $scope.l10n = l10n;

    $scope.selected = undefined;
    $scope.edit = false;
    $scope.selectBrand = false;
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

    $scope.$watch('selected', function (old, newVal) {
        $log.info(old);
        $scope.dropdownSelector(newVal);
    });

    $scope.onSelect = function (item, model, label, event) {
        $scope.brand = item;
    };

    $scope.save = function () {
        Product.setBrand({ brandID: $scope.brand.brandID, Name: $scope.brand.Name, productID: $scope.ID });
        store = $scope.brand;
        $scope.edit = false;
        $scope.select = false;
        $scope.savebtn = false;
        $scope.remove = false;
    }

    $scope.cancel = function () {
        $scope.brand = store;
        $scope.edit = false;
        $scope.select = false;
        $scope.savebtn = false;
        $scope.remove = false;
    }

    var timeout = false;
    $scope.selectorSet = [];
    $scope.dropdownSelector = function (nameInput) {
        $http({
            method: 'GET', url: '/api/brand/query',
            params: {
                Name: nameInput
            }
        }).then(function (success) {
            $scope.selectorSet = success.data;
            return success.data;
        }, function (fail) {
            alerts.add(alerts.template());
        });
    };

    $scope.logscope = function () {
        $log.info($scope);
    };
}

function selectBrand($scope, l10n, $log, brandResources, loadingBar, $timeout, $http, alerts, productResources) {
    var brandSelection = brandResources.Brand();
}