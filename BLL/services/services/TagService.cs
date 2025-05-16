using System;
using System.Collections.Generic;
using System.Linq;
using DAL;

namespace BLL
{
    public class TagService
    {
        public bool AddTag(BoardContext context, Tag tag)
        {
            context.Tags.Add(tag);
            context.SaveChanges();
            return true;
        }

        public Tag FindTagByName(BoardContext context, string tagName)
        {
            return context.Tags.FirstOrDefault(t => t.Name != null && t.Name.ToLower() == tagName.ToLower());
        }
        public List<Tag> FindAllTags(BoardContext context)
        {
            return context.Tags.ToList(); 
        }
        public bool DeleteTag(BoardContext context, string tagName)
        {
            Tag tag = FindTagByName(context, tagName);
            if (tag != null)
            {
                context.Tags.Remove(tag);
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
