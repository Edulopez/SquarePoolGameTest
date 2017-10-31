using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int ballsNumber = 5;
    public int waitingTimeBetweenTurns = 2;
    public ObjectPooling ballsPooling;
    public GameObject ballsStartPosition;
    public GameObject whiteBallStartPosition;
    public GameObject whiteBall;
    private bool _isAnyBallInPlay = true;

    public GameObject cueObject = null;

    public List<Player> players;
    private Queue<Player> _playersQueue;
    private bool _isPlayerStateChanged = false;

    private static Player _currentPlayer;
    public static Player CurrentPlayer { get { return _currentPlayer; }}

    private void Start()
    {
        SetupPlayerPlayOrder();
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

    protected void CheckBallsInPlay()
    {
        if (ballsPooling.GetActiveAmount() > 0)
        {
            _isAnyBallInPlay = true;
            return;
        }
        _isAnyBallInPlay = false;
    }

    protected void ChangePlayerTurn()
    {
        if (_isPlayerStateChanged == false && _currentPlayer.State == PlayerStates.FinishingPlay)
        {
            _isPlayerStateChanged = true;
            _currentPlayer.InvokeWaitingStatus(waitingTimeBetweenTurns);
        }
        else if (_currentPlayer.State == PlayerStates.Waiting)
        {
            _playersQueue.Enqueue(_currentPlayer);
            _currentPlayer = _playersQueue.Dequeue();
            _currentPlayer.State = PlayerStates.Playing;
            _isPlayerStateChanged = false;
        }
    }

    public void ResetGame()
    {
        ballsPooling.DestroyAll();
        ResetWhiteBall();
        StartBalls();
        SetupPlayerPlayOrder();
        _isAnyBallInPlay = true;
    }
    
    public void ResetWhiteBall()
    {
        var rigidBody = whiteBall.GetComponent<Rigidbody>();
        if (rigidBody != null)
            rigidBody.velocity = Vector3.zero;
        whiteBall.transform.position = whiteBallStartPosition.transform.position;
        whiteBall.SetActive(true);
    }

    protected void StartBalls()
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

    protected void SetupPlayerPlayOrder()
    {
        _playersQueue = new Queue<Player>();
        foreach (Player player in players)
        {
            player.Reset();
            _playersQueue.Enqueue(player);
        }
        _currentPlayer = _playersQueue.Dequeue();
        _currentPlayer.State = PlayerStates.Playing;
    }
}
