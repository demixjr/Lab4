using System.Collections.Generic;
using System;
using BLL.services;
using DAL;
using System.Linq;
using System.Dynamic;
using System.Text;

namespace BLL
{
    public class ServiceFacade
    {

        BoardContext context;
        Validation validation;
        UserService userService;
        TagService tagService;
        HeadingService headingService;
        AnnouncementService announcementService;
        CategoryService categoryService;
        SubcategoryService subcategoryService;
        public ServiceFacade()
        {
            context = new BoardContext();
            validation = new Validation();
            userService = new UserService();
            tagService = new TagService();
            headingService = new HeadingService();
            categoryService = new CategoryService();
            subcategoryService = new SubcategoryService();
            announcementService = new AnnouncementService();
        }


        //
        //USER MENU
        //
        public bool AddUser(string username, string password)
        {
            if(validation.IsUsernameValid(username) && validation.IsPasswordValid(password))
            {
                User newUser = new User(username, password); 
                if(userService.AddUser(context, newUser))
                    return true;
            }
            return false;
        }

        public bool ChangeUserPassword(string username, string oldPassword, string newPassword)
        {
            if (!validation.IsPasswordValid(newPassword))
                return false;

            return userService.ChangeUserPassword(context, username, oldPassword, newPassword);
        }
        public string FindUsersAnnouncements(string username)
        {
            User user = userService.FindUserByUsername(context, username);
            string allAnnouncements = "";
            var announcements = user.Announcements;
            foreach(Announcement announcement in announcements)
            {
                allAnnouncements += announcement.GetInfo();
            }
            return allAnnouncements;
        }
        public User FindUser(string username)
        {
            return userService.FindUserByUsername(context, username);
        }
        public bool UserLogin(string username, string password)
        {
            User user = FindUser(username);
            if (user != null && user.Password == password)
            {
                return true;
            }
            return false;
        }
        public bool DeleteUser(string username, string password)
        {
            return userService.DeleteUser(context, username, password);
        }

        
        //
        //HEADING
        //
        public bool AddHeading(string headingName)
        {
            if (validation.IsNameValid(headingName))
            {
                Heading heading = new Heading(headingName);
                context.Headings.Add(heading);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public Heading FindHeading(string name)
        {
            if (validation.IsNameValid(name))
                return headingService.FindHeading(context, name);
            return null;
        }

     
        public string FindAllHeadings()
        {
            List<Heading> list = headingService.FindAllHeadings(context);
            return string.Join(Environment.NewLine, list.Select(h => h.Name));

        }

        //
        //CATEGORY
        //
        public bool AddCategory(string name, string headingName)
        {
            if (validation.IsNameValid(name))
            {
                Heading heading = headingService.FindHeading(context, headingName);
                if (heading == null)
                    throw new Exception("Такої рубрики не існує");

                categoryService.AddCategory(context, name, heading);
                Category category = FindCategory(name);
                headingService.AddCategoryToHeading(context, heading, category);
                return true;
            }
            return false;
        }

        public Category FindCategory(string name)
        {
            if (validation.IsNameValid(name))
                return categoryService.FindCategory(context, name);
            return null;
        }
 
        public string FindAllCategories()
        {
            List<Category> list = categoryService.FindAllCategories(context);

            var grouped = list.GroupBy(c => c.Heading.Name);

            string result = "";

            foreach (var group in grouped)
            {
                result += group.Key + "\n"; 
                foreach (var category in group)
                {
                    result += "-" + category.Name + "\n";
                }
            }

            return result;
        }

        //
        //SUBCATEGORY
        //
        public bool AddSubcategory(string name, string categoryName)
        {
            if (validation.IsNameValid(name))
            {
                Category category = categoryService.FindCategory(context, categoryName);
                if (category == null)
                    throw new Exception("Такої категорії не існує");

                Subcategory subcategory = new Subcategory(name, category);
                context.Subcategories.Add(subcategory);
                categoryService.AddSubcategoryToCategory(context, category, subcategory);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public Subcategory FindSubcategory(string name)
        {
            if (validation.IsNameValid(name))
                return subcategoryService.FindSubcategory(context, name);
            return null;
        }

        public string FindAllSubcategories()
        {
           
            List<Subcategory> list = subcategoryService.FindAllSubcategories(context);
            var grouped = list.GroupBy(c => c.Category.Name);

            string result = "";

            foreach (var group in grouped)
            {
                result += group.Key + "\n";
                foreach (var category in group)
                {
                    result += "-" + category.Name + "\n";
                }
            }

            return result;

        }
        //
        //TAG MENU
        //
        public bool AddTag(string tagName)
        { 
            Tag tag = new Tag(tagName);
            return tagService.AddTag(context, tag);
        }

        public Tag FindTag(string tagName)
        {
            return tagService.FindTagByName(context, tagName);   
        }


        public string FindAllTags()
        {
            List<Tag> list = tagService.FindAllTags(context);
            return string.Join(Environment.NewLine, list.Select(t => t.Name));
        }

        //
        //ANNOUNCEMENT MENU
        //


        public bool AddAnnouncement(string title, string description, string categoryName, string subcategoryName, List<string> tagNames, string username)
        {
            User user = userService.FindUserByUsername(context, username);
            Category category = categoryService.FindCategory(context, categoryName);
            Subcategory subcategory = subcategoryService.FindSubcategory(context, subcategoryName);
            List<Tag> tags = new List<Tag>();
            foreach (string tagName in tagNames)
            {
                tags.Add(FindTag(tagName));
            }
            if (user != null)
            {
                if (validation.IsNameValid(title) && validation.IsDescriptionValid(description))
                {
                    Announcement announcement = new Announcement(category, subcategory, tags, title, description, user);
                    announcementService.AddAnnouncement(context, announcement);
                    category.Announcements.Add(announcement);
                    subcategory.Announcements.Add(announcement);
                    foreach (Tag tag in tags)
                    {
                        tag.Announcements.Add(announcement);
                    }
                    user.Announcements.Add(announcement);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public Announcement FindAnnouncement(string name)
        {
            if (validation.IsNameValid(name))
                return announcementService.FindAnnouncementByTitle(context, name);
            return null;
        }

        public string FindAnnouncementByTag(string tagName)
        {
            Tag tag = tagService.FindTagByName(context, tagName);
            if (tag == null)
                return "Тег не знайдено.";
            var announcements = tag.Announcements;
            if (announcements == null)
                return "Оголошень за таким тегом не знайдено";
            string allInfo = "";
            foreach (Announcement a in announcements)
            {
                allInfo += a.GetInfo() + "\n";
            }
            return allInfo;
        }
        
        public string FindAllAnnouncements()
        {
            string allInfo = "";
            List<Announcement> list =  announcementService.FindAllAnnouncements(context);
            foreach(Announcement a in list)
            {
                allInfo += a.GetInfo() + "\n";
            }
            return allInfo;
        }

        public bool DeleteAnnouncement(string title, string username, string password)
        {
           Announcement announcement = announcementService.FindAnnouncementByTitle(context, title);
            User user = userService.FindUserByUsername(context, username);  
            if (announcement == null)
                throw new ValidationException("Такого оголошення не існує");
            if(announcement.User.Username == user.Username)
            {
                announcementService.DeleteAnnouncement(context, announcement, user);
                return true;
            }
            return false;
        }

    }
}
