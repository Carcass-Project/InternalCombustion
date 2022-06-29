using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;

namespace InternalCombustion
{
    public class ICWindow : GameWindow
    {
        public static NativeWindowSettings nwst {get{
            var nw = new NativeWindowSettings();
            nw.NumberOfSamples = 4;
            nw.Size = new OpenTK.Mathematics.Vector2i(800,600);
            nw.Title = "Internal Combustion";
            return nw;
        }}

        public Action? _OnLoad;
        public Action<double>? _OnRender;
        public Action<double>? _OnUpdate;
        public Action? _OnExit;

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Multisample);

            _OnLoad?.Invoke();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            DevInput._deviceDeltaTime = (float)args.Time;
            _OnRender?.Invoke(args.Time);
            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            _OnUpdate?.Invoke(args.Time);
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            DevInput.currentKeyPressed = e.Key;
            DevInput.RaiseKeyPressedEvent();
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);

            DevInput.mousePosition = e.Position;
            DevInput.RaiseMouseMoveEvent();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            DevInput.currentMouseButton = e.Button;
            DevInput.RaiseMouseButtonEvent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _OnExit?.Invoke();
        }

        public void Start()
        {
            this.Run();
        }

        public ICWindow() : base(new GameWindowSettings(), nwst)
        {

        }
    }
}
