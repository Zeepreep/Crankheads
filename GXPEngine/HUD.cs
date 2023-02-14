using System;
using System.Drawing;
using System.Security.Principal;
using GXPEngine;

class HUD : GameObject {
    EasyDraw scoreCounter;
    EasyDraw highscoreCounter;
    Font sans;
    public int score;
    public static bool highscoreHUD;
    public static bool hudNeeded;
    
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
        sans = Utils.LoadFont("Tw Cen MT.ttf", 24);
        scoreCounter = new EasyDraw(200, 40, false);
        scoreCounter.TextFont(sans);
        scoreCounter.TextAlign(CenterMode.Min, CenterMode.Max);
        AddChild(scoreCounter);

        highscoreCounter = new EasyDraw(200, 40, false);
        highscoreCounter.TextFont(sans);
        highscoreCounter.TextAlign(CenterMode.Min, CenterMode.Max);
        AddChild(highscoreCounter);
    }

    /// <summary>
    /// Updates the score on the HUD and checks if the HUD is needed, if not, it will be hidden.
    /// </summary>
    void UpdateScore()
    {
        scoreCounter.Clear(0, 0, 0, 0);
        scoreCounter.Text("Score: " + score);
        if (hudNeeded == true) {
            scoreCounter.Fill(255, 255, 0);
            scoreCounter.SetXY(235, 0);  
        } else {
            highscoreHUD = false;
            scoreCounter.Fill(255, 255, 0, 0);
        }
    }

    void Update()
    {
        if (hudNeeded == true) { 
        UpdateScore();
        }
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
