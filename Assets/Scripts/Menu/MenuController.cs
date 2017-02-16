using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dk.Billekode.Asteroids.Flow;
using UnityEngine.UI;

namespace dk.Billekode.Asteroids.HighScores
{
    public class MenuController : MonoBehaviour 
    {
        public static MenuController Instance;

        [SerializeField]
        private GameObject mainMenuPanel;

        [SerializeField]
        private GameObject highScorePanel;

        [SerializeField]
        private GameObject highScoreRoot;

        [SerializeField]
        private GameObject highScoreEntryPrefab;

	    void Awake () 
        {
            if (MenuController.Instance != null)
                Debug.LogError("[MenuController] - instance already set!!");
            else
                MenuController.Instance = this;

            this.mainMenuPanel.SetActive(true);
            this.highScorePanel.SetActive(false);
	    }

        public void OnQuitButtonClicked()
        {
            ApplicationController.Instance.State = EApplicationState.Shutdown;
        }

        public void OnHighScoreButtonClicked()
        {
            this.mainMenuPanel.SetActive(false);
            this.highScorePanel.SetActive(true);

            ShowHighScores();
        }

        public void OnStartGameButtonClicked()
        {
            ApplicationController.Instance.State = EApplicationState.Game;
        }

        public void OnBackToMenuButtonClicked()
        {
            DeleteHighScoreEntries();

            this.mainMenuPanel.SetActive(true);
            this.highScorePanel.SetActive(false);
        }

        private void ShowHighScores()
        {
            List<HighScore> highScores = HighScoreController.Instance.HighScoresClone;
            for (int i = highScores.Count - 1; i >= 0; i--)
            {
                HighScore highScore = highScores[i];
                GameObject scoreEntry = Instantiate(this.highScoreEntryPrefab, this.highScoreRoot.transform, false);
                scoreEntry.GetComponent<Text>().text = string.Format("Player: {0} Score: {1}", highScore.name, highScore.score.ToString());
            }
        }

        private void DeleteHighScoreEntries()
        {
            foreach (Transform highScoreEntry in this.highScoreRoot.transform)
            {
                Destroy(highScoreEntry.gameObject);
            }
        }

	}
}
