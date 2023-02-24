using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

    class Bullet : Sprite
    {
    Sound bulletHit;

    GameObject owner;
    float vx, vy;
    float screenWidth = 1366;


    public Bullet(float pVx, float pVy, GameObject pOwner) : base("torpedo.png") {
        SetOrigin(width / 2, height / 2);
        SetScaleXY(0.5f, 0.5f);
        vx = pVx;
        vy = pVy;
        owner = pOwner; 
        collider.isTrigger = true;
        bulletHit = new Sound("Something_getting_hit_2.wav", false, false);

    }

    public void Movement()
    {
        x += vx;
        y += vy;
    }

    void OnCollision(GameObject other)
    {
        if (other != owner)
        {
            Player._score += 14;
            other.LateDestroy();
            LateDestroy();
            bulletHit.Play();
            ((MyGame)game).currentLevel.enemySpawner.createEnemy();
        }
    }

    void OffScreenCheck()
    {
        Console.WriteLine(screenWidth);
        if (x > Level.player.x + 750)
        {
            LateDestroy();
        }

    }
    
    void Update()
    {
        Movement();
        OffScreenCheck();
    }
}