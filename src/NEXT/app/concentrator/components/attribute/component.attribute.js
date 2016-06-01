angular.module('concentrator.component')
    .controller('attributePartialController', ['$scope', '$http', 'l10n', '$log', 'productResources', 'cfpLoadingBar', 'alertService', 'l10n', 'attributeResources', 'vendorResources', 'languageResources', attributePartialController]);

function attributePartialController($scope, $http, l10n, $log, productResources, loadingBar, alertService, l10n, attributeResources, vendorResources, languageResources) {
    $scope.attribute = $scope.$parent.$parent.it.data;
    $scope.productID = $scope.$parent.$parent.it.productID;
    $scope.l10n = l10n;

    //DEFAULT READ STATE
    $scope.newItem = false;
    $scope.edit = false;
    $scope.select = false;
    $scope.remove = false;
    $scope.savebtn = false;
    $scope.remove = false;

    function loadObject() {
        $scope.attribute = _.clone($scope.$parent.$parent.it.data);
        loadingBar.start();
        vendorResources.Vendor().get({ vendorID: $scope.attribute.VendorID }, function (vendorData) {
            $scope.attribute.Vendor = vendorData;
        });

        languageResources.Language().get({ languageID: $scope.attribute.LanguageID }, function (languageData) {
            $scope.attribute.Language = languageData;
        });

        attributeResources.Attribute().get({ attributeID: $scope.attribute.AttributeID }, function (attributeData) {
            $scope.attribute.Attribute = attributeData;
        });
        loadingBar.complete();
    }

    if ($scope.$parent.$parent.it.newItem) {
        $scope.newItem = true;
        $scope.edit = true;
        $scope.select = true;
        $scope.savebtn = true;
        $scope.attribute = {
            Attribute: null,
            AttributeID: null,
            Language: null,
            LanguageID: null,
            Vendor: null,
            VendorID: null,
            Product: null,
            ProductID: $scope.$parent.$parent.it.productID,
            Value: null
        }
    } else {
        loadObject();
    }

    var store = _.clone($scope.attribute);



    $scope.onLanguageSelect = function ($item, $model, $label, $event) {
        $scope.attribute.LanguageID = $item.LanguageID;
        $scope.attribute.Language = $item;
    }


    $scope.toggleSelect = function () {
        $scope.select = !$scope.select;
        $scope.savebtn = !$scope.savebtn;
        this.store = _.clone($scope.attribute);
    }

    $scope.toggleEdit = function () {
        $scope.edit = !$scope.edit;
        $scope.savebtn = !$scope.savebtn;
        $scope.select = !$scope.select;
    }


    $scope.toggleRemove = function () {
        $scope.remove = !$scope.remove;
        $scope.savebtn = !$scope.savebtn;
    }

    $scope.save = function () {
        loadingBar.start();

                    $log.info($scope.attribute);
        if ($scope.edit | $scope.create) {
            productResources.getClass().saveAttribute({productID: $scope.attribute.ProductID},
                $scope.attribute
                ,
                function (success) {
                    loadingBar.complete();
                    this.store = $scope.attribute;
                }, function (fail, headers) {
                    loadingBar.complete();
                    alertService.add({ msg: 'could not update this attribute : ' + fail.statusText, type: 'danger' });
                    $log.error(fail);
                    $log.error(headers);
                    $scope.attribute = this.store;
                })
        } else if ($scope.remove) {
            productResources.getClass().removeAttribute($scope.attribute,
                function (success) {
                    loadingBar.complete();
                }, function (failure) {
                    loadingBar.complete();
                    alertService.add({ msg: 'could not delete attribute : ' + failure.statusText, type: 'danger' });
                })
        }
        $scope.edit = false;
        $scope.remove = false;
        $scope.select = false;
        $scope.savebtn = false;
    }

    $scope.cancel = function () {
        $scope.attribute = store;
        loadObject();
        $scope.edit = false;
        $scope.remove = false;
        $scope.select = false;
        $scope.savebtn = false;

    }


    $scope.dropdownSelector = function (nameInput) {
        loadingBar.start();
    };


    //NOTE attributeTypeDropdown
    $scope.attributeTypeSet = [];
    $scope.attributeTypeSelection = '';
    $scope.attributeTypeQuery = function (val) {
        loadingBar.start();
        return $http.get('api/attribute', {
            params: {
                Name: val
            }
        }).then(function (response) {
            loadingBar.complete();
            return response.data;
        }, function (failure) {
            $log.error(failure);
            alertService.add({ msg: 'failed to get attributeTypes ! ' + failure.statusText, type: 'danger' });
        });
    };

    $scope.onAttributeTypeSelect = function ($item, $model, $label, $event) {
        $log.info($item);
        $scope.attribute.Attribute = $item;
        $scope.attribute.AttributeID = $item.attributeID;
        $log.info($scope.attribute.Attribute);
    }

    //NOTE VendorDropdown
    $scope.VendorQuery = function (val) {
        loadingBar.start();
        return $http.get('api/vendor', {
            params: {
                Name: val
            }
        }).then(function (response) {
            loadingBar.complete();
            return response.data;
        }, function (failure) {
            loadingBar.complete();
            $log.error(failure);
            alertService.add({ msg: 'failed to get Vendors ! ' + failure.statusText, type: 'danger' });
        });
    };
    $scope.VendorSet = $scope.VendorQuery('');
    $scope.VendorSelection = '';

    $scope.onVendorSelect = function ($item, $model, $label, $event) {
        $scope.attribute.Vendor = $item;
        $log.info($item);
        $scope.attribute.VendorID = $item.VendorID;
    }

    //NOTE LanguageDropdown
    $scope.LanguageSet = [];
    $scope.LanguageSelection = '';
    $scope.LanguageQuery = function (val) {
        loadingBar.start();
        return $http.get('api/language', {
            params: {
                Name: val
            }
        }).then(function (response) {
            loadingBar.complete();
            return response.data;
        }, function (failure) {
            loadingBar.complete();
            $log.error(failure);
            alertService.add({ msg: 'failed to get Languages ! ' + failure.statusText, type: 'danger' });
        });
    };


}