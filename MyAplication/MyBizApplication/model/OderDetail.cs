using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBizApplication.model
{
    public class OderDetail
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public int CustomerId { get; internal set; }
    }
}