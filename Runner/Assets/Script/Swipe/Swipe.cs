using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{

    private bool _isMobile;
    Platform _platform;

    private void Start()
    {
        if(Application.isMobilePlatform)
        {
            Debug.Log("Mobile");
            _platform = new Mobile();
        }
        else
        {
            _platform = new PC();
        }
    }

    private void Update()
    {
        _platform.Svipe();
    }
}

public class PC : Platform
{
    public override void Svipe()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _topPosition = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            ResetSwipe(Input.mousePosition);
        }
    }
}

public class Mobile : Platform
{
    public override void Svipe()
    {
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _topPosition = Input.GetTouch(0).position;
            }
            else if( Input.GetTouch(0).phase == TouchPhase.Moved || 
                Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                ResetSwipe(Input.GetTouch(0).position);
            }
        }
    }
}


public abstract class Platform
{
    protected Vector2 _topPosition;
    public abstract void Svipe();

    public static System.Action _Jump;
    public static System.Action<int> _Displacement;
    public static System.Action _Slide;

    protected void ResetSwipe(Vector2 endPos)
    {
        Vector2 vector = endPos - _topPosition;
        if(Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
        {
            if(vector.x > 0)
            {
                _Displacement(1);
            }
            else
            {
                _Displacement(-1);
            }
        }
        else
        {
            if(vector.y > 0)
            {
                _Jump?.Invoke();
            }
            else
            {
                Debug.Log("Down");
                _Slide?.Invoke();
            }
        }
    }
}

