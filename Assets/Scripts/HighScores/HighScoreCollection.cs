using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;


namespace dk.Billekode.Asteroids.HighScores
{
    [XmlRoot("HighScoreCollection")]
    public class HighScoreCollection 
    {
        [XmlArray("HighScores")]
        [XmlArrayItem("HighScore")]
        private List<HighScore> highScores = new List<HighScore>();
        /// <summary>
        /// sorted list of high scores
        /// of max size 10
        /// </summary>
        public List<HighScore> HighScores
        {
            get
            {
                this.highScores.Sort();
                this.Trim();
                return this.highScores;
            }
        }

        public void Save()
        {
            Save(Path.Combine(Application.dataPath, "HighScores.xml"));
        }

        public void Save(string path)
        { 
            XmlSerializer serializer = new XmlSerializer(typeof(HighScoreCollection));
            FileStream stream = new FileStream(path, FileMode.Create);
            serializer.Serialize(stream, this);
            stream.Close();
        }

        public static HighScoreCollection Load()
        {
            return Load(Path.Combine(Application.dataPath, "HighScores.xml"));
        }

        public static HighScoreCollection Load(string path)
        {
            if (!File.Exists(path))
            {   //if no high score exists yet, create one and save it
                HighScoreCollection highScoreCollection = new HighScoreCollection();
                highScoreCollection.Save();
            }

            var serializer = new XmlSerializer(typeof(HighScoreCollection));
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(stream) as HighScoreCollection;
            }
        }

        /// <summary>
        /// trim high scores to size 10
        /// </summary>
        public void Trim()
        {
            while (this.highScores.Count > 10)
            {
                this.highScores.RemoveAt(0);
            }
        }

        public void PrintHighScoresToLog()
        {
            this.highScores.Sort();

            foreach (HighScore highScore in this.highScores)
            {
                highScore.PrintHighScoreToLog();
            }
        }

    }
}
