using System.Collections.Generic;

namespace PXRLT
{
    public class Exercise
    {
        /// <summary>
        /// ID, it should be unique for each new exercise you want to make
        /// </summary>
        private string _id;
        /// <summary>
        /// Registration ID, it's a ID which define an instance of an exercise
        /// It should be the same from attempt trace to complete trace
        /// </summary>
        private string _registrationId;
        /// <summary>
        /// Name of the activity for each available language
        /// </summary>
        private List<LanguagePair> _namePairs = new List<LanguagePair>();
        /// <summary>
        /// Description of the activity for each available language
        /// </summary>
        private List<LanguagePair> _descriptionPairs = new List<LanguagePair>();

        /// <summary>
        /// Getter / Setter for _id
        /// </summary>
        public string Id { get { return _id; } set { _id = value; } }
        /// <summary>
        /// Getter / Setter for _registrationId
        /// </summary>
        public string RegistrationId { get { return _registrationId; } set { _registrationId = value; } }
        /// <summary>
        /// Getter / Setter for _namePairs
        /// </summary>
        public List<LanguagePair> NamePairs { get { return _namePairs; } set { _namePairs = value; } }
        /// <summary>
        /// Getter / Setter for _descriptionPairs
        /// </summary>
        public List<LanguagePair> DescriptionPairs { get { return _descriptionPairs; } set { _descriptionPairs = value; } }
    }
}
