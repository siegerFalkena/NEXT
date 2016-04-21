'use strict';
/**
 *  @author Sieger
 *  @module common.auth
 *  @memberof angular_module
 */
angular.module('common.auth')
    .factory('auth', ['$http', '$log', '$cookies', '$window', authServiceF])
    

function authServiceF($http, $log, $cookies, $window) {
    var authService = {};
     /**
     * authenticates user to server
     *
     * @method     doAuthF
     * @param      string    username  
     * @param      string    password  
     * @param      Function  cb_result(boolean) callback function
     * @memberof   authService
     */
    authService.auth =  function doAuthF(username, password, cb_result) {
        function cb_success(response) {
            $log.info("authentication: " + response.status + "\t" + response.statusText);
            cb_result(true);
        }

        function cb_failure(response) {
            $log.info("authentication: " + response.status + "\t" + response.statusText);
            cb_result(false);
        }

        $http.defaults.headers.common.username = username;
        $http.defaults.headers.common.password = password;
        $http.defaults.headers.common.remember = true;
        var temp = $http.post('localhost:57319/api/auth', '').then(cb_success, cb_failure);
        return temp;
    };

    authService.isAuth = function isAuthedF() {
        var authToken = $cookies.get('authToken');
        var user = $cookies.get('user');
        var role = $cookies.get('role');
        if (authToken == undefined || user == undefined || role == undefined) {
            return false;
        } else {
            return true
        }
    };

    authService.logout = function logoutF() {
        $cookies.remove("user");
        $cookies.remove("role");
        $cookies.remove("authToken");
        $window.location.href = "/"
    };

    return {
        auth :function(username, password, cb_result){
            return authService.auth(username, password, cb_result);
        },
        isAuth: function(){
            return authService.isAuth()
        },
        logout: function(){
            return authService.logout()
        }
    }
}
