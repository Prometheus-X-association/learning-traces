using System.Collections.Generic;
using UnityEngine;

namespace PXRLT
{
    public class Activity
    {
        private string _exerciseId;
        private string _registrationId;
        private string _platformName;
        private LanguageData _languageUsed;
        private List<LanguagePair> _namePairs = new List<LanguagePair>();
        private List<LanguagePair> _descriptionPairs = new List<LanguagePair>();

        #region Accessors
        public string ExerciseId { get { return _exerciseId; } set { _exerciseId = value; } }
        public string RegistrationId { get { return _registrationId; } set { _registrationId = value; } }
        public string PlatformName { get { return _platformName; } set { _platformName = value; } }
        public LanguageData LanguageUsed { get { return _languageUsed; } set { _languageUsed = value; } }
        public List<LanguagePair> NamePairs { get { return _namePairs; } set { _namePairs = value; } }
        public List<LanguagePair> DescriptionPairs { get { return _descriptionPairs; } set { _descriptionPairs = value; } }
        #endregion
    }
}
