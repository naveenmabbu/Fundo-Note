namespace BusinessLayer.Interface
{
    using CommonLayer.Model;
    using RepositoryLayer.Entity;

    /// <summary>
    /// ok ok
    /// </summary>
    public interface IUserBL
    {
        /// <summary>
        /// Registrations the specified user.
        /// </summary>
        /// <param name="User">The user.</param>
        /// <returns>null null.</returns>
        public UserEntity Registration(UserRegistration User);

        /// <summary>
        /// Logins the specified user login.
        /// </summary>
        /// <param name="userLogin">The user login.</param>
        /// <returns>null null.</returns>
        public string login(UserLogin userLogin);

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>null null.</returns>
        public string ForgetPassword(string email);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="conformPassword">The conform password.</param>
        /// <returns>null null.</returns>
        public bool ResetPassword(ResetPass resetPass, string email);
    }
}
