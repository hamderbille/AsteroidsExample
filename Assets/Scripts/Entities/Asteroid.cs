using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dk.Billekode.Asteroids.Entities
{
    public class Asteroid : ARoundMapEntity 
    {
        [SerializeField]
        private int scoreValue;

        [SerializeField]
        private float newSpawnDistanceModifier = 0.05f;

        private int size;
        public int Size
        {
            set
            {
                this.size = value;
                this.transform.localScale = new Vector3(size, size, 0);
            }
        }

        void Start()
        {
            GameController.Instance.numAsteroidsInGame++;
        }

        void Update()
        {
            KeepWithinTorus();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<SpaceShip>() != null)
            {
                GameController.Instance.SpaceshipDestroyed();
            }
            else if (collision.gameObject.GetComponent<Projectile>() != null)
            {
                //deploy two new asteroids of smaller size, if current is not smallest size
                if (this.size > 1)
                {
                    this.size--;

                    //spawn asteroids adding and subtracting a bit so they won't be on top of each other.
                    GameController.Instance.SpawnAsteroid(this.size, this.transform.position + new Vector3(Random.value * MapWidth, Random.value * MapHeight, 0) * this.newSpawnDistanceModifier);
                    GameController.Instance.SpawnAsteroid(this.size, this.transform.position - new Vector3(Random.value * MapWidth, Random.value * MapHeight, 0) * this.newSpawnDistanceModifier);
                }

                //add score
                GameController.Instance.AddScore(this.scoreValue);

                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }
        }

        public void OnDestroy()
        {
            GameController.Instance.numAsteroidsInGame--;
        }
	}
}
