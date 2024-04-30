using AutoMapper;
using WebApp.Models;
using WebApp.Models.Dtos;

namespace WebApp.AutoMapper
{
    public class TransactionMappingProfiles : Profile
    {
        public TransactionMappingProfiles()
        {
            CreateMap<Transaction, TransactionGetDto>();
            CreateMap<TransactionGetDto, Transaction>();
        }
    }
}
