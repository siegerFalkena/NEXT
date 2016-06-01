'use strict'
angular.module('concentrator.model')
    .service('vendorResources', ['$resource', '$log', vendorAPI]);

function vendorAPI($resource, $log) {
    this.Vendor = function () {
        return $resource('/api/vendor/:vendorID', { vendorID: '@id' }, {
            'query': {
                method: 'GET',
                params: {
                    Name: null,
                    vendorID: null
                },
                isArray: true,
                url: '/api/vendor/query'
            }
        });
    };
};
