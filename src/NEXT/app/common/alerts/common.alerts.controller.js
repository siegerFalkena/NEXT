angular.module('common.alerts', ['ui.bootstrap.alert'])
    .factory('alertService', [alertService]);

function alertService() {
    var alerts = [];

    function addAlert(alert) {
        alerts.push(alert);
    }

    function removeAlert(index) {
        alerts.splice(index, 1);
    }

    function purgeAlerts() {
        alerts = [];
    }
    return {
        purge: function () {
            return purgeAlerts()
        },
        add: function (alert) {
            addAlert(alert);
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