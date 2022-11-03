using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System.Threading.Tasks;

public class Database : MonoBehaviour
{
    private static DatabaseReference _databaseReference;

    private void Start()
    {
        Auth._SetName += SetName;
        Auth._SetRecord += SetRecord;
        Auth._SetCoins += SetCoins;
        Facebookauth._SetName += SetName;
        Facebookauth._SetRecord += SetRecord;
        Facebookauth._SetCoins += SetCoins;
        Points._NewRecord += SetRecord;
        Coins._NewCoins += SetCoins;
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        DontDestroyOnLoad(this);
    }

    private void SetName()
    {
        StartCoroutine(CR_SetNam());
    }
    private void SetCoins(int value)
    {
        StartCoroutine(CR_SetCoins(value));
    }
    private void SetRecord(int value)
    {
        StartCoroutine(CR_SetRecord(value));
    }
    public async static Task<string> ReadName()
    {
        var snapshot = _databaseReference.Child("Users").Child(Auth._user.UserId).Child("Name").GetValueAsync();
        await snapshot;

        if(snapshot.Result.Value == null)
        {
            return null;
        }


        return snapshot.Result.Value.ToString();
    }
    public async static Task<string> ReadPoints()
    {
        var snapshot = _databaseReference.Child("Users").Child(Auth._user.UserId).Child("Record").GetValueAsync();
        await snapshot;

        if (snapshot.Result.Value == null)
        {
            return null;
        }


        return snapshot.Result.Value.ToString();
    }
    public async static Task<string> ReadCoins()
    {
        var snapshot = _databaseReference.Child("Users").Child(Auth._user.UserId).Child("Coin").GetValueAsync();
        await snapshot;

        if (snapshot.Result.Value == null)
        {
            return null;
        }


        return snapshot.Result.Value.ToString();
    }
    private IEnumerator CR_SetNam()
    {
        var loginTask = _databaseReference.Child("Users").Child(Auth._user.UserId).Child("Name").SetValueAsync(Auth._user.DisplayName);
        yield return new WaitUntil(predicate: () => loginTask.IsCanceled);
    }
    private IEnumerator CR_SetCoins(int coins)
    {
        var loginTask = _databaseReference.Child("Users").Child(Auth._user.UserId).Child("Coin").SetValueAsync(coins);
        yield return new WaitUntil(predicate: () => loginTask.IsCanceled);
    }
    private IEnumerator CR_SetRecord(int points)
    {
        var loginTask = _databaseReference.Child("Users").Child(Auth._user.UserId).Child("Record").SetValueAsync(points);
        yield return new WaitUntil(predicate: () => loginTask.IsCanceled);
    }

    private void OnDestroy()
    {
        Auth._SetName -= SetName;
        Auth._SetRecord -= SetRecord;
        Auth._SetCoins -= SetCoins;

        Points._NewRecord -= SetRecord;
        Coins._NewCoins -= SetCoins;

        Facebookauth._SetName -= SetName;
        Facebookauth._SetRecord -= SetRecord;
        Facebookauth._SetCoins -= SetCoins;
    }
}
