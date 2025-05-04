using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using DAL;

namespace BLL
{
    internal class CategoryService
    {
        public bool AddCategory(BoardContext context, string categoryName, Heading heading)
        {
            Category newCategory = new Category (categoryName, heading);
            context.Categories.Add(newCategory);
            context.SaveChanges();
            return true;
        }
        public void AddSubcategoryToCategory(BoardContext context, Category category, Subcategory subcategory)
        {
            category.Subcategories.Add(subcategory);
            context.SaveChanges();
        }
        public void AddAnnouncementToCategory(BoardContext context,Announcement announcement, Category category)
        {
            category.Announcements.Add(announcement);
            context.SaveChanges();
        }
        public Category FindCategory(BoardContext context, string name)
        {
            return context.Categories.FirstOrDefault(c => c.Name.ToLower() == name.ToLower());
        }

        public List<Category> FindAllCategories(BoardContext context)
        {
            return context.Categories.ToList();
        }
    }
}
