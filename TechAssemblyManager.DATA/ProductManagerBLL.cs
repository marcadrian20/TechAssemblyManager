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
        private FirebaseHelper _firebaseHelper;

        public ProductManagerBLL(FirebaseHelper firebaseHelper)
        {
            _firebaseHelper = firebaseHelper;
        }

        public async Task<List<Product>> GetAllActiveProductsAsync()
        {
            return await _firebaseHelper.GetAllActiveProductsAsync();
        }
        public async Task<bool> AddProductAsync(Product product, User currentUser)
        {
            if (string.IsNullOrWhiteSpace(product.productId) ||  ///Validation whether there's a valid category
                currentUser == null ||                          //Or whether there is ab existing user 
                currentUser.userType != "employee"
                || !currentUser.employeeData.isSenior              //being an employee
                )
            { return false; }
            //#TODO @Omixii add validation for Seniors
            //#TODO @Omixii validation for the description to be <=100 words??
            //#TODO @Omixii verifica si tu coaie daca are nume produsu(ca doar nu bagi null)
            //#TODO @Omixii verify price >0 , rating <=5
            // Check if category exists
            //ALTFEL TE DAU LA JAMAL


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
            ////#TODO/////////////
            //senior check
            ////////////////////////
            //Validation for the:
            //category
            //name
            //type
            //description
            /////////////////////
            if (string.IsNullOrWhiteSpace(productCategory.categoryId))
            { return false; }

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
                    // default:
                    // MessageBox.Show("Unknown sort option selected.");
                    // return;
            }
        }

        public async Task<bool> UpdateProductAsync(Product product, User currentUser)
        {
            if (currentUser == null || currentUser.userType != "employee" || !currentUser.employeeData.isSenior)
                return false;
            if (product == null || string.IsNullOrWhiteSpace(product.productId))
                return false;

            // Optionally: validate fields (price > 0, rating <= 5, etc.)
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
        // public async Task<bool> AddProductToCart(string productId, int quantity)
        // {
        //     var selectedProduct = new SelectedProduct
        //     {
        //         quantity = quantity
        //     };

        //     var result = await _firebaseHelper.AddProductToCart(productId, selectedProduct);
        //     return result;
        // }

        // public async Task<bool> RemoveProductFromCart(string productId)
        // {
        //     var result = await _firebaseHelper.RemoveProductFromCart(productId);
        //     return result;
        // }

        // public async Task<List<SelectedProducts>> GetProductsInCart()
        // {
        //     var products = await _firebaseHelper.GetProductsInCart();
        //     return products;
        // }
    }
}
