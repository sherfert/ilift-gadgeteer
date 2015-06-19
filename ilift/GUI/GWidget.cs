using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;

namespace ilift.GUI
{
    /// <summary>
    /// Interface that all elements that are displayed on the display inherit from
    /// </summary>
    interface GWidget
    {
        /// <summary>
        /// Draw abstract method that takes display object
        /// </summary>
        /// <param name="display">Display Object</param>
        void draw(DisplayTE35 display);
    }
}
