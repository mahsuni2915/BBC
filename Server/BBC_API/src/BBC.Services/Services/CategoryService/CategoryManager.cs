﻿using AutoMapper;
using BBC.Core.Dependency;
using BBC.Core.Domain;
using BBC.Core.Repositories.Base;
using BBC.Infrastructure.Data;
using BBC.Services.Exceptions;
using BBC.Services.Services.CategoryService.Dto;
using BBC.Services.Services.Common.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.CategoryService
{
    public class CategoryManager: BaseService,ICategoryService
    {
        private readonly IRepositoryBase<BBCContext, Category, int> _repository;
        public CategoryManager(IRepositoryBase<BBCContext, Category, int> repository)
        {
            _repository = repository;
        }

        public async Task<List<CategoryListDto>> GetAllCategories()
        {
            var categories= await _repository.GetListAsync();
            var result = _mapper.Map<List<CategoryListDto>>(categories);
            return result;
        }
        public void GetData()
        {
            var dd = _repository.GetList();
            var bb = _mapper.Map<List<CategoryListDto>>(dd);
        }

        public void InsertData()
        {   
            var insert = _repository.Insert(new Category() { Name = "testetet" });
            _repository.SaveChanges();
        }

        public void Exception()
        {
            throw new ServiceException("trial exception");
        }

        public async Task CreateCategories(CreateCategoryDto input)
        {
            //Admin: Yeni Kategori Oluşturma
            var category = _mapper.Map<Category>(input);
            await _repository.InsertAsync(category);
        }

        public async Task EditCategories(EditCategoryDto input)
        {
            //Admin: Kategori düzenleme
            var category = _mapper.Map<Category>(input);
            await _repository.UpdateAsync(category);
        }

        public async Task DeleteCategory(int id)
        {
            //Admin: Kategori silme
            await _repository.DeleteAsync(id);
        }

        //CRUD OPERATION WITH ASP.NET WEB API

    }
}
