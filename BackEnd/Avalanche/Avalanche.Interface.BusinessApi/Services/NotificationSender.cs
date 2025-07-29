using AutoMapper;
using Avalanche.Core.Application.Dtos.Authorization;
using Avalanche.Core.Application.Dtos.Notification;
using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Application.Interfaces.Services;
using Avalanche.Core.Domain.Entities;
using Avalanche.Interface.BusinessApi.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Avalanche.Interface.BusinessApi.Services
{
    public class NotificationSender : INotificationSender
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<NotificationSender> _logger;

        public NotificationSender(IHubContext<NotificationHub> hubContext, INotificationRepository notificationRepository,
            IMapper mapper, ILogger<NotificationSender> logger)
        {
            _hubContext = hubContext;
            _notificationRepository = notificationRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task SendAuthorizationAssignedAsync(string analystSub, NotificationDTO notificationDTO)
        {
            try
            {
                var analyst = _mapper.Map<Notification>(notificationDTO);

                await _notificationRepository.AddAsync(analyst);
                _logger.LogInformation("Se ingresó la notificación satisfactoriamente");

                var dto = _mapper.Map<AuthorizationNotificationDTO>(notificationDTO);

                await _hubContext.Clients.User(analystSub).SendAsync("AuthorizationAssigned", dto);
                _logger.LogInformation("Se envío la notificación al analista " + analystSub);
            }
            catch (Exception ex)
            {
                _logger.LogError("Hubo un error al tratar de notificar al analista");
            }
        }
    }
}
