using InventoryManagement.Application.DTOs.CommandDTOs.AuthCommandDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Commands.AuthCommands
{
    public record RefreshTokenCommand() : IRequest<RefreshTokenResponseDTO>;
    //public class RefreshTokenCommandHandler() : IRequestHandler<RefreshTokenCommand, RefreshTokenResponseDTO>
    //{
    //    public Task<RefreshTokenResponseDTO> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    //    {
            
    //    }
    //}
}
