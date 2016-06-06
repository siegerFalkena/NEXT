'use strict';
angular.module('concentrator.controller.product')
.controller('productDetailCtrl', ['$q', '$location', '$scope', 'productResources', '$log', 'l10n', '$rootScope', 'cfpLoadingBar', 'alertService', productDetailCtrl]);


function productDetailCtrl($q, $location,  $scope, productResources, $log, l10n, $rootScope, cfpLoadingBar, alertService) {
    var $stateParams = $rootScope.$stateParams;
    var state = $rootScope.$stateParams.state;
    $scope.l10n = l10n;
    var Product = productResources.getClass();

    $scope.STATE = 'detail';


    function attributes(cb_attributes) {
        productResources.getClass().attributes({ productID: $stateParams.id }, function (attributes) {
            _.each(attributes, function (attribute) {
                $log.info(attribute);
                $scope.cards.push({
                    template: "concentrator/partials/attribute/AttributePartial.html",
                    edit: true,
                    select: false,
                    remove: false,
                    data: attribute
                });
            });
            $scope.cards.push({
                template: "concentrator/partials/attribute/AttributePartial.html",
                edit: true,
                select: true,
                remove: false,
                newItem: true,
                productID: $stateParams.id
            });
        }, function (fail) {
            $log.error(fail)
            alertService.add({ type: 'warning', msg: 'could not get product attributes! statusText: ' + fail.statusText })
        });
    };

    function channels(cb_channels) {
        productResources.getClass().channels({ productID: $stateParams.id }, function (channels) {
            $log.info(channels);
            _.each(channels, function (channel) {
                $scope.cards.push({
                    template: "concentrator/partials/channel/channelPartial.html",
                    edit: false,
                    select: false,
                    remove: true,
                    data: channel
                });
            });

        }, function (fail) {
            $log.error(fail);
            alertService.add({ type: 'warning', msg: 'could not get product channels! statusText: ' + fail.statusText })
        });
    };

    function vendors(cb_vendors) {
        productResources.getClass().vendors({ productID: $stateParams.id }, function (vendors) {
            _.each(vendors, function (vendor) {
                $scope.cards.push({
                    template: "concentrator/partials/vendor/vendorPartial.html",
                    edit: false,
                    select: false,
                    remove: true,
                    data: vendor
                });
            });

        }, function (fail) {
            $log.error(fail);
            alertService.add({ type: 'warning', msg: 'could not get product vendors! statusText: ' + fail.statusText })
        })
    }


    function productCore(cb_core) {
        productResources.getClass().get({ productID: $stateParams.id }, function (product) {
            $scope.product = product;
            $scope.cards.push({
                template: "concentrator/partials/product/productPartial.html",
                edit: true,
                select: false,
                remove: false,
                isNew: $stateParams.isNew,
                data: product
            })
        }, function (fail) {
            $location.path('product/overview')
            alertService.add({type: 'danger', msg: 'could not get product! ' + fail.statusText })
        });
    };

    function productChildren() {
        productResources.getClass().children({ productID: $stateParams.id, results: null, page: null }, function (childProducts) {
            _.each(childProducts, function (child) {
                $scope.cards.push({
                    template: "concentrator/partials/product/productPartial.html",
                    edit: false,
                    select: false,
                    remove: false,
                    data: child
                });
            });
        }, function (fail) {
            $log.error(fail)
            alertService.add({ type: 'warning', msg: 'could not get child products! statusText: ' + fail.statusText })
        })
    }


    $scope.cards = [];
    $scope.refresh = function () {
        $scope.cards.splice(0, $scope.cards.length);
        if ($scope.STATE == 'detail') {
            productCore();
        } else if ($scope.STATE == 'children') {
            productChildren();
        } else if ($scope.STATE == 'vendors') {
            vendors();
        } else if ($scope.STATE == 'channels') {
            channels();
        } else if ($scope.STATE == 'attributes') {
            attributes();
        }
    };
    $scope.refresh();
};

function attributeComponentCtrl($log, $scope, l10n) {
    var editable = editable ? editable : false;
    $log.info(this);
    $scope.l10n = l10n
    $scope.type = this.type;
    $scope.obj = this.obj;
    $scope.edit = this.edit;
}