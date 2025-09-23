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

    public InputSystem(InputManager inputs, Game game) 
        : base(Aspect.All())
    {
        _inputs = inputs;
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
                    _graphicsDeviceManager.PreferredBackBufferWidth = Constants.VirtualScreenWidth;
                    _graphicsDeviceManager.PreferredBackBufferHeight = Constants.VirtualScreenHeight;
                    _graphicsDeviceManager.IsFullScreen = false;
                    _graphicsDeviceManager.ApplyChanges();
                }
                else
                {
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