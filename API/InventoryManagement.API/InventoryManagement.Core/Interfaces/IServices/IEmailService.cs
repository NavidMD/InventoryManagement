using InventoryManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Interfaces.IServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailModel email, CancellationToken cancellationToken);
    }
}
