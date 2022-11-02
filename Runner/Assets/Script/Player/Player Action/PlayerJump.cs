using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float _jumpF;

    private CharacterController _characterController;
    private float _gravity = -9.81f;
    private Vector3 _velosity = Vector3.zero;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        Player.Player._Gravity += Gravity;
        Player.Player._Jump += Jump;
    }
    private void Jump(float gravityScal)
    {
        _velosity.y = _jumpF;
        _velosity.y += _gravity * gravityScal * Time.deltaTime;
        _characterController.Move(_velosity * Time.deltaTime);
    }
    private void Gravity(float gravityScal)
    {
        _velosity.y += _gravity * gravityScal * Time.deltaTime;
        _characterController.Move(_velosity * Time.deltaTime);
    }
    private void OnDestroy()
    {
        Player.Player._Gravity -= Gravity;
        Player.Player._Jump -= Jump;
    }
}
