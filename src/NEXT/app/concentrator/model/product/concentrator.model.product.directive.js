'use strict';

function productlist() {
    return {
        restrict: 'E',
        transclude: true,
        scope: {
            searchaction: '=',
            itemlist: '=',
            messages: '=',
            sortaction: '=',
            snortaction: 'changed'
        },
        templateUrl: '/concentrator/views/product/productList.html'
    }
}
