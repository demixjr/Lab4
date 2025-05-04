using System.Collections.Generic;
using System.Linq;
using DAL;

namespace BLL.services
{
    public class HeadingService
    {
        public bool AddHeading(BoardContext context, Heading heading)
        {
            if (FindHeading(context, heading.Name) != null)
                throw new ValidationException("Така рубрика вже існує");

            context.Headings.Add(heading);
            context.SaveChanges();
            return true;
        }

        public void AddCategoryToHeading(BoardContext context,Heading heading, Category category)
        {
            heading.Categories.Add(category);
            context.SaveChanges();
        }
        public Heading FindHeading(BoardContext context, string name)
        {
            return context.Headings.FirstOrDefault(h => h.Name.ToLower() == name.ToLower());
        }

        public List<Heading> FindAllHeadings(BoardContext context)
        {
            return context.Headings.ToList();
        }

    }
}
