angular.module('concentrator.concentrator.navbar', [
        'ngAnimate',
        'ui.bootstrap',
        'concentrator.component.searchFilter',
        'concentrator.component.navbar',
        'common.localization',
        'common.auth'
    ])
    .controller('navbarCtrl', ['$scope', '$log', 'l10n', 'auth', navbarCtrl]);



function navbarCtrl($scope, $log, l10n, auth) {

    $scope.brand = {
        name: 'Jumbo',
        url: '/',
        imgSrc: 'assets/img/circle_gradient.png'
    };

    $scope.diract = {
        name: 'Concentrator',
        url: '/',
        imgSrc: 'assets/img/diract_logo.png'
    };

    $scope.l10n = l10n;
    $scope.locale = l10n.getLocale();    
    $scope.auth = auth;

};
