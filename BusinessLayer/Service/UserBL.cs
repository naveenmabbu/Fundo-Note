using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;

        }

        public string ForgetPassword(string email)
        {
            try
            {
                return userRL.ForgetPassword(email);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string login(UserLogin userLogin)
        {
            return userRL.login(userLogin);
        }

        public UserEntity Registration(UserRegistration User)
        {
            try
            {
                return userRL.Registration(User);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ResetPassword(string email, string password, string conformPassword)
        {
            try
            {
                return userRL.ResetPassword(email, password,conformPassword);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
