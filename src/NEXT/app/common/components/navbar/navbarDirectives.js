angular.module('concentrator.component.navbar')
    .directive('dropdown', navbarDropdown)
    .directive('navbar', navbar)
    .directive('navbarTabs', navbarTabs)
    .service('navbarConfig', config);

function navbarDropdown() {
    return {
        restrict: 'E',
        scope: {
            dropdown: '='
        },
        templateUrl: '/common/components/navbar/partials/navDropdown.html'
    };
};

function brand() {
    return {
        restrict: 'E',
        scope: {
            brand: '='
        },
        templateUrl: '/common/components/navbar/partials/navBrand.html'
    };
};

function navbar() {
    return {
        templateUrl: '/common/components/navbar/partials/navbar.html'
    };
};

function navbarTabs() {
    return {
        templateUrl: '/common/components/navbar/partials/navbarTabs.html'
    };
};



function config() {

    this.brand = function() {
        return {
            label: 'Jumbo',
            url: '/',
            imgSrc: 'assets/img/jumbo.png'
        }
    };


    this.items = function() {
        return [{
            name: 'name',
            url: '#/url'
        }]
    };

    this.admin = function() {
        return {
            title: 'Admin',
            links: [{
                name: 'users',
                url: '#/users'
            }]
        }
    };
}
