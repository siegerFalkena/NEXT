angular.module('common.alerts', ['ui.bootstrap.alert'])
    .factory('alertService', [alertService]);

function alertService() {
    var alerts = [{ msg: 'exampleAlert', type: 'warning' }];

    function addAlert(alert) {
        this.alerts.push(alert);
    }

    function removeAlert(index) {
        alerts.splice(index, 1);
    }

    function purgeAlerts() {

    }
    return {
        purge: function () {
            return this.purgeAlerts()
        },
        add: function (alert) {
            return this.addAlert(alert);
        },
        remove: function (index) {
            return removeAlert(index);
        },
        template: function () {
            return { msg: '', type: ['danger', 'success', 'warning', 'info'] }
        },
        alerts: function () {
            return alerts;
        }
    };
}