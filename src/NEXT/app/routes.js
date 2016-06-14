'use strict';
angular.module('concentrator')
    .run(['$rootScope', '$state', '$stateParams', '$log',
        function ($rootScope, $state, $stateParams, $log) {
            $rootScope.$state = $state;
            $rootScope.$stateParams = $stateParams;
        }
    ])
    .config(
        ['$stateProvider', '$urlRouterProvider', '$urlMatcherFactoryProvider',
            function ($stateProvider, $urlRouterProvider,
                $urlMatcherFactoryProvider) {
                $urlRouterProvider.otherwise('/app/404');

                $stateProvider.state('overview', {
                    url: '/overview',
                    templateUrl: 'concentrator/views/Home/Home.html'
                })
                .state('app', {
                    url: '/app',
                    abstract: true,
                    template: '<navbar></navbar> <div ui-view style=\"height:100%; padding-top: 50px\"></div>'
                })
                .state('app.product', {
                    url: '/product',
                    abstract: true,
                    template: "<div ui-view style=\"height:100%;\"></div>"
                })
                .state('app.product.overview', {
                    url: '/overview',
                    controller: 'productOverviewCtrl',
                    templateUrl: 'concentrator/views/product/productOverview.html'
                })
                .state('app.product.list', {
                    url: '/query',
                    controller: 'productListCtrl',
                    templateUrl: "concentrator/views/product/productList.html"
                })
                .state("app.product.new", {
                    url: '/new',
                    controller: 'productNewCtrl',
                    templateUrl: "concentrator/views/product/productViewLarge.html"
                })
                .state("app.product.detail", {
                    url: '/{id}',
                    controller: 'productDetailCtrl',
                    templateUrl: "concentrator/views/product/productSidebar.html"
                })
                .state("app.product.detail.children", {
                    url: '/children',
                    controller: 'productChildCtrl',
                    templateUrl: "concentrator/views/product/productChildren.html"
                })
                .state('app.404', {
                    url: '/404',
                    templateUrl: '404.html'
                })
                .state('login', {
                    url: '/',
                    templateUrl: "common/auth/loginScreen.html"
                });

            }
        ]
    );
