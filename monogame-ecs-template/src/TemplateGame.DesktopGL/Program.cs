using System;
using NLog;

namespace TemplateGame.DesktopGL;

public static class Program
{
    private readonly static Logger Logger = LogManager.GetCurrentClassLogger();
    
    public static void Main(string[] args)
    {
        Logger.Info("Starting...");
        var game = new TemplateGameGame();
        try
        {
            game.Run();
        }
        catch (Exception ex)
        {
            Logger.Fatal(ex.ToString());
        }
        finally
        {
            game.Dispose();
            Logger.Info("Exiting...");
        }
    }
}