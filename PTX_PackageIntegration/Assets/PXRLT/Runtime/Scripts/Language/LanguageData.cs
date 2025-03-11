using UnityEngine;

namespace PXRLT
{
    [CreateAssetMenu(menuName = "xAPI/Language data", fileName = "Language Data")]
    public class LanguageData : ScriptableObject
    {
        /// <summary>
        /// Variable in BCP-47 norm is required in this field (ex : en-US, fr-FR, ...)
        /// </summary>
        [SerializeField, Tooltip("ex : en-US, fr-FR, ... Look for BCP-47 norm")]
        private string _name;
        /// <summary>
        /// Full language name in english (ex : English, French, ...)
        /// </summary>
        [SerializeField, Tooltip("ex : English, French, ...")]
        private string _fullName;

        #region Accessors
        public string Name => _name;
        public string FullName => _fullName;
        #endregion
    }
}
