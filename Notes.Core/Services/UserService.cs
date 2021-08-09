using System;
using System.Threading.Tasks;
using Notes.Core.Interfaces.IServices;
using Notes.Core.Interfaces;
using Notes.Core.Entities;

namespace Notes.Core.Services
{
    public class UserService : IUserService
    {

        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetUser(int id)
        {
            try
            {
                return await _unitOfWork.UserRepository.GetById(id);
            }catch(Exception ex)
            {
                throw new Exception("Error in server get ");
            }
        }
        public async Task InsertUser(User user)
        {
            try
            {
                user.Created_time = DateTime.Now;
                user.Updated_time = DateTime.Now;
                user.Deleted_time = null;
                user.Token = null;
                await _unitOfWork.UserRepository.Add(user);
                await _unitOfWork.SaveChangesAsysc();
            }
            catch (Exception e)
            {
                throw new Exception("Error in server register, duplicate");
            }
        }
        public async Task<bool> UpdateUser(User user)
        {
            try
            {
                var exitingUser = await _unitOfWork.UserRepository.GetById(user.IdUser);

                if(exitingUser == null)
                {
                    return false;
                }
                exitingUser.Name = user.Name;
                exitingUser.Updated_time = DateTime.Now;
                exitingUser.Password = user.Password != null  ? user.Password : exitingUser.Password ;

                _unitOfWork.UserRepository.Update(exitingUser);
                await _unitOfWork.SaveChangesAsysc();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("Error in server update user");
            }

        }
        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                await _unitOfWork.UserRepository.Delete(id);
                await _unitOfWork.SaveChangesAsysc();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error in server delete user");
            }
        }
        public async Task<User> GetLoginByCredentials(UserLogin userLogin)
        {
            try
            {
                return await _unitOfWork.UserRepository.GetLoginByCredentials(userLogin);
            }
            catch (Exception e)
            {
                throw new Exception("Error in server get login");
            }
        }
    }
}
