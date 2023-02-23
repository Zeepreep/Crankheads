using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;
class Level : GameObject
{
    public static Player player;
    GameObject scrollObject;
    TiledLoader loader;
    string currentLevelName;
    public Collectible collectibleSpawner;

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
        scrollObject = FindObjectOfType<ScrollObject>();
       

    }

    /// <summary>
    /// Handles the level scrolling.
    /// </summary>
    void Scrolling()
    {
        int boundary = 1000;
        int rightBoundary = 1000;

        if (scrollObject.x + x < boundary)
        {
            x = boundary - scrollObject.x;
        }
        if (scrollObject.x + x > game.width - rightBoundary)
        {
            x = game.width - rightBoundary - scrollObject.x;
        }
    }

    /// <summary>
    /// Creates usable buttons in Tiled and sets up the pipe loader.
    /// </summary>

    void ObjectCreateCallback(Sprite sprite, TiledObject obj)
    {
        if (sprite != null) Console.WriteLine("Creating" + sprite.name);
        if (obj.Type == "Button")
        {
            AddChild(new Button(sprite, obj));
        }
        if(obj.Type == "Collectible")
        {
            //RESEARCH THIS
            //RESEARCH THE BUTTON THING TOO
            collectibleSpawner = (Collectible)sprite;
        }

    }

    void Update()
    {
        if (scrollObject == null) return;

        Scrolling();
    }
}

