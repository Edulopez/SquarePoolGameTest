using System;
using System.Collections;
using System.Collections.Generic;
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

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        if (useRandomColor)
            ChangeColor();
        _rigidbody = this.GetComponent<Rigidbody>();
    }

    private void StopWhenSlow()
    {
        if (_rigidbody.velocity.magnitude < minimunSpeed)
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void ChangeColor()
    {
        var render = this.GetComponent<Renderer>();
        var colorsArray = new Color[] { Color.black, Color.blue, Color.yellow, Color.green, Color.red };
        render.material.color = colorsArray[Random.Range(0, colorsArray.Length)];
    }

    public void GetHit(Vector3 direction, float force)
    {
        _rigidbody.AddForce(direction * force);
    }
}
