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
    public record CreateCategoryCommand(CreateCategoryRequestDTO category) : IRequest<CreateCategoryResponseDTO>;

    public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<CreateCategoryCommand, CreateCategoryResponseDTO>
    {
        public async Task<CreateCategoryResponseDTO> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var newCategory = new Category()
            {
                Title = request.category.Title,
                ParentCategoryId = request.category.ParentCategoryId,
            };
            var result = await categoryRepository.CreateAsync(newCategory);

            return new CreateCategoryResponseDTO()
            {
                Id = result.Id,
                Title = result.Title,
                ParentCategoryId = result.ParentCategoryId
            };
        }
    }
}
