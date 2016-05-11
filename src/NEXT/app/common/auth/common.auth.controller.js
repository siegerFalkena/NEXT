angular.module('common.auth').controller('loginCtrl', ['$scope', '$cookies', '$log', '$window', 'auth', 'l10n', loginCtrl]);

/**
 * controller for login screen
 *
 * @method     loginCtrl
 * @param      {$scope}  $scope    loginScreen $scope
 * @param      {$cookies}  $cookies  angularJS cookie store
 * @param      {$log}  $log      angular log service
 * @param      {$window}  $window   window angular window object 
 * @param      {concentrator.auth}  auth      auth service authentication service
 * @param      {concentrator.l10n}  l10n        concentrator localization service
 */
function loginCtrl($scope, $cookies, $log, $window, auth, l10n) {

    $scope.client = {
        name: 'Jumbo',
        url: '/',
        imgSrc: 'assets/img/circle_gradient.png'
    };

    $scope.brand = {
        name: 'Concentrator',
        url: '/',
        imgSrc: 'assets/img/diract_logo.png'
    };

    $scope.scope = function() {
        $log.info($scope);
    }

    $scope.login = function() {
        function cb_result(b_success) {
            var herp = 'derp';
            $log.info(b_success);
            //$window.location.href = "/"
        };
        var temp = auth.auth($scope.username, $scope.password, $scope.rememberMe, cb_result);
    };

    $scope.messages = []

    $scope.l10n = l10n;
    $scope.locale = l10n.getLocale();
    
}