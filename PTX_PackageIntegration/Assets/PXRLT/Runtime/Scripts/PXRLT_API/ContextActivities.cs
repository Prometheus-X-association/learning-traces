using System.Collections.Generic;

namespace PXRLT
{
    public class ContextActivities
    {
        private string _parentActivityId = string.Empty;
        private List<LanguagePair> _parentActivityNamePairs = new List<LanguagePair>();
        private List<LanguagePair> _parentActivityDescriptionPairs = new List<LanguagePair>();
        private List<LanguagePair> _activityCategoryPairs = new List<LanguagePair>();

        #region Accessors
        public string ParentActivityId { get { return _parentActivityId; } set { _parentActivityId = value; } }
        public List<LanguagePair> ParentActivityNamePairs { get { return _parentActivityNamePairs; } set { _parentActivityNamePairs = value; } }
        public List<LanguagePair> ParentActivityDescriptionPairs { get { return _parentActivityDescriptionPairs; } set { _parentActivityDescriptionPairs = value; } }
        public List<LanguagePair> ActivityCategoryPairs { get { return _activityCategoryPairs; } set { _activityCategoryPairs = value; } }
        #endregion
    }
}
