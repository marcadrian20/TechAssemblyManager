using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechAssemblyManager.Models;
using FirebaseWrapper;

namespace TechAssemblyManager.BLL
{
    public class ProductManagerBLL
    {
        private IFirebaseHelper _firebaseHelper;

        public ProductManagerBLL(IFirebaseHelper firebaseHelper)
        {
            _firebaseHelper = firebaseHelper;
        }

        public async Task<List<Product>> GetAllActiveProductsAsync()
        {
            return await _firebaseHelper.GetAllActiveProductsAsync();
        }
        public async Task<bool> AddProductAsync(Product product, User currentUser)
        {
            if (product == null ||  ///Validation whether there's a valid category
                currentUser == null ||                          //Or whether there is ab existing user 
                currentUser.userType != "employee"
                || !currentUser.employeeData.isSenior              //being an employee
                )
            { return false; }
            // Basic field validation
            if (string.IsNullOrWhiteSpace(product.productId) ||
                string.IsNullOrWhiteSpace(product.name) ||
                string.IsNullOrWhiteSpace(product.description))
            {
                return false;
            }

            // Price validation
            if (product.price <= 0)
                return false;

            // Rating validation (0-5 range)
            if (product.rating < 0 || product.rating > 5)
                return false;

            // Description word count validation (<=100 words)
            var wordCount = product.description.Split(new char[] { ' ', '\t', '\n', '\r' },
                StringSplitOptions.RemoveEmptyEntries).Length;
            if (wordCount > 100)
                return false;

            // Check if category exists
            var category = await GetProductCategoryByIdAsync(product.categoryId);
            if (category == null)
                return false;

            // Check if product ID already exists
            var existingProduct = await GetProductByIdAsync(product.productId);
            if (existingProduct != null)
                return false;

            //DAL call
            return await _firebaseHelper.AddProductAsync(product);

        }

        public async Task<List<Product>> GetProductsByCategoryAsync(string categoryId)
        {
            var allProducts = await _firebaseHelper.GetAllActiveProductsAsync();
            return allProducts.FindAll(p => p.categoryId == categoryId);
        }

        public async Task<bool> AddProductCategoryAsync(ProductCategory productCategory, User currentUser)
        {
            if (currentUser == null
            || currentUser.userType != "employee"
                || !currentUser.employeeData.isSenior)
            { return false; }

            // Category validation
            if (productCategory == null)
                return false;

            // Basic field validation
            if (string.IsNullOrWhiteSpace(productCategory.categoryId) ||
                string.IsNullOrWhiteSpace(productCategory.name) ||
                string.IsNullOrWhiteSpace(productCategory.type))
            {
                return false;
            }

            // Type validation (should be "system" or "component")
            if (productCategory.type != "system" && productCategory.type != "component")
                return false;

            // Description validation (optional field, but if provided should be reasonable length)
            if (!string.IsNullOrWhiteSpace(productCategory.description))
            {
                var wordCount = productCategory.description.Split(new char[] { ' ', '\t', '\n', '\r' },
                    StringSplitOptions.RemoveEmptyEntries).Length;
                if (wordCount > 100)
                    return false;
            }

            // Check if category ID already exists
            var existingCategory = await GetProductCategoryByIdAsync(productCategory.categoryId);
            if (existingCategory != null)
                return false;

            return await _firebaseHelper.AddProductCategoryAsync(productCategory);
        }

        public async Task<List<Product>> GetProductsOrderedBy(/*string categoryId, */string selectedFilter)
        {
            var products = await _firebaseHelper.GetAllActiveProductsAsync();//GetProductsByCategoryAsync(/*categoryId*/);
            switch (selectedFilter)
            {
                case "Category [A -> Z]":
                    return products.OrderBy(p => p.categoryId).ToList();
                case "Category [Z -> A]":
                    return products.OrderByDescending(p => p.categoryId).ToList();
                case "Price [Low -> High]":
                    return products.OrderBy(p => p.price).ToList();
                case "Price [High -> Low]":
                    return products.OrderByDescending(p => p.price).ToList();
                case "Name [A -> Z]":
                    return products.OrderBy(p => p.name).ToList();
                case "Name [Z -> A]":
                    return products.OrderByDescending(p => p.name).ToList();
                default:
                    return products;
            }
        }
        public async Task<ProductCategory?> GetProductCategoryByIdAsync(string categoryId)
        {
            if (string.IsNullOrWhiteSpace(categoryId))
                return null;

            var categories = await GetProductCategoriesAsync();
            return categories.FirstOrDefault(c => c.categoryId == categoryId);
        }
        public async Task<List<ProductCategory>> GetProductCategoriesAsync()
        {
            var categoryDict = await _firebaseHelper.GetAsync<Dictionary<string, ProductCategory>>("ProductCategories");
            return categoryDict?.Values.ToList() ?? new List<ProductCategory>();
        }

        public async Task<bool> UpdateProductAsync(Product product, User currentUser)
        {
            if (currentUser == null || currentUser.userType != "employee" || !currentUser.employeeData.isSenior)
                return false;
            if (product == null || string.IsNullOrWhiteSpace(product.productId))
                return false;

            // Basic field validation
            if (string.IsNullOrWhiteSpace(product.name) ||
                string.IsNullOrWhiteSpace(product.description))
                return false;

            // Price validation
            if (product.price <= 0)
                return false;

            // Rating validation (0-5 range)
            if (product.rating < 0 || product.rating > 5)
                return false;

            // Description word count validation (<=100 words)
            var wordCount = product.description.Split(new char[] { ' ', '\t', '\n', '\r' },
                StringSplitOptions.RemoveEmptyEntries).Length;
            if (wordCount > 100)
                return false;
            // Check if category exists (if categoryId is being updated)
            if (!string.IsNullOrWhiteSpace(product.categoryId))
            {
                var category = await GetProductCategoryByIdAsync(product.categoryId);
                if (category == null)
                    return false;
            }

            // Check if product exists before updating
            var existingProduct = await GetProductByIdAsync(product.productId);
            if (existingProduct == null)
                return false;
            await _firebaseHelper.UpdateAsync($"Products/{product.productId}", product);
            return true;
        }

        public async Task<bool> DeleteProductAsync(string productId, User currentUser)
        {
            if (currentUser == null || currentUser.userType != "employee" || !currentUser.employeeData.isSenior)
                return false;
            if (string.IsNullOrWhiteSpace(productId))
                return false;

            await _firebaseHelper.DeleteAsync($"Products/{productId}");
            return true;
        }

        public async Task<Product?> GetProductByIdAsync(string productId)
        {
            if (string.IsNullOrWhiteSpace(productId))
                return null;
            return await _firebaseHelper.GetAsync<Product>($"Products/{productId}");
        }

        public async Task<List<ProductCategory>> GetCategoriesByTypeAsync(string type)
        {
            return await _firebaseHelper.GetCategoriesByTypeAsync(type);
        }
    }
}