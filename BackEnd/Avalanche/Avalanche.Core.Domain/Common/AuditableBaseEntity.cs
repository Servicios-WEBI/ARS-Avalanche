namespace Avalanche.Core.Domain.Common
{
    public class AuditableBaseEntity
	{
		public virtual string Id { get; set; }
		public string CreatedBy { get; set; }
		public DateTime Created { get; set; }
		public string? LastModifiedBy { get; set; }
		public DateTime? LastModified { get; set; }

		public AuditableBaseEntity ()
		{
			Id = Guid.NewGuid().ToString().Substring(2, 12);
        }
	}
}
