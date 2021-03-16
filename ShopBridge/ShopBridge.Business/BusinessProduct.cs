using ShopBridge.Entities;
using ShopBridge.Infrastructure;
using System;
using System.Collections.Generic;

namespace ShopBridge.Business
{
    public class BusinessProduct
    {

        private readonly IRepository<ProductDetails> reposatory = new RepositoryBase<ProductDetails>();

        public IEnumerable<ProductDetails> GetAllProduct()
        {
            return reposatory.GetMany(c=>c.IsDeleted != true);   
        }

        public int AddProduct(ProductDetails product)
        {
            if (product == null) return 0;
            reposatory.Insert(product);
            reposatory.Commit();
            return product.Id;
        }
        public int UpdateProduct(ProductDetails product)
        {
            if (product == null) return 0;
            var productDetail = reposatory.GetById(product.Id);
            productDetail.ProductName = product.ProductName;
            productDetail.ProductDescription = product.ProductDescription;
            productDetail.ProductPrice = product.ProductPrice;
            productDetail.ImageName = product.ImageName;
            productDetail.FolderPath = product.FolderPath;
            productDetail.UpdatedDate = product.UpdatedDate;
            reposatory.Update(productDetail);
            reposatory.Commit();
            return product.Id;
        }

        public bool DeleteProductById(int productId)
        {
            var productDetail = reposatory.Get(c => c.Id == productId);
            productDetail.UpdatedDate = DateTime.Now;
            productDetail.IsDeleted = true;
            reposatory.Update(productDetail);
            reposatory.Commit();
            return true;
        }
        public ProductDetails GetProductDetailsById(int productId)
        {
            return reposatory.GetById(productId);
        }

    }
}
