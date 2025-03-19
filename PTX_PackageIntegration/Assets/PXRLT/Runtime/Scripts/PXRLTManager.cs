using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PXRLT
{
    /// <summary>
    /// This class manage communication to the LRS
    /// </summary>
    public class PXRLTManager : MonoBehaviour
    {
        #region Singleton Pattern
        /// <summary>
        /// Instance of PXRLTManager
        /// </summary>
        private static PXRLTManager _instance = null;
        /// <summary>
        /// Getter for _instance
        /// </summary>
        public static PXRLTManager Instance => _instance;
        #endregion

        /// <summary>
        /// Languages available in PXRLT manager
        /// </summary>
        [SerializeField]
        private List<LanguageData> _languagesAvailable = new List<LanguageData>();
        /// <summary>
        /// Verbs available in PXRLT manager
        /// </summary>
        [SerializeField]
        private List<Verb> _verbsAvailable = new List<Verb>();

        /// <summary>
        /// (Optionnal) LMS session name, use to link your trace with LMS
        /// </summary>
        private string _lmsSessionName = string.Empty;
        /// <summary>
        /// XAPI context activities
        /// </summary>
        private XAPI.ContextActivities _currentContext = null;
        /// <summary>
        /// XAPI actor
        /// </summary>
        private XAPI.Actor _currentActor = new XAPI.Actor();

        /// <summary>
        /// Verb : Completed XAPI
        /// </summary>
        private Verb _completeVerb = null;
        /// <summary>
        /// Verb : Initialized XAPI
        /// </summary>
        private Verb _initializeVerb = null;
        /// <summary>
        /// Verb : Interacted XAPI
        /// </summary>
        private Verb _interactVerb = null;

        /// <summary>
        /// Send an event when a trace is sent
        /// </summary>
        private bool _logTrace = false;

        #region Accessors
        /// <summary>
        /// Getter for _lmsSessionName
        /// </summary>
        public string LMSSessionName => _lmsSessionName;
        /// <summary>
        /// Getter for _languagesAvailable
        /// </summary>
        public List<LanguageData> LanguagesAvailable => _languagesAvailable;
        /// <summary>
        /// Getter for _logTrace
        /// (only available in assembly)
        /// </summary>
        internal bool LogTrace { get { return _logTrace; } set { _logTrace = value; } }
        #endregion

        #region Event
        /// <summary>
        /// Event send when a trace is sent and _logTrace is true
        /// (only available in assembly)
        /// </summary>
        internal UnityEvent<XAPI.StatementStoredResponse> OnTraceSend = new UnityEvent<XAPI.StatementStoredResponse>();
        #endregion

        /// <summary>
        /// Unity Awake methods
        /// </summary>
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
        /// <summary>
        /// Initialize context activity 
        /// </summary>
        /// <param name="lmsSessionName"></param>
        /// <param name="contextActivities"></param>
        public void InitializeContext(PXRLT.ContextActivities contextActivities, string lmsSessionName = null)
        {
            if (lmsSessionName == null)
                _lmsSessionName = string.Empty;
            else
                _lmsSessionName = lmsSessionName;
            _currentContext = new XAPI.ContextActivities();

            if (string.IsNullOrWhiteSpace(contextActivities.ParentActivityId))
            {
                XAPI.Activity activity = new XAPI.Activity(contextActivities.ParentActivityId);
                XAPI.ActivityMetaData activiyMetaData = new XAPI.ActivityMetaData();
                activiyMetaData.ActivityType = "http://adlnet.gov/expapi/activities/course";
                AddNameAndDefinition(ref activiyMetaData, contextActivities.ParentActivityNamePairs, contextActivities.ParentActivityDescriptionPairs);
                activity.Definition = activiyMetaData;
                _currentContext.Parents = new List<XAPI.Activity> { activity };
            }

            XAPI.Activity categoryActivity = new XAPI.Activity("https://w3id.org/xapi/simulation/v1.0");
            XAPI.ActivityMetaData categoryMetaData = new XAPI.ActivityMetaData();
            categoryMetaData.ActivityType = "http://id.tincanapi.com/activitytype/category";
            AddNameAndDefinition(ref categoryMetaData, contextActivities.ActivityCategoryPairs, new List<LanguagePair>());
            categoryActivity.Definition = categoryMetaData;
            _currentContext.Categories = new List<XAPI.Activity> {  categoryActivity };
        }

        /// <summary>
        /// Initialize user information
        /// </summary>
        /// <param name="isAnonymous"></param>
        /// <param name="userInformation"></param>
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

        /// <summary>
        /// Clear context activity
        /// </summary>
        public void ClearContext()
        {
            Log("Clear context activities for xAPI purposes");
            _currentContext = null;
            _lmsSessionName = string.Empty;
        }

        /// <summary>
        /// Clear user information
        /// </summary>
        public void ClearUserInformation()
        {
            Log("Clear user informations for xAPI purposes");
            _currentActor = null;
        }

        /// <summary>
        /// Send trace with initialized verb
        /// It's the beginning of exercise traces
        /// </summary>
        /// <param name="activity"></param>
        public void SendInitializeTrace(Activity activity)
        {
            XAPI.Verb verb = _initializeVerb.CreateVerb();

            XAPI.Activity currentActivity = new XAPI.Activity($"https://navy.mil/netc/xapi/activities/simulations/{activity.ExerciseId}");
            XAPI.ActivityMetaData currentActivityMetaData = new XAPI.ActivityMetaData();
            currentActivityMetaData.ActivityType = "http://adlnet.gov/expapi/activities/simulation";
            AddNameAndDefinition(ref currentActivityMetaData, activity.NamePairs, activity.DescriptionPairs);
            currentActivity.Definition = currentActivityMetaData;

            XAPI.Statement statement = new XAPI.Statement(_currentActor, verb, currentActivity);
            statement.Context = new XAPI.Context();
            statement.Context.Registration = activity.RegistrationId;
            statement.Context.Platform = activity.PlatformName;
            statement.Context.Language = activity.LanguageUsed.Name;
            if (_currentContext != null)
                statement.Context.ContextActivities = _currentContext;
            if (!string.IsNullOrWhiteSpace(_lmsSessionName))
            {
                statement.Context.Extensions = new Dictionary<string, string>();
                statement.Context.Extensions.Add("https://w3id.org/xapi/cmi5/context/extensions/sessionid", _lmsSessionName);
            }

            XAPI.XAPIWrapper.SendStatement(statement, res =>
            {
                Log($"Sent beginning statement!  LRS stored with ID: {res.StatementID}");
                if (_logTrace)
                    OnTraceSend?.Invoke(res);
            });
        }

        /// <summary>
        /// Send trace for event
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="eventToSend"></param>
        public void SendEventTrace(Activity activity, Event eventToSend)
        {
            XAPI.Verb verb = _interactVerb.CreateVerb();

            XAPI.Activity currentActivity = new XAPI.Activity($"https://navy.mil/netc/xapi/activities/simulations/{activity.ExerciseId}/events/{eventToSend.Id}");
            XAPI.ActivityMetaData currentActivityMetaData = new XAPI.ActivityMetaData();
            currentActivityMetaData.ActivityType = "http://adlnet.gov/expapi/activities/interaction";
            AddNameAndDefinition(ref currentActivityMetaData, eventToSend.NamePairs, new List<LanguagePair>());
            currentActivity.Definition = currentActivityMetaData;

            XAPI.Result result = new XAPI.Result(eventToSend.Status != EventStatus.ERROR);
            result.Extensions.Add("http://id.tincanapi.com/extension/severity", eventToSend.Status.ToString().ToLower());

            XAPI.Statement statement = new XAPI.Statement(_currentActor, verb, currentActivity);
            statement.Result = result;
            statement.Context = new XAPI.Context();
            statement.Context.Registration = activity.RegistrationId;
            if (_currentContext != null)
                statement.Context.ContextActivities = _currentContext;
            if (!string.IsNullOrEmpty(_lmsSessionName))
            {
                statement.Context.Extensions = new Dictionary<string, string>();
                statement.Context.Extensions.Add("https://w3id.org/xapi/cmi5/context/extensions/sessionid", _lmsSessionName);
            }

            XAPI.XAPIWrapper.SendStatement(statement, res =>
            {
                Log($"Sent statement!  LRS stored with ID: {res.StatementID}");
                if (_logTrace)
                    OnTraceSend?.Invoke(res);
            });
        }

        /// <summary>
        /// Send trace with completed verb
        /// It's the ending of an exercise traces
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="result"></param>
        public void SendResultTrace(Activity activity, Result result)
        {
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
                sensorsResult[sensor.Id] = sensor.Value;
            xapiResult.Extensions.Add($"https://navy.mil/netc/xapi/activities/simulations/{activity.ExerciseId}/sensors/score", sensorsResult);

            XAPI.Statement statement = new XAPI.Statement(_currentActor, verb, currentActivity);
            statement.Result = xapiResult;
            statement.Context = new XAPI.Context();
            statement.Context.Registration = activity.RegistrationId;
            if (_currentContext != null)
                statement.Context.ContextActivities = _currentContext;
            if (!string.IsNullOrEmpty(_lmsSessionName))
            {
                statement.Context.Extensions = new Dictionary<string, string>();
                statement.Context.Extensions.Add("https://w3id.org/xapi/cmi5/context/extensions/sessionid", _lmsSessionName);
            }

            XAPI.XAPIWrapper.SendStatement(statement, res =>
            {
                Log($"Sent statement!  LRS stored with ID: {res.StatementID}");
                if (_logTrace)
                    OnTraceSend?.Invoke(res);
            });
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metaData"></param>
        /// <param name="namePairs"></param>
        /// <param name="definitionPairs"></param>
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
        /// <summary>
        /// Custom log 
        /// </summary>
        /// <param name="message"></param>
        private void Log(string message)
        {
            Debug.Log($"[PXRLT Manager] {message}");
        }

        /// <summary>
        /// Custom log error
        /// </summary>
        /// <param name="message"></param>
        private void LogError(string message)
        {
            Debug.LogError($"[PXRLT Manager] {message}");
        }
        #endregion
    }
}
