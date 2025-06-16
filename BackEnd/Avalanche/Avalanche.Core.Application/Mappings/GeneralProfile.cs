using AutoMapper;
using Avalanche.Core.Application.Dtos.Account;
using Avalanche.Core.Application.Dtos.Coverage;
using Avalanche.Core.Application.Dtos.Plan;
using Avalanche.Core.Application.Features.Account.Commands.Authenticate;
using Avalanche.Core.Application.Features.Account.Commands.RegisterUser;
using Avalanche.Core.Application.Features.Coverage.Command.Add;
using Avalanche.Core.Application.Features.Coverage.Queries.GetAll;
using Avalanche.Core.Application.Features.Plan.Command.Add;
using Avalanche.Core.Application.Features.Plan.Queries.GetAll;
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
            CreateMap<Plan, PlanDTO>()
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.Details, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.PlanCoverages, opt => opt.Ignore())
                .ForMember(x => x.Policies, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Plan, PlanSeedDTO>()
                .ReverseMap()
                .ForMember(x => x.Policies, opt => opt.Ignore())
                .ForMember(x => x.PlanCoverages, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Plan, GetAllPlanQueryResponseChild>()
                .ReverseMap()
                .ForMember(x => x.Policies, opt => opt.Ignore())
                .ForMember(x => x.PlanCoverages, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Plan, AddPlanCommand>()
                .ReverseMap()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Policies, opt => opt.Ignore())
                .ForMember(x => x.PlanCoverages, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region Coverage
            CreateMap<Coverage, CoverageDTO>()
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.Details, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.PlanCoverages, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Coverage, GetAllCoverageQueryResponseChild>()
                .ReverseMap()
                .ForMember(x => x.PlanCoverages, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Coverage, AddCoverageCommand>()
                .ReverseMap()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.PlanCoverages, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion
        }
    }
}
