using UnityEngine;
using Rewired; 

namespace Jam
{
    // Handles single player input for standard 18 inputs. Assumes buttons are pressed instead of held. 
    public class InputHandler : MonoBehaviour
    {

        CarController controller; 

        private float horizontalAxis;
        private float verticalAxis;
        private bool powerupDown;
        private bool pauseDown; 
        
        public float HorizontalAxis
        {
            get
            {
                return horizontalAxis;
            }
        }

        public float VerticalAxis
        {
            get
            {
                return verticalAxis;
            }
        }

        public bool PowerupDown
        {
            get
            {
                return powerupDown;
            }
        }

        public bool PauseDown
        {
            get
            {
                return pauseDown; 
            }
        }

        Rewired.Player player;
        private bool inputActive = false; 

        private void Awake()
        {
            controller = GetComponent<CarController>();
            inputActive = false; 
            
        }

        public void EnableInput(int index)
        {
            inputActive = true; 
            player = ReInput.players.GetPlayer(index);
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.Instance.CurrentState != GameManager.GAME_STATE.RUNNING)
                return;

            if (!inputActive)
                return; 

            // Axes
            horizontalAxis = player.GetAxis(RewiredConsts.Action.HorizontalAxis);
            verticalAxis = player.GetAxis(RewiredConsts.Action.VerticalAxis);

            // Buttons
            powerupDown = player.GetButtonDown(RewiredConsts.Action.Powerup);

            pauseDown = player.GetButtonDown(RewiredConsts.Action.Pause);

            if (pauseDown)
                Debug.Log("Pause from " + controller.CarName); 
        }
    }
}


