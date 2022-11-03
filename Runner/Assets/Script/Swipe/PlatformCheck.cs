using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCheck : MonoBehaviour
{

    private bool _isMobile;
    Platform _platform;

    private void Awake()
    {
        if(Application.isMobilePlatform)
        {
            Debug.Log("Mobile");
            _platform = new Mobile();
        }
        else
        {
            Debug.Log("PC");
            _platform = new PC();
        }
        StartingState._Enter += Disable;
        StartingState._Exit += Enable;
        DeathingState._Enter += Disable;
    }

    private void Update()
    {
        _platform.Svipe();
    }

    private void Disable()
    {
        GetComponent<PlatformCheck>().enabled = false;
    }
    private void Enable()
    {
        GetComponent<PlatformCheck>().enabled = true;
    }
    private void OnDestroy()
    {
        StartingState._Enter -= Disable;
        StartingState._Exit -= Enable;
        DeathingState._Enter -= Disable;
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
            else if(Input.GetTouch(0).phase == TouchPhase.Ended)
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
        if(Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y) < 150)
        {
            return;
        }
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
                _Slide?.Invoke();
            }
        }
    }
}

