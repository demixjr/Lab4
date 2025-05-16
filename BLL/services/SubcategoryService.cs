using DAL;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;

namespace BLL.services
{
    internal class SubcategoryService
    {
        IMapper mapper;
        public SubcategoryService (IMapper mapper)
        {
            this.mapper = mapper;
        }
        public bool AddSubcategory(UnitOfWork unitOfWork, SubcategoryDto subcategoryDto)
        {
            if (FindSubcategory(unitOfWork, subcategoryDto.Name) != null)
                throw new ValidationException("Така підкатегорія вже існує");

            var subcategory = mapper.Map<Subcategory>(subcategoryDto);
            var category = unitOfWork.GetRepository<Category>().Find(s => s.Name == subcategoryDto.Category.Name);
            category.Subcategories.Add(subcategory);
            subcategory.Category = category;
            unitOfWork.GetRepository<Category>().Update(category);
            unitOfWork.GetRepository<Subcategory>().Add(subcategory);
            unitOfWork.Save();
            return true;
        }
       
        public SubcategoryDto FindSubcategory(UnitOfWork unitOfWork, string name)
        {
            return mapper.Map<SubcategoryDto>(unitOfWork.GetRepository<Subcategory>().Find(c => c.Name == name));
        }

        public List<SubcategoryDto> FindAllSubcategories(UnitOfWork unitOfWork)
        {
            return mapper.Map<List<SubcategoryDto>>(unitOfWork.GetRepository<Subcategory>().GetAll());
        }
    }
}
