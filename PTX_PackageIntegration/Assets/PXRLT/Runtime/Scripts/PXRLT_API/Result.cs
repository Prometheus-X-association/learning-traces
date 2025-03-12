using System.Collections.Generic;

namespace PXRLT
{
    /// <summary>
    /// Definiation of a result
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Scaled score, mostly between 0-1
        /// </summary>
        private float _score = 0f;
        /// <summary>
        /// Exercise status
        /// Tell if the user completed the exercise succefully or not
        /// </summary>
        private ExerciseStatus _exerciseStatus;
        /// <summary>
        /// Result status
        /// Tell how the exercise finish
        /// </summary>
        private ResultStatus _resultStatus;
        /// <summary>
        /// Name of the result for each available language
        /// </summary>
        private List<LanguagePair> _namePairs = new List<LanguagePair>();
        /// <summary>
        /// Sensor list which represent subresult score
        /// They define categories of result
        /// </summary>
        private List<ResultSensor> _sensors = new List<ResultSensor>();

        #region Accessors
        /// <summary>
        /// Getter / Setter for _score
        /// </summary>
        public float Score { get { return _score; } set { _score = value; } }
        /// <summary>
        /// Getter / Setter for _exerciseStatus
        /// </summary>
        public ExerciseStatus ExerciseStatus { get { return _exerciseStatus; } set { _exerciseStatus = value; } }
        /// <summary>
        /// Getter / Setter for _resultStatus
        /// </summary>
        public ResultStatus ResultStatus { get { return _resultStatus; } set { _resultStatus = value; } }
        /// <summary>
        /// Getter / Setter for _namePairs
        /// </summary>
        public List<LanguagePair> NamePairs { get { return _namePairs; } set { _namePairs = value; } }
        /// <summary>
        /// Getter / Setter for _sensors
        /// </summary>
        public List<ResultSensor> Sensors { get { return _sensors; } set { _sensors = value; } }
        #endregion
    }

    /// <summary>
    /// Exercise status
    /// Define due to score calculation
    /// </summary>
    public enum ExerciseStatus
    {
        SUCCEED,    // Exercise success
        FAILED      // Exercise failed
    }

    /// <summary>
    /// Result status
    /// How the user finish the exercise
    /// </summary>
    public enum ResultStatus
    {
        FINISHED,   // Exercise finished
        ABORTED,    // Exercise aborted
        FATAL_ERROR,// Exercise finished with a fatal error
    }
}
