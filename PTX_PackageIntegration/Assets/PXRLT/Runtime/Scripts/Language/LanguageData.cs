using UnityEngine;

namespace PXRLT
{
    /// <summary>
    /// Language Data is a class to store
    /// BCP-47 language value with full translation of it
    /// </summary>
    [CreateAssetMenu(menuName = "xAPI/Language data", fileName = "Language Data")]
    public class LanguageData : ScriptableObject
    {
        /// <summary>
        /// Variable in RFC-5646 norm is required in this field (ex : en-US, fr-FR, ...)
        /// </summary>
        [SerializeField, Tooltip("ex : en-US, fr-FR, ... Look for RFC-5646 norm")]
        private string _name;
        /// <summary>
        /// Full language name in english (ex : English, French, ...)
        /// </summary>
        [SerializeField, Tooltip("ex : English, French, ...")]
        private string _fullName;

        #region Accessors
        /// <summary>
        /// Getter for _name value
        /// </summary>
        public string Name => _name;
        /// <summary>
        /// Getter for _fullname value
        /// </summary>
        public string FullName => _fullName;
        #endregion
    }
}
