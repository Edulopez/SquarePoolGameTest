using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.AI;

namespace Assets.Scripts.Helpers
{
    public class NamesHelper
    {
        private static int currentNameId =-1;
        private static readonly string[] Names
            = {"Jarvis", "Cortana", "Siri", "Alexa", "Okey G", "Samantha"};

        public static string GetName()
        {
            currentNameId++;
            currentNameId %= Names.Length;
            return Names[currentNameId];
        }

    }
}
