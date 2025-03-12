namespace PXRLT
{
    /// <summary>
    /// Definition for user information
    /// </summary>
    public class UserInformation
    {
        /// <summary>
        /// Full name of the user
        /// </summary>
        private string _fullname;
        /// <summary>
        /// Email of the user
        /// </summary>
        private string _email;

        #region Accessors
        /// <summary>
        /// Getter / Setter for _fullname
        /// </summary>
        public string Fullname { get { return _fullname; } set { _fullname = value; } }
        /// <summary>
        /// Getter / Setter for _email
        /// </summary>
        public string Email { get { return _email; } set { _email = value; } }
        #endregion
    }
}
