using Newtonsoft.Json;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace PXRLT.Test
{
    public class UnitTest : MonoBehaviour
    {
        [SerializeField]
        private TextAsset _initializedTraceFile = null;
        [SerializeField]
        private TextAsset _eventSuccessTraceFile = null;
        [SerializeField]
        private TextAsset _eventInformationTraceFile = null;
        [SerializeField]
        private TextAsset _eventWarningTraceFile = null;
        [SerializeField]
        private TextAsset _eventErrorTraceFile = null;
        [SerializeField]
        private TextAsset _resultTraceFile = null;

        private bool _waitResponseResult = false;
        private TextAsset _currentTraceFile = null;

        private void Start()
        {
            PXRLTManager.Instance.LogTrace = true;
            PXRLTManager.Instance.OnTraceSend?.AddListener(CompareTraceValue);
        }

        public void RunTest()
        {
            StartCoroutine(RunTestCoroutine());
        }

        private IEnumerator RunTestCoroutine()
        {
            #region Setup tests
            // Creation of user information to send
            UserInformation userInformation = new UserInformation();
            userInformation.Fullname = "Test";
            userInformation.Email = "test@mimbus.com";

            // Creation of context activities
            ContextActivities contextActivities = new ContextActivities();
            // (optionnal) Parent activity Id to link with an LMS for example 
            contextActivities.ParentActivityId = "https://example.com/courses/XXXX";
            foreach (LanguageData language in PXRLTManager.Instance.LanguagesAvailable)
            {
                contextActivities.ParentActivityNamePairs.Add(new LanguagePair(language, $"Name for {language.FullName}"));
                contextActivities.ParentActivityDescriptionPairs.Add(new LanguagePair(language, $"Description for {language.FullName}"));
                contextActivities.ActivityCategoryPairs.Add(new LanguagePair(language, $"Category for {language.FullName}"));
            }

            // Creation of the activity
            Activity activity = new Activity();
            // id : Name of your exercise, unique for each one
            activity.ExerciseId = "EXERCISE_ID";
            // id : GUID to this linked trace to other from the same exercise
            activity.RegistrationId = System.Guid.Empty.ToString(); //(REGISTRATION_ID)
            // Name of your project
            activity.PlatformName = "NAME_OF_YOUR_PLATFORM";
            // language used on languages available (ex : { en-US, English (US) } { fr-FR, French (France) })
            activity.LanguageUsed = PXRLTManager.Instance.LanguagesAvailable.First();
            foreach (LanguageData language in PXRLTManager.Instance.LanguagesAvailable)
            {
                activity.NamePairs.Add(new LanguagePair(language, $"Name for {language.FullName}"));
                activity.DescriptionPairs.Add(new LanguagePair(language, $"Name for {language.FullName}"));
            }

            // Initiliaze context on PXRLTManager
            PXRLTManager.Instance.InitializeContext(contextActivities);
            // Initiliaze user information on PXRLTManager
            PXRLTManager.Instance.InitializeUserInformation(false, userInformation);
            #endregion

            #region Test Initiliaze trace
            // Send Initialize trace
            PXRLTManager.Instance.SendInitializeTrace(activity);
            // Wait initialize trace
            _currentTraceFile = _initializedTraceFile;
            _waitResponseResult = true;
            while (_waitResponseResult)
                yield return null;
            #endregion

            #region Test event success trace
            // Create Success event
            Event evtSuccess = CreateEvent(EventStatus.SUCCESS, $"EVENT_SUCCESS");
            PXRLTManager.Instance.SendEventTrace(activity, evtSuccess);
            // Wait success event trace
            _currentTraceFile = _eventSuccessTraceFile;
            _waitResponseResult = true;
            while (_waitResponseResult)
                yield return null;
            #endregion

            #region Test event information trace
            // Create Information event
            Event evtInformation = CreateEvent(EventStatus.INFO, $"EVENT_INFO");
            PXRLTManager.Instance.SendEventTrace(activity, evtInformation);
            // Wait success event trace
            _currentTraceFile = _eventInformationTraceFile;
            _waitResponseResult = true;
            while (_waitResponseResult)
                yield return null;
            #endregion

            #region Test event warning trace
            // Create Warning event
            Event evtWarning = CreateEvent(EventStatus.WARNING, $"EVENT_WARNING");
            PXRLTManager.Instance.SendEventTrace(activity, evtWarning);
            // Wait success event trace
            _currentTraceFile = _eventWarningTraceFile;
            _waitResponseResult = true;
            while (_waitResponseResult)
                yield return null;
            #endregion

            #region Test event error trace
            // Create error event
            Event evtError = CreateEvent(EventStatus.ERROR, $"EVENT_ERROR");
            PXRLTManager.Instance.SendEventTrace(activity, evtError);
            // Wait success event trace
            _currentTraceFile = _eventErrorTraceFile;
            _waitResponseResult = true;
            while (_waitResponseResult)
                yield return null;
            #endregion

            #region Test result trace
            // Create result
            Result result = new Result();
            result.Score = 1.0f;
            result.ExerciseStatus = ExerciseStatus.SUCCEED;
            result.ResultStatus = ResultStatus.FINISHED;
            foreach (LanguageData language in PXRLTManager.Instance.LanguagesAvailable)
                result.NamePairs.Add(new LanguagePair(language, $"Result for {language.FullName}"));
            result.Sensors.Add(new ResultSensor("SENSOR_ID", 0.5f));
            PXRLTManager.Instance.SendResultTrace(activity, result);
            // Wait success event trace
            _currentTraceFile = _resultTraceFile;
            _waitResponseResult = true;
            while (_waitResponseResult)
                yield return null;
            #endregion
        }

        private Event CreateEvent(EventStatus status, string id)
        {
            Event evt = new Event();
            evt.Id = id;
            evt.Status = status;
            foreach (LanguageData language in PXRLTManager.Instance.LanguagesAvailable)
                evt.NamePairs.Add(new LanguagePair(language, $"Event {status} for {language.FullName}"));
            return evt;
        }

        private void CompareTraceValue(XAPI.StatementStoredResponse response)
        {
            // Timestamp is created when you send it
            // for testing purpose we need to reset it
            response.Statement.Timestamp = "";
            string serializedStatement = JsonConvert.SerializeObject(response.Statement);

            if (_currentTraceFile == null)
                Debug.Log("[TEST] no test file set");
            else if (serializedStatement.Equals(_currentTraceFile.text))
                LogSuccess($"test success for {response.StatementID}");
            else
                LogError($"test error for {response.StatementID}");
            _waitResponseResult = false;
        }

        private void LogSuccess(string message)
        {
            Color color = Color.green;
            Debug.Log(string.Format("[<color={0}>Unit test success</color>] {1}", ToHex(color), message));
        }

        private void LogError(string message)
        {
            Color color = Color.red;
            Debug.Log(string.Format("[<color={0}>Unit test failed</color>] {1}", ToHex(color), message));
        }

        private string ToHex(Color color)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", ToByte(color.r), ToByte(color.g), ToByte(color.b));
        }

        private byte ToByte(float value)
        {
            value = Mathf.Clamp01(value);
            return (byte)(value * 255);
        }
    }
}
