﻿using System;
using System.Text;
using Gadgeteer.Modules.GHIElectronics;
using ilift.GUI;
using ilift.Model;
using Microsoft.SPOT;
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;

namespace ilift.Controller
{
    /// <summary>
    /// In this state the exercise for a chosen equipment can be chosen.
    /// </summary>
    class SelectExerciseState : ExecutionState
    {
        private const String SELECTED_EQUIPEMENT_TEXT = "Selected equipment: ";
        private const String CHOOSE_EXERCISE_TEXT = "Choose an exercise";
        private const String CANCEL_TEXT = "Cancel";
        private const String EXERCISE_KEY = "exercise";
        private const int DEFAULT_MARGIN = 10;
        private const int LEFT_OFFSET = 10;
        private const int SPACING = 15;
        private const int CANCEL_LABEL_OFFSET = 120;
        private readonly Font FONT = Resources.GetFont(Resources.FontResources.NinaB);
        private readonly Color TEXT_COLOR = Gadgeteer.Color.Black;
        private readonly Color CANCEL_BUTTON_COLOR = Gadgeteer.Color.Red;
        private readonly Color EXERCISE_BUTTON_COLOR = Gadgeteer.Color.Gray;

        private const int MAX_EXERCISE_BUTTONS = 4;

        private Text _selectedEquipment;
        private Text _chooseExercise;

        private ParameterizedRectangle[] exButtons = new ParameterizedRectangle[4];
        private Text[] buttonLabels = new Text[4];
        private ParameterizedRectangle _cancelButton;
        private Text _cancelLabel;

        public SelectExerciseState(DisplayTE35 display, StateManager state)
            : base(display, state)
        {

        }

        public override void init()
        {
            // Get all available exercises for that equipment
            Exercise[] exercises = stateManager.GetSession().Equipment.Type.AvailableExercises;

            _selectedEquipment = new Text(FONT, SELECTED_EQUIPEMENT_TEXT + stateManager.GetSession().Equipment.Type.Name);
            _selectedEquipment.ForeColor = Gadgeteer.Color.Green;
            canvas.Children.Add(_selectedEquipment);
            Canvas.SetTop(_selectedEquipment, 20);
            Canvas.SetLeft(_selectedEquipment, LEFT_OFFSET);

            _chooseExercise = new Text(FONT, CHOOSE_EXERCISE_TEXT);
            _chooseExercise.ForeColor = Gadgeteer.Color.Black;
            canvas.Children.Add(_chooseExercise);
            Canvas.SetTop(_chooseExercise, 50);
            Canvas.SetLeft(_chooseExercise, LEFT_OFFSET);

            int buttonWidth = (int)(display.Width * 0.40);
            int buttonHeight = (int)(display.Height * 0.15);
            int startY = 2 * buttonHeight;
            int startX = LEFT_OFFSET;

            int minimum = System.Math.Min(MAX_EXERCISE_BUTTONS, exercises.Length);

            // Create all exercise buttons
            for (int i = 0; i < minimum; i++)
            {
                int xOffset = i % 2 == 0 ? 0 : buttonWidth + SPACING;
                int yOffset = i / 2 == 0 ? 0 : buttonHeight + SPACING;

                exButtons[i] = new ParameterizedRectangle(buttonWidth, buttonHeight);
                exButtons[i].AddParameter(EXERCISE_KEY, exercises[i]);
                exButtons[i].Fill = new SolidColorBrush(EXERCISE_BUTTON_COLOR);
                exButtons[i].SetMargin(DEFAULT_MARGIN);
                buttonLabels[i] = new Text(FONT, exercises[i].Name);
                buttonLabels[i].ForeColor = TEXT_COLOR;

                Canvas.SetTop(exButtons[i], startY + yOffset);
                Canvas.SetLeft(exButtons[i], startX + xOffset);
                Canvas.SetTop(buttonLabels[i], startY + buttonHeight / 2 + yOffset);
                Canvas.SetLeft(buttonLabels[i], xOffset + startX + SPACING);

                canvas.Children.Add(exButtons[i]);
                canvas.Children.Add(buttonLabels[i]);
                exButtons[i].TouchDown += ExerciseSelectedHandler;
            }

            // Create a cancel button
            _cancelButton = new ParameterizedRectangle(2 * buttonWidth + SPACING, buttonHeight);
            _cancelButton.Fill = new SolidColorBrush(CANCEL_BUTTON_COLOR);

            _cancelButton.SetMargin(DEFAULT_MARGIN);
            Canvas.SetTop(_cancelButton, startY + 2 * (buttonHeight + SPACING));
            Canvas.SetLeft(_cancelButton, startX);

            _cancelLabel = new Text(FONT, CANCEL_TEXT);
            _cancelLabel.ForeColor = TEXT_COLOR;
            Canvas.SetTop(_cancelLabel, startY + buttonHeight / 2 +
                2 * (buttonHeight + SPACING));
            Canvas.SetLeft(_cancelLabel, startX + CANCEL_LABEL_OFFSET);

            _cancelButton.TouchDown += OnCancelClicked;

            canvas.Children.Add(_cancelButton);
            canvas.Children.Add(_cancelLabel);

            display.WPFWindow.Child = canvas;
        }

        private void OnCancelClicked(object sender, TouchEventArgs touchEventArgs)
        {
            stateManager.SwitchState(new SelectEquipmentState(display, stateManager));
        }

        private void ExerciseSelectedHandler(object sender, TouchEventArgs touchEventArgs)
        {
            ParameterizedRectangle r = (ParameterizedRectangle)sender;
            Exercise exercise = (Exercise)r.GetParameter(EXERCISE_KEY);
            // Set the chosen exercise in the shared session object
            stateManager.GetSession().Exercise = exercise;
            // Switch to the start exercise state
            stateManager.SwitchState(new StartExerciseState(display, stateManager));
        }

        public override void finish()
        {

        }
    }
}
