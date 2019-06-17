using Domain;
using Domain.Interface;
using Domain.Models;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
   public class TransactionRepository: ITransactionRepository
    {
       // private DbContext _context;
        private readonly AgroContext _context;
        public TransactionRepository(AgroContext context)
        {
            _context = context;
        }

        public async Task<PaymentModel> SaveProductDetails(OrderModel model)
        {
            var storeProduct = new Order().Assign(model);
            var productDetails = await _context.Products.FirstOrDefaultAsync(r=>r.Id == model.productId);
           // var productDetails = await _context.Set<Product>().FirstOrDefaultAsync(r => r.Id == model.productId);
            productDetails.Quantity = productDetails.Quantity - model.quantityBought;
            storeProduct.productOwnerId = productDetails.UserId;
            storeProduct.currentPriceAtPurchase = productDetails.Price;
            if(model.OrderOption == OrderOption.Delivery.ToString())
            {
                storeProduct.OrderOption = OrderOption.Delivery;
                storeProduct.deliveryAddress = model.deliveryAddress;
                storeProduct.buyerContact = model.buyerContact;
            }

            _context.Order.Add(storeProduct);
            //_context.Set<Order>().Add(storeProduct);
            await _context.SaveChangesAsync();

            var paymentModel = new PaymentModel().Assign(storeProduct);
            paymentModel.callback_url = model.callbackUrl;
            paymentModel.amount = model.TotalAmountPaid;
            paymentModel.reference = model.reference;
            paymentModel.userId = storeProduct.productOwnerId;


            return paymentModel;


        }
        //public async Task<PaymentModel> SaveDeliveryDetails(OrderModel model)
        //{
        //    var storeProduct = new Order().Assign(model);
        //    var productDetails = await _context.Products.FirstOrDefaultAsync(r => r.Id == model.productId);

        //    //var productDetails = await _context.Set<Product>().FirstOrDefaultAsync(r => r.Id == model.productId);
        //    productDetails.Quantity = productDetails.Quantity - model.quantityBought;
        //    storeProduct.productOwnerId = productDetails.UserId;
        //    storeProduct.currentPriceAtPurchase = productDetails.Price;

        //    _context.Order.Add(storeProduct);
        //    //_context.Set<Order>().Add(storeProduct);
        //    await _context.SaveChangesAsync();

        //    var paymentModel = new PaymentModel().Assign(storeProduct);
        //    paymentModel.callback_url = model.callbackUrl;
        //    paymentModel.amount = model.TotalAmountPaid;
        //    paymentModel.reference = model.reference;
        //    paymentModel.userId = storeProduct.productOwnerId;


        //    return paymentModel;


        //}
       
    }
}
