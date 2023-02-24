using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

class Pickup : AnimationSprite {

    Sound pickupSound;
    Random rand = new Random();
    static string EnemyName;
    static int cols;
    static int rows;

    //public Pickup(TiledObject obj=null) : base(EnemyName, cols, rows) {
    public Pickup(TiledObject obj = null) : base("squid_all.png", 8, 2) {
        collider.isTrigger = true;
        pickupSound = new Sound("sfx_point.wav", false, false);
        SetScaleXY(1f, 0.5f);
        SetCycle(0, 2, 255, true);
    }
    /// <summary>
    /// Handles audio and destruction for pickups.
    /// </summary>
    public void Grab()
        {
           /* pickupSound.Play();
            Console.WriteLine("Pickup Grabbed!"); */
            LateDestroy();
        }

    void RandomizeEnemies()
    {
        int enemyNumber = 1;
            //rand.Next(0, 0);
        if(enemyNumber == 1)
        {
            EnemyName = "enemy1.png";
            cols = 3;
            rows = 3;
        }
    }

    /// <summary>
    /// Creates the pickups used in Tiled.
    /// </summary>

    public Pickup(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, rows, cols) {
        collider.isTrigger = true;
    }

    void Update()
    {
        RandomizeEnemies();
        Animate();
    }
}
