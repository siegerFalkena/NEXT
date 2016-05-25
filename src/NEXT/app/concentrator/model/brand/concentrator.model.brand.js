'use strict'
angular.module('concentrator.model.brand', [
    'ngResource'
])
    .config(['$resourceProvider', brandResources])
    .service('brandResources', ['$resource', '$log', brandAPI]);

function brandResources($resourceProvider) {
    $resourceProvider.defaults.stripTrailingSlashes = true;
}

function brandAPI($resource, $log) {
    this.Brand = function () {
        return $resource('/api/brand/:brandID', { brandID: '@id' }, {
            'query': {
                method: 'GET', params: {
                    Name: null,
                    brandID: null
                }, isArray: true
            }
        });
    };
};
