using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public class SoundHelper
    {
        public static void PlaySound(AudioSource audioSource, AudioClip clip)
        {
            if(audioSource != null)
                audioSource.PlayOneShot(clip);
        }
    }
}
