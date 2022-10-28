using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _displacementSpeed;
        [SerializeField] private float _jumpF;
        [SerializeField] private CharacterController _characterController;
        [SerializeField, Min(0.1f)] private float _gravityControl;

        private float _gravity = -9.81f;
        private bool _isDisplacement = true;
        private Vector3 _velosity = Vector3.zero;
        Vector3 vector = new Vector3(0, 0, 1);

        private void Start()
        {
            Platform._Jump += Jump;
            Platform._Displacement += Displacement;
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
                _velosity.y = 0;
            }
            _velosity.y += _gravity * _gravityControl * Time.deltaTime;
            _characterController.Move(_velosity * Time.deltaTime);
        }
        private void Jump()
        {
            _velosity.y = _jumpF;
            _velosity.y += _gravity * _gravityControl * Time.deltaTime;
            _characterController.Move(_velosity * Time.deltaTime);
        }
        private void Displacement(int vector)
        {
            if(_isDisplacement)
            {
                _isDisplacement = false;
                StartCoroutine(CR_Displacement(vector));
            }
        }

        private IEnumerator CR_Displacement(int vector)
        {
            float v = 0;
            while(Mathf.Abs(v) < 2f)
            {
                v += vector * Time.deltaTime * _displacementSpeed;
                _characterController.Move(new Vector3(vector * Time.deltaTime * _displacementSpeed, 0, 0));
                yield return new WaitForSeconds(Time.deltaTime);
            }
            _isDisplacement = true;

        }

        private void OnDestroy()
        {
            Platform._Jump -= Jump;
            Platform._Displacement -= Displacement;
        }
    }
}
