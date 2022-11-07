using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System.Threading.Tasks;
using Unity.VisualScripting;

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
        Store._Save += SetBuy;
        Store._SaveContant += SetApply;
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
    private void SetBuy(string name, bool value)
    {
        StartCoroutine(CR_SetBuy(name, value));
    }
    private void SetApply(int id)
    {
        StartCoroutine(CR_SetApply(id));
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
    public async static Task<bool> ReadBuy(string name)
    {
        var snapshot = _databaseReference.Child("Users").Child(Auth._user.UserId).Child("Buy").Child(name).GetValueAsync();
        await snapshot;

        if (snapshot.Result.Value == null)
        {
            return false;
        }


        return (bool)snapshot.Result.Value;
    }
    public async static Task<int> ReadApply()
    {
        var snapshot = _databaseReference.Child("Users").Child(Auth._user.UserId).Child("Apply").GetValueAsync();
        await snapshot;

        if (snapshot.Result.Value == null)
        {
            return 0;
        }
        Debug.Log(snapshot.Result.Value);

        return snapshot.Result.Value.ConvertTo<int>();
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
    private IEnumerator CR_SetBuy(string name, bool value)
    {
        var loginTask = _databaseReference.Child("Users").Child(Auth._user.UserId).Child("Buy").Child(name).SetValueAsync(value);
        yield return new WaitUntil(predicate: () => loginTask.IsCanceled);
    }
    private IEnumerator CR_SetApply(int id)
    {
        var loginTask = _databaseReference.Child("Users").Child(Auth._user.UserId).Child("Apply").SetValueAsync(id);
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

        Store._Save -= SetBuy;
        Store._SaveContant -= SetApply;
    }
}
