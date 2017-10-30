using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cue : MonoBehaviour
{

    private float _maxDistance = 1f;
    public float maxHitForce;

    public Ball whiteBall;

    public void StrikeBall(Vector3 startingClickPoint, Vector3 endingClickPoint)
    {
        float cueForce = GetForce(startingClickPoint, endingClickPoint);
        whiteBall.GetHit(this.transform.forward, cueForce);
    }

    public void UpdateCuePosition(Vector3 initialPosition, Vector3 direction)
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
}
