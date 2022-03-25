namespace FundoNote.Controllers
{
    using System;
    using System.Security.Claims;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Mvc;
    using RepositoryLayer.Entity;

    /// <summary>
    /// ok ok
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// The userBL
        /// </summary>
        private readonly IUserBL userBL;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userBL">The userBL.</param>
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        /// <summary>
        /// Registrations the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>null null.</returns>
        [HttpPost("Register")]
        public IActionResult Registration(UserRegistration user)
        {
            try
            {
                var result = this.userBL.Registration(user);
                if (result != null)
                {
                    return this.Ok(new ExceptionModeel<UserEntity> { Status = true, Message = "Registration Successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new ExceptionModeel<String> { Status = true, Message = "Registration Successfull"});
                }
            }
            catch (Exception)
            {
                return this.BadRequest(new ExceptionModeel<String> { Status = true, Message = "Registration Successfull" });
            }
        }

        /// <summary>
        /// Logins the specified user login.
        /// </summary>
        /// <param name="userLogin">The user login.</param>
        /// <returns>null null.</returns>
        [HttpPost("Login")]
        public IActionResult login(UserLogin userLogin)
        {
            try
            {
                var result = this.userBL.login(userLogin);
                if (result != null)
                {
                    return this.Ok(new ExceptionModeel<string> { Status = true, Message = "Login Successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new ExceptionModeel<String> { Status = true, Message = "Login UnSuccessfull" });
                }
            }
            catch (Exception)
            {
                return this.BadRequest(new ExceptionModeel<String> { Status = true, Message = " Login UnSuccessfull" });
            }
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>null null.</returns>
        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var result = this.userBL.ForgetPassword( email);
                if (result != null)
                {
                    return this.Ok(new ExceptionModeel<string> { Status = true, Message = "Forget Successfull"});
                }
                else
                {
                    return this.BadRequest(new ExceptionModeel<String> { Status = true, Message = " Forget UnSuccessfull" });
                }
            }
            catch (Exception)
            {
                return this.BadRequest(new ExceptionModeel<String> { Status = true, Message = " Forget UnSuccessfull" });
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="confirmpassword">The confirmpassword.</param>
        /// <returns>null null.</returns>
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(string password, string confirmpassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var user = userBL.ResetPassword(email, password, confirmpassword);
                if (!user)
                {
                    return this.Ok(new ExceptionModeel<string> { Status = true, Message = "Reset Successfull" });
                }
                else
                {
                    return this.BadRequest(new ExceptionModeel<String> { Status = true, Message = " Reset UnSuccessfull" });
                }
            }
            catch (Exception)
            {
                return this.BadRequest(new ExceptionModeel<String> { Status = true, Message = " Reset UnSuccessfull" });
            }
        }
    }
}
