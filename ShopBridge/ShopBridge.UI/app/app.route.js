ShopBidgeApp.config(['$routeProvider',
    function config($routeProvider) {
        $routeProvider.
            when('/product', {
                templateUrl: 'app/components/product/productView.html',
                controller: 'productController'
            }).
            when('/productDetailsView/:productID',
            {
                templateUrl: 'app/components/productDetailsView/productDetailsView.html',
                controller: 'productDetailsController'                   
            }).
            otherwise('/product');
    }
]);