using System.Collections.Generic;

namespace PXRLT
{
    /// <summary>
    /// Definition of an event
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Unique key of this event
        /// It's a key to be able to identify this event
        /// </summary>
        private string _key;
        /// <summary>
        /// Status type of this event
        /// </summary>
        private EventStatus _status;
        /// <summary>
        /// Name of the event for each available languages
        /// </summary>
        private List<LanguagePair> _namePairs = new List<LanguagePair>();

        #region Accessors
        /// <summary>
        /// Getter / Setter for _key
        /// </summary>
        public string Key { get { return _key; } set { _key = value; } }
        /// <summary>
        /// Getter / Setter for _status
        /// </summary>
        public EventStatus Status { get { return _status; } set { _status = value; } }
        /// <summary>
        /// Getter / Setter for _namePairs
        /// </summary>
        public List<LanguagePair> NamePairs { get { return _namePairs; } set { _namePairs = value; } }
        #endregion
    }

    /// <summary>
    /// Enum for each type of event
    /// </summary>
    public enum EventStatus
    {
        INFO,       // Information event
        WARNING,    // Warning event
        ERROR,      // Error event
        SUCCESS     // Success event
    }
}
