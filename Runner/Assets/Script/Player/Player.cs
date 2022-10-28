using Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private RoadInfo _roadInfo;
        [SerializeField] private float _speed;
        [SerializeField] private float _displacementSpeed;
        [SerializeField] private float _jumpF;
        [SerializeField] private CharacterController _characterController;
        [SerializeField, Min(0.1f)] private float _gravityControl;
        [SerializeField] private Animator _animator;
        [SerializeField] private Animation _cameraClip;

        private float _gravity = -9.81f;
        private float _gravityScal;
        private bool _isDisplacement = true;
        private bool _isJump = true;
        private bool _isSlide = true;
        private Coroutine _coroutine;
        private Vector3 _velosity = Vector3.zero;
        Vector3 vector = new Vector3(0, 0, 1);
        private int _num = 0;
        [SerializeField] private int _min;
        [SerializeField] private int _max;

        private void Start()
        {
            Platform._Jump += Jump;
            Platform._Displacement += Displacement;
            Platform._Slide += Slide;
            _gravityScal = _gravityControl;

            _min = -(_roadInfo._numRoads - 1) / 2;
            _max = _roadInfo._numRoads + (_min-1);
            Debug.Log(_num);
        }
        private void Update()
        {
            Gravity();
            Move();
        }

        private void Move()
        {
            _characterController.Move(vector * _speed * Time.deltaTime);
        }
        private void Gravity()
        {
            if (_characterController.isGrounded)
            {
                Debug.Log("1__Yes");
                _isJump = true;
                _velosity.y = 0;
            }
            _velosity.y += _gravity * _gravityScal * Time.deltaTime;
            _characterController.Move(_velosity * Time.deltaTime);
            
            if(_characterController.gameObject.transform.position.y < 0.03f)
            {
                _characterController.gameObject.transform.position -= new Vector3(0, _characterController.gameObject.transform.position.y, 0);
            }
        }
        private void Jump()
        {
            Debug.Log("2__Yes");
            if (_isJump)
            {
                _gravityScal = _gravityControl;
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                    _isSlide = true;
                }
                _isJump = false;
                _animator.SetTrigger("Jump");
                _velosity.y = _jumpF;
                _velosity.y += _gravity * _gravityScal * Time.deltaTime;
                _characterController.Move(_velosity * Time.deltaTime);
            }
        }
        private void Slide()
        {
            if (_isSlide)
            {
                _isJump = true;
                _gravityScal *= 10; 

                _isSlide = false;
                _cameraClip.Play("Came Slide");
                _animator.SetTrigger("Slide");
                _coroutine = StartCoroutine(CR_Slide());
            }
        }
        private void Displacement(int vector)
        {
            if(_isDisplacement && _num + vector >= _min && _num + vector <= _max)
            {
                _isDisplacement = false;
                StartCoroutine(CR_Displacement(vector));
            }
        }

        private IEnumerator CR_Slide()
        {
            yield return new WaitForSeconds(1.5f);
            _gravityScal = _gravityControl;
            _isSlide = true;
        }

        private IEnumerator CR_Displacement(int vector)
        {
            _num+= vector;
            float test = _characterController.gameObject.transform.localPosition.x;
            float test_2 = _characterController.gameObject.transform.localPosition.x;
            while (_roadInfo._width >= Mathf.Abs(test_2 - test))
            {
                _characterController.Move(new Vector3(vector * Time.deltaTime * _displacementSpeed, 0, 0));
                yield return new WaitForSeconds(Time.deltaTime);
                test_2 = _characterController.gameObject.transform.localPosition.x;
            }
            _characterController.gameObject.transform.localPosition = new Vector3(_num * _roadInfo._width, _characterController.gameObject.transform.localPosition.y,
                _characterController.gameObject.transform.localPosition.z);
            _isDisplacement = true;

        }

        private void OnDestroy()
        {
            Platform._Jump -= Jump;
            Platform._Displacement -= Displacement;
            Platform._Slide -= Slide;
        }
    }
}
