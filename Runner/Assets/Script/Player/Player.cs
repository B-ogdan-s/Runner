using Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField, Min(0.1f)] private float _gravityControl;
        [SerializeField] private GameObject _player;
        [SerializeField] private float _accelerationMove = 1;

        private bool _isJump = true;
        private bool _isSlide = true;
        private float _accelerationGravity = 1;
        private Coroutine _coroutine;

        private Animator _animator;

        public static System.Action<float> _Gravity;
        public static System.Action<float> _Move;
        public static System.Action<float> _Jump;

        public static System.Action _Restart;

        private void Awake()
        {
            StartingState._Enter += Disable;
            StartingState._Exit += Enable;
            DeathingState._Enter += PlayerDeath;
            DeathingState._Exit += PlayerRestart;
            Store._SetAnimator += SetAnimator;
        }

        /*
        private void Start()
        {
            _animator = _player.GetComponentInChildren<Animator>();
        }
        */
        private void OnEnable()
        {
            Platform._Slide += Slide;
            Platform._Jump += Jump;
            CheckIsGround._Check += JumpCheck;
        }
        private void Update()
        {
            _Gravity?.Invoke(_gravityControl * _accelerationGravity);
            _Move?.Invoke(_accelerationMove);
        }

        private void SetAnimator(Animator animator)
        {
            _animator = animator;
        }

        private void Jump()
        {
            if (_isJump)
            {
                if(_coroutine != null)
                    StopCoroutine(_coroutine);

                _accelerationGravity = 1;
                _isSlide = true;

                _isJump = false;
                _Jump?.Invoke(_gravityControl);
                _animator.SetTrigger("Jump");
            }
        }

        private void Slide()
        {
            if (_isSlide)
            {
                _isJump = true;
                _accelerationGravity = 10;

                _isSlide = false;
                _animator.SetTrigger("Slide");
                _coroutine = StartCoroutine(CR_Slide());
            }
        }

        private void JumpCheck()
        {
            _isJump = true;
        }

        private IEnumerator CR_Slide()
        {
            yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
            _accelerationGravity = 1;
            _isSlide = true;
        }

        private void PlayerDeath()
        {
            _animator.SetTrigger("Death");
            GetComponent<BoxCollider>().enabled = false;
            this.enabled = false;
        }
        private void PlayerRestart()
        {
            GetComponent<BoxCollider>().enabled = true;
            _animator.SetBool("Run", false);
            _animator.SetTrigger("Idel");
            _Restart?.Invoke();
        }

        private void Disable()
        {
            this.enabled = false;
        }
        private void Enable()
        {
            this.enabled = true;
            _animator.SetBool("Run", true);
        }

        private void OnDisable()
        {
            Platform._Jump -= Jump;
            Platform._Slide -= Slide;
            CheckIsGround._Check -= JumpCheck;
        }

        private void OnDestroy()
        {
            StartingState._Enter -= Disable;
            StartingState._Exit -= Enable;
            DeathingState._Enter -= PlayerDeath;
            DeathingState._Exit -= PlayerRestart;

            Store._SetAnimator -= SetAnimator;
        }
    }
}
