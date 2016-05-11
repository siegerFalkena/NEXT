'use strict';
angular.module('concentrator.model.product')
    .config(['$resourceProvider', productResources])
    .service('productResources', ['$resource', '$log', productAPI]);

function productResources($resourceProvider) {
    $resourceProvider.defaults.stripTrailingSlashes = true;
}

function productAPI($resource, $log) {
    var Product = $resource('/api/product/:ID', { ID: '@ID' }, {
        'query': {method: 'GET', isArray: false}
    });

    this.getClass = function() {
        return Product;
    };

    this.newProduct = function(name, description, price, callback) {
        var product = new Product({
            name: name,
            description: description,
            price: price
        });
        product.name = name;
        product.price = price;
        product.description = description;
        product.$save();
        return product;
    };

};
