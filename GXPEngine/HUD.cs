using System;
using System.Drawing;
using GXPEngine;

class HUD : GameObject {
    EasyDraw scoreCounter;
    Font sans;
    public int score;

    /// <summary>
    /// Ensures there is always a HUD on screen.
    /// </summary>
    public static HUD main
    {
        get
        {
            if (_main == null)
            {
                _main = new HUD();
            }
            return _main;
        }
    }

    static HUD _main;

    /// <summary>
    /// Creates the text for the HUD.
    /// </summary>
    public HUD()
    {
        sans = Utils.LoadFont("betterComicSans.ttf", 20);
        scoreCounter = new EasyDraw(200, 40, false);
        scoreCounter.TextFont(sans);
        scoreCounter.TextAlign(CenterMode.Min, CenterMode.Max);
        AddChild(scoreCounter);
    }

    void Update()
    {
        scoreCounter.Clear(0,0,0,0);
        scoreCounter.Fill(255, 255, 0);
        scoreCounter.Text("Score: " + score);
    }

    /// <summary>
    /// Removes the main from player upon death.
    /// </summary>
    protected override void OnDestroy()
    {
        if (this == _main)
        {
            _main = null;
        }
    }
}
