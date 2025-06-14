using AutoMapper;
using Avalanche.Core.Application.Dtos.Account;
using Avalanche.Core.Application.Dtos.Plan;
using Avalanche.Core.Application.Dtos.PlanCoverage;
using Avalanche.Core.Application.Features.Account.Commands.Authenticate;
using Avalanche.Core.Application.Features.Account.Commands.RegisterUser;
using Avalanche.Core.Domain.Entities;

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

            #region Plan
            CreateMap<Plan, PlanSeedDTO>()
                .ReverseMap()
                .ForMember(x => x.Policies, opt => opt.Ignore())
                .ForMember(x => x.PlanCoverages, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore()); ;

            #endregion
        }
    }
}
