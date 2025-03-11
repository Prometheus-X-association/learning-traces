using System.Collections.Generic;
using UnityEngine;

namespace PXRLT
{
    public class Result
    {
        private float _score = 0f;
        private ExerciseStatus _exerciseStatus;
        private ResultStatus _resultStatus;
        private List<LanguagePair> _namePairs = new List<LanguagePair>();
        private List<ResultSensor> _sensors = new List<ResultSensor>();


        #region Accessors
        public float Score { get { return _score; } set { _score = value; } }
        public ExerciseStatus ExerciseStatus { get { return _exerciseStatus; } set { _exerciseStatus = value; } }
        public ResultStatus ResultStatus { get { return _resultStatus; } set { _resultStatus = value; } }
        public List<LanguagePair> NamePairs { get { return _namePairs; } set { _namePairs = value; } }
        public List<ResultSensor> Sensors { get { return _sensors; } set { _sensors = value; } }
        #endregion
    }

    //
    // Summary:
    //     Exercise result status
    public enum ExerciseStatus
    {
        //
        // Summary:
        //     Successfully done exercise
        SUCCEED,
        //
        // Summary:
        //     Exercise failed, score was too low
        FAILED
    }

    public enum ResultStatus
    {
        FINISHED,
        ABORTED,
        FATAL_ERROR,
    }
}
