using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace InternalCombustion
{
    public struct ICKeyEventArgs
    {
        public Keys key;
        public float _dt;

        public ICKeyEventArgs(Keys k, float dt)
        {
            this.key = k;
            this._dt = dt;
        }
    }

    public struct ICMouseEventArgs
    {
        public Vector2 position;
        public MouseButton mouseButton;

        public ICMouseEventArgs(MouseButton mouseButton, Vector2 pos)
        {
            this.mouseButton = mouseButton;
            this.position = pos;
        }
    }

    public static class DevInput
    {
        public static Vector2 mousePosition { get; internal set; }
        public static Keys currentKeyPressed { get; internal set; }
        public static MouseButton currentMouseButton { get; internal set; }

        public static float _deviceDeltaTime { get; internal set; }

        public delegate void OnMouseMove(ICMouseEventArgs _args);
        public static event OnMouseMove? onMouseMove;

        public delegate void OnMouseButtonPress(ICMouseEventArgs _args);
        public static event OnMouseButtonPress? onMouseButtonPressed;

        public delegate void OnKeyPressed(ICKeyEventArgs _args);
        public static event OnKeyPressed? onKeyPressed;

        public static bool IsKeyPressed(Keys key)
        {
            if (currentKeyPressed == key)
                return true;
            return false;
        }

        /// <summary>
        /// Don't use this. You can break some stuff..
        /// </summary>
        public static void RaiseKeyPressedEvent()
        {
            onKeyPressed?.Invoke(new ICKeyEventArgs(currentKeyPressed, _deviceDeltaTime));
        }
        public static void RaiseMouseMoveEvent()
        {
            onMouseMove?.Invoke(new ICMouseEventArgs(currentMouseButton, mousePosition));
        }
        public static void RaiseMouseButtonEvent()
        {
            onMouseButtonPressed?.Invoke(new ICMouseEventArgs(currentMouseButton, mousePosition));
        }
    }
}
