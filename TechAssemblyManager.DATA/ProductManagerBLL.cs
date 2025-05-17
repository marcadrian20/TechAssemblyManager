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
        private readonly FirebaseHelper _firebaseHelper;

        public ProductManagerBLL(FirebaseHelper firebaseHelper)
        {
            _firebaseHelper = firebaseHelper;
        }

        public async Task<bool> AddProductAsync(Product product, User currentUser)
        {
            if (string.IsNullOrWhiteSpace(product.productId) ||  ///Validation whether there's a valid category
                currentUser == null //||                          //Or whether there is ab existing user 
                //currentUser.userType != "employee"              //being an employee
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
        //#TODO migrate the sort system for the products to be more robust:
       //#Example:
        // public async Task<List<Product>> GetProductsOrderedBy(string categoryId, string selectedFilter)
        // {
        //     var products = await GetProductsByCategoryAsync(categoryId);
        //     switch (selectedFilter)
        //     {
        //         case "Category [A -> Z]":
        //             products = products.OrderBy(p => p.categoryId).ToList();
        //             break;
        //         case "Category [Z -> A]":
        //             products = products.OrderByDescending(p => p.categoryId).ToList();
        //             break;
        //         case "Price [Low -> High]":
        //             products = products.OrderBy(p => p.price).ToList();
        //             break;
        //         case "Price [High -> Low]":
        //             products = products.OrderByDescending(p => p.price).ToList();
        //             break;
        //         case "Name [A -> Z]":
        //             products = products.OrderBy(p => p.name).ToList();
        //             break;
        //         case "Name [Z -> A]":
        //             products = products.OrderByDescending(p => p.name).ToList();
        //             break;
        //             // default:
        //             // MessageBox.Show("Unknown sort option selected.");
        //             // return;
        //     }
        //     return products;
        // }


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
