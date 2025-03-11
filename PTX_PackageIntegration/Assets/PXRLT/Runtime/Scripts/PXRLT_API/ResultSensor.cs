using UnityEngine;

namespace PXRLT
{
    public class ResultSensor
    {
        private string _key = string.Empty;
        private float _value = 0.0f;

        #region Accessors
        public string Key { get { return _key; } set { _key = value; } }
        public float Value { get { return _value; } set { _value = value; } }
        #endregion

        public ResultSensor(string key, float value)
        {
            _key = key;
            _value = value;
        }
    }
}