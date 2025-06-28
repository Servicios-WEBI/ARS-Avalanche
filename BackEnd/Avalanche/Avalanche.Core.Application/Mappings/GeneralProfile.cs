using AutoMapper;
using Avalanche.Core.Application.Dtos.Account;
using Avalanche.Core.Application.Dtos.Affiliate;
using Avalanche.Core.Application.Dtos.Client;
using Avalanche.Core.Application.Dtos.Coverage;
using Avalanche.Core.Application.Dtos.Plan;
using Avalanche.Core.Application.Dtos.Policy;
using Avalanche.Core.Application.Features.Account.Commands.Authenticate;
using Avalanche.Core.Application.Features.Account.Commands.RegisterAnalyst;
using Avalanche.Core.Application.Features.Account.Commands.RegisterUser;
using Avalanche.Core.Application.Features.Affiliate.Command.Add;
using Avalanche.Core.Application.Features.Client.Command.Add;
using Avalanche.Core.Application.Features.Coverage.Command.Add;
using Avalanche.Core.Application.Features.Coverage.Queries.GetAll;
using Avalanche.Core.Application.Features.Plan.Command.Add;
using Avalanche.Core.Application.Features.Plan.Queries.GetAll;
using Avalanche.Core.Application.Features.Policy.Command.Add;
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

            CreateMap<RegisterRequest, RegisterAnalystCommand>()
                .ReverseMap()
                .ForMember(x => x.Password, opt => opt.Ignore());
            #endregion

            #region Affiliate
            CreateMap<Affiliate, AffiliateDTO>()
                .ForMember(x => x.AffiliateStatus, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.Details, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.DocumentTypeId, opt => opt.Ignore())
                .ForMember(x => x.DocumentType, opt => opt.Ignore())
                .ForMember(x => x.StatusId, opt => opt.Ignore())
                .ForMember(x => x.AffiliatePolicies, opt => opt.Ignore())
                .ForMember(x => x.Authorizations, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.Client, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Affiliate, AddAffiliateCommand>()
                .ForMember(x => x.DocumentType, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.DocumentTypeId, opt => opt.Ignore())
                .ForMember(x => x.DocumentType, opt => opt.Ignore())
                .ForMember(x => x.StatusId, opt => opt.Ignore())
                .ForMember(x => x.AffiliatePolicies, opt => opt.Ignore())
                .ForMember(x => x.Authorizations, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.Client, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region Client
            CreateMap<Client, ClientDTO>()
                .ForMember(x => x.ClientStatus, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.Details, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.DocumentTypeId, opt => opt.Ignore())
                .ForMember(x => x.DocumentType, opt => opt.Ignore())
                .ForMember(x => x.StatusId, opt => opt.Ignore())
                .ForMember(x => x.Affiliates, opt => opt.Ignore())
                .ForMember(x => x.Policies, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Client, AddClientCommand>()
                .ForMember(x => x.DocumentType, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.DocumentTypeId, opt => opt.Ignore())
                .ForMember(x => x.DocumentType, opt => opt.Ignore())
                .ForMember(x => x.StatusId, opt => opt.Ignore())
                .ForMember(x => x.Affiliates, opt => opt.Ignore())
                .ForMember(x => x.Policies, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Client, Affiliate>()
                .ForMember(x => x.BirthDate, opt => opt.Ignore())
                .ForMember(x => x.AffiliateDate, opt => opt.Ignore())
                .ForMember(x => x.Gender, opt => opt.Ignore())
                .ForMember(x => x.Client, opt => opt.Ignore())
                .ForMember(x => x.ClientId, opt => opt.Ignore())
                .ForMember(x => x.AffiliatePolicies, opt => opt.Ignore())
                .ForMember(x => x.Authorizations, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Address, opt => opt.Ignore())
                .ForMember(x => x.Phone, opt => opt.Ignore())
                .ForMember(x => x.Email, opt => opt.Ignore())
                .ForMember(x => x.CustomerSince, opt => opt.Ignore())
                .ForMember(x => x.Affiliates, opt => opt.Ignore())
                .ForMember(x => x.Policies, opt => opt.Ignore());
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

            #region Policy
            CreateMap<Policy, PolicyDTO>()
                .ForMember(x => x.PolicyStatus, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.Details, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.Client, opt => opt.Ignore())
                .ForMember(x => x.Plan, opt => opt.Ignore())
                .ForMember(x => x.AffiliatePolicies, opt => opt.Ignore())
                .ForMember(x => x.Authorizations, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Policy, AddPolicyCommand>()
                .ReverseMap()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Number, opt => opt.Ignore())
                .ForMember(x => x.EffectiveStartDate, opt => opt.Ignore())
                .ForMember(x => x.EffectiveEndDate, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.StatusId, opt => opt.Ignore())
                .ForMember(x => x.Client, opt => opt.Ignore())
                .ForMember(x => x.Plan, opt => opt.Ignore())
                .ForMember(x => x.AffiliatePolicies, opt => opt.Ignore())
                .ForMember(x => x.Authorizations, opt => opt.Ignore())
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
