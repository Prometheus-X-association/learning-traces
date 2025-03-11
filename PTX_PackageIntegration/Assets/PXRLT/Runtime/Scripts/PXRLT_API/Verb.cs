using System.Collections.Generic;
using UnityEngine;

namespace PXRLT
{
    [CreateAssetMenu(menuName = "xAPI/Verb", fileName = "New Verb")]
    public class Verb : ScriptableObject
    {
        [SerializeField]
        private string _name = string.Empty;
        [SerializeField]
        private string _id = string.Empty;
        [SerializeField]
        private List<LanguagePair> _displayPairs = new List<LanguagePair>();

        #region Accessors
        public string Name => _name;
        #endregion

        public XAPI.Verb CreateVerb()
        {
            XAPI.Verb verb = new XAPI.Verb(_name, _id);
            foreach (var displayPair in _displayPairs)
                verb.AddDisplayPair(displayPair.Language.Name, displayPair.Value);
            return verb;
        }
    }
}
