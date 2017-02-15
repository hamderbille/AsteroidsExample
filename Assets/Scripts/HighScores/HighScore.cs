using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System;

namespace dk.Billekode.Asteroids.HighScores
{
    public class HighScore : IComparable<HighScore>
    {
        [XmlAttribute("name")]
        public string name;

        [XmlAttribute("score")]
        public int score;


        public int CompareTo(HighScore other)
        {
            return this.score.CompareTo(other.score);
        }

        public void PrintHighScore()
        {
            Debug.Log("High Score, name: " + this.name + " score: " + score);
        }
    }
}
