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
    public float verticalAngle = 0;

    public Vector3 cuePosition;
    public GameObject cueObject = null;
    public GameObject whiteBall = null;

    public Vector3 startingClickPoint;
    public Vector3 endingClickPoint;

    private bool _isChargingHit = false;
    public bool IsChargingHit
    {
        get { return _isChargingHit; }
    }

    public PlayerStates State { get; set; }

    public Score CurrentScore { get; set; }

    public Player()
    {
        CurrentScore = new Score();
    }

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
            State = PlayerStates.FinishingPlay;
        }
        else if (_isChargingHit == false)
        {
            PositionHelper.RotateAround(Input.GetAxis("Horizontal"),whiteBall, cueObject);
        }

        if (!_isChargingHit) return;

        var myCue = cueObject.GetComponent<Cue>();
        myCue.UpdateCuePosition(startingClickPoint, Vector3.back);
        endingClickPoint = cueObject.transform.position;
    }

    public void InvokeWaitingStatus(float invokeTime)
    {
        Invoke("PutPlayerInWaitingState", invokeTime);
    }

    private void PutPlayerInWaitingState()
    {
        CancelInvoke("PutPlayerInWaitingState");
        if (State != PlayerStates.Playing)
        {
            State = PlayerStates.Waiting;
        }
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
            cue.StrikeBall(startingClickPoint, endingClickPoint);
        cueObject.transform.position = startingClickPoint;
    }
}



    
