using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using Assets.Scripts.Helpers;
using UnityEngine;

public class PocketHole : MonoBehaviour
{
    public AudioClip pockedSoundClip;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == Tags.Ball || col.tag == Tags.WhiteBall)
        {
            var ball = col.gameObject.GetComponent<Ball>();
            if (ball == null) return;

            GivePoints(ball, GameController.CurrentPlayer);
            ball.Destroy();
        }
    }

    protected void GivePoints(Ball ball, Player player)
    {
        player.AddPoints(ball);
        PlayBallPocketSound();
    }

    protected  void PlayBallPocketSound()
    {
        SoundHelper.PlaySound(_audioSource, pockedSoundClip);
    }
}
