angular.module("umbraco").controller("Community.ChecklistController", function ($http, $scope, $route, editorService) {
    const vm = this;

    const API_URL = '/umbraco/backoffice/Community/Checklist/';

    vm.getHeaderInfo = function () {
        $http.get(API_URL + 'GetHeaderInfo/')
            .then(function (response) {
                vm.headerInfo = response.data;
            });
    };


    vm.init = function () {
        this.statusReport();
        this.getAll();
    };

    vm.getAll = function () {
        $http.get(API_URL + 'GetAll/')
            .then(function (response) {
                vm.categories = response.data;
            });
    };

    vm.statusReport = function () {
        $http.get(API_URL + 'StatusReport/')
            .then(function (response) {
                vm.statusReport = response.data;
            });
    };


    vm.approve = function (entry) {
        $http.post(API_URL + 'Save/', JSON.stringify(entry))
            .then(function (response) {
                if (response.data) {
                    $scope.msg = 'Post Data Submitted Successfully!';
                    //$route.reload();
                    vm.getAll();
                    $http.get(API_URL + 'StatusReport/')
                        .then(function (response) {
                            vm.statusReport = response.data;
                        });
                }
            }, function (response) {
                $scope.msg = 'Service not Exists';
                $scope.statusval = response.status;
                $scope.statustext = response.statusText;
            });
    };

    vm.refresh = function () {
        $http.post(API_URL + 'refresh/')
            .then(function (response) {
                if (response.data) {
                    $scope.msg = 'Refresh Successfully!';
                    //$route.reload();
                    vm.getAll();
                    $http.get(API_URL + 'StatusReport/')
                        .then(function (response) {
                            vm.statusReport = response.data;
                        });
                }
            }, function (response) {
                $scope.msg = 'Service not Exists';
                $scope.statusval = response.status;
                $scope.statustext = response.statusText;
            });
    };

    vm.init();


});

