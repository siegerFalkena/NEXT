'use strict'
angular.module('concentrator.model.product', [
    'ngResource'
]).config(['$resourceProvider', productResources])
    .service('productResources', ['$resource', '$log', productAPI]);

function productResources($resourceProvider) {
    $resourceProvider.defaults.stripTrailingSlashes = true;
}

function productAPI($resource, $log) {

    this.getClass = function () {
        return $resource('/api/product/:productID', { productID: '@id' }, {
            'query': {
                method: 'GET', params: {
                    page: null,
                    results: null,
                    min_Created: null,
                    max_Created: null,
                    CreatedBy: null,
                    ExternalProductIdentifier: null,
                    min_LastModified: null,
                    max_LastModified: null,
                    brand: null,
                    type: null,
                    productTypeName: null,
                    productBrandName: null,
                    LastModifiedBy: null,
                    ParentProductID: null,
                    ProductTypeID: null,
                    SKU: null,
                    orderBy: null,
                    ascending: null
                }, isArray: true
            },
            'brand': {
                method: 'get',
                url: 'api/product/:productID/brand',
                params: {
                    productID: '@productID'
                }
            },
            'type': {
                method: 'get',
                url: 'api/product/:productID/type',
                params: {
                    productID: '@productID'
                }
            },
            'vendor': {
                method: 'get',
                url: 'api/product/:productID/vendors/:productVendorID',
                params: {
                    productID: '@productID', vendorID: '@productVendorID'
                }
            },
            'vendors': {
                method: 'get',
                url: 'api/product/:productID/vendors',
                params: {
                    results: null,
                    page: null,
                    Name: null
                },
                isArray: true
            },
            'children': {
                method: 'get',
                url: 'api/product/:productID/children',
                params: {
                    results: 25,
                    page: 0
                },
                isArray: true
            },
            'channel': {
                method: 'get',
                url: 'api/product/:productID/channels/:productChannelID',
                params: {
                    productChannelID: '@productChannelID'
                }
            },
            'channels': {
                method: 'get',
                url: 'api/product/:productID/channels',
                params: {
                    Name: null,
                    Results: null,
                    Page: null
                },
                isArray: true
            },
            'attribute': {
                method: 'get',
                url: 'api/product/:productID/attributes/:attributeID',
                params: {
                    type: '@productType',
                    Value: '@value'

                }
            },
            'attributes': {
                method: 'get',
                url: 'api/product/:productID/attributes',
                param: {
                    //query things
                    Type: null,
                    Code: null,
                    Value: null
                },
                isArray: true
            }

        });
    };
};