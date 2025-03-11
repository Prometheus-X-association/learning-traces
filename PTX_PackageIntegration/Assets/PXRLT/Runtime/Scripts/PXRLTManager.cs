using System.Collections.Generic;
using UnityEngine;
using XAPI;

namespace PXRLT
{
    public class PXRLTManager : MonoBehaviour
    {
        #region Singleton Pattern
        private static PXRLTManager _instance = null;
        public static PXRLTManager Instance => _instance;
        #endregion

        private string _lmsSessionName = string.Empty;
        private XAPI.ContextActivities _currentContext = new XAPI.ContextActivities();
        private XAPI.Actor _currentActor = new XAPI.Actor();
        [SerializeField]
        private List<LanguageData> _languagesAvailable =  new List<LanguageData>();
        [SerializeField]
        private List<Verb> _verbsAvailable = new List<Verb>();

        private Verb _completeVerb = null;
        private Verb _initializeVerb = null;
        private Verb _interactVerb = null;

        #region Accessors
        public string LMSSessionName => _lmsSessionName;

        public List<LanguageData> LanguagesAvailable => _languagesAvailable;
        #endregion

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);

                _completeVerb = _verbsAvailable.Find(x => x.Name == "completed");
                _initializeVerb = _verbsAvailable.Find(x => x.Name == "initialized");
                _interactVerb = _verbsAvailable.Find(x => x.Name == "interacted");
            }
            else
            {
                Destroy(this);
            }
        }

        #region Public Methods
        public void InitializeContext(string lmsSessionName, PXRLT.ContextActivities contextActivities)
        {
            _lmsSessionName = lmsSessionName;

            XAPI.Activity activity = new XAPI.Activity(contextActivities.ParentActivityId);
            XAPI.ActivityMetaData activiyMetaData = new XAPI.ActivityMetaData();
            activiyMetaData.ActivityType = "http://adlnet.gov/expapi/activities/course";
            AddNameAndDefinition(ref activiyMetaData, contextActivities.ParentActivityNamePairs, contextActivities.ParentActivityDescriptionPairs);
            activity.Definition = activiyMetaData;

            XAPI.Activity categoryActivity = new XAPI.Activity("https://w3id.org/xapi/simulation/v1.0");
            XAPI.ActivityMetaData categoryMetaData = new XAPI.ActivityMetaData();
            categoryMetaData.ActivityType = "http://id.tincanapi.com/activitytype/category";
            AddNameAndDefinition(ref categoryMetaData, contextActivities.ActivityCategoryPairs, new List<LanguagePair>());
            categoryActivity.Definition = categoryMetaData;

            _currentContext = new XAPI.ContextActivities();
            _currentContext.Parents = new List<XAPI.Activity> { activity };
            _currentContext.Categories = new List<XAPI.Activity> {  categoryActivity };
        }

        public void InitializeUserInformation(bool isAnonymous, UserInformation userInformation = null)
        {
            if (isAnonymous)
            {
                _currentActor = XAPI.Actor.FromMailbox("anonymous@domain.com");
                _currentActor.Name = System.Guid.NewGuid().ToString();
            }
            else
            {
                _currentActor = XAPI.Actor.FromMailbox(userInformation.Email);
                _currentActor.Name = userInformation.Fullname;
            }
        }

        public void ClearContext()
        {
            Log("Clear context activities for xAPI purposes");
            _currentContext = new XAPI.ContextActivities();
            _lmsSessionName = string.Empty;
        }

        public void ClearUserInformation()
        {

        }

        public void SendInitializeTrace(Activity activity)
        {
            if (_currentContext == null)
            {
                LogError("Define a context before sending traces");
                return;
            }

            XAPI.Verb verb = _initializeVerb.CreateVerb();

            XAPI.Activity currentActivity = new XAPI.Activity($"https://navy.mil/netc/xapi/activities/simulations/{activity.ExerciseId}");
            XAPI.ActivityMetaData currentActivityMetaData = new XAPI.ActivityMetaData();
            currentActivityMetaData.ActivityType = "http://adlnet.gov/expapi/activities/simulation";
            AddNameAndDefinition(ref currentActivityMetaData, activity.NamePairs, activity.DescriptionPairs);
            currentActivity.Definition = currentActivityMetaData;

            XAPI.Statement statement = new XAPI.Statement(_currentActor, verb, currentActivity);
            statement.Context = new XAPI.Context();
            statement.Context.ContextActivities = _currentContext;
            statement.Context.Registration = activity.RegistrationId;
            statement.Context.Platform = activity.PlatformName;
            statement.Context.Language = activity.LanguageUsed.Name;
            if (!string.IsNullOrWhiteSpace(_lmsSessionName))
            {
                statement.Context.Extensions = new Dictionary<string, string>();
                statement.Context.Extensions.Add("https://w3id.org/xapi/cmi5/context/extensions/sessionid", _lmsSessionName);
            }

            XAPI.XAPIWrapper.SendStatement(statement, res =>
            {
                Log($"Sent beginning statement!  LRS stored with ID: {res.StatementID}");
            });
        }

        public void SendEventTrace(Activity activity, Event eventToSend)
        {
            if (_currentContext == null)
            {
                LogError("Define a context before sending traces");
                return;
            }

            XAPI.Verb verb = _interactVerb.CreateVerb();

            XAPI.Activity currentActivity = new XAPI.Activity($"https://navy.mil/netc/xapi/activities/simulations/{activity.ExerciseId}/events/{eventToSend.Key}");
            XAPI.ActivityMetaData currentActivityMetaData = new XAPI.ActivityMetaData();
            currentActivityMetaData.ActivityType = "http://adlnet.gov/expapi/activities/interaction";
            AddNameAndDefinition(ref currentActivityMetaData, eventToSend.NamePairs, new List<LanguagePair>());
            currentActivity.Definition = currentActivityMetaData;

            XAPI.Result result = new XAPI.Result(eventToSend.Status != EventStatus.ERROR);
            result.Extensions.Add("http://id.tincanapi.com/extension/severity", eventToSend.Status.ToString().ToLower());

            XAPI.Statement statement = new XAPI.Statement(_currentActor, verb, currentActivity);
            statement.Result = result;
            statement.Context = new XAPI.Context();
            statement.Context.ContextActivities = _currentContext;
            statement.Context.Registration = activity.RegistrationId;
            if (!string.IsNullOrEmpty(_lmsSessionName))
            {
                statement.Context.Extensions = new Dictionary<string, string>();
                statement.Context.Extensions.Add("https://w3id.org/xapi/cmi5/context/extensions/sessionid", _lmsSessionName);
            }

            XAPI.XAPIWrapper.SendStatement(statement, res =>
            {
                Log($"Sent statement!  LRS stored with ID: {res.StatementID}");
            });
        }

        public void SendResultTrace(Activity activity, Result result)
        {
            if (_currentContext == null)
            {
                LogError("Define a context before sending traces");
                return;
            }

            XAPI.Verb verb = _completeVerb.CreateVerb();

            XAPI.Activity currentActivity = new XAPI.Activity($"https://navy.mil/netc/xapi/activities/simulations/{activity.ExerciseId}");
            XAPI.ActivityMetaData currentActivityMetaData = new XAPI.ActivityMetaData();
            currentActivityMetaData.ActivityType = "http://adlnet.gov/expapi/activities/exercise";
            AddNameAndDefinition(ref currentActivityMetaData, result.NamePairs, new List<LanguagePair>());
            currentActivity.Definition = currentActivityMetaData;

            XAPI.Result xapiResult = new XAPI.Result(result.ExerciseStatus == ExerciseStatus.SUCCEED, result.ResultStatus == ResultStatus.FINISHED);
            xapiResult.Score = new XAPI.Score();
            xapiResult.Score.Scaled = result.Score;
            xapiResult.Response = "";

            Dictionary<string, float> sensorsResult = new Dictionary<string, float>();
            foreach (ResultSensor sensor in result.Sensors)
                sensorsResult[sensor.Key] = sensor.Value;
            xapiResult.Extensions.Add($"https://navy.mil/netc/xapi/activities/simulations/{activity.ExerciseId}/sensors/score", sensorsResult);

            XAPI.Statement statement = new XAPI.Statement(_currentActor, verb, currentActivity);
            statement.Result = xapiResult;
            statement.Context = new XAPI.Context();
            statement.Context.ContextActivities = _currentContext;
            statement.Context.Registration = activity.RegistrationId;
            if (!string.IsNullOrEmpty(_lmsSessionName))
            {
                statement.Context.Extensions = new Dictionary<string, string>();
                statement.Context.Extensions.Add("https://w3id.org/xapi/cmi5/context/extensions/sessionid", _lmsSessionName);
            }

            XAPI.XAPIWrapper.SendStatement(statement, res =>
            {
                Log($"Sent statement!  LRS stored with ID: {res.StatementID}");
            });
        }
        #endregion

        private void AddNameAndDefinition(ref XAPI.ActivityMetaData metaData, in List<LanguagePair> namePairs, in List<LanguagePair> definitionPairs)
        {
            foreach (var namePair in namePairs)
            {
                if (!_languagesAvailable.Exists(x => x == namePair.Language))
                    continue;
                metaData.NamePairs[namePair.Language.Name] = namePair.Value;
            }
            foreach (var descriptionPair in definitionPairs)
            {
                if (!_languagesAvailable.Exists(x => x == descriptionPair.Language))
                    continue;
                metaData.DescriptionPairs[descriptionPair.Language.Name] = descriptionPair.Value;
            }
        }

        #region Log
        private void Log(string message)
        {
            Debug.Log($"[PXRLT Manager] {message}");
        }

        private void LogError(string message)
        {
            Debug.LogError($"[PXRLT Manager] {message}");
        }
        #endregion
    }
}
