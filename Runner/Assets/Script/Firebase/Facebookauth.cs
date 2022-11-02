using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using Firebase.Auth;
using System.Threading.Tasks;
using UnityEngine.UI;


public class Facebookauth : MonoBehaviour
{

    public static Action _CloseUi;
    public static Action _SetName;
    public static Action<int> _SetRecord;

    //FirebaseAuth auth;
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            Debug.Log("1_No");
            FB.Init(InitCallBack, OnHideUnity);
        }
        else
        {
            Debug.Log("1_Yes");
            FB.ActivateApp();
        }
    }
    private void InitCallBack()
    {
        if (!FB.IsInitialized)
        {
            Debug.Log("not failed");
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Failed to initialize");
        }
    }
    private void OnHideUnity(bool isgameshown)
    {
        if (!isgameshown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Facebook_Login()
    {
        Debug.Log("2_No");
        var permission = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(permission, AuthCallBack);
    }

    private void AuthCallBack(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
          //  debug.text = (aToken.UserId);

            string accesstoken;
            string[] data;
            string acc;
            string[] some;
#if UNITY_EDITOR
            Debug.Log("this is raw access " + result.RawResult);
            data = result.RawResult.Split(',');
            Debug.Log("this is access" + data[3]);
            acc = data[3];
            some = acc.Split('"');
            Debug.Log("this is access " + some[3]);
            accesstoken = some[3];
#elif UNITY_ANDROID
            Debug.Log("this is raw access "+result.RawResult);
            data = result.RawResult.Split(',');
            Debug.Log("this is access"+data[0]);
             acc = data[0];
             some = acc.Split('"');
            Debug.Log("this is access " + some[3]);


             accesstoken = some[3];
#endif
            authwithfirebase(accesstoken);
        }
        else
        {
          Debug.Log("User Cancelled login");
        }
    }
  public void authwithfirebase(string accesstoken)
    {
        Auth._auth = FirebaseAuth.DefaultInstance;
        Firebase.Auth.Credential credential = Firebase.Auth.FacebookAuthProvider.GetCredential(accesstoken);
        Auth._auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
               Debug.Log("singin encountered error" + task.Exception);
            }
            Debug.Log("_______________");
            Auth._user = task.Result;
            UserInfo();
            _CloseUi?.Invoke();
        });
    }

    private async void UserInfo()
    {
        var s = Database.ReadName();
        var r = Database.ReadPoints();
        await Task.WhenAll(s);

        if (s.Result == null)
        {
            _SetName?.Invoke();
        }
        if(r.Result == null)
        {
            _SetRecord?.Invoke(0);
        }
    }
}
