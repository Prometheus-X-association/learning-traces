using System;
using UnityEngine;

namespace PXRLT
{
    [Serializable]
    public class LanguagePair
    {
        [SerializeField]
        private LanguageData _language = null;
        [SerializeField]
        private string _value;

        #region Accessors
        public LanguageData Language => _language;
        public string Value => _value;
        #endregion

        public LanguagePair(LanguageData language, string value)
        {
            _language = language;
            _value = value;
        }
    }
}
