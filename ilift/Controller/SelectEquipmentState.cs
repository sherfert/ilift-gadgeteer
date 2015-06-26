using System;
using Gadgeteer.Modules.GHIElectronics;
using ilift.Network;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Controls;

namespace ilift.Controller
{
    public class SelectEquipmentState : ExecutionState
    {
        private const string WELCOME_TEXT = "Welcome ";
        private const string SCAN_AN_EQUIPMENT_TEXT = "Scan an Equipment";
        private const string ERROR_TEXT = "Undefined Equipment";

        private Text _welcomeLabel;
        private Text _scanAnEquipmentLabel;

        public SelectEquipmentState(DisplayTE35 display, StateManager state) : base(display, state)
        {
        }

        public override void init()
        {
            display.WPFWindow.UpdateLayout();
            Font font = Resources.GetFont(Resources.FontResources.NinaB);
            _welcomeLabel = new Text(font, WELCOME_TEXT + stateManager.GetSession().User.username);
            _welcomeLabel.ForeColor = Gadgeteer.Color.Black;
            Canvas.SetTop(_welcomeLabel, 50);
            Canvas.SetLeft(_welcomeLabel, 100);

            _scanAnEquipmentLabel = new Text(font, SCAN_AN_EQUIPMENT_TEXT);
            _scanAnEquipmentLabel.ForeColor = Gadgeteer.Color.Red;
            Canvas.SetTop(_scanAnEquipmentLabel, 100);
            Canvas.SetLeft(_scanAnEquipmentLabel, 100);

            canvas.Children.Add(_welcomeLabel);
            canvas.Children.Add(_scanAnEquipmentLabel);
            
            display.WPFWindow.Child = canvas;

            //Events 
            stateManager.OnCardRead += BindEquipment;
            //TODO add a Log out button so that it returns to the previous state
        }

        private void BindEquipment(string tag)
        {
            NetworkClient.GetEquipmentByTag(tag, equipment =>
            {
                //TODO handle null equipment
                stateManager.GetSession().Equipment = equipment;
                stateManager.SwitchState(new SelectExerciseState(display,stateManager));
            });
        }

        public override void finish()
        {
            stateManager.OnCardRead -= BindEquipment;
        }
    }
}
