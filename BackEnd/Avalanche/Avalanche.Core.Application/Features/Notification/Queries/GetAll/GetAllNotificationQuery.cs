using AutoMapper;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using System.Linq.Expressions;

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
                    getAlls = await _notificationRepository.GetAllByPropertyWithIncludeAsync(n => n.AssignedAnalyst == query.AssignedAnalyst,
                    new List<Expression<Func<Domain.Entities.Notification, object>>>
                    {
                        n => n.Analyst
                    });
                }
                else
                {
                    getAlls = await _notificationRepository.GetAllWithIncludeAsync(new List<Expression<Func<Domain.Entities.Notification, object>>>
                    {
                        n => n.Analyst
                    });
                }
                
                var notification = getAlls.OrderByDescending(x => x.Created).Select(n => new GetAllNotificationQueryResponseChild()
                {
                    Id = n.Id,
                    AuthorizationId = n.AuthorizationId,
                    AssignedAnalyst = n.Analyst.FullName,
                    AssignedAnalystId = n.AssignedAnalyst,
                    NotificationDate = n.NotificationDate
                }).ToList();

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
