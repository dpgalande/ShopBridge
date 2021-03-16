ShopBidgeApp.controller('productController', function ($scope, $http, $location, $window) {
   
    $scope.product = {};
    $scope.productDetails = {};
    $scope.APIPath = "http://localhost/ShopBridge.WebAPI";
    

    $scope.GetProductDetails = function () {
        var Url = $scope.APIPath+"/api/Product/GetProductDetails";
        var req = {
            method: 'GET',
            url: Url
        }
        $http(req)
            .then(function (res) {
                if (res.data != null && res.data != undefined)
                {
                    $scope.productDetails = res.data;
                    console.log($scope.productDetails);
                }
            },
                function (error) {
                    console.log(error);
                });
    };

    $scope.AddProduct = function (product) {

             var fd = new FormData();
             fd.append('files', product.uploadedfile);
             fd.append('ProductName', product.ProductName);
             fd.append('ProductDescription', product.ProductDescription);
             fd.append('ProductPrice', product.ProductPrice);
             var Url = $scope.APIPath+"/api/Product/AddUpdateProduct"

            if (product.Id == undefined) {
                var fd = new FormData();
                fd.append('files', product.uploadedfile);
                fd.append('ProductName', product.ProductName);
                fd.append('ProductDescription', product.ProductDescription);
                fd.append('ProductPrice', product.ProductPrice);
            }
            else { fd.append('Id', product.Id); }

            var req = {
                method: 'POST',
                url: Url,
                data: fd,
                headers: { 'Content-Type': undefined }
            }

            $http(req)
                .then(function (res) {
                    console.log(res);
                    $scope.Cancel();
                    $scope.GetProductDetails();

                    var toastEvent = $A.get("e.force:showToast");
                    toastEvent.setParams({
                        "title": "Success!",
                        "type": 'success',
                        "mode": 'dismissible',
                        "duration": 5000,
                        "message": "The record has been created successfully."
                    });
                    toastEvent.fire();
                },
                function (error) {
                    console.log(error);
                });
    };

    $scope.Cancel = function ()
    {
        $("#btnAdd").prop("value", "Add");
        $scope.product.Id = undefined;
        $scope.product.ProductName = "";
        $scope.product.ProductDescription = "";
        $scope.product.ProductPrice = "";     
        $scope.product.uploadedfile = "";
        $scope.productform.imgUploadThumb = undefined;
        $scope.productform.$setPristine();
    };

    $scope.EditProduct = function (product)
    {
       

        $("#btnAdd").prop("value", "Update");

        $scope.product.Id = product.Id;
        $scope.product.ProductName = product.ProductName;
        $scope.product.ProductDescription = product.ProductDescription;
        $scope.product.ProductPrice = product.ProductPrice; 
        $scope.product.uploadedfile =  $scope.APIPath + product.FolderPath + product.ImageName;
        $scope.productform.imgUploadThumb = $scope.APIPath + product.FolderPath + product.ImageName;
    };
    
    $scope.DeleteProduct = function (product) {

        var Url = $scope.APIPath+"/api/Product/DeleteProduct";
        var productobj = { "Id": product.Id };
        var req = {
            method: 'POST',
            url: Url,
            data: productobj
        }
        $http(req)
            .then(function (res) {
               
                $scope.Cancel();
                $scope.GetProductDetails();
                
            },
            function (error) {
                console.log(error);
                });

    }; 

    $scope.ViewProductDetails = function (productId)
    {
        console.log(productId);
        var baseurl = $location.$$absUrl;
        if (baseurl)
            var splittedurl = baseurl.split('#');
        localStorage.setItem("productId", productId);
        $window.open(splittedurl[0] + '#!/productDetailsView', '_blank');
    };

    $scope.imageUpload = function (element) {
        var reader = new FileReader();
        reader.onload = $scope.imageIsLoaded;
        reader.readAsDataURL(element.files[0]);
    };

    $scope.imageIsLoaded = function (e) {
        $scope.$apply(function () {
            $scope.productform.imgUploadThumb = e.target.result;
        });
    };
    
    $scope.GetProductDetails();
});

//$scope.AddProduct = function (product) {

//    if (product.Id == undefined) {
//        var fd = new FormData();
//        fd.append('files', product.uploadedfile);
//        fd.append('ProductName', product.ProductName);
//        fd.append('ProductDescription', product.ProductDescription);
//        fd.append('ProductPrice', product.ProductPrice);
//        //var productobj = { "ProductName": product.ProductName, "ProductDescription": product.ProductDescription, "ProductPrice": product.ProductPrice };
//        var Url = "http://localhost/ShopBridge.WebAPI/api/Product/AddProduct"

//    }
//    else {
//        var productobj = { "Id": product.Id, "ProductName": product.ProductName, "ProductDescription": product.ProductDescription, "ProductPrice": product.ProductPrice };
//        var Url = "http://localhost/ShopBridge.WebAPI/api/Product/UpdateProduct"
//    }

//    var req = {
//        method: 'POST',
//        url: Url,
//        data: fd,
//        headers: { 'Content-Type': undefined }
//    }

//    $http(req)
//        .then(function (res) {
//            console.log(res);
//            $scope.Cancel();
//            $scope.GetProductDetails();
//        },
//            function (error) {
//                console.log(error);
//            });
//};