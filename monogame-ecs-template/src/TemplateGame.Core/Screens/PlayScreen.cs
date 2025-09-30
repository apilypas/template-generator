using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.ECS;
using MonoGame.Extended.Screens;
using NLog;
using TemplateGame.Core.Data;
using TemplateGame.Core.ECS.Entities;
using TemplateGame.Core.ECS.Systems;
using TemplateGame.Core.Services;

namespace TemplateGame.Core.Screens;

public class PlayScreen : GameScreen
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly World _world;
    private readonly InputManager _inputs;

    public PlayScreen(Game game) : base(game)
    {
        var gameState = new GameState();

        var bindings = new[]
        {
            InputBinding.FromKeys(InputActions.Fullscreen, Keys.LeftAlt, Keys.Enter),
            InputBinding.FromKeys(InputActions.Fullscreen, Keys.RightAlt, Keys.Enter)
        };

        _inputs = new InputManager(bindings);

        var entityFactory = new EntityFactory();

        _logger.Info("Building world systems");

        _world = new WorldBuilder()
            .AddSystem(new InputSystem(_inputs, Game.Window, Game))
            .Build();

        entityFactory.Initialize(_world);

        _logger.Info("Building world entities");
    }

    public override void UnloadContent()
    {
        _logger.Info("Unloading screen content");
    }

    public override void Update(GameTime gameTime)
    {
        _inputs.Update();
        _world.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        _world.Draw(gameTime);
    }
}