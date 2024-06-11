using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBizApplication.model;
using MyBizApplication.sevice;

namespace MyBizApplication.controller
{
    public class Odercontroller
    {
        private IOderRepository oderService;

        public Odercontroller(IOderRepository oderService)
        {
            this.oderService = oderService;
        }
         public List<Order> GetAllOrders(){
            return oderService.GetAllOrders();
        }
        public Order GetOrderById(int id){
            return oderService.GetOrderById(id);
        }

        internal void AddOrder(Order newOrder)
        {
            oderService.AddOrder(newOrder);
        }
    }

}