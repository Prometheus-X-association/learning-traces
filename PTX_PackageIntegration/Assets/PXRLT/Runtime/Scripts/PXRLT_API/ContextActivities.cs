using System.Collections.Generic;

namespace PXRLT
{
    /// <summary>
    /// Definition of a context activities
    /// Encapsulation for an interpretation of XAPI.ContextActivities
    /// </summary>
    public class ContextActivities
    {
        /// <summary>
        /// Parent activity ID, most of the time come from a LMS
        /// </summary>
        private string _parentActivityId = string.Empty;
        /// <summary>
        /// Name of the parent activity for each available language
        /// </summary>
        private List<LanguagePair> _parentActivityNamePairs = new List<LanguagePair>();
        /// <summary>
        /// Description of the parent activity for each available language
        /// </summary>
        private List<LanguagePair> _parentActivityDescriptionPairs = new List<LanguagePair>();
        /// <summary>
        /// Name of the category for this activity for each available language
        /// </summary>
        private List<LanguagePair> _activityCategoryPairs = new List<LanguagePair>();

        #region Accessors
        /// <summary>
        /// Getter / Setter for _parentActivityId
        /// </summary>
        public string ParentActivityId { get { return _parentActivityId; } set { _parentActivityId = value; } }
        /// <summary>
        /// Getter / Setter for _parentActivityNamePairs
        /// </summary>
        public List<LanguagePair> ParentActivityNamePairs { get { return _parentActivityNamePairs; } set { _parentActivityNamePairs = value; } }
        /// <summary>
        /// Getter / Setter for _parentActivityDescriptionPairs
        /// </summary>
        public List<LanguagePair> ParentActivityDescriptionPairs { get { return _parentActivityDescriptionPairs; } set { _parentActivityDescriptionPairs = value; } }
        /// <summary>
        /// Getter / Setter for _activityCategoryPairs
        /// </summary>
        public List<LanguagePair> ActivityCategoryPairs { get { return _activityCategoryPairs; } set { _activityCategoryPairs = value; } }
        #endregion
    }
}
