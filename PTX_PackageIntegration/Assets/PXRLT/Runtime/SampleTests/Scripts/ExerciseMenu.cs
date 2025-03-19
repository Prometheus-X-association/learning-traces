using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PXRLT.Test
{
    public class ExerciseMenu : MonoBehaviour
    {
        private PXRLTManager _manager;
        [SerializeField]
        private UIManager _uiManager;

        [Header("UI Components")]
        [SerializeField]
        private TMP_InputField _sensorsName1 = null;
        [SerializeField]
        private Slider _sensorsValue1 = null;
        [SerializeField]
        private TMP_InputField _sensorsName2 = null;
        [SerializeField]
        private Slider _sensorsValue2 = null;
        [SerializeField]
        private TMP_InputField _sensorsName3 = null;
        [SerializeField]
        private Slider _sensorsValue3 = null;

        private void Start()
        {
            _manager = PXRLTManager.Instance;
            if (_manager == null)
            {
                Debug.LogError("You should have a PXRLTManager to run this script");
                return;
            }
        }

        private void OnEnable()
        {
            _sensorsName1.text = "First Sensors";
            _sensorsValue1.value = 0;
            _sensorsName2.text = "Second Sensors";
            _sensorsValue2.value = 0;
            _sensorsName3.text = "Third Sensors";
            _sensorsValue3.value = 0;
        }

        public void SendSuccessEvent()
        {
            List<LanguagePair> eventNames = new List<LanguagePair>();
            foreach (LanguageData language in _manager.LanguagesAvailable)
                eventNames.Add(new LanguagePair(language, $"Event success for {language.FullName}"));
            Event evt = CreateEvent(EventStatus.SUCCESS, $"EVENT_SUCCESS", eventNames);
            _manager.SendEventTrace(_uiManager.CurrentActivity, evt);
        }

        public void SendInformationEvent()
        {
            List<LanguagePair> eventNames = new List<LanguagePair>();
            foreach (LanguageData language in _manager.LanguagesAvailable)
                eventNames.Add(new LanguagePair(language, $"Event information for {language.FullName}"));
            Event evt = CreateEvent(EventStatus.INFO, $"EVENT_INFORMATION", eventNames);
            _manager.SendEventTrace(_uiManager.CurrentActivity, evt);
        }

        public void SendWarningEvent()
        {
            List<LanguagePair> eventNames = new List<LanguagePair>();
            foreach (LanguageData language in _manager.LanguagesAvailable)
                eventNames.Add(new LanguagePair(language, $"Event warning for {language.FullName}"));
            Event evt = CreateEvent(EventStatus.WARNING, $"EVENT_WARNING", eventNames);
            _manager.SendEventTrace(_uiManager.CurrentActivity, evt);
        }

        public void SendErrorEvent()
        {
            List<LanguagePair> eventNames = new List<LanguagePair>();
            foreach (LanguageData language in _manager.LanguagesAvailable)
                eventNames.Add(new LanguagePair(language, $"Event error for {language.FullName}"));
            Event evt = CreateEvent(EventStatus.ERROR, $"EVENT_ERROR", eventNames);
            _manager.SendEventTrace(_uiManager.CurrentActivity, evt);
        }

        public void EndExercise()
        {
            Result result = new Result();
            result.Score = (_sensorsValue1.value + _sensorsValue2.value + _sensorsValue3.value) / 3.0f;
            result.ExerciseStatus = ExerciseStatus.SUCCEED;
            result.ResultStatus = ResultStatus.FINISHED;
            foreach (LanguageData language in _manager.LanguagesAvailable)
                result.NamePairs.Add(new LanguagePair(language, $"Result for {language.FullName}"));
            result.Sensors.Add(new ResultSensor(_sensorsName1.text, _sensorsValue1.value));
            result.Sensors.Add(new ResultSensor(_sensorsName2.text, _sensorsValue2.value));
            result.Sensors.Add(new ResultSensor(_sensorsName3.text, _sensorsValue3.value));

            _manager.SendResultTrace(_uiManager.CurrentActivity, result);
            _manager.ClearContext();
        }

        private Event CreateEvent(EventStatus status, string id, List<LanguagePair> namePairs)
        {
            Event evt = new Event();
            evt.Id = id;
            evt.Status = status;
            evt.NamePairs = namePairs;
            return evt;
        }
    }
}
