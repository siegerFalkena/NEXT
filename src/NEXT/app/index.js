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
        'concentrator.model',
        'concentrator.controller.product',
        'common.localization',
        'common.auth',
        'common.alerts',
        'ngCookies',
        'angular-loading-bar'
])
    .config(function (cfpLoadingBarProvider) {
        cfpLoadingBarProvider.latencyThreshold = 0;
        cfpLoadingBarProvider.includeSpinner = false;
        cfpLoadingBarProvider.parentSelector = '#loading-bar-container';
    })
    .run(runInit)
    .controller('coreCtrl', ['$scope', 'auth', 'alertService', '$rootScope', '$log', coreCtrl])
    .directive('loginscreen', loginscreen);

/**
 * manages index.html landing
 *
 * @method     coreCtrl
 * @param      {$scope}  $scope  { description }
 * @param      {auth}  auth    @link concentrator.auth
 */
function coreCtrl($scope, auth, alertService, $rootScope, $log) {
    $log.info($rootScope.$state);
    
    $scope.alerts = alertService.alerts();
    $scope.closeAlert = function (index) { alertService.remove(index) };
    $scope.addError = function () {
        alertService.add(alertService.template());
    };
    $scope.$rootScope = $rootScope;
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
