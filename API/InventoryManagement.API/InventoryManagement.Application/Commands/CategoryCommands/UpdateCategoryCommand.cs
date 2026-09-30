using InventoryManagement.Application.DTOs.CommandDTOs;
using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Interfaces.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Commands.CategoryCommands
{
    public record UpdateCategoryCommand(Guid id, UpdateCategoryRequestDTO updatedCategory) : IRequest<bool>;
    public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<UpdateCategoryCommand, bool>
    {
        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var newCategory = new Category()
            {
                Id = request.id,
                Title = request.updatedCategory.Title
            };
            var response = await categoryRepository.UpdateAsync(request.id, newCategory);
            if(response != true)
            {
                throw new KeyNotFoundException("دسته بندی بندی با این کلید جهت ویرایش یافت نشد!");
            }
            return response;
        }
    }
}
