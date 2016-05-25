'use strict';
angular.module('concentrator.controller.product')
.controller('productDetailCtrl', ['$q', '$scope', 'productResources', '$log', 'l10n', '$rootScope', 'cfpLoadingBar', 'brandResources', productDetailCtrl]);


function productDetailCtrl($q, $scope, productResources, $log, l10n, $rootScope, cfpLoadingBar, brandResource) {
    var $stateParams = $rootScope.$stateParams;
    $scope.l10n = l10n;
    $scope.context = 'BASE';
    $scope.state = 'Details';
    var Product = productResources.getClass();


    $scope.cards = [];


    var i = 0;
    $scope.cards.splice(0, $scope.cards.length);

    function attributes() {
        var tempcards = [];
        productResources.ProductAttributes().query({ productID: $stateParams.id }, function (attributes) {
            _.each(attributes, function (attribute) {
                tempcards.push({
                    template: "concentrator/partials/attribute/AttributePartial.html",
                    edit: true,
                    select: false,
                    remove: false,
                    data: attribute
                });
            });
        }, function (fail) {
            $scope.alerts.push({ type: 'warning', msg: 'could not get brand! statusText: ' + fail.statusText });
        });
    };

    function loadChannels() {
        var tempcards = [];
        productResources.ProductChannels().query({ productID: $stateParams.id }, function (channels) {
            _.each(channels, function (channel) {
                tempcards.push({
                    template: "concentrator/partials/channel/channelPartial.html",
                    edit: false,
                    select: false,
                    remove: false,
                    data: channel
                });
            });

        }, function (fail) {
            $log.info('failed');
            $scope.alerts.push({ type: 'warning', msg: 'could not get channel! statusText: ' + fail.statusText });
        });
        return tempcards;
    };

    function vendors() {
        var tempCards = [];
        productResources.ProductVendors().query({ productID: $stateParams.id }, function (vendors) {
            _.each(vendors, function (vendor) {
                tempCards.push({
                    template: "concentrator/partials/vendor/vendorPartial.html",
                    edit: false,
                    select: false,
                    remove: false,
                    data: vendor
                });
            });

        }, function (fail) {
            $scope.alerts.push({ type: 'warning', msg: 'could not get channel! statusText: ' + fail.statusText });
        })
    }


    function productType() {
        productResources.ProductType().get({ productID: $stateParams.id }, function (productType) {
            return {
                template: "concentrator/partials/productType/productTypePartial.html",
                edit: false,
                select: true,
                ProductTypeResource: productResources.ProductType(),
                remove: false,
                data: productType
            }
        }, function (fail) {
            $scope.alerts.push({ type: 'warning', msg: 'could not get brand! statusText: ' + fail.statusText });
        });
    };

    function brandCore() {
        productResources.ProductBrand().get({ productID: $stateParams.id }, function (brand) {
            return {
                template: "concentrator/partials/brand/brandPartial.html",
                edit: false,
                select: true,
                brandResource: brandResource.Brand(),
                remove: false,
                data: brand
            };
        }, function (fail) {
            $scope.alerts.push({ type: 'warning', msg: 'could not get brand! statusText: ' + fail.statusText });
        });
    }

    function productCore() {
        productResources.getClass().get({ productID: $stateParams.id }, function (product) {
            return {
                template: "concentrator/partials/product/productPartial.html",
                edit: true,
                select: false,
                remove: false,
                data: product
            };
        }, function (fail) {
            $scope.alerts.push({ type: 'warning', msg: 'could not get product! statusText: ' + fail.statusText });
        });
    };
    //attributes();
    //productType();
    //brandCore();
    //vendors();
    //loadChannels();
    //productCore();

    $scope.refresh = function (strState) {
        var tempcards = [];
        if (strState == 'Overview') {
            tempcards.push(attributes());
            tempcards.push(productType());
            tempcards.push(brandCore());
            tempcards.push(vendors());
            _.each(vendors(), function (vendor) {
                tempcards.push(vendor);
            });
            _.each(loadChannels(), function (channel) {
                tempcards.push(channel);
            });
            tempcards.push(productCore());
        } else if (strState == 'Children') {

        } else if (strState == 'Channels') {
        } else if (strState == 'Vendors') {

        };

    }

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

    //$log.info($scope);
};

function attributeComponentCtrl($log, $scope, l10n) {
    var editable = editable ? editable : false;
    $log.info(this);
    $scope.l10n = l10n
    $scope.type = this.type;
    $scope.obj = this.obj;
    $scope.edit = this.edit;
}