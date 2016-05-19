'use strict';
angular.module('concentrator.controller.product')
.controller('productDetailCtrl', ['$q', '$scope', 'productResources', '$log', 'l10n', '$rootScope', 'cfpLoadingBar', 'attributeComponentService', productDetailCtrl])
.component('attributePartial', {
    templateUrl: 'concentrator/partials/product/AttributeSelector.html',
    controller: attributeComponentCtrl,
    bindings: {
        obj: '='
    }
})
.component('attributePartial', {
    templateUrl: 'concentrator/partials/product/AttributeSelector.html',
    controller: attributeComponentCtrl,
    bindings: {
        obj: '='
    }
})
//TODO remove magic
.factory('attributeComponentService', ['$http', '$log', '$cookies', '$window', function () {
    function determineOrigin(productAttribute) {

    }
    return {};
}])

function productDetailCtrl($q, $scope, productResources, $log, l10n, $rootScope, cfpLoadingBar, componentService) {
    var $stateParams = $rootScope.$stateParams;
    $scope.l10n = l10n;
    $scope.context = 'BASE';

    $scope.gridsterOpts = {
        mobileBreakPoint: 800,
        mobileModeEnabled: true,
        pushing: true,
        maxRows: 999999,
        minColumns: 12,
        maxColumns: 12,
        defaultSizeX: 0,
        defaultsizeY: 0,
        margins: [5, 5],
        resizable: {
            enabled: false
        },
        draggable: { enabled: false }
    }

    $scope.items = [];
    function decomposeProduct(product) {
        $scope.items.splice(0, $scope.items.length);
        $scope.items.push({
            type: 'SKU',
            obj: product.SKU,
            editable: false,
            background: 'lightyellow',
            color: 'black'
        });
        $scope.items.push({
            type: 'ProductID',
            obj: product.productID,
            editable: false,
            background: 'lightyellow',
            color: 'black'
        });
        $scope.items.push({
            type: 'LastModifed',
            obj: product.LastModified,
            editable: false,
            background: 'lightyellow',
            color: 'black'
        });
        $scope.items.push({
            type: 'Created',
            title: l10n.getLocalized('productID'),
            obj: product.LastModified,
            editable: false,
            background: 'lightyellow',
            color: 'black'
        });
        $scope.items.push({
            type: 'ProductID',
            obj: product.productID,
            editable: false,
            background: 'lightyellow',
            color: 'black'
        });
        $scope.items.push({
            type: 'externalID',
            obj: product.ExternalProductIdentifier,
            editable: false,
            background: 'lightyellow',
            color: 'black'
        });
        $scope.items.push({
            type: 'brand',
            obj: product.SKU,
            editable: false,
            background: 'lightyellow',
            color: 'black'
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
};

function attributeComponentCtrl($log, $scope) {
    var editable = editable ? editable : false;
    $log.info(this);
    $scope.type = this.type;
    $scope.obj = this.obj;
    $scope.edit = this.edit;
}