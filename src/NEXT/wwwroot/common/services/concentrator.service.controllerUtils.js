'use strict';
angular.module('concentrator.service.controllerUtils', [])

.service('controllerCommons', ['$log', controllerCommons]);


function controllerCommons($log) {

    this.resolvePromise = function resolvePromise(promise, callback) {
        promise.then(function(item) {
            callback(angular.fromJson(item, false));
        }, function(reason) {
            $log.error('Failed: ' + reason);
        }, function(update) {
            $log.info('Got notification: ' + update);
        });
    };
}
