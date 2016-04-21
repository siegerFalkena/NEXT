'use strict';
angular.module('concentrator.model.product').directive('productlist',
    productlist);

function productlist() {
    return {
        restrict: 'E',
        transclude: true,
        scope: {
            searchaction: '=',
            itemlist: '=',
            messages: '=',
            sortaction: '='
        },
        templateUrl: '/concentrator/views/product/productList.html'
    }
}
