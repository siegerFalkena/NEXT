'use strict';
angular.module('concentrator')
    .run(['$rootScope', '$state', '$stateParams',
        function($rootScope, $state, $stateParams) {

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
            function($stateProvider, $urlRouterProvider,
                $urlMatcherFactoryProvider) {

                $urlMatcherFactoryProvider.type('contact', ['contacts',
                    function(contacts) {
                        return {
                            encode: function(contact) {
                                return contact.id;
                            },
                            decode: function(id) {
                                return contacts.get(id);
                            },
                            pattern: /[0-9]{1,4}/
                        };
                    }
                ]);

                /////////////////////////////
                // Redirects and Otherwise //
                /////////////////////////////

                // Use $urlRouterProvider to configure any redirects (when) and invalid urls (otherwise).
                $urlRouterProvider

                // The `when` method says if the url is ever the 1st param, then redirect to the 2nd param
                // Here we are just setting up some convenience urls.
                // .when('/c?id', '/contacts/:id')
                // .when('/user/:id', '/contacts/:id')

                // If the url is ever invalid, e.g. '/asdf', then redirect to '/' aka the home state
                    .otherwise('/');


                //////////////////////////
                // State Configurations //
                //////////////////////////

                // Use $stateProvider to configure your states.
                $stateProvider

                //////////
                // Home //
                //////////

                    .state("product", {
                        url: '/product',
                        templateUrl: 'concentrator/views/product/productView.html'
                    });

            }
        ]
    );
