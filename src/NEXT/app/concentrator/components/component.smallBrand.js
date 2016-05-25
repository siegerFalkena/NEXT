'use strict';
angular.module('concentrator.component.product')
.component('brandComponent', {
    templateUrl: 'concentrator/partials/brand/brandPartial.html',
    controller:
        function brandComponentController($log, $scope, $http, l10n) {
            $scope.l10n = l10n;
            $scope.title = l10n.getLocalized('LastModified');
            $scope.object = this.object;
        },
    bindings: {
        object: '='
    }
})
.component('attributeComponent', {
    templateUrl: 'concentrator/partials/product/AttributePartial.html',
    controller:
        function ($log, $scope, $http, l10n) {
            $scope.l10n = l10n;
            $scope.title = l10n.getLocalized('Attribute');
            $scope.AttributeName = l10n.getLocalized('AttributeName');
            $scope.AttributeValue = l10n.getLocalized('AttributeValue');
            $scope.Value = l10n.getLocalized('AttributeValue');
            $scope.object = this.object.obj;
        },
    bindings: {
        object: '='
    }
})
.component('externalIdComponent', {
    templateUrl: 'concentrator/partials/product/IdentifierPartial.html',
    controller:
        function ($log, $scope, $http, l10n) {
            $scope.l10n = l10n;
            $scope.title = l10n.getLocalized('ExternalIdentifier');
            $scope.object = this.object.obj.ExternalProductIdentifier;
        },
    bindings: {
        object: '='
    }
})
    .component('idComponent', {
        templateUrl: 'concentrator/partials/product/IdentifierPartial.html',
        controller:
            function ($log, $scope, $http, l10n) {
                $scope.l10n = l10n;
                $scope.title = l10n.getLocalized('LastModified');
                $scope.object = this.object.obj.productID;
            },
        bindings: {
            object: '='
        }
    })
.component('skuComponent', {
    templateUrl: 'concentrator/partials/product/SKUPartial.html',
    controller:
        function ($log, $scope, $http, l10n) {
            $scope.l10n = l10n;
            $scope.title = l10n.getLocalized('StoreKeepingUnit');
            $scope.object = this.object;
        },
    bindings: {
        object: '='
    }
})
.component('createdComponent', {
    templateUrl: 'concentrator/partials/product/datePartial.html',
    controller:
        function ($log, $scope, $http, l10n) {
            $scope.l10n = l10n;
            $scope.title = l10n.getLocalized('LastModified');
            $scope.object = this.object;
        },
    bindings: {
        object: '='
    }
})
.component('lastModifiedComponent', {
    templateUrl: 'concentrator/partials/product/datePartial.html',
    controller:
        function ($log, $scope, $http, l10n) {
            $scope.l10n = l10n;
            $scope.createdBy = l10n.getLocalized('createdBy')
            $scope.title = l10n.getLocalized('LastModified');
            $scope.object = this.object.obj;
        },
    bindings: {
        object: '='
    }
});

