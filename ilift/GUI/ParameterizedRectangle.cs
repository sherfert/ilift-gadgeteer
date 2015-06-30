using System;
using System.Collections;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Shapes;

namespace ilift.GUI
{
    /// <summary>
    /// Represents a Rectangle with the possibility to add extra parameters
    /// </summary>
    public class ParameterizedRectangle : Rectangle
    {

        /// <summary>
        /// parameters that contains
        /// </summary>
        private Hashtable parameters = new Hashtable();

        /// <summary>
        /// Constructs a new ParameterizedRectangle
        /// </summary>
        /// <param name="width">Given width</param>
        /// <param name="height">Given height</param>
        public ParameterizedRectangle(int width, int height) : base(width, height)
        {
        }

        /// <summary>
        /// Add a new Parameter to this Rectangle
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public void AddParameter(object key, object value)
        {
            parameters[key] = value;
        }

        /// <summary>
        /// Get the parameter associated to the given key
        /// </summary>
        /// <param name="key">The given key</param>
        /// <returns>object associated with the given key</returns>
        public object GetParameter(object key)
        {
            return parameters[key];
        }
    }
}
