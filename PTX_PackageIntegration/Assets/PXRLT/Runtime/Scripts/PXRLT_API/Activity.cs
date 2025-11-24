using System.Collections.Generic;

namespace PXRLT
{
    /// <summary>
    /// Definition of an activity
    /// Encapsulation for an interpretation of XAPI.Activity
    /// </summary>
    public class Activity
    {
        /// <summary>
        /// ID, it should be unique for each new activity you want to make
        /// </summary>
        private string _sessionId;
        /// <summary>
        /// Name of the project or application you want to link your traces
        /// </summary>
        private string _platformName;
        /// <summary>
        /// Language used by the user in this activity
        /// </summary>
        private LanguageData _languageUsed;
        /// <summary>
        /// Name of the activity for each available language
        /// </summary>
        private List<LanguagePair> _namePairs = new List<LanguagePair>();
        /// <summary>
        /// Description of the activity for each available language
        /// </summary>
        private List<LanguagePair> _descriptionPairs = new List<LanguagePair>();

        #region Accessors
        /// <summary>
        /// Getter / Setter for _sessionId
        /// </summary>
        public string SessionId { get { return _sessionId; } set { _sessionId = value; } }
        /// <summary>
        /// Getter / Setter for _platformName
        /// </summary>
        public string PlatformName { get { return _platformName; } set { _platformName = value; } }
        /// <summary>
        /// Getter / Setter for _languageUsed
        /// </summary>
        public LanguageData LanguageUsed { get { return _languageUsed; } set { _languageUsed = value; } }
        /// <summary>
        /// Getter / Setter for _namePairs
        /// </summary>
        public List<LanguagePair> NamePairs { get { return _namePairs; } set { _namePairs = value; } }
        /// <summary>
        /// Getter / Setter for _descriptionPairs
        /// </summary>
        public List<LanguagePair> DescriptionPairs { get { return _descriptionPairs; } set { _descriptionPairs = value; } }
        #endregion
    }
}
