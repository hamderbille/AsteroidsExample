using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dk.Billekode.Asteroids.Entities;

namespace dk.Billekode.Asteroids.Entities
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;
        private int score = 0;
        [SerializeField]
        GameObject[] asteroidPrefabs;

        void Awake()
        {
            if (GameController.Instance != null)
                Debug.LogError("[GameController] - instance already set!!");
            else
                GameController.Instance = this;
        }

        void Start()
        {
            ARoundMapEntity.yMin = -Camera.main.orthographicSize;
            ARoundMapEntity.yMax = Camera.main.orthographicSize;
            //Debug.Log("yMin: " + ARoundMapEntity.yMin);

            ARoundMapEntity.xMin = ARoundMapEntity.yMin * Camera.main.aspect;
            ARoundMapEntity.xMax = ARoundMapEntity.yMax * Camera.main.aspect;

            StartGame();
        }

        private void StartGame()
        { 
            //Instaniate some asteroids
            GameObject instantiatedAsteroid = Instantiate<GameObject>(GameController.Instance.RandomAsteroidPrefab(), new Vector3(Random.Range(ARoundMapEntity.xMin, ARoundMapEntity.xMax), Random.Range(ARoundMapEntity.yMin, ARoundMapEntity.yMax), 0f), new Quaternion() );
            instantiatedAsteroid.GetComponent<Asteroid>().Size = 3;

        }

        public GameObject RandomAsteroidPrefab()
        { 
            return this.asteroidPrefabs[Random.Range(0, this.asteroidPrefabs.Length - 1)];
        }


        public void AddScore(int scoreGain)
        {
            this.score += scoreGain;
        }
    }
}
