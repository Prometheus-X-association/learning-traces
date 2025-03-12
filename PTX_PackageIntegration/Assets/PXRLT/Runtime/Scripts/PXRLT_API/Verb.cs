using System.Collections.Generic;
using UnityEngine;

namespace PXRLT
{
    /// <summary>
    /// Definition of a verb
    /// Encapsulation for an interpretation of XAPI.Verb
    /// This class is made to create scalable variation of verb for each language
    /// </summary>
    [CreateAssetMenu(menuName = "xAPI/Verb", fileName = "New Verb")]
    public class Verb : ScriptableObject
    {
        /// <summary>
        /// Name of the verb
        /// </summary>
        [SerializeField]
        private string _name = string.Empty;
        /// <summary>
        /// ID of the verb
        /// </summary>
        [SerializeField]
        private string _id = string.Empty;
        /// <summary>
        /// Display name of the verb for each available languages
        /// </summary>
        [SerializeField]
        private List<LanguagePair> _displayPairs = new List<LanguagePair>();

        #region Accessors
        /// <summary>
        /// Getter for _name
        /// </summary>
        public string Name => _name;
        #endregion

        /// <summary>
        /// Create XAPI.Verb
        /// </summary>
        /// <returns></returns>
        public XAPI.Verb CreateVerb()
        {
            XAPI.Verb verb = new XAPI.Verb(_name, _id);
            foreach (var displayPair in _displayPairs)
                verb.AddDisplayPair(displayPair.Language.Name, displayPair.Value);
            return verb;
        }
    }
}
