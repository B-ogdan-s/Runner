using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float _jumpF;

    private CharacterController _characterController;
    private float _gravity = -9.81f;
    private Vector3 _velosity = Vector3.zero;

    private bool _check;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        Player.Player._Gravity += Gravity;
        Player.Player._Jump += Jump;
        CheckIsGround._Check += Ground;
    }
    private void Jump(float gravityScal)
    {
        _check = false;
        _velosity.y = _jumpF;
        _velosity.y += _gravity * gravityScal * Time.deltaTime;
        //_characterController.Move(_velosity * Time.deltaTime);
        transform.localPosition += _velosity * Time.deltaTime;
    }
    private void Gravity(float gravityScal)
    {
        if (!_check && transform.localPosition.y >= 0)
        {
            _velosity.y += _gravity * gravityScal * Time.deltaTime;
            //_characterController.Move(_velosity * Time.deltaTime);
            transform.localPosition += _velosity * Time.deltaTime;
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
        }
    }

    private void Ground()
    {
        _check = true;
    }

    private void OnDestroy()
    {
        Player.Player._Gravity -= Gravity;
        Player.Player._Jump -= Jump;
        CheckIsGround._Check -= Ground;
    }
}
