using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class StoreUI : MonoBehaviour
{
    [SerializeField] private GameObject _contant;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private GameObject _button;

    private Canvas _storeCanvas;

    public static System.Action _Open;
    public static System.Action _Close;

    public System.Func<int, ContentInfo> _Swipe;

    private float _startPos;

    private void Awake()
    {
        _storeCanvas = GetComponent<Canvas>();
        _storeCanvas.enabled = false;
        _contant.SetActive(false);
    }

    public void PointDown()
    {
        _startPos = Input.GetTouch(0).position.x;
    }
    public void PointUp()
    {
        float value = Input.GetTouch(0).position.x - _startPos;
        if (Mathf.Abs(value) >= 100)
        {
            ContentInfo info;
            if(value > 0)
                info = _Swipe?.Invoke(-1);
            else
                info = _Swipe?.Invoke(1);

            NextContent(info);
        }
    }

    public void NextContent(ContentInfo info)
    {
        _name.text = info._name;


        if (info._buy)
        {
            _price.text = "";
            _button.GetComponentInChildren<TextMeshProUGUI>().text = "Apply";
        }
        else
        {
            _price.text = info._price.ToString();
            _button.GetComponentInChildren<TextMeshProUGUI>().text = "Buy";
        }
    }

    public void OpenStore()
    {
        _contant.SetActive(true);
        _Open?.Invoke();
        _storeCanvas.enabled = true;
    }

    public void CloseStore()
    {
        _contant.SetActive(false);
        _Close?.Invoke();
        _storeCanvas.enabled = false;
    }
}
