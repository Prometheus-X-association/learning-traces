using Codice.Client.Common.Cloaked;
using UnityEngine;

namespace PXRLT
{
    public class UserInformation
    {
        private string _fullname;
        private string _email;

        #region Accessors
        public string Fullname { get { return _fullname; } set { _fullname = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        #endregion
    }
}
