using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;
using GXPEngine;

class EnemySpawner : AnimationSprite
{
    static int enemyDistance;
    static int enemyToCreate;
    int enemiesCreated = 0;
    public static bool infiniteEnemies = true;
    public static int winScore = 100;

    public EnemySpawner(string fileName, int cols, int rows, TiledObject obj=null) : base(fileName, cols, rows, addCollider:false)
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
            enemyToCreate = obj.GetIntProperty("CollectibleAmount", 100);
            infiniteEnemies = obj.GetBoolProperty("InfiniteCollectibles", true);
            winScore = obj.GetIntProperty("WinScore", 10);
            enemyDistance = obj.GetIntProperty("BetweenCollectibleDistance", 175);


        }
    }

    /// <summary>
    /// Creates the enemies.
    /// </summary>
    public void createEnemy()
    {
        EnemyObject collectible = new EnemyObject();
        parent.LateAddChild(collectible);
        collectible.SetXY(x + (enemiesCreated * enemyDistance), (game.height / 2) + Utils.Random(-300, 300));
        enemiesCreated++;
    }

    void Update()
    {        

        while (enemyToCreate > 0) 
        {
            createEnemy();
            enemyToCreate--;
        }
    }
}
