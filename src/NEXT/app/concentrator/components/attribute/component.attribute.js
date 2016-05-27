angular.module('concentrator.component')
    .controller('attributePartialController', ['$scope', 'l10n', '$log', 'productResources', 'cfpLoadingBar', attributePartialController]);

function attributePartialController($scope, l10n, $log, productResources, loadingBar) {
    $scope.attribute = $scope.$parent.$parent.it.data;
    $scope.productID = $scope.$parent.$parent.it.productID;
    $log.info($scope.attribute);
    $scope.selectorItems = [];

    $scope.edit = false;
    $scope.select = false;
    $scope.remove = false;
    $scope.savebtn = false;
    $scope.remove = false;

    this.store = _.clone($scope.attribute);

    $scope.toggleSelect = function () {
        $scope.select = !$scope.select;
        $scope.savebtn = !$scope.savebtn;
        this.store = _.clone($scope.attribute);
    }

    $scope.toggleEdit = function () {
        $scope.edit = !$scope.edit;
        $scope.savebtn = !$scope.savebtn;
    }


    $scope.toggleRemove = function () {
        $scope.remove = !$scope.remove;
        $scope.savebtn = !$scope.savebtn;
    }

    $scope.save = function () {
        if ($scope.edit) {
            productResources.getClass().editAttribute({ value: product.value },
                function (success) {
                    this.store = $scope.attribute;
                }, function (fail) {
                    $scope.attribute = this.store;
                })
        } else if ($scope.remove) {
            productResources.getClass().removeAttribute({ productID: $scope.productID, attributeID: $scope.attribute.attributeID }
                , function (success) {

                }, function (failure) {
                    
                })
        } else if ($scope.select) {

        }
    }

    $scope.cancel = function () {
        $scope.edit = false;
        $scope.select = false;
        $scope.remove = false;
        $scope.select = false;

        $scope.attribute = this.store;
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