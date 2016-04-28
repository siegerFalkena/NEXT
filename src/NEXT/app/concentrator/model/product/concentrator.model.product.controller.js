'use strict';
/**
 * 
 */
angular.module('concentrator.model.product')

.controller('productCtrl', ['$q', '$scope','productResources','$log','l10n',
    productCtrl
]);

function productCtrl($q, $scope ,productResources , $log ,l10n) {

    //product resource class
    var Product = productResources.getClass();
    var query = Product.query();
    var promise = query.$promise;
    $scope.gridOptions = {};

    function cb_success(resolvedValue) {
        $scope.gridOptions.data = resolvedValue;
        $log.info(resolvedValue);
    }
    function cb_failure(response){
        $log.error(response);
    }
    function cb_notify(notification){
        $log.error(notification);
    }
    promise.then(cb_success, cb_failure, cb_notify);

    $scope.l10n = l10n;
};
