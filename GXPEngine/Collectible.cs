using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;
using GXPEngine;

class Collectible : AnimationSprite
{
    static int collectibleDistance;
    static int collectibleToCreate;
    int collectibleCreated = 0;
    public static bool infiniteCollectibles = false;
    public static int winScore = 100;

    public Collectible(string fileName, int cols, int rows, TiledObject obj=null) : base(fileName, cols, rows, addCollider:false)
    {
        Initialize(obj);

        this.visible = false;

        this.scale = 0;
    }

    /// <summary>
    /// Sets up the customizable properties for use in Tiled.
    /// </summary>
    void Initialize(TiledObject obj=null)
    {
        if (obj != null)
        {
            collectibleToCreate = obj.GetIntProperty("CollectibleAmount", 10);
            infiniteCollectibles = obj.GetBoolProperty("InfiniteCollectibles", true);
            winScore = obj.GetIntProperty("WinScore", 10);
            collectibleDistance = obj.GetIntProperty("BetweenCollectibleDistance", 175);


        }
    }

    /// <summary>
    /// Creates the pipes.
    /// </summary>
    public void createCollectible()
    {
        CollectibleObject collectible = new CollectibleObject();
        parent.AddChild(collectible);
        collectible.SetXY(x + (collectibleCreated * collectibleDistance), (game.height / 2) + Utils.Random(-300, 300));
        collectibleCreated++;
    }

    void Update()
    {
        
        while (collectibleToCreate > 0) 
        {
            createCollectible();
            collectibleToCreate--;
        }
    }
}
