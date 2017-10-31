using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Assets.Scripts.Enums;

// too much about you dissertation
// crees que ellos realmente saben sobre eso?
public class PlayerTest {

    [Test]
    public void PlayerShouldStartCharginHitWhenItStartChargingHit()
    {
        var player = new Player();
        player.cueObject = new GameObject();
        player.StartChargingHit();
        Assert.AreEqual(true, player.IsChargingHit);
    }

    [Test]
    public void PlayerShouldBeInPlayingStateWhenItStartChargingHit()
    {
        var player = new Player();
        player.cueObject = new GameObject();
        player.StartChargingHit();
        Assert.AreEqual(PlayerStates.Playing, player.State);
    }

    [Test]
    public void PlayerIsNotCharginHitWhenItHit()
    {
        var player = new Player();
        player.cueObject = new GameObject();
        player.StartChargingHit();
        player.Hit();

        Assert.AreEqual(false, player.IsChargingHit);
    }

    [Test]
    public void PlayerIsFinishingPlayingWhenItHit()
    {
        var player = new Player();
        player.cueObject = new GameObject();
        player.cueObject.AddComponent<Cue>();

        var cue = player.cueObject.GetComponent<Cue>();
        cue.whiteBall = new Ball();

        player.StartChargingHit();
        player.Hit();
        Assert.AreEqual(PlayerStates.FinishingPlay, player.State);
    }

    [Test]
    public void PlayerShouldGetPointsFromBallWhenGiven()
    {
        var player = new Player();
        int points = player.Points;

        var gameObject = new GameObject().AddComponent<Ball>();

        var ball = gameObject.GetComponent<Ball>();
        ball.points = 3;

        player.AddPoints(ball);
        Assert.AreEqual(points+ ball.points,player.Points);
    }

    [Test]
    public void PlayerStateShouldBeWaitingAfterReset()
    {
        var player = new Player();
        player.State = PlayerStates.FinishingPlay;
        player.Reset();
        Assert.AreEqual(PlayerStates.Waiting, player.State);
    }

    [Test]
    public void PlayerShouldNotBeChargingHitAfterReset()
    {
        var player = new Player();
        player.cueObject = new GameObject();
        player.StartChargingHit();
        player.Reset();
        Assert.AreEqual(false, player.IsChargingHit);
    }

    [Test]
    public void PlayerPointsShouldBeZeroAfterReset()
    {
        var player = new Player();
        int points = player.Points;

        var gameObject = new GameObject().AddComponent<Ball>();

        var ball = gameObject.GetComponent<Ball>();
        ball.points = 3;

        player.AddPoints(ball);
        player.Reset();
        Assert.AreEqual(0, player.Points);
    }
}
