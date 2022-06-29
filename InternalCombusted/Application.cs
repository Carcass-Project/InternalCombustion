using System;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using InternalCombustion;

class Application
{
    public ICWindow window = new ICWindow();
    public ICRenderer? renderer;

    public Shader basicShader;

    public ICCamera cam;

    public Mesh msh;

    void onLoad()
    {
        GL.Enable(EnableCap.CullFace);
        GL.Enable(EnableCap.DepthTest);

        renderer = new ICRenderer(System.Drawing.Color.DeepSkyBlue, 800, 600);

        basicShader = new Shader("vertex.glsl", "fragment.glsl");

        msh = Mesh.GenMeshCube();

        var mat = new ICMaterial(basicShader);
        mat.matColor = Color4.Black;
        msh.material = mat;

        msh.rotation = Matrix4.CreateRotationX(0) * Matrix4.CreateRotationY(0) * Matrix4.CreateRotationZ(0);

        basicShader.Use();

        DevInput.onMouseMove += OnMouseMove;
        DevInput.onKeyPressed += OnKeyDown;

        cam = new ICCamera(new Vector3(0, 0, 0), window.Size.X / (float)window.Size.Y);

        window.CursorGrabbed = true;
    }

    void onFrame(double dt)
    {
        renderer?.Clear();

        //basicShader.SetMatrix4("viewproj", cam.GetProjectionMatrix() * cam.GetViewMatrix());

        basicShader.SetMatrix4("view", cam.GetViewMatrix());
        basicShader.SetMatrix4("proj", cam.GetProjectionMatrix());

        GL.DepthMask(true);
        msh.Draw();

        Thread.Sleep(1/60);
    }

    int i = 0;


    void OnKeyDown(ICKeyEventArgs args)
    {
        if(args.key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.W)
            cam.Position += cam.Front * 90.5f * (float)args._dt;
        if (args.key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.S)
            cam.Position -= cam.Front * 90.5f * (float)args._dt;
        if (args.key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.A)
            cam.Position -= cam.Right * 90.5f * (float)args._dt;
        if (args.key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.D)
            cam.Position += cam.Right * 90.5f * (float)args._dt;
        if (args.key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.Escape)
            window.Close();
    }

    bool firstMouse = true;
    float lastX = 0, lastY = 0;
    void OnMouseMove(ICMouseEventArgs e)
    {
        if (firstMouse)
        {
            lastX = e.position.X;
            lastY = e.position.Y;
            firstMouse = false;
        }
        float xOffset = (e.position.X - lastX);
        float yOffset = (lastY - e.position.Y);
        lastX = e.position.X;
        lastY = e.position.Y;
        float sensitivity = 0.5f;
        xOffset *= sensitivity;
        yOffset *= sensitivity;

        cam.Yaw += xOffset;
        cam.Pitch += yOffset;
    }

    void onUpdate(double dt)
    {
        //i++;
        //msh.rotation = Matrix4.CreateRotationX(i*(float)dt) * Matrix4.CreateRotationY(i* (float)dt) * Matrix4.CreateRotationZ(0);
    }

    void onExit()
    {
        msh.Delete();
    }

    public void Run()
    {
        window._OnLoad = onLoad;
        window._OnRender = onFrame;
        window._OnUpdate = onUpdate;
        window._OnExit = onExit;

        window.Start();
    }
}