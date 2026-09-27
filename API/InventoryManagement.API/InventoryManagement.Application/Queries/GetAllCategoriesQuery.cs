using InventoryManagement.Application.DTOs.QueryDTOs.GetAllCategories;
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
                Products = rootCategory.Products.Select(product => new GetAllCategoriesProductDTO()
                {
                    Id = product.Id,
                    Title = product.Title,
                    SerialNumber = product.SerialNumber
                }).ToList(),
                SubCategories = allCategories
                    .Where(category => category.ParentCategoryId == rootCategory.Id)
                    .Select(subCategory => MapSubCategory(subCategory, allCategories))
                    .ToList()
            }).ToList();
            return response;
        }

        private GetAllCategoriesSubCategoryDTO MapSubCategory(Category subCategory, IEnumerable<Category> allCategories)
        {
            //چون ما در ریپازیتوری کل دسته بندی هارو گرفتیم دیگه فرقی نداره که دسته بندی ای که داریم روش مپینگ انجام میدیم چه سطحی هست 
            //در ریپازیتوریمون دستور اینکلود که نوشتیم هر دسته بندی ای با هر سطحی رو که داشته باشیم میگیره و محصولاتش رو هم بهش میچسبونه
            //در این متد فقط با استفاده از آی دی دسته بندی والد ارتباطشون رو مرتب میکنیم
            return new GetAllCategoriesSubCategoryDTO()
            {
                Id = subCategory.Id,
                Title = subCategory.Title,
                Products = subCategory.Products.Select(p => new GetAllCategoriesProductDTO()
                {
                    Id = p.Id,
                    Title = p.Title,
                    SerialNumber = p.SerialNumber
                }).ToList(),
                SubCategories = allCategories
                    .Where(category => category.ParentCategoryId == subCategory.Id)
                    .Select(innerSubCat => MapSubCategory(innerSubCat, allCategories))
                    .ToList()
            };
        }
    }
}
