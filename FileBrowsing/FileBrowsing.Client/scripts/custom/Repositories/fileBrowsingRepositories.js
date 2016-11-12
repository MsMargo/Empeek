angular.module('myApp').service('fileBrowsingRepository', function ($http) {
    this.httpService = $http;
    var that = this;
    that._api = 'http://localhost:57768/api/';

    return {
        getDataForSelectedFolder: function (path) {
            return that.httpService.get(that._api + 'explorer?path=' + encodeURIComponent(path));
        }
    }
});