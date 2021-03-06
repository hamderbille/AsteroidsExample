﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dk.Billekode.Asteroids.Entities;
using UnityEngine.UI;
using dk.Billekode.Asteroids.HighScores;
using dk.Billekode.Asteroids.Flow;

namespace dk.Billekode.Asteroids.Entities
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;
        
        private int score = 0;

        [SerializeField]
        GameObject[] asteroidPrefabs;

        [SerializeField]
        private float newAsteroidTorqueMax;

        [SerializeField]
        private float newAsteroidForceMax;

        [SerializeField]
        private Text scoreText;

        [SerializeField]
        private Text waveText;

        [SerializeField]
        private GameObject GameOverUI;

        [SerializeField]
        private GameObject GameOverEnterScoreUI;

        [SerializeField]
        private int baseNumberOfAsteroids = 4;

        [SerializeField]
        private int numExtraAsteroidsPerWave = 2;

        private SpaceShip spaceShip;

        private int wave = 0;

        public int numAsteroidsInGame;

        void Awake()
        {
            if (GameController.Instance != null)
                Debug.LogError("[GameController] - instance already set!!");
            else
                GameController.Instance = this;

            this.GameOverEnterScoreUI.SetActive(false);
            this.GameOverUI.SetActive(false);
        }

        void Start()
        {
            ARoundMapEntity.yMin = -Camera.main.orthographicSize;
            ARoundMapEntity.yMax = Camera.main.orthographicSize;
            //Debug.Log("yMin: " + ARoundMapEntity.yMin);

            ARoundMapEntity.xMin = ARoundMapEntity.yMin * Camera.main.aspect;
            ARoundMapEntity.xMax = ARoundMapEntity.yMax * Camera.main.aspect;

            this.spaceShip = GameObject.FindObjectOfType<SpaceShip>();

            StartGame();
        }

        void Update()
        {
            this.scoreText.text = "Score: " + this.score;
            this.waveText.text = "Wave: " + this.wave;

            //Debug.Log("Num roids: " + this.numAsteroidsInGame);
            if (this.numAsteroidsInGame == 0)
            {
                this.wave++;
                score += 100 * this.wave;
                SpawnAsteroids();
            }
        }

        private void StartGame()
        { 
            //Instaniate some asteroids
            SpawnAsteroids();
        }

        private GameObject RandomAsteroidPrefab()
        { 
            return this.asteroidPrefabs[Random.Range(0, this.asteroidPrefabs.Length)];
        }


        public void AddScore(int scoreGain)
        {
            this.score += scoreGain;
        }

        private void SpawnAsteroids()
        {
            int numberToSpawn = this.baseNumberOfAsteroids + this.numExtraAsteroidsPerWave * this.wave;
            for (int i = 0; i < numberToSpawn; i++)
            {
                SpawnAsteroid(3, RandomPositionAtDistanceToShip() );
            }
        }

        public Vector3 RandomPositionAtDistanceToShip()
        {
            Vector3 position = new Vector3();
            while (true)
            {
                position.x = Random.Range(ARoundMapEntity.xMin, ARoundMapEntity.xMax);
                position.y = Random.Range(ARoundMapEntity.yMin, ARoundMapEntity.yMax);

                if (Vector3.Distance(position, spaceShip.transform.position) > ARoundMapEntity.MapWidth * 0.1f)
                    break;
                //else
                //    Debug.Log("Re-rolling!");
            }

            return position;
        }

        public void SpawnAsteroid(int size, Vector3 position)
        {
            Vector2 force = new Vector2(Random.Range(-this.newAsteroidForceMax, this.newAsteroidForceMax), Random.Range(-this.newAsteroidForceMax, this.newAsteroidForceMax));
            SpawnAsteroid(size, position, force);
        }

        public void SpawnAsteroid(int size, Vector3 position, Vector2 force)
        {
            GameObject instantiatedAsteroid = Instantiate<GameObject>(RandomAsteroidPrefab(), position, Quaternion.Euler(0f, 0f, Random.Range(0f,360f)));
            instantiatedAsteroid.GetComponent<Asteroid>().Size = size;
            instantiatedAsteroid.GetComponent<Rigidbody2D>().AddForce(force);
            instantiatedAsteroid.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-this.newAsteroidTorqueMax, this.newAsteroidTorqueMax));
        }

        public void SpaceshipDestroyed()
        { 
            if (HighScoreController.Instance.GoodEnoughForHighScore(this.score))
            {
                this.GameOverEnterScoreUI.SetActive(true);
            }
            else
            {
                this.GameOverUI.SetActive(true);
            }
        }

        public void OnBackToMenu()
        {
            ApplicationController.Instance.State = EApplicationState.Menu;
        }

        public void OnSaveScoreBackToMenu()
        {
            HighScoreController.Instance.TryAdd(this.GameOverEnterScoreUI.GetComponentInChildren<InputField>().text, score);
            ApplicationController.Instance.State = EApplicationState.Menu;
        }

    }
}
