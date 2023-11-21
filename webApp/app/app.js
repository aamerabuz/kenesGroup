var Kenes = angular.module("Kenes", ['ngRoute']);

Kenes.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $locationProvider.hashPrefix('');
    $routeProvider
        .when('/home', {
            templateUrl: 'views/home.html'
        })
        .when('/about', {
            templateUrl: 'views/about.html'
        })
        .when('/orders', {
            templateUrl: 'views/orders.html',
            controller: 'OrdersController'
        }).otherwise({
            redirectTo: '/home'
        })
}]);



Kenes.directive('orderDetails', [function () {
    return {
        restrict: 'E',
        templateUrl: 'views/orderDetailsModal.html'
    }
}])

Kenes.controller('OrdersController', ['$scope', '$http', function ($scope, $http) {
    var apiUrl = 'http://localhost:5135/api/';
    var config = {
        headers: {
            'Content-Type': 'application/json;'
        }
    };
    $scope.orders = [];
    $scope.orderDetails = null;
    $scope.openModal = false;
    $scope.order_id = null;

    $scope.openOrderModal = function (order) {
        $scope.openModal = true;
        $scope.order_id = order;
        console.log($scope.order_id);
        $scope.loadOrderDetails();
    }
    $scope.closeOrderModal = function () {
        $scope.openModal = false;
    }

    $scope.loadOrderDetails = async function () {
        try {
            var response = await $http.get(`${apiUrl}GetOrderById/${$scope.order_id}`, config).then(function (data) {
                console.log(data)
                if (data !== null) {
                    $scope.orderDetails = data.data;
                }
                console.log($scope.orders)
            }).catch(function (error) {
                console.log(error)
            });
        }
        catch (error) {
            console.error('Error fetching data:', error);
        }
    }

    $scope.loadData = async function () {
        try {
            var response = await $http.get(`${apiUrl}GetOrders`, config).then(function (data) {
                console.log(data)
                if (data !== null) {
                    $scope.orders = data.data;
                }
                console.log($scope.orders)
            }).catch(function (error) {
                console.log(error)
            });
        }
        catch (error) {
            console.error('Error fetching data:', error);
        }
    }

    $scope.loadData();
}])