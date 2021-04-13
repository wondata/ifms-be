using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ChartOfAccount.Queries
{
    public class GetAllAccountTypesQuery : IRequest<Response<IEnumerable<AccountTypeEntity>>>
    {
        
    }
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllAccountTypesQuery, Response<IEnumerable<AccountTypeEntity>>>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public GetAllProductsQueryHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<AccountTypeEntity>>> Handle(GetAllAccountTypesQuery request, CancellationToken cancellationToken)
        {
            var accountTypes = await _repository.GetAllAsync<CoreAccountType>();
            var productViewModel = _mapper.Map<IEnumerable<AccountTypeEntity>>(accountTypes);
            return new Response<IEnumerable<AccountTypeEntity>>(productViewModel);
        }
    }
}
