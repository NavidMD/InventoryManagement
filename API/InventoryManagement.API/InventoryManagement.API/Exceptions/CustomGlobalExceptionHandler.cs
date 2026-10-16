using Microsoft.AspNetCore.Diagnostics;

namespace InventoryManagement.API.Exceptions
{
    public class CustomGlobalExceptionHandler : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
