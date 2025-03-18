using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PXRLT.Test
{
    public class ConnexionMenu : MonoBehaviour
    {
        private PXRLTManager _manager;
        [SerializeField]
        private UIManager _uiManager;

        [Header("UI Components")]
        [SerializeField]
        private TMP_InputField _fullNameField = null;
        [SerializeField]
        private TMP_InputField _emailAdressField = null;
        [SerializeField]
        private TMP_InputField _exerciseIDField = null;
        [SerializeField]
        private TMP_InputField _lmsSessionName = null;
        [SerializeField]
        private Button _startExerciseButton = null;
        [SerializeField]
        private Toggle _sendAnonymousTracksToggle = null;

        private void Start()
        {
            _manager = PXRLTManager.Instance;
            if (_manager == null)
            {
                Debug.LogError("You should have a PXRLTManager to run this script");
                return;
            }
            CreateDefaultContext();
        }

        private void OnEnable()
        {
            _fullNameField.text = "Test";
            _emailAdressField.text = "test@mimbus.com";
            _exerciseIDField.text = "0000-0000-0000-0000";
            _lmsSessionName.text = "moodle-12345";
        }

        private void CreateDefaultContext()
        {
            foreach (LanguageData language in _manager.LanguagesAvailable)
            {
                // Link to parent courses : http://example.com/courses/XXXX
                _uiManager.ContextActivities.ParentActivityId = $"http://example.com/courses/XXXX";
                _uiManager.ContextActivities.ParentActivityNamePairs.Add(new LanguagePair(language, $"Name for {language.FullName}"));
                _uiManager.ContextActivities.ParentActivityDescriptionPairs.Add(new LanguagePair(language, $"Description for {language.FullName}"));
                _uiManager.ContextActivities.ActivityCategoryPairs.Add(new LanguagePair(language, $"Category for {language.FullName}"));
            }
        }

        public void StartExercise()
        {
            _uiManager.UserInformation.Fullname = _fullNameField.text;
            _uiManager.UserInformation.Email = _emailAdressField.text;

            _manager.InitializeContext(_uiManager.ContextActivities, _lmsSessionName.text);
            _manager.InitializeUserInformation(_sendAnonymousTracksToggle.isOn, _uiManager.UserInformation);

            _uiManager.CurrentActivity = new Activity();
            _uiManager.CurrentActivity.ExerciseId = _exerciseIDField.text;
            _uiManager.CurrentActivity.RegistrationId = System.Guid.NewGuid().ToString();
            _uiManager.CurrentActivity.PlatformName = Application.productName;
            _uiManager.CurrentActivity.LanguageUsed = _manager.LanguagesAvailable.First();
            foreach (LanguageData language in _manager.LanguagesAvailable)
            {
                _uiManager.CurrentActivity.NamePairs.Add(new LanguagePair(language, $"Name for {language.FullName}"));
                _uiManager.CurrentActivity.DescriptionPairs.Add(new LanguagePair(language, $"Description for {language.FullName}"));
            }

            _manager.SendInitializeTrace(_uiManager.CurrentActivity);
        }
    }
}
