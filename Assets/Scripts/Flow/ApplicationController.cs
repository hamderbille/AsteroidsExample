using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace dk.Billekode.Asteroids.Flow
{
    public class ApplicationController : MonoBehaviour 
    {

        public static ApplicationController Instance;

        private EApplicationState state;

        public EApplicationState State
        {
            get 
            {
                return this.state;
            }
            set
            {
                ReactToStateSwitch(value);
                this.state = value;
            }
        }

	    void Awake () 
        {
            if (ApplicationController.Instance != null)
                Debug.LogError("[ApplicationController] - instance already set!!");
            else
                ApplicationController.Instance = this;

            this.state = EApplicationState.Initializing;
	    }

        void Start()
        {
            this.State = EApplicationState.Menu;
        }

        private void ReactToStateSwitch(EApplicationState newState)
        {
            switch (newState)
            { 
                case EApplicationState.Menu:
                    SceneManager.LoadScene("Menu");
                    break;
                case EApplicationState.Game:
                    SceneManager.LoadScene("Game");
                    break;
                default:
                    Debug.LogWarning("[ApplicationController] urecognized state : " + newState);
                    break;
            }
        }

		
	}
}
