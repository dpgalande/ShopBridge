ShopBidgeApp.controller('productDetailsController', function ($scope, $http, $routeParams) {

    $scope.ProductId = $routeParams.productID;
    $scope.APIPath = "http://localhost/ShopBridge.WebAPI";


    $scope.GetProductDetailById = function () {
        var Url = $scope.APIPath + "/api/Product/GetProductDetailsById?productId=" + $routeParams.productID;
        var req = {
            method: 'POST',
            url: Url
        }
        $http(req)
            .then(function (res) {
                if (res.data != null && res.data != undefined) {
                    $scope.productDetail = res.data;
                    console.log($scope.productDetail);
                }
            },
                function (error) {
                    console.log(error);
                });
    };

    $scope.GetProductDetailById();
});