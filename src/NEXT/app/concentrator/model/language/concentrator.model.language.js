'use strict'
angular.module('concentrator.model')
    .service('languageResources', ['$resource', '$log', languageAPI]);


function languageAPI($resource, $log) {
    this.Language = function () {
        return $resource('/api/language/:languageID', { languageID: '@id' }, {
            'query': {
                method: 'GET',
                params: {
                    Name: null,
                    languageID: null
                },
                isArray: true,
                url: '/api/language'
            }
        });
    };
};
