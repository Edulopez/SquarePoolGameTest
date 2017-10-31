using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUIRenderer : MonoBehaviour
{
    public Text playerName;
    public Text pointsLabel;
    public Text pointsValue;

    private void Update ()
    {
        playerName.text = GameController.CurrentPlayer.NameId;
	    pointsValue.text = GameController.CurrentPlayer.Points.ToString();
    }
}
