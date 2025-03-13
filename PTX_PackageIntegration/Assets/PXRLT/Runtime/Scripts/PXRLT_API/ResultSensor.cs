using UnityEngine;

namespace PXRLT
{
    /// <summary>
    /// Definition of subresult score
    /// </summary>
    public class ResultSensor
    {
        /// <summary>
        /// Unique key defining ths sensor
        /// </summary>
        private string _id = string.Empty;
        /// <summary>
        /// Value of the sensor
        /// </summary>
        private float _value = 0.0f;

        #region Accessors
        /// <summary>
        /// Getter for _key
        /// </summary>
        public string Id { get { return _id; } }
        /// <summary>
        /// Getter for _value
        /// </summary>
        public float Value { get { return _value; } }
        #endregion

        /// <summary>
        /// Constructor of result sensor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public ResultSensor(string id, float value)
        {
            _id = id;
            _value = value;
        }
    }
}