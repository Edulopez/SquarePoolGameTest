using Assets.Scripts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using UnityEngine;


public class Player : MonoBehaviour
{
    public Vector3 cuePosition;
    public GameObject cueObject = null;
    public GameObject whiteBall = null;

    public Vector3 startingClickPoint;
    public Vector3 endingClickPoint;

    public bool IsChargingHit {get { return _isChargingHit; } }
    private bool _isChargingHit = false;

    public PlayerStates State { get; set; }

    public int Points { get { return _points; } }
    private int _points = 0;

    private void Update()
    {
        if (State != PlayerStates.Playing)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChargeHit();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Hit();
        }
        else if (_isChargingHit == false)
        {
            PositionHelper.RotateAround(Input.GetAxis("Horizontal"),whiteBall, cueObject);
        }

        if (!_isChargingHit) return;

        var myCue = cueObject.GetComponent<Cue>();
        myCue.ChargeHit(startingClickPoint, Vector3.back);
        endingClickPoint = cueObject.transform.position;
    }

    public void InvokeWaitingStatus(float invokeTime)
    {
        Invoke("PutPlayerInWaitingState", invokeTime);
    }

    protected void PutPlayerInWaitingState()
    {
        CancelInvoke("PutPlayerInWaitingState");
        if (State != PlayerStates.Playing)
        {
            State = PlayerStates.Waiting;
        }
    }

    public void Reset()
    {
        _points = 0;
        State = PlayerStates.Waiting;
    }

    public void ChargeHit()
    {
        if (_isChargingHit) return;
        startingClickPoint = cueObject.gameObject.transform.position;
        _isChargingHit = true;
    }

    public void Hit()
    {
        if (!_isChargingHit)
        {
            startingClickPoint = Vector3.zero;
            return;
        }

        _isChargingHit = false;
        var cue = cueObject.GetComponent<Cue>();
        if (cue != null)
        {
            State = PlayerStates.FinishingPlay;
            cue.StrikeBall(startingClickPoint, endingClickPoint);
        }
        cueObject.transform.position = startingClickPoint;
    }

    public void AddPoints(Ball ball)
    {
        if(ball == null) return;
        _points += ball.points;
    }
}