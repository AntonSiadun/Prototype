using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace NewScripts
{
    public class Player : MonoBehaviourPunCallbacks,IPunObservable
    {
        private PlayerInput _playerInput;
        private PlayerAnimator _playerAnimator;
        private PlayerMoving _playerMoving;
        private PlayerTrigger _playerTrigger;

        public bool canMove = true;
        public bool isWin;
        public bool IsDead { get; private set; }

        private void Awake()
        {
            _playerInput = GameObject.Find("InputConnect").GetComponent<PlayerInput>();
            _playerAnimator = GetComponent<PlayerAnimator>();
            _playerTrigger = GetComponent<PlayerTrigger>();
            _playerTrigger.SetPlayer(this);
            
            _playerMoving = new PlayerMoving(_playerInput.joystick,
                GetComponent<CharacterController>());
            
            //Button Events dont need to control for enemy Object
            if (!photonView.IsMine)
                return;
            
            _playerInput.AddAttackingSkill_1_Listener(DirectAttack);
            _playerInput.AddAttackingSkill_2_Listener(SlashAttack);
            _playerInput.AddDefendingSkillListener(Roll);
        }

        private void Update()
        {
            //Contol only our Object
            if (!photonView.IsMine)
                return;

            if(IsFalling())
            {
                IsDead = true;
            }
            
            if(!canMove||isWin||IsDead)
                return;
            
            _playerMoving.MoveCharacter(canMove);
            _playerAnimator.PlayMoveAnimation(_playerMoving.GetInput());
            
            if (GameRules.Enemy == null)
                return;
            transform.LookAt(GameRules.Enemy.transform);
        }
        
        IEnumerator BlockMovingForTime(float time)
        {
            canMove = false;
            yield return new WaitForSeconds(time);
            canMove = true;
        }

        #region Skills

        private void Roll()
        {
            if (!canMove || IsDead)
                return;
            _playerAnimator.Roll(_playerMoving.GetInput());
            StartCoroutine(BlockMovingForTime(2f));
        }

        private void DirectAttack()
        {
            if (!canMove|| IsDead)
                return;
            _playerAnimator.DirectAttack();
            StartCoroutine(BlockMovingForTime(2.35f));
        }

        private void SlashAttack()
        {
            if (!canMove|| IsDead)
                return;
            _playerAnimator.SlashAttack();
            StartCoroutine(BlockMovingForTime(1.85f));
        }

        #endregion

        #region Finite States

        public void Kill()
        {
            IsDead = true;
            _playerMoving.TurnOffCharacterController();
            _playerAnimator.Die();
        }
        
        public void React()
        {
            _playerAnimator.React();
        }
        
        [PunRPC]
        public void Win()
        {
            Debug.Log($"{photonView.ViewID}");
            _playerAnimator.Win();
        }

        private bool IsFalling()
        {
            return transform.position.y < -2f;

        }
        
        #endregion

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_playerAnimator.sword.activeSelf);
                stream.SendNext(IsDead);
                stream.SendNext(canMove);
            }
            else
            {
                var isActive = (bool) stream.ReceiveNext();
                IsDead = (bool) stream.ReceiveNext();
                canMove = (bool) stream.ReceiveNext();
                
                
                _playerAnimator.sword.SetActive(isActive);
            }
        }

        public override void OnLeftRoom()
        {
            Destroy(this);
        }
    }
}
