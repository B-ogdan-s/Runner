using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPS : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _FPSText;

    private float _deltsTime = 0;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        _deltsTime += (Time.deltaTime - _deltsTime) * 0.1f;
        float fps = 1 / _deltsTime;
        _FPSText.text = fps.ToString();
    }
}
