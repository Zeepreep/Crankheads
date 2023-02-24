using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

class Player : AnimationSprite {

    public static float speed = 10;
    float jumpStrength = 10f;
    public static int _score;
    float vy = 0;
    float dx;
    bool isMoving;
    public static int health = 10;

    Sound bulletShoot;
    Sound playerHit;

    /// <summary>
    /// Player with a pre-defined sprite.
    /// </summary>
    public Player(TiledObject obj = null) : base("submarine.png", 1, 1)
    {
        SetCycle(0, 0);
        SetXY(0, 0);
        _score = 0;
        Initialize(obj);

        bulletShoot = new Sound("Character_shooting.wav", false, false);
        playerHit = new Sound("Something_getting_hit_4.wav", false, false);
    }

    /// <summary>
    /// Player with parameters for tiled.
    /// </summary>

    public Player(string FileName, int cols, int rows, TiledObject obj = null) : base(FileName, cols, rows)
    {
        Initialize(obj);
    }

    void Initialize(TiledObject obj = null)
    {
        if (obj != null)
        {
            //scale = obj.GetIntProperty("PlayerScale", 0.7f);
        }
    }

    void Shooting()
    {
        if(Input.GetKeyDown(Key.SPACE))
        {
            Bullet bullet = new Bullet(_mirrorX ? -3 : 30, 0, this);
            bullet.SetXY(x + (_mirrorX ? -1 : 1) * (width / 2), y);
            parent.AddChild(bullet);

            bulletShoot.Play();

        }
    }


    float GetHorizontalInput()
    {
         dx = 0;
            dx += speed;
            Mirror(false, false);
            isMoving = true;
        return dx;
    }


    void Move()
    {
        if (Input.GetKey(Key.UP))
        {
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

        x += speed;

        if (Input.GetKey(Key.RIGHT))
        {
            x += jumpStrength;
        }
        if (Input.GetKey(Key.LEFT))
        {
            x -= jumpStrength;
        }
    }

    void DeathCheck()
    {
        float oldx = x;
        float oldy = y;

        Collision colY = MoveUntilCollision(0, vy);

        if (y > game.height || y < 0)
        {
            y = oldy;
        }
        if (x < ScrollObject.wallPositionX || x > ScrollObject.wallPositionX + 1966)
        {
            ((MyGame)game).GameOver();
        }


        if (health <= 0)
        {
            ((MyGame)game).GameOver();
        }
    }

    void SpeedUp()
    {
        if(_score > 500)
        {
            speed = 8 ;
        }
    }


    void Collisions()
    {


        GameObject[] collisions = GetCollisions();
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i] is Pickup)
            {
                ((Pickup)collisions[i]).Grab();

                health -= 1;

                playerHit.Play();
            }
        }   
    }


    void Update() {

        HUD.main.score = _score;

        if (isMoving)
        {
            Animate(0.1f);
        }

        if (isMoving == true)
        {
            _score += 1;
        }

        SpeedUp();
        Shooting();
        DeathCheck();
        Collisions();
        Move();
    }
}

