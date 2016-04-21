'use strict';
angular.module('concentrator.component.searchFilter')
    .directive('searchfilter', navSearchFilter)
    .directive('navsearchfilter', navSearchFilter)
    .service('searchFilterConfig', config);

function filter() {
    return {
        restrict: 'E',
        transclude: true,
        scope: {
            searchcategories: '=',
            searchaction: '='
        },
        templateUrl: '/common/components/searchFilter/partials/navSearchFilter.html'
    }
}

function navSearchFilter() {
    return {
        restrict: 'E',
        transclude: true,
        scope: {
            searchcategories: '=',
            searchaction: '='
        },
        templateUrl: '/common/components/searchFilter/partials/navSearchFilter.html'
    }
}


function config() {
    var filterConfig = function filterConfigF() {
        return {
        query: '',
        querycategory: {name: '*', shown: true},
        search: {},
        filterchange: function(searchaction) {
            this.debug()
            var object = angular.fromJson(searchaction.querycategory);
            searchaction.search = {};
            if (object.name == '*') {
                searchaction.search = searchaction.query;
            } else {
                searchaction.search[object.name] = searchaction.query;
            }
        },
        optionany: true,
        actionLabel: 'Add filter',
        go: function(searchaction) {
            searchaction.debug();
        },
        searchmetacategories: [{
            name: '*',
            shown: true
        }],
        searchcategories: [{
            name: 'ID',
            shown: true
        }, {
            name: 'name',
            shown: true
        }, {
            name: 'price',
            shown: true
        }],
        debug: function() {
            console.log(this);
        },
        sortType: 'ID',
        sortReverse: false

    };
    };
}
