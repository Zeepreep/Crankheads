using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

class Player : AnimationSprite {

    public static float speed = 5;
    float jumpStrength = 10f;
    public static int _score;
    Sound jumpSound;
    float vy = 0;
    bool isMoving;

    /// <summary>
    /// Player with a pre-defined sprite.
    /// </summary>
    public Player(TiledObject obj = null) : base("Basic_Submarine.png", 1, 1)
    {
        scale = 1;
        SetCycle(0, 0);
        SetXY(0, 0);
        _score = 0;
        Initialize(obj);
    }

    /// <summary>
    /// Player with parameters for tiled.
    /// </summary>

    public Player(string FileName, int cols, int rows, TiledObject obj = null) : base(FileName, cols, rows)
    {
        scale = 1;
        SetCycle(0, 3);
        SetXY(0, 0);
        jumpSound = new Sound("sfx_wing.wav", false, false);
    }

    void Initialize(TiledObject obj = null)
    {
        if (obj != null)
        {
            scale = obj.GetIntProperty("PlayerScale", 1);
        }
    }

    void Shooting()
    {
        if(Input.GetKeyDown(Key.SPACE))
        {
            Bullet bullet = new Bullet(_mirrorX ? -3 : 15, 0, this);
            bullet.SetXY(x + (_mirrorX ? -1 : 1) * (width / 2), y);
            parent.AddChild(bullet);

            //Console.WriteLine("BULLET CREATED");
        }
    }

    /// <summary>
    /// Ensures that the player moves to the right.
    /// </summary>
    /// <returns></returns>
    float GetHorizontalInput()
    {
        float dx = 0;
            dx += speed;
            Mirror(false, false);
            isMoving = true;
        return dx;
    }

    /// <summary>
    /// Adds gravity and jumping mechanics.
    /// </summary>
    void Move()
    {
        if (Input.GetKey(Key.UP)) {
            vy = -jumpStrength;
        }
        else
        {
            vy = 0;
        }

        if (Input.GetKey(Key.DOWN))
        {
            vy = +jumpStrength;
        }
    }

    /// <summary>
    /// Checks for collision with pipes or ground and promptly kills the player.
    /// </summary>
    void DeathCheck()
    {
        float dx = GetHorizontalInput();

        Collision colX = MoveUntilCollision(dx, 0);
        if (colX != null)
        {
            Console.WriteLine("CollectibleObject Touched on X-axis!");
            ((MyGame)game).GameOver();
        }
        Collision colY = MoveUntilCollision(0, vy);
        //if (colY != null)
        //{
        //    Console.WriteLine("CollectibleObject Touched on Y-axis!");
        //    ((MyGame)game).GameOver();
        //}
        if (y > game.height)
        {
            Console.WriteLine("Ground Touched!");
            ((MyGame)game).GameOver();
        }
    }

    void SpeedUp()
    {
        if(_score > 500)
        {
            speed = 8;
        }
    }

    /// <summary>
    /// Handles the invisible pickup between the pipes.
    /// When it is picked up there is 1 point added to the score and a new pipe is created.
    /// </summary>
    void Collisions()
    {
        GameObject[] collisions = GetCollisions();
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i] is Pickup)
            {
                ((Pickup)collisions[i]).Grab();

               // _score += 1;

                //if (Collectible.infiniteCollectibles == true)
                //{
                //((MyGame)game).currentLevel.collectibleSpawner.createCollectible();
                //}
            }
        }
    }


    void Update() {

        HUD.main.score = _score;

        if (isMoving)
        {
            Animate(0.1f);
        }

        SpeedUp();
        Shooting();
        DeathCheck();
        Collisions();
        Move();
    }
}

