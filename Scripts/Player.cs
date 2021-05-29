using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace NewScripts
{
    public class Player : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private PlayerAnimator _playerAnimator;
        private PlayerMoving _playerMoving;
        private PlayerHealthCheck _playerHealthCheck;

        private bool _canMove;
        private void Awake()
        {
            _canMove = true;
        
            _playerInput = GameObject.Find("InputConnect").GetComponent<PlayerInput>();

            _playerHealthCheck=gameObject.AddComponent<PlayerHealthCheck>();
            _playerAnimator = gameObject.AddComponent<PlayerAnimator>();
            _playerMoving = new PlayerMoving(_playerInput.joystick,
                GetComponent<CharacterController>());

            _playerInput.AddAttackingSkill_1_Listener(DirectAttack);
            _playerInput.AddAttackingSkill_2_Listener(SlashAttack);
            _playerInput.AddDefendingSkillListener(Roll);
        }

        private void Update()
        {
            if(IsFalling())
            {
                GameSceneManager.Instance.LeaveRoom();
                DestroyImmediate(this);
            }
            if(!_canMove)
                return;
        
            _playerMoving.MoveCharacter(_canMove);
            _playerAnimator.PlayMoveAnimation(_playerMoving.GetInput());
        }

        #region Moving Block
    
        public void BlockMoving()
        {
            SetMovingStatus(false);
        }

        public void UnlockMoving()
        {
            SetMovingStatus(true);
        }

        private void SetMovingStatus(bool status)
        {
            _canMove = status;
        }
    
    
        IEnumerator BlockMovingForTime(float time)
        {
            _canMove = false;
            yield return new WaitForSeconds(time);
            _canMove = true;
        }
        #endregion

        #region Skills

        private void Roll()
        {
            if (!_canMove)
                return;
            _playerAnimator.Roll(_playerMoving.GetInput());
            StartCoroutine(BlockMovingForTime(2f));
        }

        private void DirectAttack()
        {
            if (!_canMove)
                return;
            _playerAnimator.DirectAttack();
            StartCoroutine(BlockMovingForTime(2.35f));
        }

        private void SlashAttack()
        {
            if (!_canMove)
                return;
            _playerAnimator.SlashAttack();
            StartCoroutine(BlockMovingForTime(1.85f));
        }

        #endregion

        public void Die()
        {
            _playerMoving.TurnOffCharacterController();
            BlockMoving();
            
            _playerAnimator.Die();
        }

        public void React()
        {
            _playerAnimator.React();
        }

        private bool IsFalling()
        {
            return transform.position.y < -5f ? true : false;

        }
    }
}
