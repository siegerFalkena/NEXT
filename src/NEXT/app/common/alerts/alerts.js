    'use strict';

    angular
        .module('app', ['ui.bootstrap.alerts'])
        .component('alerts', {
            templateUrl: 'common/alerts/alerts.html',
            controller: ['ui', ],
            bindings: {
                hero: '='
            }
        });

