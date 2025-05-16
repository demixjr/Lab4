using System.Collections.Generic;
using System;
using BLL.services;
using DAL;
using System.Linq;
using System.Dynamic;
using System.Text;
using AutoMapper;

namespace BLL
{
    public class ServiceFacade
    {

        UnitOfWork unitOfWork;
        IMapper mapper;
        Validation validation;
        UserService userService;
        TagService tagService;
        HeadingService headingService;
        AnnouncementService announcementService;
        CategoryService categoryService;
        SubcategoryService subcategoryService;
        public ServiceFacade()
        {
            BoardContext boardContext = new BoardContext();
            unitOfWork = new UnitOfWork(boardContext);
            validation = new Validation();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            mapper = config.CreateMapper();
            userService = new UserService(mapper);
            tagService = new TagService(mapper);
            headingService = new HeadingService(mapper);
            categoryService = new CategoryService(mapper);
            subcategoryService = new SubcategoryService(mapper);
            announcementService = new AnnouncementService(mapper);
        }


        //
        //USER MENU
        //
        public bool AddUser(string username, string password)
        {
            if(validation.IsUsernameValid(username) && validation.IsPasswordValid(password))
            {
                UserDto newUser = new UserDto
                {
                    Username = username,
                    Password = password
                }; 
                if(userService.AddUser(unitOfWork, newUser))
                    return true;
            }
            return false;
        }

        public bool ChangeUserPassword(string username, string newPassword)
        {
            if (!validation.IsPasswordValid(newPassword))
                return false;
            UserDto userDto = new UserDto
            {
                Username = username
            };
            return userService.ChangeUserPassword(unitOfWork, userDto, newPassword);
        }
        public string FindUsersAnnouncements(string username)
        {
            UserDto userDto = userService.FindUserByUsername(unitOfWork, username);
            var announcements = userDto.Announcements;
            string allAnnouncements = "";
            foreach(var announcement in announcements)
            {
                allAnnouncements += announcement.GetInfo();
            }
            return allAnnouncements;
        }
        public UserDto FindUser(string username)
        {
            return userService.FindUserByUsername(unitOfWork, username);
        }
        public bool UserLogin(string username, string password)
        {
            UserDto user = FindUser(username);
            if (user != null && user.Password == password)
            {
                return true;
            }
            return false;
        }
        public bool DeleteUser(string username)
        {
            UserDto userDto = new UserDto
            {
                Username = username
            };
            return userService.DeleteUser(unitOfWork, userDto);
        }

        
        //
        //HEADING
        //
        public bool AddHeading(string headingName)
        {
            if (validation.IsNameValid(headingName))
            {
                HeadingDto headingDto = new HeadingDto
                {
                    Name = headingName
                };
                headingService.AddHeading(unitOfWork, headingDto);
                return true;
            }
            return false;
        }

        public HeadingDto FindHeading(string name)
        {
            if (validation.IsNameValid(name))
                return headingService.FindHeading(unitOfWork, name);
            return null;
        }

     
        public string FindAllHeadings()
        {
            List<HeadingDto> list = headingService.FindAllHeadings(unitOfWork);
            return string.Join(Environment.NewLine, list.Select(h => h.Name));

        }

        //
        //CATEGORY
        //
        public bool AddCategory(string name, string headingName)
        {
            if (validation.IsNameValid(name))
            {

                CategoryDto categoryDto = new CategoryDto
                {
                    Name = name,
                    Heading = new HeadingDto { Name = headingName}
                };
                categoryService.AddCategory(unitOfWork, categoryDto);
                return true;
            }
            return false;
        }

        public CategoryDto FindCategory(string name)
        {
            if (validation.IsNameValid(name))
                return categoryService.FindCategory(unitOfWork, name);
            return null;
        }
 
        public string FindAllCategories()
        {
            var list = categoryService.FindAllCategories(unitOfWork);

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
                SubcategoryDto subcategoryDto = new SubcategoryDto
                {
                    Name = name,
                    Category = new CategoryDto { Name = categoryName }
                };
                subcategoryService.AddSubcategory(unitOfWork, subcategoryDto);
                return true;
            }
            return false;
        }

        public SubcategoryDto FindSubcategory(string name)
        {
            if (validation.IsNameValid(name))
                return subcategoryService.FindSubcategory(unitOfWork, name);
            return null;
        }

        public string FindAllSubcategories()
        {
           
            var list = subcategoryService.FindAllSubcategories(unitOfWork);
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
            if (tagName.Count() < 2)
                throw new ValidationException("Тег занадто короткий");

            TagDto tagDto = new TagDto { Name = tagName };
            return tagService.AddTag(unitOfWork, tagDto);
        }

        public TagDto FindTag(string tagName)
        {
            return tagService.FindTagByName(unitOfWork, tagName);   
        }


        public string FindAllTags()
        {
            var list = tagService.FindAllTags(unitOfWork);
            return string.Join(Environment.NewLine, list.Select(t => t.Name));
        }

        //
        //ANNOUNCEMENT MENU
        //


        public bool AddAnnouncement(string title, string description, string categoryName, string subcategoryName, List<string> tagNames, string username)
        {
            UserDto user = userService.FindUserByUsername(unitOfWork, username);
            List<TagDto> tags = new List<TagDto>();
            foreach (string tagName in tagNames)
            {
                tags.Add(FindTag(tagName));
            }

            if (user != null)
            {
                if (announcementService.FindAnnouncementByTitle(unitOfWork, title) != null)
                    throw new ValidationException("У вас вже існує оголошення з такою назвою");

                if (validation.IsNameValid(title) && validation.IsDescriptionValid(description))
                {
                    AnnouncementDto announcementDto = new AnnouncementDto
                    {
                        Title = title,
                        Description = description,
                        Category = new CategoryDto { Name = categoryName},
                        Username = username,
                        Subcategory = new SubcategoryDto { Name = subcategoryName},
                        Tags = tags
                    };
                    announcementService.AddAnnouncement(unitOfWork, announcementDto);
                }
            }
            return false;
        }

        public AnnouncementDto FindAnnouncement(string name)
        {
            if (validation.IsNameValid(name))
                return announcementService.FindAnnouncementByTitle(unitOfWork, name);
            return null;
        }

        public string FindAnnouncementByTag(string tagName)
        {
            TagDto tag = tagService.FindTagByName(unitOfWork, tagName);
            if (tag == null)
                return "Тег не знайдено.";
            var announcements = tag.Announcements;
            if (announcements == null)
                return "Оголошень за таким тегом не знайдено";
            string allInfo = "";
            foreach (var a in announcements)
            {
                allInfo += a.GetInfo() + "\n";
            }
            return allInfo;
        }
        
        public string FindAllAnnouncements()
        {
            string allInfo = "";
            var list =  announcementService.FindAllAnnouncements(unitOfWork);
            foreach(var a in list)
            {
                allInfo += a.GetInfo() + "\n";
            }
            return allInfo;
        }

        public bool DeleteAnnouncement(string title, string username)
        {
           var announcement = announcementService.FindAnnouncementByTitle(unitOfWork, title);
            if (announcement == null)
                throw new ValidationException("Такого оголошення не існує");
            if(announcement.Username == username)
            {
                announcementService.DeleteAnnouncement(unitOfWork, title, username);
                return true;
            }
            return false;
        }

    }
}
