angular.module('concentrator.controller.product')
    .controller('productOverviewCtrl', ['$scope', 'l10n', productOverviewCtrl]);


function productOverviewCtrl($scope, l10n) {
    $scope.l10n = l10n;
}