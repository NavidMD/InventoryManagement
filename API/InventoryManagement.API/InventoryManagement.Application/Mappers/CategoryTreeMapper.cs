using InventoryManagement.Application.DTOs.QueryDTOs.DefaultDTOs;
using InventoryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Mappers
{
    public static class CategoryTreeMapper
    {
        public static CategoryTreeDTO MapSubCategory(Category subCategory, IEnumerable<Category> allCategories)
        {
            //چون ما در ریپازیتوری کل دسته بندی هارو گرفتیم دیگه فرقی نداره که دسته بندی ای که داریم روش مپینگ انجام میدیم چه سطحی هست 
            //در ریپازیتوریمون دستور اینکلود که نوشتیم هر دسته بندی ای با هر سطحی رو که داشته باشیم میگیره و محصولاتش رو هم بهش میچسبونه
            //در این متد فقط با استفاده از آی دی دسته بندی والد ارتباطشون رو مرتب میکنیم
            return new CategoryTreeDTO()
            {
                Id = subCategory.Id,
                Title = subCategory.Title,
                Products = subCategory.Products.Select(p => new CategoryProductsDTO()
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
