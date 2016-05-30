'use strict'
angular.module('concentrator.model.productType', [
    'ngResource'
])
    .service('productTypeResources', ['$resource', '$log', productTypeAPI]);


function productTypeAPI($resource, $log) {
    this.ProductType = function () {
        return $resource('/api/producttype/:productTypeID', { productTypeID: '@id' }, {
            'query': {
                method: 'GET',
                params: {
                    Name: null,
                    productTypeID: null
                },
                isArray: true,
                url: '/api/producttype/query'
            }
        });
    };
};
