using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;
class Level : GameObject
{
    Player player;
    TiledLoader loader;
    string currentLevelName;
    public PipeLoader pipeLoader;

    public Level(string filename)
    {
        Console.WriteLine("---Creating new Level object");
        currentLevelName = filename;
        loader = new TiledLoader(filename);
        loader.OnObjectCreated += ObjectCreateCallback;
        CreateLevel();
    }

    /// <summary>
    /// Sets up the layers used in Tiled.
    /// </summary>
    /// <param name="includeImageLayers"></param>
    void CreateLevel(bool includeImageLayers=true)
    {
        Console.WriteLine("Spawning level elements");

        loader.addColliders = false;
        loader.rootObject = game;
        loader.LoadImageLayers();

        loader.rootObject = this;
        loader.addColliders = false;
        loader.LoadTileLayers(0);
        loader.addColliders = true;
        loader.LoadTileLayers(1);
        loader.autoInstance = true;
        loader.LoadObjectGroups();

        player = FindObjectOfType<Player>();
    }

    /// <summary>
    /// Handles the level scrolling.
    /// </summary>
    void Scrolling()
    {
        int boundary = 400;
        int rightBoundary = 400;

        if (player.x + x < boundary)
        {
            x = boundary - player.x;
        }
        if (player.x + x > game.width - rightBoundary)
        {
            x = game.width - rightBoundary - player.x;
        }
    }

    /// <summary>
    /// Loads the button for the menu and the pipeloader.
    /// </summary>
    /// <param name="sprite"></param>
    /// <param name="obj"></param>
    void ObjectCreateCallback(Sprite sprite, TiledObject obj)
    {
        if (sprite != null) Console.WriteLine("Creating" + sprite.name);
        if (obj.Type == "Button")
        {
            AddChild(new Button(sprite, obj));
        }
        if(obj.Type == "PipeLoader")
        {
            pipeLoader = (PipeLoader)sprite;
        }
    }

    void Update()
    {
        if (player == null) return;

        Scrolling();
    }
}

