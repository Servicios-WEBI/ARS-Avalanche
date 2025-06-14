using Avalanche.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalanche.Core.Domain.Entities
{
    public class DocumentType : AuditableBaseEntity
    {
        public string Name { get; set; }
        public List<Affiliate> Affiliates { get; set; }
        public List<Client> Clients { get; set; }
    }
}
