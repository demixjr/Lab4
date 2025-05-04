using DAL;

namespace BLL
{
    public class UserService
    {

        public bool AddUser(BoardContext context, User user)
        {
            if (FindUserByUsername(context, user.Username) != null)
                throw new ValidationException("Такий користувач вже існує");

            context.Users.Add(user);
            context.SaveChanges();
            return true;
        }

        public User FindUserByUsername(BoardContext context, string username)
        {
            return context.Users.Find(username);
        }
        public bool ChangeUserPassword(BoardContext context, string username, string oldPassword, string newPassword)
        {
            User user = FindUserByUsername(context, username);
            if (user != null && user.Password == oldPassword)
            {
                user.Password = newPassword;
                context.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public bool DeleteUser(BoardContext context, string username, string password)
        {
            User user = FindUserByUsername(context, username);
            if (user != null && user.Password == password)
            {
                context.Users.Remove(user);
                context.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}
