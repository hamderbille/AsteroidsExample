using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dk.Billekode.Asteroids.Entities
{
    public class SpaceShip : ARoundMapEntity 
    {
        [SerializeField]
        private float torque;

        [SerializeField]
        private float thrusterForce;

        [SerializeField]
        private GameObject projectilePrefab;

        [SerializeField]
        private float timeBetweenShots = 0.3f;
        private bool readyToFire = true;

        [SerializeField]
        private float projectileStartForce = 20f;

        private Rigidbody2D shipRigidbody;


	    void Start () 
        {
            this.shipRigidbody = GetComponent<Rigidbody2D>();
	    }
	
        void Update()
        {
            HandleRotation();

            HandleThrust();

            HandleFire();

            this.KeepWithinTorus();
        }

        private void HandleRotation()
        {
            this.shipRigidbody.AddTorque(-Input.GetAxis("Horizontal") * this.torque * Time.deltaTime);
        }

        private void HandleThrust()
        {
            this.shipRigidbody.AddForce(this.transform.up * Input.GetAxis("Vertical") * this.thrusterForce * Time.deltaTime);
        }

        private void HandleFire()
        {
            if (this.readyToFire && Input.GetButtonDown("Fire1"))
            {
                //Debug.Log("BLLAAAM!");
                GameObject instantiatedProjectile = Instantiate<GameObject>(this.projectilePrefab, this.transform.position + this.transform.up * 0.3f, this.transform.rotation);
                instantiatedProjectile.GetComponent<Rigidbody2D>().AddForce(this.transform.up * this.projectileStartForce);

                this.readyToFire = false;
                StartCoroutine(SetReadyToFire(this.timeBetweenShots));
            }
        }

        private IEnumerator SetReadyToFire(float delay)
        {
            yield return new WaitForSeconds(delay);
            this.readyToFire = true;
        }
	}
}
