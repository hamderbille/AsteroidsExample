using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dk.Billekode.Asteroids.Entities
{
    public abstract class ARoundMapEntity : MonoBehaviour 
    {
        public static float xMin = -20f;
        public static float xMax = 20f;
        public static float yMin = -20f;
        public static float yMax = 20f;

        protected void KeepWithinTorus()
        {
            bool changed = false;

            float newX = this.transform.position.x;
            if (this.transform.position.x < xMin)
            {
                newX = xMax;
                changed = true;
            }
            else if (this.transform.position.x > xMax)
            {
                newX = xMin;
                changed = true;
            }

            float newY = this.transform.position.y;
            if (this.transform.position.y < yMin)
            {
                newY = yMax;
                changed = true;
            }
            else if (this.transform.position.y > yMax)
            {
                newY = yMin;
                changed = true;
            }

            if(changed)
                this.transform.position = new Vector2(newX, newY);
        }


		
	}
}
