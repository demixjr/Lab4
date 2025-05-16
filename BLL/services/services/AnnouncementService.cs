using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL.services
{
    public class AnnouncementService
    {
        public bool AddAnnouncement(BoardContext context, Announcement announcement)
        {

            if (FindAnnouncementByTitle(context, announcement.Title) != null)
                throw new ValidationException("Оголошення з такою назвою вже існує");

            context.Announcements.Add(announcement);
            context.SaveChanges();
            return true;
        }

        public Announcement FindAnnouncementByTitle(BoardContext context, string title)
        {
            return context.Announcements.FirstOrDefault(a => a.Title.ToLower() == title.ToLower());
        }
        public List<Announcement> FindAllAnnouncements(BoardContext context)
        {
            return context.Announcements.ToList();
        }
        public bool DeleteAnnouncement(BoardContext context,Announcement announcement, User user)
        {
            if(user.Username ==  announcement.Username)
            {
                List<Tag> tags = announcement.Tags.ToList();
                context.Announcements.Remove(announcement);
                user.Announcements.Remove(announcement);
                announcement.Subcategory.Announcements.Remove(announcement);
                announcement.Category.Announcements.Remove(announcement);
                foreach(Tag tag in tags)
                {
                    tag.Announcements.Remove(announcement);
                }
                context.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}
