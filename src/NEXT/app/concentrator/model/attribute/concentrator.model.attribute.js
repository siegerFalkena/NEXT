'use strict'
angular.module('concentrator.model')
    .service('attributeResources', ['$resource', '$log', attributeAPI]);

function attributeAPI($resource, $log) {
    this.Attribute = function () {
        return $resource('/api/attribute/:attributeID', { attributeID: '@id' }, {
            'query': {
                method: 'GET',
                params: {
                    Name: null,
                    attributeID: null
                },
                isArray: true,
                url: '/api/attribute'
            }
        });
    };
};
