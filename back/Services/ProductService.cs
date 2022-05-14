using System;
using System.Collections.Generic;
using grud_backend.Helpers;
using grud_backend.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace grud_backend.Services
{

    public interface IProductService
    {

        IEnumerable<Product> GetAll();
        
        Product Get(string id);

        List<Product> Get();

        Product GetById(string id);

        Product Create(Product producto);

        void Update(string id, Product productIn);

        void Delete(string id);

        List<Product> getByProvider(string id);

    }

    public class ProductService : IProductService
    {

        private readonly IMongoCollection<Product> _mongoProducts;

        private IMongoQueryable<Product> _products;

        public ProductService(IAppSettings appSettings)
        {
            var client = new MongoClient(appSettings.ConnectionString);
            var database = client.GetDatabase(appSettings.DatabaseName);
            
            _mongoProducts = database.GetCollection<Product>(appSettings.ProductsCollectionName);
            _products = database.GetCollection<Product>(appSettings.ProductsCollectionName).AsQueryable();
        }

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public Product GetById(string id)
        {
            return _mongoProducts.Find(product => product.Id.ToLower().Contains(id.ToLower())).FirstOrDefault();
        }

        public Product Get(string id)
        {
            return  _mongoProducts.Find(product => product.Id == id).FirstOrDefault();
        }

        public List<Product> Get() => _mongoProducts.Find(product => true).ToList();

        public Product Create(Product producto)
        {
            producto.Id = Guid.NewGuid().ToString("D");
            _mongoProducts.InsertOne(producto);
            return producto;
        }

        public void Update(string id, Product productIn)
        {
            _mongoProducts.ReplaceOne(producto => producto.Id == id, productIn);
        }

        public void Delete(string id)
        {
            _mongoProducts.DeleteOne((producto => producto.Id == id));
        }

        public List<Product> getByProvider(string id)
        {
            return _mongoProducts.Find(product => product.IdProveedor == id).ToList();
        }

    }

}