using Microsoft.Xna.Framework;
using MonoGame.Extended.ECS;
using MonoGame.Extended.Screens;
using NLog;

namespace TemplateGame.Core.Screens;

public class PlayScreen(Game game) : GameScreen(game)
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private World _world;

    public override void LoadContent()
    {
        _logger.Info("Building world systems");

        var worldBuilder = new WorldBuilder();
        
        _world = worldBuilder.Build();
        
        _logger.Info("Building world entities");
    }

    public override void UnloadContent()
    {
        _logger.Info("Unloading screen content");
    }

    public override void Update(GameTime gameTime)
    {
        _world.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        _world.Draw(gameTime);
    }
}