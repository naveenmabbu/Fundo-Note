namespace FundoNote.Controllers
{
    using System;
    using System.Security.Claims;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Mvc;

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
                    return this.Ok(new { success = true, message = "registration successful", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "registration unsuccessful" });
                }
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
        /// <returns>null null.</returns>
        [HttpPost("Login")]
        public IActionResult login(UserLogin userLogin)
        {
            try
            {
                var result = this.userBL.login(userLogin);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Login successful", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Login unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
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
                    return this.Ok(new { success = true, message = "mail sent succesful", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "enter a valid email" });
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
                    return this.BadRequest(new { success = false, message = "enter a valid email" });
                }
                else
                {
                    return this.Ok(new { success = true, message = "rest password succesful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
