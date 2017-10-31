using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Helpers;
using UnityEngine;

public class Cue : MonoBehaviour
{
    public AudioClip strikAudioClip;
    private AudioSource _audioSource;

    private float _maxDistance = 0.5f;
    public float maxHitForce;

    public Ball whiteBall;

    protected void Start()
    {
        _audioSource = this.gameObject.GetComponent<AudioSource>();
    }
    
    public void StrikeBall(Vector3 startingClickPoint, Vector3 endingClickPoint)
    {
        float cueForce = GetForce(startingClickPoint, endingClickPoint);
        whiteBall.GetHit(this.transform.forward, cueForce);
        PlayStrikeSound();
    }

    public void ChargeHit(Vector3 initialPosition, Vector3 direction)
    {
        var newPos = this.gameObject.transform.position
                    + this.gameObject.transform
                    .TransformDirection(direction * Time.deltaTime );

        if (Vector3.Distance(initialPosition, newPos) > _maxDistance)
            return;

        this.gameObject.transform.position = newPos;
    }

    public float GetForce(Vector3 startingClickPoint, Vector3 endingClickPoint)
    {
        var cueDistance = Vector3.Distance(startingClickPoint, endingClickPoint);
        cueDistance = Math.Min(_maxDistance, cueDistance);
        return maxHitForce * (cueDistance / _maxDistance);
    }

    protected void PlayStrikeSound()
    {
        SoundHelper.PlaySound(_audioSource, strikAudioClip);
    }
}
