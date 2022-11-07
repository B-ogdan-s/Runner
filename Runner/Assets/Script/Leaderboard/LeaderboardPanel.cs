using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _num;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _record;

    public void SetNum(string num)
    {
        _num.text = num;
    }
    public void SetName(string name)
    {
        _name.text = name;
    }
    public void SetRecord(string record)
    {
        _record.text = record;
    }
}
