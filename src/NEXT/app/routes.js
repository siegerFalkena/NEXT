'use strict';
angular.module('concentrator')
    .run(['$rootScope', '$state', '$stateParams',
        function ($rootScope, $state, $stateParams) {
            $rootScope.$state = $state;
            $rootScope.$stateParams = $stateParams;
        }
    ])
    .config(
        ['$stateProvider', '$urlRouterProvider', '$urlMatcherFactoryProvider',
            function ($stateProvider, $urlRouterProvider,
                $urlMatcherFactoryProvider) {
                $urlRouterProvider.otherwise('/404');

                $stateProvider.state('overview', {
                    url: '/overview',
                    templateUrl: 'concentrator/views/Home/Home.html'
                })
                .state('product', {
                    url: '/product',
                    abstract: true,
                    template: "<div ui-view style=\"height:100%;\"></div>"
                })
                .state('product.overview', {
                    url: '/overview',
                    controller: 'productOverviewCtrl',
                    templateUrl: 'concentrator/views/product/productOverview.html'
                })
                .state('product.list', {
                    url: '/query',
                    controller: 'productListCtrl',
                    templateUrl: "concentrator/views/product/productList.html"
                })
                .state("product.new", {
                    url: '/new',
                    controller: 'productNewCtrl',
                    templateUrl: "concentrator/views/product/productViewLarge.html"
                })
                .state("product.detail", {
                    url: '/{id}',
                    controller: 'productDetailCtrl',
                    templateUrl: "concentrator/views/product/productViewLarge.html"
                })
                .state('404', {
                    url: '/404',
                    templateUrl: '404.html'
                });

            }
        ]
    );
