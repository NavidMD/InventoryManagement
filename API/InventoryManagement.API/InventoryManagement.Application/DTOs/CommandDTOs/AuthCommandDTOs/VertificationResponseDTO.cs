using InventoryManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.CommandDTOs.AuthCommandDTOs
{
    public class VertificationResponseDTO
    {
        public AccessTokenResultModel AccessToken { get; set; } = new AccessTokenResultModel();
        public RawRefreshTokenResultModel RefreshToken { get; set; } = new RawRefreshTokenResultModel();
    }
}
