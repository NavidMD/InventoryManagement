using InventoryManagement.Core.Interfaces.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Commands.CategoryCommands
{
    public record DeleteCategoryCommand(Guid categoryId) : IRequest<bool>;
    public class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<DeleteCategoryCommand, bool>
    {
        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = await categoryRepository.DeleteAsync(request.categoryId);
            if(!response)
            {
                throw new KeyNotFoundException("دسته بندی با این شناسه جهت حذف یافت نشد!");
            }
            return response;
        }
    }
}
