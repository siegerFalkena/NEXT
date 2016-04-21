'use strict';

angular.module('concentrator.shared.component.filter', [

	] )
.service('filter', ['$http', '$cookies', '$log', ]);

function filterFactory($http, $cookies, $log){
	this.getFilter = getFilterF;
	/** Functions of KV pairs
	*/
	function getFilterF(){
		
	}
}