using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Xamarin.Essentials;

namespace RepositoryLayer.Service
{
    public class UserRL:IUserRL
    {
        private readonly FundoContext fundoContext;
        private readonly IConfiguration _Toolsettings;
        public UserRL(FundoContext fundoContext,IConfiguration _Toolsettings)
        {
            this.fundoContext = fundoContext;
            this._Toolsettings = _Toolsettings;
        }

        public UserEntity GetEmail(string collabEmail)
        {
            try
            {
                var result = fundoContext.User.FirstOrDefault(e => e.Email == collabEmail);

                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public UserEntity Registration(UserRegistration User)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = User.FirstName;
                userEntity.LastName = User.LastName;
                userEntity.Email = User.Email;
                userEntity.Password = this.EncryptPassword(User.Password);
                fundoContext.Add(userEntity);
                int result = fundoContext.SaveChanges();
                if (result > 0)
                    return userEntity;
                else 
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private string EncryptPassword(string password)
        {
            try
            {
                byte[] encode = new byte[password.Length];
                encode = Encoding.UTF8.GetBytes(password);
                string encryptPass = Convert.ToBase64String(encode);
                return encryptPass;
            }
            catch (Exception)
            {
                throw;
            }
        }
       

        public string login(UserLogin userLogin)
        {
            try
            {
                // if Email and password is empty return null. 
                if (string.IsNullOrEmpty(userLogin.Email) || string.IsNullOrEmpty(userLogin.Password))
                {
                    return null;
                }
                var result = fundoContext.User.Where(x => x.Email == userLogin.Email).FirstOrDefault();
                string dcryptPass = this.DecryptPassword(result.Password);
                if (result != null && dcryptPass == userLogin.Password)
                {
                    string token = GenerateSecurityToken(result.Email, result.Id);
                    return token;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private string GenerateSecurityToken(string Email, long Id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Toolsettings["Jwt:secretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.Email,Email),
                new Claim("Id",Id.ToString())
            };
            var token = new JwtSecurityToken(_Toolsettings["Jwt:Issuer"],
              _Toolsettings["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        public string ForgetPassword(string email)
        {
            try
            {
                var user = fundoContext.User.Where(x => x.Email == email).FirstOrDefault();
                if(user!=null)
                {
                    var token = GenerateSecurityToken(user.Email, user.Id);
                    new Msmq().sender(token);
                    return token;
                }
                else
                {
                    return null;
                }

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
                if(password.Equals(conformPassword))
                {
                    var user = fundoContext.User.Where(x => x.Email == email).FirstOrDefault();
                    user.Password = conformPassword;
                    fundoContext.SaveChanges();
                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
