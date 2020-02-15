using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebAPI.DTOs
{
    public class OrderStatusDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<CustomerOrderDTO> CustomerOrder { get; set; }
    }
}
