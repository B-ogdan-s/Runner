using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System.Linq;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private LeaderboardUI _leaderboardUI;

    DatabaseReference _database;

    private void Start()
    {
        _database = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void OpenLeaderboard()
    {
        StartCoroutine(CR_Leaderboard());
    }

    private IEnumerator CR_Leaderboard()
    {
        var leder = _database.Child("Users").OrderByChild("Record").GetValueAsync();
        yield return new WaitUntil(predicate: () => leder.IsCompleted);

        if (leder.Exception != null)
        {
            Debug.Log("Error");
        }
        else if (leder.Result.Value == null)
        {
            Debug.Log("Null");
        }
        else
        {
            DataSnapshot dataSnapshot = leder.Result;

            foreach (DataSnapshot s in dataSnapshot.Children.Reverse())
            {
                Debug.Log(s.Child("Name").Value.ToString());
            }

            _leaderboardUI.Open(dataSnapshot);
        }
    }
}
