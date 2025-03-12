using System;
using UnityEngine;

namespace PXRLT
{
    /// <summary>
    /// Class made to pair a language to a value
    /// It's made to create a scalable choice of language in the future
    /// </summary>
    [Serializable]
    public class LanguagePair
    {
        /// <summary>
        /// Language key of this pair
        /// </summary>
        [SerializeField]
        private LanguageData _language = null;
        /// <summary>
        /// Value for the language key
        /// </summary>
        [SerializeField]
        private string _value;

        #region Accessors
        /// <summary>
        /// Getter for _language
        /// </summary>
        public LanguageData Language => _language;
        /// <summary>
        /// Getter for _value
        /// </summary>
        public string Value => _value;
        #endregion

        /// <summary>
        /// Constructor fo LanguagePair
        /// </summary>
        /// <param name="language">language key</param>
        /// <param name="value">language value</param>
        public LanguagePair(LanguageData language, string value)
        {
            _language = language;
            _value = value;
        }
    }
}
