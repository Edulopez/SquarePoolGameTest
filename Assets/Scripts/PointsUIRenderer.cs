using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsUIRenderer : MonoBehaviour
{

    public Text pointsLabel;
    public Text pointsValue;
    
	void Update ()
	{
	    pointsValue.text = GameController.CurrentPlayer.CurrentScore.Points.ToString();
    }
}
