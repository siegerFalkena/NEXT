'use strict';
angular.module('concentrator.controller.product')
.controller('productDetailCtrl', ['$q', '$scope', 'productResources', '$log', 'l10n', '$rootScope','cfpLoadingBar', productDetailCtrl]);

function productDetailCtrl($q, $scope, productResources, $log, l10n, $rootScope, cfpLoadingBar) {
    var $stateParams = $rootScope.$stateParams;
    $scope.l10n = l10n;

    $scope.gridsterOpts = {
        mobileBreakPoint: 800,
        mobileModeEnabled: true,
        pushing: true,
        maxRows: 999999,
        minColumns:12,
        maxColumns: 12,
        defaultSizeX: 0,
        defaultsizeY: 0,
        margins:[5,5],
        resizable: {
            enabled: false
        },
        draggable: {enabled: false}
    }


    $scope.items = [
    ];

    function decomposeProduct(product) {
        $scope.item = [];
        $scope.items.push({
            size: { x: 2, y: 0},
            position: [0, 0] ,
            template: 'concentrator/partials/product/brandPartial.html',
            background: 'wheat',
            color: 'black'
        });
        $scope.items.push({
            size: { x: 0, y: 0 },
            position: [0, 0],
            template: 'concentrator/partials/product/SKUPartial.html',
            background: 'papayawhip',
            color: 'black'
        });
        $scope.items.push({
            size: { x: 0, y: 0 },
            position: [0, 0],
            template: 'concentrator/partials/product/ExternalIDPartial.html',
            background: 'lavender',
            color: 'black'
        });
        $scope.items.push({
            template: 'concentrator/partials/product/datePartial.html',
            background: 'blanchedalmond',
            color: 'black'
        });
        if (product.LastModified != null) {
            $scope.items.push({
                template: 'concentrator/partials/product/lastModifiedPartial.html',
                background: 'aliceblue',
                color: 'black'
            });
        };
        for (var i = 0; i < 60; i++) {
            $scope.items.push({
                template: 'concentrator/partials/product/datePartial.html',
                background: 'blanchedalmond',
                color: 'black'
            });
        }

    }

    //ALERTS
    $scope.alerts = [ ];
    $scope.closeAlert = function (alert) {
        var index = _.each($scope.alerts, function (item, index) {
            if (_.has(item, 'msg') && item.msg == alert.msg) {
                $scope.alerts.splice(index, 1);
            }
        });
    };

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
            $log.info(object);
            $scope.product = object;
            decomposeProduct(object);
        }, function (errorEvent) {
            cfpLoadingBar.complete();
            $scope.alerts.push({type: 'danger', msg: 'could not connect to backend! statusText: ' + errorEvent.statusText});
            $log.error(errorEvent);
        });
    };
};
