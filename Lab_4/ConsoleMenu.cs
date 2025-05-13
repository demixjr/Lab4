using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using BLL;

namespace Lab_4
{
    internal class ConsoleMenu
    {
        public ConsoleMenu(ServiceFacade facade)
        {
            this.facade = facade;
        }
        ServiceFacade facade;
        public void Menu()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            bool exit = false;

          //  TestInfoForDB();

            while (!exit)
            {
                Console.WriteLine("Меню");
                Console.WriteLine("1. Створити користувача");
                Console.WriteLine("2. Вхід в акаунт");
                Console.WriteLine("3. Переглянути усі оголошення");
                Console.WriteLine("4. Переглянути усі рубрики");
                Console.WriteLine("5. Переглянути усі категорії");
                Console.WriteLine("6. Переглянути усі підкатегорії");
                Console.WriteLine("7. Знайти оголошення за тегом");
                Console.WriteLine("0. Вихід");

                try
                {
                    int choose1 = int.Parse(Console.ReadLine());
                    switch (choose1)
                    {
                        case 0:
                            exit = true;
                            break;
                        case 1:
                                Console.WriteLine("Введіть нікнейм");
                                string username = Console.ReadLine();
                                Console.WriteLine("Введіть пароль");
                                string password = Console.ReadLine();
                                if (facade.AddUser(username, password))
                                {
                                    Console.WriteLine("Користувача додано");
                                }
                                else
                                    Console.WriteLine("Не вдалось додати користувача");

                            break;

                        case 2:
                            
                                Console.WriteLine("Введіть нікнейм");
                                string username2 = Console.ReadLine();
                                Console.WriteLine("Введіть пароль");
                                string password2 = Console.ReadLine();
                                if (facade.UserLogin(username2, password2))
                                {
                                    bool userCycle = false;
                                    while (!userCycle)
                                    {
                                        Console.WriteLine("Оберіть опцію");
                                        Console.WriteLine("1. Створити оголошення");
                                        Console.WriteLine("2. Переглянуті створені оголошення");
                                        Console.WriteLine("3. Видалити оголошення");
                                        Console.WriteLine("4. Додати нову рубрику");
                                        Console.WriteLine("5. Додати нову категорію");
                                        Console.WriteLine("6. Додати нову підкатегорію");
                                        Console.WriteLine("7. Додати новий тег");
                                        Console.WriteLine("8. Змінити пароль");
                                        Console.WriteLine("9. Видалити акаунт");
                                        Console.WriteLine("0. Вийти з акаунту");

                                        try
                                        {
                                            int choose2 = int.Parse(Console.ReadLine());
                                            switch(choose2)
                                            {

                                                case 0:
                                                    userCycle = true;
                                                    break;
                                                case 1:
                                                    Console.WriteLine("Введіть заголовок:");
                                                    string title = Console.ReadLine();
                                                    Console.WriteLine("Введіть опис:");
                                                    string desc = Console.ReadLine();
                                                    Console.WriteLine("Введіть назву категорії:");
                                                    string categoryName = Console.ReadLine();
                                                    Console.WriteLine("Введіть назву підкатегорії:");
                                                    string subcategoryName = Console.ReadLine();
                                                    Console.WriteLine("Введіть теги через кому:");
                                                    string[] tagsInput = Console.ReadLine().Split(',');
                                                    var tagList = new List<string>(tagsInput.Select(t => t.Trim()));

                                                    if (facade.AddAnnouncement(title, desc, categoryName, subcategoryName, tagList, username2))
                                                        Console.WriteLine("Оголошення створено");
                                                    else
                                                        Console.WriteLine("Не вдалося створити оголошення");
                                                    break;

                                                case 2:
                                                    string userAnnouncements = facade.FindUsersAnnouncements(username2);
                                                    Console.WriteLine(string.IsNullOrEmpty(userAnnouncements) ? "У вас немає оголошень" : userAnnouncements);
                                                    break;

                                                case 3:
                                                    Console.WriteLine("Введіть назву оголошення для видалення:");
                                                    string titleForDel = Console.ReadLine();
                                                    

                                                    if (facade.DeleteAnnouncement(titleForDel, username2))
                                                        Console.WriteLine("Оголошення успішно видалено");
                                                    else
                                                        Console.WriteLine("Не вдалося видалити оголошення");
                                                    break;

                                                case 4:
                                                    Console.WriteLine("Введіть назву рубрики:");
                                                    string heading = Console.ReadLine();
                                                    if (facade.AddHeading(heading))
                                                        Console.WriteLine("Рубрику додано");
                                                    else
                                                        Console.WriteLine("Не вдалося додати рубрику");
                                                    break;

                                                case 5:
                                                    Console.WriteLine("Введіть назву категорії:");
                                                    string category = Console.ReadLine();
                                                    Console.WriteLine("Введіть назву рубрики, до якої належить категорія:");
                                                    string parentHeading = Console.ReadLine();
                                                    if (facade.AddCategory(category, parentHeading))
                                                        Console.WriteLine("Категорію додано");
                                                    else
                                                        Console.WriteLine("Не вдалося додати категорію");
                                                    break;

                                                case 6:
                                                    Console.WriteLine("Введіть назву підкатегорії:");
                                                    string subcat = Console.ReadLine();
                                                    Console.WriteLine("Введіть назву категорії, до якої належить підкатегорія:");
                                                    string parentCategory = Console.ReadLine();
                                                    if (facade.AddSubcategory(subcat, parentCategory))
                                                        Console.WriteLine("Підкатегорію додано");
                                                    else
                                                        Console.WriteLine("Не вдалося додати підкатегорію");
                                                    break;

                                                case 7:
                                                    Console.WriteLine("Введіть назву тегу:");
                                                    string tagName = Console.ReadLine();
                                                    if (facade.AddTag(tagName))
                                                        Console.WriteLine("Тег додано");
                                                    else
                                                        Console.WriteLine("Не вдалося додати тег");
                                                    break;

                                                case 8:
                                                    Console.WriteLine("Введіть новий пароль:");
                                                    string newPassword = Console.ReadLine();
                                                    if (facade.ChangeUserPassword(username2, newPassword))
                                                        Console.WriteLine("Пароль змінено");
                                                    else
                                                        Console.WriteLine("Не вдалося змінити пароль");
                                                    break;

                                                case 9:
                                                    if (facade.DeleteUser(username2))
                                                    {
                                                        Console.WriteLine("Акаунт видалено");
                                                        userCycle = false;
                                                    }
                                                    else
                                                        Console.WriteLine("Не вдалося видалити акаунт");
                                                    break;

                                                default:
                                                    Console.WriteLine("Невірна опція");
                                                    break;
                                            }
                                        
                                        }
                                         catch(EntityNotFoundException enfx)
                                        {

                                        }
                                        catch(ValidationException vex)
                                        {
                                            
                                        }
                                        catch(Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                                else
                                    Console.WriteLine("Не вдалося увійти в акаунт");
                          
                            break;

                        case 3:
                            string announcements = facade.FindAllAnnouncements();
                            if(announcements != null)
                            {
                                Console.WriteLine("Список оголошень:");
                                Console.WriteLine(announcements);
                            }
                            else
                                Console.WriteLine("Оголошень поки немає");
                            break;

                        case 4:
                            string headings = facade.FindAllHeadings();
                            if(headings != null)
                            {
                                Console.WriteLine("Список рубрик:");
                                Console.WriteLine(headings);
                            }
                            else
                                Console.WriteLine("Рубрик поки немає");
                            break;

                        case 5:
                            string categories = facade.FindAllCategories();
                            if (categories != null)
                            {
                                Console.WriteLine("Список категорій:");
                                Console.WriteLine(categories);
                            }
                            else
                                Console.WriteLine("Категорій поки немає");

                                break;
                        case 6:
                            string subcategories = facade.FindAllSubcategories();
                            if (subcategories != null)
                            {
                                Console.WriteLine("Список підкатегорій:");
                                Console.WriteLine(subcategories);
                            }
                            else
                                Console.WriteLine("Підкатегорій поки немає");
                            break;
                        case 7:
                            Console.WriteLine("Введіть тег:");
                            string tag = Console.ReadLine();
                            string announcementsByTag = facade.FindAnnouncementByTag(tag);

                            if (string.IsNullOrEmpty(announcementsByTag))
                            {
                                Console.WriteLine("Оголошень за таким тегом не знайдено.");
                            }
                            else
                            {
                                Console.WriteLine(announcementsByTag);
                            }
                            break;

                    }

                
                }
                catch(EntityNotFoundException entf)
                {
                    Console.WriteLine(entf.Message);
                }
                catch (ValidationException vex)
                {
                    Console.WriteLine(vex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

    /*    public void TestInfoForDB()
        {
            facade.AddHeading("Нерухомість");
            facade.AddCategory("Продаж", "Нерухомість");
            facade.AddCategory("Оренда", "Нерухомість");
            facade.AddSubcategory("Квартира", "Оренда");
            facade.AddSubcategory("Будинок", "Продаж");
            facade.AddSubcategory("Комерційна нерухомість", "Продаж");
            facade.AddSubcategory("Земельна ділянка", "Продаж");
            facade.AddSubcategory("Гараж", "Продаж");

            facade.AddHeading("Транспорт");
            facade.AddCategory("Продаж транспорту", "Транспорт");
            facade.AddCategory("Транспортні послуги", "Транспорт");
            facade.AddCategory("Оренда транспорту", "транспорт");
            facade.AddSubcategory("Легкові авто", "Оренда транспорту");
            facade.AddSubcategory("Мотоцикли", "Продаж транспорту");
            facade.AddSubcategory("Вантажівки", "Продаж транспорту");
            facade.AddSubcategory("Спецтехніка", "Продаж транспорту");
            facade.AddSubcategory("Автозапчастини", "Продаж транспорту");

            facade.AddHeading("Робота");
            facade.AddCategory("Шукаю роботу", "Робота");
            facade.AddCategory("Пропоную роботу", "Робота");
            facade.AddSubcategory("Програмування", "Пропоную роботу");
            facade.AddSubcategory("Будівництво", "Пропоную роботу");
            facade.AddSubcategory("Продажі", "Пропоную роботу");
            facade.AddSubcategory("Водії", "Шукаю роботу");


            facade.AddHeading("Товари");
            facade.AddCategory("Продаж товарів", "Товари");
            facade.AddCategory("Обмін товарів", "Товари");
            facade.AddSubcategory("Електроніка", "Продаж товарів");
            facade.AddSubcategory("Одяг", "Продаж товарів");
            facade.AddSubcategory("Дитячі товари", "Обмін товарів");
            facade.AddSubcategory("Меблі", "Продаж товарів");
            facade.AddSubcategory("Побутова техніка", "Продаж товарів");

            facade.AddHeading("Послуги");
            facade.AddCategory("Пропоную послуги", "Послуги");
            facade.AddCategory("Шукаю послугу", "Послуги");
            facade.AddSubcategory("Ремонт", "Пропоную послуги");
            facade.AddSubcategory("Навчання", "Пропоную послуги");
            facade.AddSubcategory("Переклад", "Пропоную послуги");
            facade.AddSubcategory("Монтаж", "Шукаю послугу");

            facade.AddTag("Новий");
            facade.AddTag("Б/в");
            facade.AddTag("Терміново");
            facade.AddTag("Доставка");
            facade.AddTag("Недорого");
            facade.AddTag("Професіонал");
            facade.AddTag("Гарантія");
            facade.AddTag("Без посередників");
            facade.AddTag("Онлайн");
            facade.AddTag("З фото");

            facade.AddUser("admin", "12345678");
            facade.AddUser("user1", "pass1234");
            facade.AddUser("user2", "qwerty56");
            facade.AddUser("user3", "secure789");
            var testList = new List<string> {  "Недорого"  };
            facade.AddAnnouncement("Ремонт техніки", 
                "Комп'ютери, планшети, телефони та інші гаджети", 
                "Пропоную послуги", 
                "Ремонт",
                testList, 
                "admin");


            var tags1 = new List<string> { "Гарантія", "Професіонал" };
            facade.AddAnnouncement(
                "Укладання плитки",
                "Пропоную якісне укладання плитки у ванній кімнаті та кухні",
                "Пропоную послуги",
                "Ремонт",
                tags1,
                "admin"
            );

            var tags2 = new List<string> { "Б/в", "Недорого" };
            facade.AddAnnouncement(
                "Продам мотоцикл Honda",
                "Модель 2015 року, в хорошому стані",
                "Продаж транспорту",
                "Мотоцикли",
                tags2,
                "user2"
            );

            var tags3 = new List<string> { "З фото", "Онлайн" };
            facade.AddAnnouncement(
                "Онлайн уроки англійської",
                "Індивідуальні заняття з досвідченим викладачем",
                "Пропоную послуги",
                "Навчання",
                tags3,
                "user1"
            );

            var tags4 = new List<string> { "Терміново", "Без посередників" };
            facade.AddAnnouncement(
                "Оренда квартири в центрі",
                "1-кімнатна квартира, всі зручності",
                "Оренда",
                "Квартира",
                tags4,
                "user4"
            );
        }
    */
    }

    
}
