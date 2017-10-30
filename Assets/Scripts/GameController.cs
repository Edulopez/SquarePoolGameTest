using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int ballsNumber = 5;
    private bool _isAnyBallInPlay = true;

    public ObjectPooling ballsPooling;
    public GameObject ballsStartPosition;
    public GameObject whiteBallStartPosition;
    public GameObject whiteBall;

    public GameObject cueObject = null;

    public Player testPlayer;
    private Queue<Player> _playersQueue;
    private bool _isPlayerStateChanged = false;

    private static Player _currentPlayer;

    public static Player CurrentPlayer
    {
        get { return _currentPlayer; }
    }

    private void Start()
    {
        _playersQueue = new Queue<Player>();
        _currentPlayer = testPlayer;
        _currentPlayer.State = PlayerStates.Playing;
        InvokeRepeating("CheckBallsInPlay", 1, 2);
    }

    private void Update()
    {
        if (!_isAnyBallInPlay)
        {
            ResetGame();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (Input.GetKeyDown(KeyCode.Alpha1))
            ResetGame();

        if (!whiteBall.activeInHierarchy)
            ResetWhiteBall();

        ChangePlayerTurn();
    }

    private void CheckBallsInPlay()
    {
        if (ballsPooling.GetActiveAmount() > 0)
        {
            _isAnyBallInPlay = true;
            return;
        }
        _isAnyBallInPlay = false;
    }

    private void ChangePlayerTurn( )
    {
        if (_currentPlayer.State == PlayerStates.FinishingPlay && _isPlayerStateChanged == false)
        {
            _isPlayerStateChanged = true;
            _currentPlayer.InvokeWaitingStatus(0);
        }
        else if (_currentPlayer.State == PlayerStates.Waiting)
        {
            _playersQueue.Enqueue(_currentPlayer);
            _currentPlayer = _playersQueue.Dequeue();
            _currentPlayer.State = PlayerStates.Playing;
            _isPlayerStateChanged = false;
        }
    }

    public void StartGame()
    {
        whiteBall.transform.position = whiteBallStartPosition.transform.position;
    }

    public void ResetGame()
    {
        ballsPooling.DestroyAll();
        ResetWhiteBall();
        StartBalls();
        _isAnyBallInPlay = true;
    }
    
    public void ResetWhiteBall()
    {
        var rigidBody = whiteBall.GetComponent<Rigidbody>();
        if (rigidBody != null)
            rigidBody.velocity = Vector3.zero;
        whiteBall.transform.position = whiteBallStartPosition.transform.position;
    }

    private void StartBalls()
    {
        for (int i = 0; i < ballsNumber; i++)
        {
            var ball = ballsPooling.GetPooledObject();
            var newPosition = new Vector3
                (ballsStartPosition.transform.position.x + Random.Range(-0.3f, 0.3f)
                , ballsStartPosition.transform.position.y
                , ballsStartPosition.transform.position.z + Random.Range(-0.3f, 0.3f)
                );
            ball.transform.position= newPosition;
        }
    }

    private void SetCueHigh()
    {
        var yPosition = whiteBall.transform.position.y;
        cueObject.transform.position = new Vector3(cueObject.transform.position.x, yPosition, cueObject.transform.position.z);
    }

    private void RestartCueAndWhiteBallObjectPosition()
    {
        cueObject.transform.position = whiteBall.transform.position;
        cueObject.transform.Translate(cueObject.transform.forward);
    }
}
