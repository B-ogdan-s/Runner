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
        Facebookauth._SetName += SetName;
        Facebookauth._SetRecord += SetRecord;
        Points._NewRecord += SetRecord;
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        DontDestroyOnLoad(this);
    }

    private void SetName()
    {
        StartCoroutine(CR_SetNam());
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

        Debug.Log(snapshot.Result.Value.ToString());

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

        Debug.Log(snapshot.Result.Value.ToString());

        return snapshot.Result.Value.ToString();
    }
    private IEnumerator CR_SetNam()
    {
        var loginTask = _databaseReference.Child("Users").Child(Auth._user.UserId).Child("Name").SetValueAsync(Auth._user.DisplayName);
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
        Points._NewRecord -= SetRecord;

        Facebookauth._SetName -= SetName;
        Facebookauth._SetRecord -= SetRecord;
    }
}
