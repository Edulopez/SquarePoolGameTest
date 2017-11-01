using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using Assets.Scripts.Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public int points = 1;
    public bool useRandomColor = true;

    public float minimunSpeed = 0.02f;
    public bool IsMoving
    {
        get { return _rigidbody.velocity.magnitude >= minimunSpeed; }
    }

    private Rigidbody _rigidbody;

    public AudioClip hitAudioClip;
    private AudioSource _audioSource;

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == Tags.Cue) return;
        if(col.gameObject.tag == Tags.PocketHole) return;

        PlayCollisionSound();
    }

    private void Start()
    {
        if (useRandomColor)
            ChangeColor();
        _rigidbody = this.gameObject.GetComponent<Rigidbody>();
        _audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    public void ChangeColor()
    {
        var render = this.GetComponent<Renderer>();
        var colorsArray = new Color[] { Color.black, Color.blue, Color.yellow, Color.green, Color.red };
        render.material.color = colorsArray[Random.Range(0, colorsArray.Length)];
    }

    public void GetHit(Vector3 direction, float force)
    {
        if (_rigidbody == null)
        return;
        _rigidbody.AddForce(direction * force);
    }

    protected void PlayCollisionSound()
    {
        SoundHelper.PlaySound(_audioSource, hitAudioClip);
    }
    
}
