'use strict';
angular.module('concentrator.component.messagelist', ['ui.bootstrap']).directive(
    'messagelist', messagelist);

function messagelist() {
    return {
        restrict: 'E',
        transclude: true,
        scope: {
            messages: '='
        },
        templateUrl: '/shared/components/messageList/partials/messageList.html'
    }
}
