using System.Collections.Generic;
using UnityEngine;

namespace PXRLT
{
    public class Event
    {
        [SerializeField]
        private string _key;
        [SerializeField]
        private EventStatus _status;
        [SerializeField]
        private List<LanguagePair> _namePairs = new List<LanguagePair>();

        #region Accessors
        public string Key { get { return _key; } set { _key = value; } }
        public EventStatus Status { get { return _status; } set { _status = value; } }
        public List<LanguagePair> NamePairs { get { return _namePairs; } set { _namePairs = value; } }
        #endregion
    }

    public enum EventStatus
    {
        INFO,
        WARNING,
        ERROR,
        SUCCESS
    }
}
