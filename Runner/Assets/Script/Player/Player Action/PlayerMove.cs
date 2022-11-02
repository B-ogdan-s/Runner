using Level;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private RoadInfo _roadInfo;
    [SerializeField] private float _speed;
    [SerializeField] private float _displacementSpeed;

    [SerializeField]private CharacterController _characterController;
    private Vector3 _vector = new Vector3(0, 0, 1);

    private float _acceleration = 0;

    private int _num = 0;
    private int _min;
    private int _max;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        Player.Player._Move += Move;
        Player.Player._Restart += PlayerRestart;

        _min = -(_roadInfo._numRoads - 1) / 2;
        _max = _roadInfo._numRoads + (_min-1);
        Platform._Displacement += Displacement;
    }

    public void PlayerRestart()
    {
        transform.position = Vector3.zero;
        _num = 0;
        _acceleration = 0;
    }

    private void Move(float acceleration)
    {
        _acceleration += acceleration * Time.deltaTime;
        _characterController.Move(_vector * (_speed + _acceleration) * Time.deltaTime);
    }
    private void Displacement(int vector)
    {
        if (_num + vector >= _min && _num + vector <= _max)
        {
            StartCoroutine(CR_Displacement(vector));
        }
    }
    private IEnumerator CR_Displacement(int vector)
    {
        _num += vector;
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
    }

    private void OnDestroy()
    {
        Platform._Displacement -= Displacement;
        Player.Player._Move -= Move;
        Player.Player._Restart -= PlayerRestart;
    }
}
