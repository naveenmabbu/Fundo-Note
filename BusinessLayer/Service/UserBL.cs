namespace BusinessLayer.Service
{
    using System;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interface;

    /// <summary>
    /// ok ok
    /// </summary>
    /// <seealso cref="BusinessLayer.Interface.IUserBL" />
    public class UserBL : IUserBL
    {
        /// <summary>
        /// Gets or sets
        /// </summary>
        private readonly IUserRL userRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserBL"/> class.
        /// </summary>
        /// <param name="userRL">The user rl.</param>
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// null null.
        /// </returns>
        public string ForgetPassword(string email)
        {
            try
            {
                return this.userRL.ForgetPassword(email);
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
        /// <returns>
        /// null null.
        /// </returns>
        public string login(UserLogin userLogin)
        {
            return this.userRL.login(userLogin);
        }

        /// <summary>
        /// Registrations the specified user.
        /// </summary>
        /// <param name="User">The user.</param>
        /// <returns>
        /// null null.
        /// </returns>
        public UserEntity Registration(UserRegistration User)
        {
            try
            {
                return this.userRL.Registration(User);
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
        /// <returns>
        /// null null.
        /// </returns>
        public bool ResetPassword(ResetPass resetPass, string email)
        {
            try
            {
                return this.userRL.ResetPassword(resetPass,email);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
