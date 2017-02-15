using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dk.Billekode.Asteroids.Entities;
using UnityEngine.UI;
using dk.Billekode.Asteroids.HighScores;

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

        private SpaceShip spaceShip;

        private int wave = 0;

        public int numAsteroidsInGame;

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
            return this.asteroidPrefabs[Random.Range(0, this.asteroidPrefabs.Length - 1)];
        }


        public void AddScore(int scoreGain)
        {
            this.score += scoreGain;
        }

        private void SpawnAsteroids()
        {
            int numberToSpawn = 4 + 2 * this.wave;
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
            //TODO check if score is good enough, 
            //if so, show name input box + button
            //else just show button

            if (HighScoreController.Instance.GoodEnoughForHighScore(this.score))
            {
                //TODO
            }
            else
            {
                //TODO
            }
        }

    }
}
