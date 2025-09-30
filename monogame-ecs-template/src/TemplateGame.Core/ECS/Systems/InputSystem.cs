using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ECS;
using MonoGame.Extended.ECS.Systems;
using TemplateGame.Core.Data;
using TemplateGame.Core.Services;

namespace TemplateGame.Core.ECS.Systems;

public class InputSystem : EntityUpdateSystem
{
    private readonly InputManager _inputs;
    private readonly GraphicsDeviceManager _graphicsDeviceManager;
    private readonly GameWindow _window;
    private int _windowWidth = Constants.VirtualScreenWidth * 2;
    private int _windowHeight = Constants.VirtualScreenHeight * 2;

    public InputSystem(InputManager inputs, GameWindow window, Game game)
        : base(Aspect.All())
    {
        _inputs = inputs;
        _window = window;
        _graphicsDeviceManager = game.Services.GetService<GraphicsDeviceManager>();
    }

    public override void Initialize(IComponentMapperService mapperService)
    {
    }

    public override void Update(GameTime gameTime)
    {
        if (_inputs.WasActionPressed(InputActions.Fullscreen))
        {
            if (_graphicsDeviceManager != null)
            {
                if (_graphicsDeviceManager.IsFullScreen)
                {
                    _graphicsDeviceManager.PreferredBackBufferWidth = _windowWidth;
                    _graphicsDeviceManager.PreferredBackBufferHeight = _windowHeight;
                    _graphicsDeviceManager.IsFullScreen = false;
                    _graphicsDeviceManager.ApplyChanges();
                }
                else
                {
                    _windowWidth = _window.ClientBounds.Width;
                    _windowHeight = _window.ClientBounds.Height;
                    var displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
                    _graphicsDeviceManager.PreferredBackBufferWidth = displayMode.Width;
                    _graphicsDeviceManager.PreferredBackBufferHeight = displayMode.Height;
                    _graphicsDeviceManager.IsFullScreen = true;
                    _graphicsDeviceManager.ApplyChanges();
                }
            }
        }
    }
}