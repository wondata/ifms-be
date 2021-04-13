using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ChartOfAccount.Commands
{
    public partial class CreateAccountTypeCommand : IRequest<Response<Guid>>
    {
        
    }
    public class CreateAccountTypeCommandHandler : IRequestHandler<CreateAccountTypeCommand, Response<Guid>>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public CreateAccountTypeCommandHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Handle(CreateAccountTypeCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<CoreAccountType>(request);
            await _repository.AddAsync(product);
            return new Response<Guid>(product.Id);
        }
    }
}
