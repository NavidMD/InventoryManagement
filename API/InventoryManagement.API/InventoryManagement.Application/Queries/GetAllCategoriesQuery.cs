using InventoryManagement.Application.DTOs.QueryDTOs.DefaultDTOs;
using InventoryManagement.Application.DTOs.QueryDTOs.GetAllCategories;
using InventoryManagement.Application.Mappers;
using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Interfaces.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Queries
{
    public record GetAllCategoriesQuery(): IRequest<IEnumerable<GetAllCategoriesResponseDTO>>;
    public class GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetAllCategoriesQuery, IEnumerable<GetAllCategoriesResponseDTO>>
    {
        public async Task<IEnumerable<GetAllCategoriesResponseDTO>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var allCategories = await categoryRepository.GetAllAsync();
            //دسته بندی های سطح یک
            var rootCategories = allCategories.Where(c => c.ParentCategoryId == null).ToList();
            //پیدا کردن زیردسته بندی ها و سطوح داخلی با متد بازگشتی 
            var response = rootCategories.Select(rootCategory => new GetAllCategoriesResponseDTO()
            {
                Id = rootCategory.Id,
                Title = rootCategory.Title,
                ParentCategoryId = rootCategory.ParentCategoryId,
                Products = rootCategory.Products.Select(product => new CategoryProductsDTO()
                {
                    Id = product.Id,
                    Title = product.Title,
                    SerialNumber = product.SerialNumber
                }).ToList(),
                SubCategories = allCategories
                    .Where(category => category.ParentCategoryId == rootCategory.Id)
                    .Select(subCategory => CategoryTreeMapper.MapSubCategory(subCategory, allCategories))
                    .ToList()
            }).ToList();
            return response;
        }
    }
}
