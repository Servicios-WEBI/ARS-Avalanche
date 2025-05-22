using AutoMapper;
using Avalanche.Core.Application.Dtos.Account;
using Avalanche.Core.Application.Features.Account.Commands.Authenticate;
using Avalanche.Core.Application.Features.Account.Commands.RegisterUser;

namespace Avalanche.Core.Application.Mappings
{
    public class GeneralProfile : Profile
	{
		public GeneralProfile()
		{
			#region Account
			CreateMap<AuthenticationRequest, AuthenticateCommand>()
				.ReverseMap();

            CreateMap<RegisterRequest, RegisterUserCommand>()
                .ReverseMap();
            #endregion
            
        }
    }
}
