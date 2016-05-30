angular.module('concentrator.component')
    .controller('productPartialController', ['$scope', 'l10n', '$http', '$log', 'productResources', 'productTypeResources', 'brandResources', 'cfpLoadingBar', productPartialController]);

function productPartialController($scope, l10n, $http, $log, productResources, productTypeResources, brandResources, loadingBar) {
    var productSelection = productResources.getClass();
    var Brands = brandResources.Brand();
    var ProductType = productTypeResources.ProductType();


    $scope.l10n = l10n;
    $scope.product = $scope.$parent.$parent.it.data;
    $log.info($scope.product);

    $scope.modelOptions = {
        debounce: {
            default: 400,
            blur: 250
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
            $log.info(response);
            return response.data;
        });
    };
    $scope.onBrandSelect = function ($item, $model, $label, $event) {
        $scope.brandSelection = $item;
        $scope.product.brand = $scope.brandSelection;
        $scope.product.BrandID = $item.brandID;
        $log.info($item);
    };


    $scope.productTypeSet = [];
    $scope.productTypeSelection = '';
    $scope.typeQuery = function (val) {
        return $http.get('api/producttype/query', {
            params: {
                Name: val
            }
        }).then(function (response) {
            $log.info(response);
            return response.data;
        });
    };
    $scope.onProductTypeSelect = function ($item, $model, $label, $event) {
        $scope.productTypeSelection = $item;
        $scope.product.ProductType = $scope.productTypeSelection;
        $scope.product.ProductTypeID = $item.ID;
        $log.info($scope.product);
    };


    $scope.isRoot = ($scope.product.ParentProduct != undefined);
    $scope.selectorItems = [];

    var store = _.clone($scope.product);
    $scope.selectBrand = false;
    $scope.selectType = false;
    $scope.edit = false;
    $scope.select = false;
    $scope.remove = false;
    $scope.savebtn = false;


    $scope.save = function () {
        
        $scope.product.$save();
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

    $scope.logscope = function () {
        $log.info($scope);
    };
}

