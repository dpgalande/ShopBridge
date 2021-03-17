using ShopBridge.Business;
using ShopBridge.Dto;
using ShopBridge.Entities;
using ShopBridge.Infrastructure;
using ShopBridge.WebAPI.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;

namespace ShopBridge.WebAPI.Controllers
{
    public class ProductController : ApiController
    {      
        private readonly BusinessProduct busines = new BusinessProduct();

        [HttpGet]
        public async Task<IEnumerable<ProductDetails>> GetProductDetails()
        {
            return await Task.FromResult(busines.GetAllProduct());
        }

        [HttpPost]
        public  bool AddProduct(ProductDetailDto request)
        {
            var productDetail = new ProductDetails
            {
                ProductName = request.ProductName,
                ProductDescription = request.ProductDescription,
                ProductPrice = request.ProductPrice,
                CreatedDate = DateTime.Now
            };
            return busines.AddProduct(productDetail);
        }

        [HttpPost]
        public async Task<bool> AddUpdateProduct(HttpRequestMessage request)
        {
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var data = await Request.Content.ParseMultipartAsync();
            if (data != null && data.Files.ContainsKey("Files"))
            {
                var queryParams = Request.GetQueryNameValuePairs().ToDictionary(kv => kv.Key, kv => kv.Value, StringComparer.OrdinalIgnoreCase);
                byte[] file = ResizeImage(data.Files["files"].File,500,500);// data.Files["file"].File;
                var extension = Path.GetExtension(data.Files["files"].Filename);
                var fileName = Regex.Replace(data.Files["files"].Filename, ".png", "", RegexOptions.IgnoreCase);

                var savePath = $"{AppDomain.CurrentDomain.BaseDirectory}/assets/images/";
                var displayPath = $"/assets/images/";
                string filePath = SaveFile(fileName, file, extension, savePath);
                

                if (!string.IsNullOrWhiteSpace(filePath))
                {
                    bool resp ;
                    if (data.Fields.ContainsKey("Id"))
                    {

                        resp = busines.UpdateProduct(new ProductDetails
                        {
                            Id  = Convert.ToInt32(data.Fields["Id"].Value),
                            ProductName = data.Fields["ProductName"].Value,
                            ProductDescription = data.Fields["ProductDescription"].Value,
                            ProductPrice = Convert.ToInt32(data.Fields["ProductPrice"].Value),
                            ImageName = fileName+ extension,
                            FolderPath = displayPath,
                            UpdatedDate = DateTime.Now,
                        });

                    }
                    else
                    {
                        resp = busines.AddProduct(new ProductDetails
                        {
                            ProductName = data.Fields["ProductName"].Value,//data.Fields.ContainsKey("Id"),
                            ProductDescription = data.Fields["ProductDescription"].Value,
                            ProductPrice = Convert.ToInt32( data.Fields["ProductPrice"].Value),
                            ImageName = fileName + extension,
                            FolderPath = displayPath,
                            CreatedDate = DateTime.Now,
                        });
                    }

                    if (resp)
                    {
                        return await Task.FromResult(resp);
                    }
                    else
                        throw new BusinessException("Product", "Product", "NotSaved");
                }
                else
                    throw new BusinessException("Product", "Product", "NotSavedToDb");
            }
            else
                throw new BusinessException("Product", "Product", "NpFileToUpload");
            
        }

        [HttpPost]
        public bool UpdateProduct(ProductDetailDto request)
        {
            var productDetail = new ProductDetails
            {
                Id = request.Id,
                ProductName = request.ProductName,
                ProductDescription = request.ProductDescription,
                ProductPrice = request.ProductPrice,
                UpdatedDate = DateTime.Now,

            };
            return busines.UpdateProduct(productDetail);
        }

        [HttpPost]
        public bool DeleteProduct(ProductDetailDto request)
        {         
            return busines.DeleteProductById(request.Id);
        }
        [HttpPost]
        public ProductDetails GetProductDetailsById(int productId)
        {
            return busines.GetProductDetailsById(productId);
        }
        #region Reguired Methods For Resize Large File And Save File
        private static byte[] ResizeImage(byte[] file, int width, int height)
        {
            using (var srcImage = Image.FromStream(new MemoryStream(file)))
            {

                int originalWidth = srcImage.Width;
                int originalHeight = srcImage.Height;

                // To preserve the aspect ratio
                float ratioX = (float)width / (float)originalWidth;
                float ratioY = (float)height / (float)originalHeight;
                float ratio = Math.Min(ratioX, ratioY);

                float sourceRatio = (float)originalWidth / originalHeight;

                // New width and height based on aspect ratio
                int newWidth = (int)(originalWidth * ratio);
                int newHeight = (int)(originalHeight * ratio);

                using (var newImage = new Bitmap(newWidth, newHeight))
                using (var graphics = Graphics.FromImage(newImage))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.DrawImage(srcImage, new Rectangle(0, 0, newWidth, newHeight));
                    using (var stream = new MemoryStream())
                    {
                        newImage.Save(stream, ImageFormat.Png);
                        return stream.ToArray();
                    }
                }
            }
        }

        public static string SaveFile(string filename, byte[] file, string extension, string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fname = path + "\\" + filename + extension;
            File.WriteAllBytes(fname, file);

            return fname;
        }

        #endregion

    }
}