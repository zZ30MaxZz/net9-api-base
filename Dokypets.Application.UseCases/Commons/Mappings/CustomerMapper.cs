using AutoMapper;
using Dokypets.Application.Dto;
using Dokypets.Application.UseCases.Customers.Commands.CreateCustomerCommand;
using Dokypets.Application.UseCases.Customers.Commands.UpdateCustomerCommand;
using Dokypets.Domain.Entities;

namespace Dokypets.Application.UseCases.Commons.Mappings
{
    public class CustomerMapper: Profile
    {
        public CustomerMapper()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, CreateCustomerCommand>().ReverseMap();
            CreateMap<Customer, UpdateCustomerCommand>().ReverseMap();
        }
    }
}
