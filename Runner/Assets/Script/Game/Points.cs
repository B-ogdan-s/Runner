using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class Points : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _pointsText;
    [SerializeField] TextMeshProUGUI _pointsRecordText;
    [SerializeField] GameObject _player;
    private int _point = 0;

    private int _pointRecord;

    public static System.Action<int> _NewRecord; 

    private async void Start()
    {
        DeathingState._Enter += NewRecord;

        var s = Database.ReadPoints();
        await Task.WhenAll(s);

        int.TryParse(s.Result, out _pointRecord);
        _pointsRecordText.text = _pointRecord.ToString();
    }

    private void Update()
    {
        _point = (int)_player.transform.localPosition.z;
        _pointsText.text = _point.ToString();
    }

    private void NewRecord()
    {
        if(_point > _pointRecord)
        {
            _pointRecord = _point;
            _NewRecord?.Invoke(_pointRecord);
        }
        _pointsRecordText.text = _pointRecord.ToString();
    }

    private void OnDestroy()
    {
        StartingState._Enter += NewRecord;
    }
}
