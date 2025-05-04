using DAL;
using System.Linq;
using System.Collections.Generic;

namespace BLL.services
{
    internal class SubcategoryService
    {
        public bool AddSubcategory(BoardContext context, string subcategoryName, Category category)
        {
            Subcategory newSubcategory = new Subcategory(subcategoryName, category);
            context.Subcategories.Add(newSubcategory);
            context.SaveChanges();
            return true;
        }
       
        public Subcategory FindSubcategory(BoardContext context, string name)
        {
            return context.Subcategories.FirstOrDefault(c => c.Name.ToLower() == name.ToLower());
        }

        public List<Subcategory> FindAllSubcategories(BoardContext context)
        {
            return context.Subcategories.ToList();
        }
    }
}
