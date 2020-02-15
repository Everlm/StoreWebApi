using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreWebAPI.Models;


namespace StoreWebAPI.DTOs
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Customer, CustomerDTO>()
                   .ForMember(x => x.CustomerOrder, o => o.Ignore())
                   .ReverseMap();

                cfg.CreateMap<CustomerOrder, CustomerOrderDTO>()
                   .ForMember(x => x.OrderDetail, o => o.Ignore())
                   .ReverseMap();

                cfg.CreateMap<Employee, EmployeeDTO>()
                   .ReverseMap();

                cfg.CreateMap<OrderDetail, OrderDetailDTO>()
                   .ReverseMap();

                cfg.CreateMap<OrderStatus, OrderStatusDTO>()
                   .ForMember(x => x.CustomerOrder, o => o.Ignore())
                   .ReverseMap();

                cfg.CreateMap<Product, ProductDTO>()
                   .ForMember(x => x.OrderDetail, o => o.Ignore())
                   .ReverseMap();
            });
        }
    }
}
