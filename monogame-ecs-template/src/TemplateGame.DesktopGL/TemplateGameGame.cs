using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using NLog;
using TemplateGame.Core;
using TemplateGame.Core.Screens;

namespace TemplateGame.DesktopGL;

public class TemplateGameGame : Game
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    
    private GraphicsDeviceManager _graphics;
    private ScreenManager _screenManager;
    
    public TemplateGameGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = Constants.VirtualScreenWidth;
        _graphics.PreferredBackBufferHeight = Constants.VirtualScreenHeight;
        
        IsMouseVisible = true;

        Window.Title = "TemplateGame";
        Window.AllowUserResizing = true;
        
        Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
        _logger.Info("Initializing services");
        
        _screenManager = new ScreenManager();
        _screenManager.Initialize();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _screenManager.LoadScreen(new PlayScreen(this));
    }

    protected override void Update(GameTime gameTime)
    {
        _screenManager.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _screenManager.Draw(gameTime);
        base.Draw(gameTime);
    }
}