namespace RepositoryLayer.Service
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using CommonLayer.Model;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using RepositoryLayer.Context;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interface;

    /// <summary>
    /// ok ok
    /// </summary>
    /// <seealso cref="RepositoryLayer.Interface.IUserRL" />
    public class UserRL : IUserRL
    {
        /// <summary>
        /// The fundo context
        /// </summary>
        private readonly FundoContext fundoContext;

        /// <summary>
        /// Gets or sets
        /// </summary>
        private readonly IConfiguration _Toolsettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRL"/> class.
        /// </summary>
        /// <param name="fundoContext">The fundo context.</param>
        /// <param name="_Toolsettings">The toolsettings.</param>
        public UserRL(FundoContext fundoContext, IConfiguration _Toolsettings)
        {
            this.fundoContext = fundoContext;
            this._Toolsettings = _Toolsettings;
        }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <param name="collabEmail">The collab email.</param>
        /// <returns>get get</returns>
        public UserEntity GetEmail(string collabEmail)
        {
            try
            {
                var result = this.fundoContext.User.FirstOrDefault(e => e.Email == collabEmail);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Registrations the specified user.
        /// </summary>
        /// <param name="User">The user.</param>
        /// <returns>regi regi</returns>
        public UserEntity Registration(UserRegistration User)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = User.FirstName;
                userEntity.LastName = User.LastName;
                userEntity.Email = User.Email;
                userEntity.Password = this.EncryptPassword(User.Password);
                this.fundoContext.Add(userEntity);
                int result = this.fundoContext.SaveChanges();
                if (result > 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Encrypts the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>encr encr</returns>
        public string EncryptPassword(string password)
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

        /// <summary>
        /// Decrypts the password.
        /// </summary>
        /// <param name="encryptpwd">The encryptpwd.</param>
        /// <returns>decr decr</returns>
        public string DecryptPassword(string encryptpwd)
        {
            try
            {
                UTF8Encoding encoder = new UTF8Encoding();
                Decoder utf8Decode = encoder.GetDecoder();
                byte[] toDecodeByte = Convert.FromBase64String(encryptpwd);
                int charCount = utf8Decode.GetCharCount(toDecodeByte, 0, toDecodeByte.Length);
                char[] decodedChar = new char[charCount];
                utf8Decode.GetChars(toDecodeByte, 0, toDecodeByte.Length, decodedChar, 0);
                string PassDecrypt = new string(decodedChar);
                return PassDecrypt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Logins the specified user login.
        /// </summary>
        /// <param name="userLogin">The user login.</param>
        /// <returns>login login</returns>
        public string login(UserLogin userLogin)
        {
            try
            {
                // if Email and password is empty return null. 
                if (string.IsNullOrEmpty(userLogin.Email) || string.IsNullOrEmpty(userLogin.Password))
                {
                    return null;
                }

                var result = this.fundoContext.User.Where(x => x.Email == userLogin.Email).FirstOrDefault();
                string dcryptPass = this.DecryptPassword(result.Password);
                if (result != null && dcryptPass == userLogin.Password)
                {
                    string token = GenerateSecurityToken(result.Email, result.Id);
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

        /// <summary>
        /// Generates the security token.
        /// </summary>
        /// <param name="Email">The email.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>token token.</returns>
        public string GenerateSecurityToken(string Email, long Id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Toolsettings["Jwt:secretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] 
            {
                new Claim(ClaimTypes.Email, Email),
                new Claim("Id", Id.ToString())
            };
            var token = new JwtSecurityToken(_Toolsettings["Jwt:Issuer"], _Toolsettings["Jwt:Issuer"], claims, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>forget forget</returns>
        public string ForgetPassword(string email)
        {
            try
            {
                var user = this.fundoContext.User.Where(x => x.Email == email).FirstOrDefault();
                if (user != null)
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

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="conformPassword">The conform password.</param>
        /// <returns>reet reset</returns>
        public bool ResetPassword(ResetPass resetPass, string email)
        {
            try
            {
                if(resetPass.Password.Equals(resetPass.ConformPassword))
                {
                    var user = this.fundoContext.User.Where(x => x.Email == email).FirstOrDefault();
                    user.Password = this.EncryptPassword(resetPass.ConformPassword);
                    this.fundoContext.SaveChanges();
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
