'use strict';
angular.module('concentrator.controller.product')
.controller('productDetailCtrl', ['$q', '$scope', 'productResources', '$log', 'l10n', '$rootScope', 'cfpLoadingBar', productDetailCtrl]);


function productDetailCtrl($q, $scope, productResources, $log, l10n, $rootScope, cfpLoadingBar) {
    var $stateParams = $rootScope.$stateParams;
    $scope.l10n = l10n;
    var Product = productResources.getClass();

    $scope.states = {
        overview: true,
        base: true,
        children: false,
        channel: false,
        vendors: false,
        attributes: false
    };

    $scope.cards = [];
    $log.info($stateParams.isNew);

    var i = 0;
    $scope.cards.splice(0, $scope.cards.length);

    function attributes(cb_attributes) {
        productResources.getClass().attributes({ productID: $stateParams.id }, function (attributes) {
            _.each(attributes, function (attribute) {
                $scope.cards.push({
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
            $log.info('failed');
            $scope.alerts.push({ type: 'warning', msg: 'could not get channel! statusText: ' + fail.statusText });
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
            $scope.alerts.push({ type: 'warning', msg: 'could not get channel! statusText: ' + fail.statusText });
        })
    }


    function productType(cb_types) {
        productResources.getClass().type({ productID: $stateParams.id }, function (productType) {
            $scope.cards.push({
                template: "concentrator/partials/productType/productTypePartial.html",
                edit: false,
                select: true,
                remove: false,
                data: productType
            })
        }, function (fail) {
            $scope.alerts.push({ type: 'warning', msg: 'could not get brand! statusText: ' + fail.statusText });
        });
    };

    function brandCore(cb_brand) {
        productResources.getClass().brand({ productID: $stateParams.id }, function (brand) {
            $scope.cards.push({
                productID: _.clone($stateParams.id),
                template: "concentrator/partials/brand/brandPartial.html",
                edit: false,
                select: true,
                remove: false,
                data: brand
            });
        }, function (fail) {
            $log.error('could not get brand!' + fail);
            $scope.alerts.push({ type: 'warning', msg: 'could not get brand! statusText: ' + fail.statusText });
        });
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
            $scope.alerts.push({ type: 'warning', msg: 'could not get product! statusText: ' + fail.statusText });
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
            $scope.alerts.push({ type: 'warning', msg: 'could not get childProduct! statusText: ' + fail.statusText });
        })
    }

    $scope.refresh = function () {
        $scope.cards.splice(0, $scope.cards.length);
        if ($scope.states.overview) {
            productCore();
        }
        if ($scope.states.children) {
            productChildren();
        }
        if ($scope.states.vendor) {
            vendors(function () {

            });
        }
        if($scope.states.type){
            productType();
        }
        if ($scope.states.channel) {
            channels();
        }
        if ($scope.states.attributes) {
            attributes();
        }
        if ($scope.states.brand) {
            brandCore();
        } 
    };
    //ALERTS
    $scope.alerts = [];
    $scope.closeAlert = function (alert) {
        var index = _.each($scope.alerts, function (item, index) {
            if (_.has(item, 'msg') && item.msg == alert.msg && item.type == alert.type) {
                $scope.alerts.splice(index, 1);
            }
        });
    };
    $scope.alerts.push({ type: 'warning', msg: 'example error' });
    $scope.alerts.push({ type: 'danger', msg: 'example error' });
    $scope.alerts.push({ type: 'info', msg: 'example error' });
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