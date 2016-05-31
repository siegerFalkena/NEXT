angular.module('concentrator.component')
    .controller('attributePartialController', ['$scope', '$http', 'l10n', '$log', 'productResources', 'cfpLoadingBar',  'alertService', 'l10n', attributePartialController]);

function attributePartialController($scope,$http, l10n, $log, productResources, loadingBar, alertService, l10n) {
    $scope.attribute = $scope.$parent.$parent.it.data;
    $scope.productID = $scope.$parent.$parent.it.productID;
    $scope.l10n = l10n;



    $scope.edit = false;
    $scope.select = false;
    $scope.remove = false;
    $scope.savebtn = false;
    $scope.remove = false;
    $scope.newItem = $scope.$parent.$parent.it.newItem;
    if($scope.newItem){
        $scope.edit = true;
        $scope.select = true;
        $scope.savebtn = true;
    }

    if ($scope.$parent.$parent.it.newItem) {
        $scope.attribute = {
            AttributeID: null,
            LanguageID: null,
            VendorID: null,
            ProductID: $scope.productID,
            Value: null
        }
    }
    $log.info($scope.attribute);
    $scope.selectorItems = [];


    //NOTE attributeTypeDropdown
    $scope.attributeTypeSet = [];
    $scope.attributeTypeSelection = '';
    $scope.attributeTypeQuery = function (val) {
        return $http.get('api/attribute/type', {
            params: {
                Name: val
            }
        }).then(function (response) {
            $log.info(response);
            return response.data;
        }, function (failure) {
            $log.error(failure);
            alertService.add({ msg: 'failed to get attributeTypes ! ' + failure.statusText, type: 'danger' });
        });
    };

    $scope.onAttributeTypeSelect = function ($item, $model, $label, $event) {
        $scope.attribute = $item;
    }

    //NOTE VendorDropdown
    $scope.VendorSet = [];
    $scope.VendorSelection = '';
    $scope.VendorQuery = function (val) {
        return $http.get('api/vendor', {
            params: {
                Name: val
            }
        }).then(function (response) {
            $log.info(response);
            return response.data;
        }, function (failure) {
            $log.error(failure);
            alertService.add({ msg: 'failed to get Vendors ! ' + failure.statusText, type: 'danger' });
        });
    };

    $scope.onVendorSelect = function ($item, $model, $label, $event) {
        $log.info($item);
        $scope.attribute.VendorID = $item.VendorID;
    }

    //NOTE LanguageDropdown
    $scope.LanguageSet = [];
    $scope.LanguageSelection = '';
    $scope.LanguageQuery = function (val) {
        return $http.get('api/language', {
            params: {
                Name: val
            }
        }).then(function (response) {
            $log.info(response);
            return response.data;
        }, function (failure) {
            $log.error(failure);
            alertService.add({ msg: 'failed to get Languages ! ' + failure.statusText, type: 'danger' });
        });
    };

    $scope.onLanguageSelect = function ($item, $model, $label, $event) {
        $scope.attribute.LanguageID = $item;
    }



    this.store = _.clone($scope.attribute);

    $scope.toggleSelect = function () {
        $scope.select = !$scope.select;
        $scope.savebtn = !$scope.savebtn;
        this.store = _.clone($scope.attribute);
    }

    $scope.toggleEdit = function () {
        $scope.edit = !$scope.edit;
        $scope.savebtn = !$scope.savebtn;
        $scope.selectType = !$scope.selectType;
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