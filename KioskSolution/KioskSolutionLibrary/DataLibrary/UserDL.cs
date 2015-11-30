using KioskSolutionLibrary.ModelLibrary;
using KioskSolutionLibrary.ModelLibrary.EntityFrameworkLibrary;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskSolutionLibrary.DataLibrary
{
    public class UserDL
    {
        public UserDL()
        {

        }

        public static bool Save(User user)
        {
            try
            {
                string password = user.HashedPassword;
                user.HashedPassword = PasswordHash.MD5Hash(password);
                using (var context = new KioskWebDBEntities())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        context.Users.Add(user);
                        context.SaveChanges();

                        KioskSolutionLibrary.ModelLibrary.EntityFrameworkLibrary.ThirdPartyData.User thirdPartyUser = new KioskSolutionLibrary.ModelLibrary.EntityFrameworkLibrary.ThirdPartyData.User();

                        thirdPartyUser.UserName = user.Username;
                        thirdPartyUser.Password = password;
                        thirdPartyUser.UserType = "1";
                        thirdPartyUser.status = 1;
                        thirdPartyUser.OfficialEmail = user.Email;

                        if (!ThirdPartyDL.UserExists(thirdPartyUser))
                            ThirdPartyDL.Save(thirdPartyUser);

                        transaction.Commit();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool UserExists(User user)
        {
            try
            {
                var existingUser = new User();
                using (var context = new KioskWebDBEntities())
                {
                    existingUser = context.Users
                                    .Where(t => t.Username.Equals(user.Username))
                                    .FirstOrDefault();
                }

                if (existingUser == null)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static User RetrieveUserByUsername(string username)
        {
            try
            {
                var existingUser = new User();
                using (var context = new KioskWebDBEntities())
                {
                    existingUser = context.Users
                                    .Where(t => t.Username.Equals(username))
                                    .FirstOrDefault();
                }

                return existingUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ChangePassword(string username, string newPassword)
        {
            try
            {
                string newHashedPassword = PasswordHash.MD5Hash(newPassword);

                User existingUser = new User();
                using (var context = new KioskWebDBEntities())
                {
                    existingUser = context.Users
                                    .Where(t => t.Username == username)
                                    .FirstOrDefault();
                }

                if (existingUser != null)
                {
                    existingUser.HashedPassword = newHashedPassword;
                    existingUser.FirstTime = false;

                    using (var context = new KioskWebDBEntities())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            context.Entry(existingUser).State = EntityState.Modified;
                            context.SaveChanges();

                            KioskSolutionLibrary.ModelLibrary.EntityFrameworkLibrary.ThirdPartyData.User thirdPartyUser = new KioskSolutionLibrary.ModelLibrary.EntityFrameworkLibrary.ThirdPartyData.User();

                            thirdPartyUser.UserName = username;                            
                            if (ThirdPartyDL.UserExists(thirdPartyUser))
                                ThirdPartyDL.ChangePassword(username, newPassword);

                            transaction.Commit();
                        }
                    }

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<User> RetrieveUsers()
        {
            try
            {
                using (var context = new KioskWebDBEntities())
                {
                    var users = context.Users
                                        .Include("Branch")
                                        .Include("Role.RoleFunctions.Function")
                                        .ToList();

                    return users;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static User AuthenticateUser(string username, string hashedPassword)
        {
            try
            {
                using (var context = new KioskWebDBEntities())
                {
                    var users = context.Users
                                        .Where(f => f.Username == username && f.HashedPassword == hashedPassword);

                    return users.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool Update(User user)
        {
            try
            {
                User existingUser = new User();
                using (var context = new KioskWebDBEntities())
                {
                    existingUser = context.Users
                                    .Where(t => t.ID == user.ID)
                                    .FirstOrDefault();
                }

                if (existingUser != null)
                {
                    existingUser.Email = user.Email;
                    existingUser.Gender = user.Gender;
                    existingUser.PhoneNumber = user.PhoneNumber;
                    existingUser.Lastname = user.Lastname;
                    existingUser.Othernames = user.Othernames;
                    existingUser.UserRole = user.UserRole;
                    existingUser.UserBranch = user.UserBranch;

                    using (var context = new KioskWebDBEntities())
                    {
                        context.Entry(existingUser).State = EntityState.Modified;

                        context.SaveChanges();
                    }

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
