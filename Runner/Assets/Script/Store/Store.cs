using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Threading.Tasks;

public class Store : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _platform;
    [SerializeField] private Transform _player;
    [SerializeField] private ContentInfo[] _contentInfos;
    [SerializeField] private float _shift;

    int _num = 0;

    public static System.Action<string, bool> _Save;

    private GameObject[] _playerModel;

    private void Awake()
    {
        StoreUI._Swipe += NextContent;

        for (int i = 0; i < _contentInfos.Length; i++)
        {
            GameObject platform = Instantiate(_platform);
            platform.transform.SetParent(transform);
            platform.transform.localPosition = new Vector3(i * _shift, 0, 0);
            GameObject obj = Instantiate(_contentInfos[i]._content);
            obj.transform.SetParent(platform.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localEulerAngles = new Vector3(0, -90, 0);
        }

        _playerModel = new GameObject[_contentInfos.Length];

        for(int i = 0; i < _contentInfos.Length; i++)
        {
            _playerModel[i] = Instantiate(_contentInfos[i]._content);
            _playerModel[i].transform.SetParent(_player);
            _playerModel[i].transform.localPosition = Vector3.zero;
            _playerModel[i].transform.localEulerAngles = Vector3.zero;
            _playerModel[i].SetActive(false);
        }
        _playerModel[0].SetActive(true);


    }

    private async void Start()
    {
        for(int i = 0; i < _contentInfos.Length; i++)
        {
            var b = Database.ReadBuy(_contentInfos[i]._name);

            await Task.WhenAll(b);

            _contentInfos[i]._buy = b.Result;
        }
    }

    public ContentInfo NextContent (int value)
    {
        if(_num + value >= 0 && _num + value < _contentInfos.Length)
        {
            _camera.transform.localPosition += new Vector3(value * _shift, 0, 0);
            _num += value;
        }
        return _contentInfos[_num];
    }

    public void SaveStore()
    {
        _contentInfos[_num]._buy = true;
        _Save?.Invoke(_contentInfos[_num]._name, true);
        
    }

    private void OnDestroy()
    {
        StoreUI._Swipe -= NextContent;
    }
}
