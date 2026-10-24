using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Models
{
    public class RawRefreshTokenResultModel
    {
        public string RawRefreshToken { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
