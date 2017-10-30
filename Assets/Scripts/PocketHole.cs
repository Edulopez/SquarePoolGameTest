using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;

public class PocketHole : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == Tags.Ball || col.tag == Tags.WhiteBall)
        {
            var ball = col.gameObject.GetComponent<Ball>();
            if (ball == null)
                return;

            GivePoints(ball, GameController.CurrentPlayer);
            ball.Destroy();
        }
    }

    void GivePoints(Ball ball, Player player)
    {
        player.CurrentScore.AddPoints(ball);
    }
}
