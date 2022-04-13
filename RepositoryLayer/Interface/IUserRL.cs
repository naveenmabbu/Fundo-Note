namespace RepositoryLayer.Interface
{
    using CommonLayer.Model;
    using RepositoryLayer.Entity;

    /// <summary>
    /// ok ok
    /// </summary>
    public interface IUserRL
    {
        /// <summary>
        /// Registrations the specified user.
        /// </summary>
        /// <param name="User">The user.</param>
        /// <returns>register register</returns>
        public UserEntity Registration(UserRegistration User);

        /// <summary>
        /// Logins the specified user login.
        /// </summary>
        /// <param name="userLogin">The user login.</param>
        /// <returns>login login</returns>
        public string login(UserLogin userLogin);

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>forget forget</returns>
        public string ForgetPassword(string email);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="conformPassword">The conform password.</param>
        /// <returns>reset reset</returns>
        public bool ResetPassword(ResetPass resetPass, string email);
    }
}
