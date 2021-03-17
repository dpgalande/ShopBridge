using NUnit.Framework;
using ShopBridge.Business;
using ShopBridge.Dto;
using ShopBridge.Entities;
using ShopBridge.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.UnitTest
{
    [TestFixture]
    public class ShopBridgeUnitTest
    {
        private readonly BusinessProduct busines = new BusinessProduct();
        static void Main(string[] args)
        {
        }
        [TestCase]
        public void AddProduct()
        {
            var resp = busines.AddProduct(new ProductDetails {       
                ProductName = "Test",
                ProductDescription = "TestDesc",
                ProductPrice = 100,
                CreatedDate = System.DateTime.Now,
                ImageName = "Image.png",
                FolderPath = "assets",
                IsDeleted = true
            });
            Assert.AreEqual(true, resp);
        }

        [TestCase]
        public void DeleteProduct()
        {
            var resp = busines.DeleteProductById(3);
            Assert.AreEqual(true, resp);
        }

        [TestCase]
        public void UpdateProduct()
        {
            var resp = busines.UpdateProduct(new ProductDetails
            {
                Id = 1,
                ProductName = "TestUpdate",
                ProductDescription = "TestDescUpdate",
                ProductPrice = 100,
                UpdatedDate = System.DateTime.Now,
                ImageName = "Image.png",
                FolderPath = "assets",
                IsDeleted = true
            });
        }
    }
        
}
