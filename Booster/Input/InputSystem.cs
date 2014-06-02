using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Booster.Input
{
    public class InputSystem
    {
        private static InputSystem instance = null;

        private KeyboardState keyboardState;
        private GamePadState gamePadState;

        private List<VirtualButtonConfig> virtualButtonConfigs;

        public List<VirtualButtons> PreviousActions { get; set; }
        public List<VirtualButtons> CurrentActions { get; set; }

        public Vector2 LeftThumbSticks;
        public Vector2 RightThumbSticks;

        public float LeftTrigger;
        public float RightTrigger;

        private InputSystem()
        {
            keyboardState = Keyboard.GetState();
            gamePadState = GamePad.GetState(PlayerIndex.One);

            virtualButtonConfigs = new List<VirtualButtonConfig>();

            AddAction(VirtualButtons.Start, Keys.Enter, Buttons.Start);
            AddAction(VirtualButtons.Back, Keys.Escape, Buttons.Back);
            AddAction(VirtualButtons.Guide, Keys.F5, Buttons.BigButton);

            AddAction(VirtualButtons.Left, Keys.Left, Buttons.DPadLeft);
            AddAction(VirtualButtons.Right, Keys.Right, Buttons.DPadRight);
            AddAction(VirtualButtons.Up, Keys.Up, Buttons.DPadUp);
            AddAction(VirtualButtons.Down, Keys.Down, Buttons.DPadDown);

            AddAction(VirtualButtons.A, Keys.Space, Buttons.A);
            AddAction(VirtualButtons.B, Keys.Back, Buttons.B);
            AddAction(VirtualButtons.X, Keys.J, Buttons.X);
            AddAction(VirtualButtons.Y, Keys.I, Buttons.Y);

            AddAction(VirtualButtons.LB, Keys.LeftShift, Buttons.LeftShoulder);
            AddAction(VirtualButtons.RB, Keys.RightShift, Buttons.RightShoulder);

            AddAction(VirtualButtons.LT, Keys.LeftControl, Buttons.LeftTrigger);
            AddAction(VirtualButtons.RT, Keys.RightControl, Buttons.RightTrigger);

            AddAction(VirtualButtons.LeftStick, Keys.X, Buttons.LeftStick);
            AddAction(VirtualButtons.RightStick, Keys.M, Buttons.RightStick);

            LeftThumbSticks = Vector2.Zero;
            RightThumbSticks = Vector2.Zero;

            LeftTrigger = 0f;
            RightTrigger = 0f;

            PreviousActions = new List<VirtualButtons>();
            CurrentActions = new List<VirtualButtons>();
        }

        public static InputSystem GetInstance()
        {
            if (instance == null)
            {
                instance = new InputSystem();
            }

            return instance;
        }

        public void GetActions()
        {
            keyboardState = Keyboard.GetState();
            gamePadState = GamePad.GetState(PlayerIndex.One);

            PreviousActions = new List<VirtualButtons>(CurrentActions);
            CurrentActions = new List<VirtualButtons>();

            foreach (VirtualButtonConfig virtualButtonConfig in virtualButtonConfigs)
            {
                if (keyboardState.IsKeyDown(virtualButtonConfig.Key) || gamePadState.IsButtonDown(virtualButtonConfig.Button))
                {
                    CurrentActions.Add(virtualButtonConfig.VirtualButton);
                }
            }

            LeftThumbSticks = gamePadState.ThumbSticks.Left;
            RightThumbSticks = gamePadState.ThumbSticks.Right;

            LeftTrigger = gamePadState.Triggers.Left;
            RightTrigger = gamePadState.Triggers.Right;
        }

        public void AddAction(VirtualButtons virtualButton, Keys key, Buttons button)
        {
            VirtualButtonConfig virtualButtonConfig = new VirtualButtonConfig(virtualButton, key, button);
            virtualButtonConfigs.Add(virtualButtonConfig);
        }

        public void ChangeKeyConfig(VirtualButtons virtualButton, Keys key)
        {
            int configToChange = -1;
            int configWithKey = -1;

            foreach (VirtualButtonConfig virtualButtonConfig in virtualButtonConfigs)
            {
                if (virtualButtonConfig.VirtualButton == virtualButton)
                {
                    configToChange = virtualButtonConfigs.IndexOf(virtualButtonConfig);
                }
                if (virtualButtonConfig.Key == key)
                {
                    configWithKey = virtualButtonConfigs.IndexOf(virtualButtonConfig);
                }
            }

            if (configWithKey != -1)
            {
                virtualButtonConfigs[configWithKey].Key = virtualButtonConfigs[configToChange].Key;
            }

            virtualButtonConfigs[configToChange].Key = key;
        }

        public void ChangeButtonConfig(VirtualButtons virtualButton, Buttons button)
        {
            int configToChange = -1;
            int configWithButton = -1;

            foreach (VirtualButtonConfig virtualButtonConfig in virtualButtonConfigs)
            {
                if (virtualButtonConfig.VirtualButton == virtualButton)
                {
                    configToChange = virtualButtonConfigs.IndexOf(virtualButtonConfig);
                }
                if (virtualButtonConfig.Button == button)
                {
                    configWithButton = virtualButtonConfigs.IndexOf(virtualButtonConfig);
                }
            }

            if (configWithButton != -1)
            {
                virtualButtonConfigs[configWithButton].Button = virtualButtonConfigs[configToChange].Button;
            }

            virtualButtonConfigs[configToChange].Button = button;
        }
    }
}
