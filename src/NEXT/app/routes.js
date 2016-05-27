'use strict';
angular.module('concentrator')
    .run(['$rootScope', '$state', '$stateParams',
        function ($rootScope, $state, $stateParams) {

            // It's very handy to add references to $state and $stateParams to the $rootScope
            // so that you can access them from any scope within your applications.For example,
            // <li ng-class="{ active: $state.includes('contacts.list') }"> will set the <li>
            // to active whenever 'contacts.list' or one of its decendents is active.
            $rootScope.$state = $state;
            $rootScope.$stateParams = $stateParams;
        }
    ])
    .config(
        ['$stateProvider', '$urlRouterProvider', '$urlMatcherFactoryProvider',
            function ($stateProvider, $urlRouterProvider,
                $urlMatcherFactoryProvider) {
                $urlRouterProvider

                // The `when` method says if the url is ever the 1st param, then redirect to the 2nd param
                // Here we are just setting up some convenience urls.
                // .when('/c?id', '/contacts/:id')
                // .when('/user/:id', '/contacts/:id')

                // If the url is ever invalid, e.g. '/asdf', then redirect to '/' aka the home state
                    .otherwise('/404');


                //////////////////////////
                // State Configurations //
                //////////////////////////

                // Use $stateProvider to configure your states.

                //////////
                // Home //
                //////////

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
                        templateUrl: 'concentrator/views/product/productOverview.html'
                    })
                .state('product.list', {
                    url: '/query',
                    templateUrl: "concentrator/views/product/productList.html"
                }).state("product.new", {
                    url: '/new',
                    params: { isNew: true },
                    templateUrl: "concentrator/views/product/productViewLarge.html"
                })
                .state("product.detail", {
                    url: '/{id}',
                    templateUrl: "concentrator/views/product/productViewLarge.html"
                })
                .state('404', {
                    url: '/404',
                    templateUrl: '404.html'
                });

            }
        ]
    );
