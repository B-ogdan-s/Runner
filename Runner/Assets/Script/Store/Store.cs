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
    [SerializeField] private StoreUI _storeUI;
    [SerializeField] private Coins _coins;

    int _num = 0;

    public static System.Action<string, bool> _Save;
    public static System.Action<Animator> _SetAnimator;
    public static System.Action<int> _SaveContant;

    private GameObject[] _playerModel;

    private void Awake()
    {
        _storeUI._Swipe += NextContent;

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
            _contentInfos[i]._use = false;
        }


    }

    private async void Start()
    {
        var id = Database.ReadApply();

        Debug.Log("Yes        " + id);
        await id;

        for (int i = 0; i < _contentInfos.Length; i++)
        {
            var b = Database.ReadBuy(_contentInfos[i]._name);
            await b;
            _contentInfos[i]._buy = b.Result;

            if (_contentInfos[i]._id == id.Result)
            {
                _playerModel[i].SetActive(true);
                _contentInfos[i]._use = true;
                _storeUI.NextContent(_contentInfos[i]);
                _SetAnimator?.Invoke(_playerModel[i].GetComponent<Animator>());
            }
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
        if (!_contentInfos[_num]._buy)
        {
            if (_coins.Conins < _contentInfos[_num]._price)
                return;

            _coins.NewCoins(_coins.Conins - _contentInfos[_num]._price);
            _contentInfos[_num]._buy = true;
            _Save?.Invoke(_contentInfos[_num]._name, true);
            _storeUI.NextContent(_contentInfos[_num]);
        }
        else
        {
            for (int i = 0; i < _contentInfos.Length; i++)
            {
                _contentInfos[i]._use = false;
                _playerModel[i].SetActive(false);
            }
            _contentInfos[_num]._use = true;
            _playerModel[_num].SetActive(true);
            _SaveContant?.Invoke(_contentInfos[_num]._id);
            _SetAnimator?.Invoke(_playerModel[_num].GetComponent<Animator>());
        }
    }

    private void OnDestroy()
    {
        _storeUI._Swipe -= NextContent;
    }
}
