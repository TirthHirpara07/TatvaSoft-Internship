using Data_Logic_Layer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Logic_Layer
{
    public class DALAdminUser
    {
        private readonly AppDbContext _context;

        public DALAdminUser(AppDbContext context)
        {
            _context = context;
        }

        public string AddUser(User user)
        {
            var result = "";
            try
            {
                var userEmailExists = _context.User.FirstOrDefault(x => !x.IsDeleted && x.EmailAddress == user.EmailAddress);
                var newUser = new User();
                if (userEmailExists == null)
                {
                    newUser.FirstName = user.FirstName;
                    newUser.LastName = user.LastName;
                    newUser.PhoneNumber = user.PhoneNumber;
                    newUser.EmailAddress = user.EmailAddress;
                    newUser.Password = user.Password;
                    newUser.UserType = user.UserType;
                    newUser.CreatedDate = DateTime.UtcNow;
                    newUser.ModifiedDate = DateTime.UtcNow;
                    newUser.IsDeleted = false;
                  
                    _context.User.Add(newUser);
                    _context.SaveChanges();
                    
                    var newUserDetail = new UserDetail
                    {
                        UserId = newUser.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        EmailAddress = user.EmailAddress,
                        UserType = user.UserType,
                        EmployeeId = newUser.Id.ToString(),
                        Department = "IT",
                        Status = true,
                        IsDeleted= false
                    };
                    _context.UserDetail.Add(newUserDetail);
                    _context.SaveChanges();

                    result = "User Add Suceessfully.";
                }
                else
                {
                    result = "Email is already exists.";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public List<UserDetail> GetUserList()
        {
            var userDetailList = from u in _context.User
                                 join ud in _context.UserDetail on u.Id equals ud.UserId into userDetailGroup
                                 from userDetail in userDetailGroup.DefaultIfEmpty()
                                 where !u.IsDeleted && u.UserType == "user" && !userDetail.IsDeleted
                                 select new UserDetail
                                 {
                                     UserId =u.Id,
                                     FirstName = u.FirstName,
                                     LastName = u.LastName,
                                     PhoneNumber = u.PhoneNumber,
                                     EmailAddress = u.EmailAddress,
                                     EmployeeId = userDetail.EmployeeId,
                                     Department = userDetail.Department,
                                     Status = userDetail.Status,
                                 };
            return userDetailList.ToList();
        }
        public List<UserDetail> GetUserById(int id)
        {
            var userDetailList = from u in _context.User where u.Id == id 
                                 join ud in _context.UserDetail on u.Id equals ud.UserId into userDetailGroup
                                 from userDetail in userDetailGroup.DefaultIfEmpty()
                                 where !u.IsDeleted && u.UserType == "user" && !userDetail.IsDeleted
                                 select new UserDetail
                                 {
                                     UserId = u.Id,
                                     FirstName = u.FirstName,
                                     LastName = u.LastName,
                                     PhoneNumber = u.PhoneNumber,
                                     EmailAddress = u.EmailAddress,
                                     EmployeeId = userDetail.EmployeeId,
                                     Department = userDetail.Department,
                                     Status = userDetail.Status,
                                 };
            return userDetailList.ToList();
        }

        public async Task<string> DeleteUser(int userId)
        {
            try
            {
                var result = string.Empty;
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var userDetail = await _context.UserDetail.FirstOrDefaultAsync(x => x.UserId == userId);
                        if (userDetail != null)
                        {
                            userDetail.IsDeleted = true;
                        }
                        var user = await _context.User.FirstOrDefaultAsync(x => x.Id == userId);
                        if (user != null)
                        {
                            user.IsDeleted = true;
                        }

                        await _context.SaveChangesAsync();

                        await transaction.CommitAsync();

                        result = "Delete user successfully";
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw ex;
                    }

                }
                return result;
            }
            catch(Exception ex)
            {
                throw;
            }

        }
        public string UpdateUser(User user)
        {
            var result = "";
            try
            {
                var userEmailExists = _context.User.FirstOrDefault(x => !x.IsDeleted && x.Id == user.Id);
                if (userEmailExists != null)
                {
                    
                    userEmailExists.FirstName = user.FirstName;
                    userEmailExists.LastName = user.LastName;
                    userEmailExists.PhoneNumber = user.PhoneNumber;
                    userEmailExists.Password = user.Password;
                    userEmailExists.ModifiedDate = DateTime.UtcNow;

                    _context.User.Update(userEmailExists);
                    _context.SaveChanges();
                    var userDetailExist = _context.UserDetail.FirstOrDefault(x => !x.IsDeleted && x.UserId == userEmailExists.Id);
                    if (userDetailExist != null)
                    {

                        userDetailExist.FirstName = user.FirstName;
                        userDetailExist.LastName = user.LastName;
                        userDetailExist.PhoneNumber = user.PhoneNumber;
                        _context.SaveChanges();
                        _context.UserDetail.Update(userDetailExist);

                    }
                    result = "User Updated Suceessfully.";

                }
                else
                {
                    result = "Email is Doesnot exists.";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

    }
}
