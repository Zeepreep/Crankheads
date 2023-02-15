using System;									// System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using System.Collections.Generic;

class MyGame : Game
{
    public string startLevel =
    "menu.tmx";

    string nextLevel = null;
    public Level currentLevel;
    Sound hitSound;

    
    public MyGame() : base(1366, 768, false, true, 1366, 768, false)
    {
        hitSound = new Sound("sfx_hit.wav", false, false);
        OnAfterStep += CheckLoadLevel;
        LoadLevel(startLevel);
    }

    /// <summary>
    /// Plays game over sound and loads new main menu.
    /// </summary>
    public void GameOver()
    {
        hitSound.Play();
        HUD.hudNeeded = false;
        LoadLevel("LoseScreen.tmx");
    }

    /// <summary>
    /// Destroys all objects in a level..
    /// </summary>
    void DestroyAll()
    {
        List<GameObject> children = GetChildren();
        foreach (GameObject child in children)
        {
            child.Destroy();
        }
    }

    /// <summary>
    /// Queues the next level to be loaded.
    /// </summary>
    /// <param name="filename"></param>
    public void LoadLevel(string filename)
    {
        nextLevel = filename;
    }

    /// <summary>
    /// Checks if level should be loaded and adds the HUD.
    /// </summary>
    void CheckLoadLevel()
    {
        if (nextLevel != null)
        {
            DestroyAll();
            currentLevel = new Level(nextLevel);
            AddChild(currentLevel);
            nextLevel = null;
            AddChild(HUD.main);
        }
    }

    /// <summary>
    /// Adds a hotkey for the game to quickly be reloaded
    /// </summary>
    void Update()
    {
        //Hot Reload
        if (Input.GetKeyDown(Key.Q) && Input.GetKey(Key.LEFT_SHIFT))
        {
            Console.WriteLine("Reloading + starting " + startLevel);
            LoadLevel(startLevel);
        }


        if (HUD.main.score == Collectible.winScore)
        {
            LoadLevel("WinScreen.tmx");
            HUD.hudNeeded = false;
        }
    }

    /// <summary>
    /// Starts the main game.
    /// </summary>
    static void Main()                          
    {
        new MyGame().Start();                   
    }
}