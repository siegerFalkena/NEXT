'use strict'
angular.module('concentrator.model.product', [
    'ngResource'
]).config(['$resourceProvider', productResources])
    .service('productResources', ['$resource', '$log', productAPI]);

function productResources($resourceProvider) {
    $resourceProvider.defaults.stripTrailingSlashes = true;
}

function productAPI($resource, $log) {

    this.getClass = function() {
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
            }
        });
    };
    
    this.ProductBrand = function () {
        return  $resource('/api/product/:productID/brand/:brandID', { productID: '@productID', brandID: '@brandID' }, {
        
        });
    }

    this.ProductType = function () {
        return $resource('/api/product/:productID/type/:typeID', { productID: '@productID', typeID: '@typeID' }, {

        });
    };

    this.ProductAttributes = function () {
        return $resource('/api/product/:productID/attributes/:attributeID', { productID: '@productID', attributeID: '@attributeID' }, {
            add: { method: 'POST', params: {}, isArray:false}
        });
    };

    this.ProductVendors = function () {
        return $resource('/api/product/:productID/vendors/:vendorID', { productID: '@productID', vendorID: '@vendorID' }, {
            addNew: { method: 'POST', params: {}, isArray: false }
        });
    }

    this.ProductChannels = function () {
        return $resource('/api/product/:productID/channels/:ChannelID', { productID: '@productID', ChannelID: '@ChannelID' }, {

        });
    };
};