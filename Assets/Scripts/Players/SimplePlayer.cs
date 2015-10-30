using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SimplePlayer : BasePlayer
{
    private string _name;
    private int _score = 0;

    //Constructors
    public SimplePlayer(string name = "Unnamed Player")
    {
        _name = name;
    }

    public void resetScore() { _score = 0; }
    public void addScore(int amount) { _score += amount; }

    //Getters
    public int getScore() { return _score; }
    public string getName() { return _name; }
}
