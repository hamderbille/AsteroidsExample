using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dk.Billekode.Asteroids.Entities
{
    public class Projectile : ARoundMapEntity
    {
        [SerializeField]
        private float lifeTime = 3f;

        void Start()
        {
            Destroy(this.gameObject, this.lifeTime);
        }

        void Update()
        {
            KeepWithinTorus();
        }



    }
}