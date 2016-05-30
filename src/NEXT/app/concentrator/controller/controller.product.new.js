'use strict';
angular.module('concentrator.controller.product')
.controller('productNewCtrl', ['$scope', 'productResources', '$log', 'l10n', '$rootScope', 'cfpLoadingBar', newProductCtrl]);

function newProductCtrl($scope , productResources, $log, l10n, $rootScope, cfpLoadingBar) {
    $scope.l10n = l10n;
    var productResource = productResources.getClass();
    var product = {
        brandID: undefined,
        productTypeID: undefined,
        SKU: undefined,
        externalProductID: undefined
    }

    $scope.cards = [];
    $scope.cards.push({
        template: "concentrator/partials/product/productPartial.html",
        edit: true,
        select: false,
        remove: false,
        data: product
    });

    $scope.cards.push({
        template: 'concentrator/partials/brand/brandPartial.html',
        edit: false,
        select: false,
        remove: false,
        data: product.brand
    })

    $scope.cards.push({
        template: 'concentrator/partials/productType/productTypePartial.html',
        edit: false,
        select: false,
        remove: false,
        data: product.productTypeID
    });

    $scope.save(); 
}