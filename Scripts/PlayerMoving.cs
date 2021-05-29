using UnityEngine;

namespace NewScripts
{
    public class PlayerMoving 
    {
        private readonly Joystick _joystick;
        private readonly CharacterController _characterController;
        private const float Speed = 2f;

        public PlayerMoving(Joystick joystick, CharacterController characterController)
        {
            _joystick = joystick;
            _characterController = characterController;
        }
        
        ~PlayerMoving(){}
        
        public Vector3 GetInput()
        {
            var horizontal = -_joystick.Horizontal;
            var vertical = -_joystick.Vertical;
            return new Vector3(horizontal,0f,vertical);
        }
    
        public void MoveCharacter(bool canMove)
        {
            Vector3 movement = GetInput();
        
            if (movement.magnitude > 0)
            {
                movement.Normalize();
                movement *= Speed * Time.deltaTime;
                _characterController.Move(movement);
            }
        }

        public void TurnOffCharacterController()
        {
            _characterController.enabled = false;
        }
    }
}
