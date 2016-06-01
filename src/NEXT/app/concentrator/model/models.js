'use strict'
angular.module('concentrator.model', [
    'ngResource',
    'common.alerts'])
    .config(['$resourceProvider', function ($resourceProvider) {
        $resourceProvider.defaults.stripTrailingSlashes = false;
    }]);