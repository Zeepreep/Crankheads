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

    GameObject owner;
    float vx, vy;

    public Bullet(float pVx, float pVy, GameObject pOwner) : base("Basic_Submarine.png") {
        SetOrigin(width / 2, height / 2);
        SetScaleXY(0.1f, 0.1f);
        vx = pVx;
        vy = pVy;
        owner = pOwner; 
        collider.isTrigger = true;

    }

    public void Movement()
    {
        x += vx;
        y += vy;

        //GameObject[] collisions = GetCollisions();

        //foreach (GameObject col in collisions)
        //{

        //}
    }

    void OnCollision(GameObject other)
    {
        if (other != owner)
        {
            Player._score += 14;
            other.LateDestroy();
            LateDestroy();
        }
    }

    void OffScreenCheck()
    {
        if (x + width < 0 || x > game.width / game.scaleX || y + height < 0 || y > game.height / game.scaleY)
        {
            LateDestroy();
        }
    }
    
    void Update()
    {
        Movement();
        //OffScreenCheck();
    }
}