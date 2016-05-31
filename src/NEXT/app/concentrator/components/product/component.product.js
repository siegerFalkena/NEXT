angular.module('concentrator.component')
    .controller('productPartialController', ['$location' , '$scope', 'l10n', '$http', '$log', 'productResources', 'productTypeResources', 'brandResources', 'cfpLoadingBar', 'alertService', productPartialController]);

function productPartialController($location, $scope, l10n, $http, $log, productResources, productTypeResources, brandResources, loadingBar, alertService) {
    var productSelection = productResources.getClass();
    $log.info(productResources.getClass());
    var Brands = brandResources.Brand();
    var ProductType = productTypeResources.ProductType();

    $scope.l10n = l10n;
    $scope.product = $scope.$parent.$parent.it.data;
    $log.info($scope.product);
    //new product override
    if ($scope.$parent.$parent.it.newItem) {
        $scope.product = {
            SKU: null,
            ExternalProductIdentifier: null,
            BrandID: null,
            ProductTypeID: null
        }
    }
    $log.info(productSelection);
    $scope.modelOptions = {
        debounce: {
            default: 250
        },
        getterSetter: true
    };

    //NOTE brandDropdown
    $scope.brandSet = [];
    $scope.brandSelection = '';
    $scope.brandQuery = function (val) {
        return $http.get('api/brand/query', {
            params: {
                Name: val
            }
        }).then(function (response) {
            return response.data;
        }, function (failure) {
            $log.error(failure);
            alertService.add({ msg: failure.statusText, type: 'danger' });
        });
    };

    $scope.onBrandSelect = function ($item, $model, $label, $event) {
        $scope.brandSelection = $item;
        $scope.product.brand = $scope.brandSelection;
        $scope.product.BrandID = $item.brandID;
    };

    $scope.productTypeSet = [];
    $scope.productTypeSelection = '';
    $scope.typeQuery = function (val) {
        return $http.get('api/producttype/query', {
            params: {
                Name: val
            }
        }).then(function (success) {
            return success.data;
        }, function (failure) {
            $log.error(failure);
            alertService.add({ msg: failure.statusText, type: 'danger' })
        });
    };
    $scope.onProductTypeSelect = function ($item, $model, $label, $event) {
        $scope.productTypeSelection = $item;
        $scope.product.ProductType = $scope.productTypeSelection;
        $scope.product.ProductTypeID = $item.ID;
    };


    $scope.isRoot = ($scope.product.ParentProduct != undefined);
    $scope.selectorItems = [];

    //STATE SELECTION
    var store = _.clone($scope.product);
    $scope.selectBrand = false;
    $scope.selectType = false;
    $scope.edit = false;
    $scope.select = false;
    $scope.remove = false;
    $scope.savebtn = false;

    //new item override
    if ($scope.$parent.$parent.it.newItem) {
        $scope.selectBrand = true;
        $scope.selectType = true;
        $scope.edit = true;
        $scope.select = false;
        $scope.remove = false;
        $scope.savebtn = true;
    };


    $scope.save = function () {
        if ($scope.$parent.$parent.it.newItem) {
            productSelection.newProduct($scope.product, function (succ) {
                $log.info(succ);
                $scope.product = succ;
                $location.path('product/'+ succ.productID);
                $log.info('redirect ' + $location.path());
            }, function (failure) {
                $log.error(failure);
                alertService.add({msg: failure.statusText, type: 'danger'});
            });
        } else {
            $scope.product.$save({}, function (success) {
                $log.info('request success' + success);
            }, function (failure) {
                $log.info(failure);
                alertService.add({ msg: failure.statusText, type: 'danger' });
            });
        }
        store = _.clone($scope.product);
        $scope.selectBrand = false;
        $scope.selectType = false;
        $scope.edit = false;
        $scope.select = false;
        $scope.remove = false;
        $scope.savebtn = false;
    };

    $scope.cancel = function () {
        $scope.product = _.clone(store);
        $scope.selectBrand = false;
        $scope.selectType = false;
        $scope.edit = false;
        $scope.select = false;
        $scope.remove = false;
        $scope.savebtn = false;
    };


    $scope.toggleSelect = function () {
        $scope.select = !$scope.select;
        $scope.savebtn = !$scope.savebtn;
    }


    $scope.toggleEdit = function () {
        $scope.edit = !$scope.edit;
        $scope.selectBrand = !$scope.selectBrand;
        $scope.selectType = !$scope.selectType;
        $scope.savebtn = !$scope.savebtn;
    }

    $scope.toggleDelete = function () {
        $scope.delete = !$scope.delete;
        $scope.savebtn = !$scope.savebtn;
    }

    $scope.dropdownSelector = function (nameInput) {
        loadingBar.start();
    };
}

