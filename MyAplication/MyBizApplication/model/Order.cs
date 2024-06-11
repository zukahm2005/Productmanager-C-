using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBizApplication.model
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }

        public List<OderDetail>OrderDetails { get; set; }

        public Order(){
        OrderDetails = new List<OderDetail>();
    }
    }
}