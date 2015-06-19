using System;
using Gadgeteer;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;

namespace ilift.GUI
{
    /// <summary>
    /// Abstraction of a button, used in the player mode chosing
    /// </summary>
    class GButton: GWidget
    {
        //The color of selected border set to Red
        private readonly Gadgeteer.Color SELECTED_BORDER_COLOR = Gadgeteer.Color.Red;
        //The color of unselected border set to Gray
        private readonly Gadgeteer.Color UNSELECTED_BORDER_COLOR = Gadgeteer.Color.DarkGray;

        //X position of the button
        private int _x;
        //Y position of the button
        private int _y;
        //Width of the button
        private int _width;
        //Height of the button
        private int _height;
        //Text shown in the button
        private String _text;
        //X position of the text shown in the button
        private int _textX;
        //Y position of the text shown in the button
        private int _textY;
        //Is selected boolean
        private bool _isSelected;

        //Delegate declaration for the callback
        public delegate void Callback();
        private Callback _callback;

        //Color of the button
        private Gadgeteer.Color _color;
        //Thickness of the button
        private int _thickness;
        //Label object of the button
        private GString _label;

        /// <summary>
        /// Abstraction of a button, the constructor takes the dimension parameters as well as the label of the button
        /// </summary>
        /// <param name="x">Buttons X position</param>
        /// <param name="y">Buttons Y position</param>
        /// <param name="width">Buttons width</param>
        /// <param name="height">Buttons height</param>
        /// <param name="text">Buttons text</param>
        /// <param name="textX">Buttons text X position</param>
        /// <param name="textY">Buttons text Y position</param>
        public GButton(int x, int y, int width, int height, String text, int textX, int textY)
        {
            this._x = x;
            this._y = y;
            this._width = width;
            this._height = height;
            this._text = text;
            this._textX = textX;
            this._textY = textY;
            this._isSelected = false;
            this._color = Gadgeteer.Color.White;
            this._thickness = 3;
            this._label = new GString(textX, textY, text);
            
        }

     
        /// <summary>
        /// Draws the button taking the display object as parameter and using it to draw rectangle and a label on the screen
        /// Checks also if the button is selected, and according to that draws a border around it.
        /// </summary>
        /// <param name="display">Display object</param>
        public void draw(DisplayTE35 display)
        {

            if(_isSelected) {
                display.SimpleGraphics.DisplayRectangle(SELECTED_BORDER_COLOR, _thickness, _color, _x, _y, _width, _height);
            }
            else
            {
                display.SimpleGraphics.DisplayRectangle(UNSELECTED_BORDER_COLOR, _thickness, _color, _x, _y, _width, _height);

            }
            _label.draw(display);
        }

        /// <summary>
        /// Sets the callback that will be called on the trigger method
        /// </summary>
        /// <param name="c">The callback being passed</param>
        public void setCallback(Callback c)
        {
            this._callback = c;
        }

        /// <summary>
        /// Sets the button selected
        /// </summary>
        public void select()
        {
            this._isSelected = true;
        }

        /// <summary>
        /// Deselects the button
        /// </summary>
        public void deselect()
        {
            this._isSelected = false;
        }
        /// <summary>
        /// Returns the current selection state of the button
        /// </summary>
        /// <returns>selection</returns>
        public bool IsSelected()
        {
            return this._isSelected;
        }

        /// <summary>
        /// Calls the callback that was set on setCallBack method
        /// </summary>
        public void trigger()
        {
            _callback();
        }
    }
}
