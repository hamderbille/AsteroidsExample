using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dk.Billekode.Asteroids.HighScores
{
    public class HighScoreController : MonoBehaviour 
    {
        public static HighScoreController Instance;

        private HighScoreCollection highScoreCollection;

        /// <summary>
        /// a clone of the high score list, for display
        /// purposes only
        /// </summary>
        public List<HighScore> HighScoresClone
        {
            get
            {
                List<HighScore> result = new List<HighScore>(this.highScoreCollection.HighScores.Count);
                foreach (HighScore highScore in this.highScoreCollection.HighScores)
                {
                    result.Add((HighScore)highScore.Clone());
                }
                return result;
            }
        }

        void Awake()
        {
            if (HighScoreController.Instance != null)
                Debug.LogError("[HighScoreController] - instance already set!!");
            else
                HighScoreController.Instance = this;

            this.highScoreCollection = HighScoreCollection.Load();
        }

        public bool TryAdd(string name, int score)
        {
            HighScore highScore = new HighScore();
            highScore.name = name;
            highScore.score = score;

            return TryAdd(highScore);
        }

        public bool TryAdd(HighScore highScore)
        {
            this.highScoreCollection.HighScores.Add(highScore);

            bool result = this.highScoreCollection.HighScores.Contains(highScore);
            if (result)
                this.highScoreCollection.Save();

            return result;
        }

        public bool GoodEnoughForHighScore(int score)
        { 
            return this.highScoreCollection.HighScores.Count < 10 || this.highScoreCollection.HighScores[0].score < score;
        }

        public void PrintHighScoresToLog()
        {
            this.highScoreCollection.HighScores.Sort();

            foreach (HighScore highScore in this.highScoreCollection.HighScores)
            {
                highScore.PrintHighScoreToLog();
            }
        }

	}
}
