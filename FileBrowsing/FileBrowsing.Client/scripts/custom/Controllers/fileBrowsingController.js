angular.module('myApp').controller('fileBrowsingController', ['$scope', 'fileBrowsingRepository',
    function ($scope, fileBrowsingRepository) {
        var that = this
        this.scope = $scope;
        this.fileBrowsingRepository = fileBrowsingRepository;
        that.scope.path = '';
        that.scope.isRoot = true;
        that.scope.isProcessing = false;
        that.scope.preloader = true;
        

        this.scope.initialize = function () {
            getDataForSelectedFolder(that.scope.path);
        };

        this.scope.initilizeFilesAndDirectoryForPath = function (directoryName) {
            var currentPath = that.scope.currentCatalog.pathModel.currentPath;

            path = currentPath == null ? directoryName : currentPath + '\\' + directoryName;

            getDataForSelectedFolder(path);
        };

        this.scope.levelUp = function () {
            var path = that.scope.currentCatalog.pathModel.levelUpPath
            getDataForSelectedFolder(path);
        }

        function getDataForSelectedFolder(path) {

            if (that.scope.isProcessing)
                return;

            that.scope.isProcessing = true;

            that.fileBrowsingRepository.getDataForSelectedFolder(path).success(function (data) {
                if (data != null) {
                    that.scope.currentCatalog = JSON.parse(data);
                    that.scope.isRoot = that.scope.currentCatalog.pathModel.isRoot;
                    var currentPath = that.scope.currentCatalog.pathModel.currentPath;

                    if (currentPath != null)
                        that.scope.currentCatalog.pathModel.currentPath = currentPath.replace('\\\\', '\\');
                }

                that.scope.isProcessing = false;
            });
        }
    }]);