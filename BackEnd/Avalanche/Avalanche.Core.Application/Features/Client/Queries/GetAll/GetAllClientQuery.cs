using AutoMapper;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.Client.Queries.GetAll
{
    public class GetAllClientQuery : IRequest<GetAllClientQueryResponse>
    {

    }

    public class GetAllClientQueryHandler : IRequestHandler<GetAllClientQuery, GetAllClientQueryResponse>
    {
        private readonly IClientRepository _clientRepository;

        public GetAllClientQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<GetAllClientQueryResponse> Handle(GetAllClientQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetAllClientQueryResponse result = new();

                var getAlls = await _clientRepository.GetAllWithIncludeAsync(new List<Expression<Func<Domain.Entities.Client, object>>>
                {
                    m => m.DocumentType,
                    m => m.Status
                });
                var entities = getAlls.OrderByDescending(x => x.Created).ToList();

                var clients = entities.Select(a => new GetAllClientQueryResponseChild
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    MiddleName = a.MiddleName,
                    LastName = a.LastName,
                    DocumentType = a.DocumentType.Name,
                    DocumentNumber = a.DocumentNumber,
                    Phone = a.Phone,
                    Email = a.Email,
                    Address = a.Address,
                    CustomerSince = a.CustomerSince,
                    Status = a.Status.Name
                }).ToList();

                result.Clients = clients;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
