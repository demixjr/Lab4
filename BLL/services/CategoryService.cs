using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using AutoMapper;
using DAL;

namespace BLL
{
    public class CategoryService
    {
        IMapper mapper;
        public CategoryService(IMapper mapper) 
        {
            this.mapper = mapper;
        }
        public bool AddCategory(UnitOfWork unitOfWork, CategoryDto categoryDto)
        {
            var category = mapper.Map<Category>(categoryDto);
            var heading = unitOfWork.GetRepository<Heading>().Find(h => h.Name == categoryDto.Heading.Name);
            if (heading == null)
                throw new EntityNotFoundException("Такої рубрики не знайдено");

            heading.Categories.Add(category);
            category.Heading = heading;
            unitOfWork.GetRepository<Category>().Add(category);
            unitOfWork.GetRepository<Heading>().Update(heading);
            unitOfWork.Save();
            return true;
        }
        public CategoryDto FindCategory(UnitOfWork unitOfWork, string name)
        {
            return mapper.Map<CategoryDto>(unitOfWork.GetRepository<Category>().Find(c => c.Name == name));
        }

        public List<CategoryDto> FindAllCategories(UnitOfWork unitOfWork)
        {
            return mapper.Map<List<CategoryDto>>(unitOfWork.GetRepository<Category>().GetAll());
        }
    }
}
