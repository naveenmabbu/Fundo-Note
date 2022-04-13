namespace FundoNote.Controllers
{
    using System;
    using System.Security.Claims;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
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
        private readonly ILogger<UserController> _logger;

        string sessionName = "fullName";
        string sessionEmail = "email";

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userBL">The userBL.</param>
        public UserController(IUserBL userBL)//, ILogger<UserController> logger)
        {
            this.userBL = userBL;
            //this._logger = logger;
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
                    string sName = HttpContext.Session.GetString(sessionName);
                    string sEmail = HttpContext.Session.GetString(sessionEmail);
                    //_logger.LogInformation("Register successfull");
                    return this.Ok(new ExceptionModeel<string> { Status = true, Message = "Registration Successfull", Data = "Session || Name : " + sName + "|| Email Id : " + sEmail });
                }
                else
                {
                    //_logger.LogError("Register unsuccessfull");
                    return this.BadRequest(new ExceptionModeel<String> { Status = false, Message = "Registration unSuccessfull"});
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.ToString());
                return this.BadRequest(new ExceptionModeel<String> { Status = false, Message = ex.Message });
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
                    //_logger.LogInformation("login successfull");
                    return this.Ok(new ExceptionModeel<string> { Status = true, Message = "Mail Login Successfull", Data = result });
                }
                else
                {
                    //_logger.LogError("login unsuccessfull");
                    return this.BadRequest(new ExceptionModeel<String> { Status = false, Message = "enter proper login details" });
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.ToString());
                return this.BadRequest(new ExceptionModeel<String> { Status = false, Message = ex.Message });
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
                    //_logger.LogInformation("forget successfull");
                    return this.Ok(new ExceptionModeel<string> { Status = true, Message = "Forget password Successfull and token sent to mail" });
                }
                else
                {
                    //_logger.LogError("forget successfull");
                    return this.BadRequest(new ExceptionModeel<String> { Status = false, Message = " Forget password UnSuccessfull give proper username" });
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.ToString());
                return this.BadRequest(new ExceptionModeel<String> { Status = false, Message = ex.Message});
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="confirmpassword">The confirmpassword.</param>
        /// <returns>null null.</returns>
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(ResetPass resetPass)
        {
            try
            {
                //var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                string email = "Tonystark69@gmail.com";
                var user = userBL.ResetPassword(resetPass,email);
                if (!user)
                {
                    //_logger.LogError("reset unsuccessfull");
                    return this.BadRequest(new ExceptionModeel<String> { Status = true, Message = " Reset password UnSuccessfull give proper username" });
                    
                }
                else
                {
                    //_logger.LogInformation("reset successfull");
                    return this.Ok(new ExceptionModeel<string> { Status = false, Message = "Reset password Successfull and token sent to mail" });

                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.ToString());
                return this.BadRequest(new ExceptionModeel<String> { Status = false, Message = ex.Message});
            }
        }
    }
}
