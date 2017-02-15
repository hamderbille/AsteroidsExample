using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dk.Billekode.Asteroids.Entities
{
    public class Asteroid : ARoundMapEntity 
    {
        [SerializeField]
        private int scoreValue;

        private int size;
        public int Size
        {
            set
            {
                this.size = value;
                this.transform.localScale = new Vector3(size, size, 0);
            }
        }

	    void Start () 
        {
            //TEST, should be set by gmecontroller
            //this.Size = 3;
	    }

        void Update()
        {
            KeepWithinTorus();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<SpaceShip>() != null)
            {
                Debug.Log("OUCH");
                //TODO destroy ship, end game
            }
            else if (collision.gameObject.GetComponent<Projectile>() != null)
            {
                //deploy two new asteroids of smaller size, if not smallest size
                if (this.size > 1)
                {
                    this.size--;

                    GameObject instantiatedAsteroid = Instantiate<GameObject>(GameController.Instance.RandomAsteroidPrefab(), this.transform.position, new Quaternion());
                    instantiatedAsteroid.GetComponent<Asteroid>().Size = this.size;
                }

                //add score
                GameController.Instance.AddScore(this.scoreValue);

                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }
        }
	}
}
