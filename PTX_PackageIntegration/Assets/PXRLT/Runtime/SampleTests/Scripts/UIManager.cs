using UnityEngine;

namespace PXRLT.Test
{
    public class UIManager : MonoBehaviour
    {
        private UserInformation _userInformation = new UserInformation();
        private ContextActivities _contextActivities = new ContextActivities();
        private Activity _currentActivity = null;
        private Exercise _currentExercise = null;

        #region Accessors
        public UserInformation UserInformation { get { return _userInformation; } set { _userInformation = value; } }
        public ContextActivities ContextActivities { get { return _contextActivities; } set { _contextActivities = value; } }
        public Activity CurrentActivity { get { return _currentActivity; } set { _currentActivity = value; } }
        public Exercise CurrentExercise { get { return _currentExercise; } set { _currentExercise = value; } }
        #endregion
    }
}
