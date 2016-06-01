'use strict'
angular.module('concentrator.model')
    .service('brandResources', ['$resource', '$log', brandAPI]);


function brandAPI($resource, $log) {
    this.Brand = function () {
        return $resource('/api/brand/:brandID', { brandID: '@id' }, {
            'query': {
                method: 'GET',
                params: {
                    Name: null,
                    brandID: null
                },
                isArray: true,
                url: '/api/brand/query'
            }
        });
    };
};
