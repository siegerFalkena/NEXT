'use strict';
angular.module('concentrator.controller.product')
.controller('productDetailCtrl', ['$q', '$scope', 'productResources', '$log', 'l10n', '$rootScope', 'cfpLoadingBar', '$resource', productDetailCtrl]);


function productDetailCtrl($q, $scope, productResources, $log, l10n, $rootScope, cfpLoadingBar, $resource) {
    var $stateParams = $rootScope.$stateParams;
    $scope.l10n = l10n;
    $scope.context = 'BASE';
    $scope.$log = $log;
    $scope.state = 'Details';
    var Product = productResources.getClass();

    $scope.cards = [];

    function decomposeProduct(product) {
        var i = 0;
        $scope.cards.splice(0, $scope.cards.length);
        $scope.cards.push({
            template: "concentrator/partials/product/datePartial.html",
            edit: true,
            data: {
                ID: product.productID,
                Created: product.Created,
                CreatedBy: product.CreatedBy,
                LastModified: product.LastModified,
                LastModifiedBy: product.LastModifiedBy,
                SKU: product.SKU,
                ExternalProductIdentifier: product.ExternalProductIdentifier,
                product: product
            },
            save: function (data) {
                $log.info($scope.product);
                $scope.product.$save();
            }
        });
        $scope.cards.push({
            template: "concentrator/partials/brand/brandPartial.html",
            data: {
                brand: product.brand
            }
        });
        $scope.cards.push({
            template: "concentrator/partials/product/productTypePartial.html",
            edit: true,
            data: {
                ProductType: product.ProductType
            }
        });
        _.each($scope.product.attributeValues, function (attribute) {
            $scope.cards.push({
                template: "concentrator/partials/attribute/AttributePartial.html",
                data: {
                    Attribute: attribute
                }
            });
        });
        $scope.cards.push({
            template: "concentrator/partials/buttonSet.html",
            data: {
                log: $log
            }
        });
    };


    //ALERTS
    $scope.alerts = [];
    $scope.closeAlert = function (alert) {
        var index = _.each($scope.alerts, function (item, index) {
            if (_.has(item, 'msg') && item.msg == alert.msg) {
                $scope.alerts.splice(index, 1);
            }
        });
    };

    //Model

    if ($stateParams.id == '') {
        $log.info('logo');
        $scope.edit = true;
    } else {
        $scope.edit = false;
        cfpLoadingBar.start();
        var productPromise =
        Product.get({ productID: $stateParams.id }, function (object) {
            cfpLoadingBar.complete();
            $scope.product = object;
            $log.info(object);
            decomposeProduct(object);
        }, function (errorEvent) {
            cfpLoadingBar.complete();
            $scope.alerts.push({ type: 'danger', msg: 'could not connect to backend! statusText: ' + errorEvent.statusText });
            $log.error(errorEvent);
        });
    };
    $log.info($scope);
};

function attributeComponentCtrl($log, $scope, l10n) {
    var editable = editable ? editable : false;
    $log.info(this);
    $scope.l10n = l10n
    $scope.type = this.type;
    $scope.obj = this.obj;
    $scope.edit = this.edit;
}