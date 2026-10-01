using InventoryManagement.Application.DTOs.CommandDTOs.CategoryCommandDTOs;
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
    public record UpdateCategoryCommand(Guid id, UpdateCategoryRequestDTO updatedCategory) : IRequest<UpdateCategoryResponseDTO>;
    public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<UpdateCategoryCommand, UpdateCategoryResponseDTO>
    {
        public async Task<UpdateCategoryResponseDTO> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var newCategory = new Category()
            {
                Id = request.id,
                Title = request.updatedCategory.Title
            };
            var response = await categoryRepository.UpdateAsync(request.id, newCategory);
            if(response == null)
            {
                throw new KeyNotFoundException("دسته بندی با این شناسه جهت ویرایش یافت نشد!");
            }
            return new UpdateCategoryResponseDTO()
            {
                Id = response.Id,
                Title = response.Title,
            };
        }
    }
}
