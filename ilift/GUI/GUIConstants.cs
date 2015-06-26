using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace ilift.GUI
{
    public class GUIConstants
    {
        public const int DEFAULT_MARGIN = 10;
        public const int LEFT_OFFSET = 10;
        public const int DEFAULT_SPACING = 15;
        public const int LOWER_BUTTON_LABEL_OFFSET = 120;
        public static readonly Font FONT = Resources.GetFont(Resources.FontResources.NinaB);
        public static readonly Color TEXT_COLOR = Gadgeteer.Color.Black;
        public static readonly Color SPECIAL_BUTTON_COLOR = Gadgeteer.Color.Red;
        public static readonly Color NORMAL_BUTTON_COLOR = Gadgeteer.Color.Gray;
        public const double HEIGHT_PERCENTAGE = 0.15;
        public const double BIG_BUTTON_WIDTH_PERCENTAGE = 0.9;
        public const double BUTTON_WIDTH_PERCENTAGE = 0.4;



    }
}
