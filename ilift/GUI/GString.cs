using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;

namespace ilift.GUI
{
    /// <summary>
    /// Abstraction for a label that has text and position and is drawn on the display
    /// </summary>
    class GString: GWidget
    {
        //Font object that is used for drawing the label
        private readonly Font FONT = Resources.GetFont(Resources.FontResources.NinaB);
        //Color object that is used for coloring the label
        private readonly Gadgeteer.Color color = Gadgeteer.Color.Red;
        //X position of the label
        private int _x;
        //Y position of the label
        private int _y;
        //Text to be drawn on the label
        private String _text;       

        /// <summary>
        /// Sets the position and the text of the label
        /// </summary>
        /// <param name="x">X position of the text</param>
        /// <param name="y">Y position of the text</param>
        /// <param name="text">Text to be displayed</param>
        public GString(int x, int y, String text)
        {
            this._x = x;
            this._y = y;
            this._text = text;
        }

        /// <summary>
        /// Draws the label on the display using the passed display object
        /// </summary>
        /// <param name="display">Display Object</param>
        public void draw(DisplayTE35 display)
        {
            display.SimpleGraphics.DisplayText(this._text, FONT, color, this._x, this._y);
        }
    }
}
