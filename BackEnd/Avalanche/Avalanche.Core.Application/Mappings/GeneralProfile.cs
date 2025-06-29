using AutoMapper;
using Avalanche.Core.Application.Dtos.Account;
using Avalanche.Core.Application.Dtos.Affiliate;
using Avalanche.Core.Application.Dtos.Authorization;
using Avalanche.Core.Application.Dtos.Client;
using Avalanche.Core.Application.Dtos.Coverage;
using Avalanche.Core.Application.Dtos.Hospital;
using Avalanche.Core.Application.Dtos.Plan;
using Avalanche.Core.Application.Dtos.Policy;
using Avalanche.Core.Application.Features.Account.Commands.Authenticate;
using Avalanche.Core.Application.Features.Account.Commands.RegisterAnalyst;
using Avalanche.Core.Application.Features.Account.Commands.RegisterUser;
using Avalanche.Core.Application.Features.Affiliate.Command.Add;
using Avalanche.Core.Application.Features.Authorization.Command.Add;
using Avalanche.Core.Application.Features.AuthorizationType.Queries.GetAll;
using Avalanche.Core.Application.Features.Client.Command.Add;
using Avalanche.Core.Application.Features.Coverage.Command.Add;
using Avalanche.Core.Application.Features.Coverage.Queries.GetAll;
using Avalanche.Core.Application.Features.Hospital.Command.Add;
using Avalanche.Core.Application.Features.InstitutionType.Queries.GetAll;
using Avalanche.Core.Application.Features.Plan.Command.Add;
using Avalanche.Core.Application.Features.Plan.Queries.GetAll;
using Avalanche.Core.Application.Features.Policy.Command.Add;
using Avalanche.Core.Application.Features.Status.Queries.GetAll;
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

            #region Authorization
            CreateMap<Authorization, AuthorizationDTO>()
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.Details, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Affiliate, opt => opt.Ignore())
                .ForMember(x => x.AuthorizationType, opt => opt.Ignore())
                .ForMember(x => x.Hospital, opt => opt.Ignore())
                .ForMember(x => x.Policy, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Authorization, AddAuthorizationCommand>()
                .ReverseMap()
                .ForMember(x => x.ApplicationDate, opt => opt.Ignore())
                .ForMember(x => x.Affiliate, opt => opt.Ignore())
                .ForMember(x => x.AuthorizationType, opt => opt.Ignore())
                .ForMember(x => x.Hospital, opt => opt.Ignore())
                .ForMember(x => x.Policy, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region AuthorizationType
            CreateMap<AuthorizationType, GetAllAuthorizationTypeQueryResponseChild>()
                .ReverseMap()
                .ForMember(x => x.Authorizations, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
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

            #region Hospital
            CreateMap<Hospital, HospitalDTO>()
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.Details, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Authorizations, opt => opt.Ignore())
                .ForMember(x => x.InstitutionType, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Hospital, AddHospitalCommand>()
                .ReverseMap()
                .ForMember(x => x.Authorizations, opt => opt.Ignore())
                .ForMember(x => x.InstitutionType, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region InstitutionType
            CreateMap<InstitutionType, GetAllInstitutionTypeQueryResponseChild>()
                .ReverseMap()
                .ForMember(x => x.Hospitals, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
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

            #region Status
            CreateMap<Status, GetAllStatusQueryResponseChild>()
                .ReverseMap()
                .ForMember(x => x.Affiliates, opt => opt.Ignore())
                .ForMember(x => x.AffiliatePolicies, opt => opt.Ignore())
                .ForMember(x => x.Authorizations, opt => opt.Ignore())
                .ForMember(x => x.Clients, opt => opt.Ignore())
                .ForMember(x => x.Hospitals, opt => opt.Ignore())
                .ForMember(x => x.Policies, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion
        }
    }
}
