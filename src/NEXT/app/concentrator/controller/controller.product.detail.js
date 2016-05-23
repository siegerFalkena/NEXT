'use strict';
angular.module('concentrator.controller.product')
.controller('productDetailCtrl', ['$q', '$scope', 'productResources', '$log', 'l10n', '$rootScope', 'cfpLoadingBar', productDetailCtrl]);


function productDetailCtrl($q, $scope, productResources, $log, l10n, $rootScope, cfpLoadingBar) {
    var $stateParams = $rootScope.$stateParams;
    $scope.l10n = l10n;
    $scope.context = 'BASE';

    $scope.gridsterOpts = {
        isMobile: true,
        mobileBreakPoint: 860,
        mobileModeEnabled: true,
        floating: true,
        width: 'auto',
        height: 'auto',
        minSizeX: 2,
        minSizeY: 1,
        defaultSizeX: 2,
        defaultSizeY: 1,
        resizable: { enabled: true, handles: [] },
        draggable: { enabled: true, handles: [] }
    }
    var j = 0;
    $scope.cards = [];

    function decomposeProduct(product) {
        var i = 0;
        $scope.cards.splice(0, $scope.cards.length);
        $scope.cards.push({
            id: i++,
            color: 'red',
            bgColor: 'wheat',
            template: "concentrator/partials/product/datePartial.html",
            data: {
                ID: product.productID,
                Created: product.Created,
                CreatedBy: product.CreatedBy,
                LastModified: product.LastModified,
                LastModifiedBy: product.LastModifiedBy,
                SKU: product.SKU,
                ExternalProductIdentifier: product.ExternalProductIdentifier
            },
            added: 1444871272105
        });
        $scope.cards.push({
            id: i++,
            color: 'green',
            bgColor: 'aliceblue',
            template: "concentrator/partials/product/datePartial.html",
            data: {
                ID: product.productID,
                Created: product.Created,
                CreatedBy: product.CreatedBy,
                LastModified: product.LastModified,
                LastModifiedBy: product.LastModifiedBy,
                SKU: product.SKU,
                ExternalProductIdentifier: product.ExternalProductIdentifier
            },
            added: 1444871272105
        });
        $scope.cards.push({
            id: i++,
            color: 'green',
            bgColor: 'aliceblue',
            template: "concentrator/partials/brand/brandPartial.html",
            data: {
                brand: product.brand
            },
            added: 1444871272105
        });
        _.each($scope.product.attributeValues, function (attribute) {
            return {
                id: i++,
                color: 'green',
                bgColor: 'aliceblue',
                template: "concentrator/partials/product/datePartial.html",
                data: {
                    ID: product.productID,
                    Created: product.Created,
                    CreatedBy: product.CreatedBy,
                    LastModified: product.LastModified,
                    LastModifiedBy: product.LastModifiedBy,
                    SKU: product.SKU,
                    ExternalProductIdentifier: product.ExternalProductIdentifier
                },
                added: 1444871272105
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
    var Product = productResources.getClass();
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