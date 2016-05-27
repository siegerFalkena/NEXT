'use strict'
angular.module('concentrator.model.product', [
    'ngResource',
    'common.alerts'
]).config(['$resourceProvider', productResources])
    .service('productResources', ['$resource', '$log', 'alertService', productAPI]);

function productResources($resourceProvider) {
    $resourceProvider.defaults.stripTrailingSlashes = true;
}

function productAPI($resource, $log, alertService) {

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
                method: 'GET',
                url: 'api/product/:productID/brand',
                params: {
                    productID: '@productID'
                }
            },
            'setBrand': {
                method: 'POST',
                url: 'api/product/:productID/brand',
                params: {
                    productID: '@productID',
                    brandID: null,
                    Name: null
                }
            },
            'type': {
                method: 'GET',
                url: 'api/product/:productID/type',
                params: {
                    productID: '@productID'
                }
            },
            'vendor': {
                method: 'GET',
                url: 'api/product/:productID/vendors/:productVendorID',
                params: {
                    productID: '@productID', vendorID: '@productVendorID'
                }
            },
            'vendors': {
                method: 'GET',
                url: 'api/product/:productID/vendors',
                params: {
                    results: null,
                    page: null,
                    Name: null
                },
                isArray: true
            },
            'children': {
                method: 'GET',
                url: 'api/product/:productID/children',
                params: {
                    results: 25,
                    page: 0
                },
                isArray: true
            },
            'channel': {
                method: 'GET',
                url: 'api/product/:productID/channels/:productChannelID',
                params: {
                    productChannelID: '@productChannelID'
                }
            },
            'channels': {
                method: 'GET',
                url: 'api/product/:productID/channels',
                params: {
                    Name: null,
                    Results: null,
                    Page: null
                },
                isArray: true
            },
            'attribute': {
                method: 'GET',
                url: 'api/product/:productID/attributes/:attributeID',
                params: {
                    type: '@productType',
                    Value: '@value'

                }
            },
            'attributes': {
                method: 'GET',
                url: 'api/product/:productID/attributes',
                params: {
                    //query things
                    Type: null,
                    Code: null,
                    Value: null
                },
                isArray: true
            },
            'removeAttribute': {
                method: 'DELETE',
                url: 'api/product/:productID/attributes/:attributeID',
                params: {
                    productID : "@productID",
                    attributeID: "@attributeID"
                },
                isArray: true
            },
            'editAttribute': {
                method: 'POST',
                url: "api/product/:productID/attributes/:attributeID",
                params: {
                    value: null,
                    productID: '@productID',
                    attributeID: '@attributeID'
                }
            }

        });
    };
};