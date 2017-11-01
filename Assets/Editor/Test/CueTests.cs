using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor.Test
{
    class CueTests
    {
        [Test]
        public void CueForceShouldBeMaxWhenDistanceIsMaxOrAbove()
        {
            var maxDistance = 15;
            var cue = new Cue();
            cue.Construct(maxDistance);
            cue.maxHitForce = 130;

            Assert.AreEqual(cue.maxHitForce, cue.GetForce(new Vector3(0, 0, 0), new Vector3(0, 0, maxDistance)));
        }

        [Test]
        public void CueForceShouldBeCeroWhenDistanceIsCero()
        {
            var maxDistance = 15;
            var cue = new Cue();
            cue.Construct(maxDistance);
            cue.maxHitForce = 130;

            Assert.AreEqual(0, cue.GetForce(new Vector3(0, 0, 0), new Vector3(0, 0, 0)));
        }

        [Test]
        public void CueShouldNotMoveIfExceedMaxDistanceChargingHit()
        {
            var maxDistance = 0;
            var cue = new GameObject().AddComponent<Cue>();
            cue.Construct(maxDistance);

            var initialPos = cue.transform.position;
            cue.ChargeHit(initialPos,Vector3.forward);
            Assert.AreEqual(initialPos,cue.transform.position);
        }
    }
}
