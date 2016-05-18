'use strict';
angular.module('concentrator.model.product')
    .config(['$resourceProvider', productResources])
    .service('productResources', ['$resource', '$log', productAPI]);

function productResources($resourceProvider) {
    $resourceProvider.defaults.stripTrailingSlashes = true;
}

function productAPI($resource, $log) {
    var Product = $resource('/api/product/:productID', { productID: '@id' }, {
    });

    this.getClass = function() {
        return Product;
    };
};
