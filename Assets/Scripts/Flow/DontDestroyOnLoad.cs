using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dk.Billekode.Asteroids.Flow
{
    public class DontDestroyOnLoad : MonoBehaviour
    {

        void Start()
        {
            GameObject.DontDestroyOnLoad(this.gameObject);
        }

    }
}