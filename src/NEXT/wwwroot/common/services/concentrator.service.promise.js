'use strict';
angular.module('concentrator.service.promise', [])

.service('promises', ['$q', '$log', promises]);


function promises($q, $log){


	this.getRestItemlistReturnSpinner = function getRestItemlistReturnSpinner(promise, callback) {
        promise.then(function(itemlist) {
            callback(itemlist);
        }, function(reason) {
            $log.error('Failed: ' + reason);
        }, function(update) {
            $log.info('Got notification: ' + update);
        });
    }

}