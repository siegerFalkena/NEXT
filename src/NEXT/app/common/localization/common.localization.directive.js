'use strict'
angular.module('common.localization')
    .directive('flagSelector', flagSelector);

function flagSelector() {
    return {
        restrict: 'E',
        scope: {
            lln: '=',
        },
        templateUrl: '/common/localization/partials/localizationSelector.html',
        probablyUseless: "commons"
    }
}
