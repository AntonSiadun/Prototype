using System.Collections;
using UnityEngine;

namespace NewScripts
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] public GameObject sword;
        private Animator _animator;
        private Collider _swordCollider;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();

            _swordCollider = sword.GetComponent<Collider>();
        }

        public void PlayMoveAnimation(Vector3 inputDirection)
        {
            float velocityY=Vector3.Dot(inputDirection.normalized,transform.forward);
            float velocityX=Vector3.Dot(inputDirection.normalized,transform.right);

            _animator.SetFloat("Y",-velocityX,0.1f,Time.deltaTime);
            _animator.SetFloat("X",velocityY,0.1f,Time.deltaTime);
        }
    
        public void Roll(Vector3 inputDirection)
        {
            Vector3 direction = inputDirection + transform.position;
            transform.LookAt(direction);
            _animator.SetTrigger("Roll");
        }

        public void DirectAttack()
        {
            _animator.SetTrigger("DirectAttack");
            StartCoroutine(EnableSwordColliderForTime(2.35f));
        }
    
        public void SlashAttack()
        {
            _animator.SetTrigger("SlashAttack");
            StartCoroutine(EnableSwordColliderForTime(1.85f));
        }
    
        IEnumerator EnableSwordColliderForTime(float time)
        {
            _swordCollider.enabled= true;
            yield return new WaitForSeconds(time);
            _swordCollider.enabled = false;
        }

        #region Finite States
        
        public void Die()
        {
            _animator.SetTrigger("Die");
        }

        public void React()
        {
            _animator.SetTrigger("React");
        }
        
        public void Win()
        {
            _animator.SetTrigger("Win");
        }
        
        #endregion

        
        public void DisableSword()
        {
            sword.SetActive(false);
        }

    }
}
