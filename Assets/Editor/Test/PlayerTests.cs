﻿using Assets.Scripts.Enums;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor.Test
{
    public class PlayerTests {

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

            var ball = new GameObject().AddComponent<Ball>();
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

            var ball = new GameObject().AddComponent<Ball>();
            ball.points = 3;

            player.AddPoints(ball);
            player.Reset();
            Assert.AreEqual(0, player.Points);
        }
    }
}
