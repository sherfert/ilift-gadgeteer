using System;
using System.Collections;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Shapes;

namespace ilift.GUI
{
    public class ParameterizedRectangle : Rectangle
    {

        private Hashtable parameters = new Hashtable();

        public ParameterizedRectangle(int width, int height) : base(width, height)
        {
        }

        public void AddParameter(object key, object value)
        {
            parameters[key] = value;
        }

        public object GetParameter(object key)
        {
            return parameters[key];
        }
    }
}
