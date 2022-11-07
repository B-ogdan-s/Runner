using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _parents;

    private List<LeaderboardPanel> _leaderboardPanels = new List<LeaderboardPanel>();
    private Canvas _canvas;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        for(int i = 0; i < 8; i++)
        {
            GameObject obj = Instantiate(_prefab);
            obj.transform.SetParent(_parents);
            obj.transform.localScale = Vector3.one;
            _leaderboardPanels.Add(obj.GetComponent<LeaderboardPanel>());
            obj.GetComponent<LeaderboardPanel>().SetNum((i+1).ToString());
        }

        _canvas.enabled = false;
    }

    public void Open(DataSnapshot dataSnapshot)
    {
        List<DataSnapshot> snapshots = dataSnapshot.Children.Reverse().ToList<DataSnapshot>();

        for(int i = 0; i < _leaderboardPanels.Count; i++)
        {
            if(snapshots.Count > i)
            {
                _leaderboardPanels[i].SetName(snapshots[i].Child("Name").Value.ToString());
                _leaderboardPanels[i].SetRecord(snapshots[i].Child("Record").Value.ToString());
            }
            else
            {
                _leaderboardPanels[i].SetName("----------");
                _leaderboardPanels[i].SetRecord("---");
            }
        }

        _canvas.enabled = true;
    }
    public void Close()
    {
        _canvas.enabled = false;
    }

}
