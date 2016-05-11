/** core, bootstrap functions and initialization
 * @module angular_module.concentrator
 * @memberof angular_module
 */
'use strict';
angular.module('concentrator', [
        'ngAnimate',
        'ui.router',
        'ui.bootstrap',
        'ui.bootstrap.collapse',
        'ui.bootstrap.buttons',
        'ui.bootstrap.tooltip',
        'concentrator.concentrator.navbar',
        'concentrator.model.product',
        'concentrator.controller.product',
        'common.localization',
        'common.auth',
        'ngCookies'
    ]).config(['$cookiesProvider', '$httpProvider', function($cookiesProvider, $httpProvider) {}])
    .run(runInit)
    .controller('coreCtrl', ['$scope', 'auth', coreCtrl])
    .directive('loginscreen', loginscreen);

/**
 * manages index.html landing
 *
 * @method     coreCtrl
 * @param      {$scope}  $scope  { description }
 * @param      {auth}  auth    @link concentrator.auth
 */
function coreCtrl($scope, auth) {
    if (auth.isAuth()) {
        $scope.loginscreen = false
    } else {
        $scope.loginscreen = false
    }
}

function loginscreen() {
    return {
        restrict: 'E',
        transclude: true,
        templateUrl: '/common/auth/loginScreen.html'
    }
}

function runInit($locale, $cookies, $log, l10n, auth) {
    l10n.init();
    l10n.getLocale();
}
