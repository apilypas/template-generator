using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using TemplateGame.Core;
using TemplateGame.Core.Screens;
using TemplateGame.Core.Utils;

namespace TemplateGame.DesktopGL;

public class TemplateGameGame : Game
{
    private GraphicsDeviceManager _graphics;
    private ScreenManager _screenManager;

    public TemplateGameGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = Constants.VirtualScreenWidth;
        _graphics.PreferredBackBufferHeight = Constants.VirtualScreenHeight;
        Window.ClientSizeChanged += OnClientSizeChanged;

        Window.Title = $"TemplateGame ({VersionUtils.GetVersion()})";
        Window.AllowUserResizing = true;

        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        Services.AddService(_graphics);
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        _screenManager = new ScreenManager();
        _screenManager.Initialize();
        _screenManager.LoadScreen(new PlayScreen(this));
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _screenManager.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        _screenManager.Draw(gameTime);
    }
    
    private void OnClientSizeChanged(object sender, EventArgs e)
    {
        var isChanged = false;
        var width = Window.ClientBounds.Width;
        var height = Window.ClientBounds.Height;

        if (width < Constants.VirtualScreenWidth)
        {
            width = Constants.VirtualScreenWidth;
            isChanged = true;
        }
        
        if (height < Constants.VirtualScreenHeight)
        {
            height = Constants.VirtualScreenHeight;
            isChanged = true;
        }

        if (isChanged)
        {
            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = height;
            _graphics.ApplyChanges();
        }
    }
}