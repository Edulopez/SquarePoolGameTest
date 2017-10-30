using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Score 
{
    public int Points
    {
        get { return _points; }
        private set { }
    }

    int _points = 0;

    public void AddPoints(Ball ball)
    {
        _points += ball.points;
    }
}

