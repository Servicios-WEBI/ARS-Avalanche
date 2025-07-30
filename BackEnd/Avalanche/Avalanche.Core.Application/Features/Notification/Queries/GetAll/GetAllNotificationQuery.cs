using AutoMapper;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;

namespace Avalanche.Core.Application.Features.Notification.Queries.GetAll
{
    public class GetAllNotificationQuery : IRequest<GetAllNotificationQueryResponse>
    {
        public string? AssignedAnalyst { get; set; }
    }

    public class GetAllNotificationQueryHandler : IRequestHandler<GetAllNotificationQuery, GetAllNotificationQueryResponse>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public GetAllNotificationQueryHandler(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task<GetAllNotificationQueryResponse> Handle(GetAllNotificationQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetAllNotificationQueryResponse result = new();

                List<Domain.Entities.Notification> getAlls = new();

                if (!string.IsNullOrEmpty(query.AssignedAnalyst))
                {
                    getAlls = await _notificationRepository.GetAllByPropertyAsync(n => n.AssignedAnalyst == query.AssignedAnalyst);
                }
                else
                {
                    getAlls = await _notificationRepository.GetAllAsync();
                }
                
                var notification = _mapper.Map<List<GetAllNotificationQueryResponseChild>>(getAlls.OrderByDescending(x => x.Created).ToList());

                result.Notifications = notification;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
