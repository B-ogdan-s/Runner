using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;


public class Auth : MonoBehaviour
{
    public static FirebaseAuth _auth;
    public static FirebaseUser _user;

    public static Action _OpenRegistory;
    public static Action _CloseRegistory;
    public static Action _CloseLogin;

    public static Action _SetName;
    public static Action<int> _SetRecord;
    public static Action<int> _SetCoins;



    private void Start()
    {
        LoginUI._Login += Login;
        RegistrationUI._Registrat += Register;
        DontDestroyOnLoad(this);
        InitializeFirebase();
    }

    public void Login(string email, string password)
    {
        StartCoroutine(CR_Login(email, password));
    }
    public void Register(string email, string password, string name)
    {
        StartCoroutine(CR_Register(email, password, name));
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        _auth = FirebaseAuth.DefaultInstance;
        _auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    private void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (_auth.CurrentUser != _user)
        {
            bool signedIn = _user != _auth.CurrentUser && _auth.CurrentUser != null;
            if (!signedIn && _user != null)
            {
                Debug.Log("Signed out " + _user.UserId);
                _OpenRegistory?.Invoke();
            }
            _user = _auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + _user.UserId);
            }
        }
        else if(_user == null)
        {
            _OpenRegistory?.Invoke();
        }
    }
    public void OnSignOut()
    {
        _auth.SignOut();
    }

    private IEnumerator CR_Login(string email, string password)
    {
        var loginTask = _auth.SignInWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(predicate: () => loginTask.IsCompleted);

        if(loginTask.Exception != null)
        {
            Debug.Log("Error");
        }
        else
        {
            _user = loginTask.Result;
            _CloseLogin?.Invoke();
        }
    }
    private IEnumerator CR_Register(string email, string password, string name)
    {
        var registerTask = _auth.CreateUserWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(predicate: () => registerTask.IsCompleted);

        if (registerTask.Exception != null)
        {
            Debug.Log("Error");
        }
        else
        {
            _user = registerTask.Result;
            if(_user != null)
            {
                UserProfile profile = new UserProfile { DisplayName = name };
                var profileTask = _user.UpdateUserProfileAsync(profile);
                yield return new WaitUntil(predicate: () => profileTask.IsCompleted);
                if(profileTask.Exception != null)
                {
                    Debug.Log("Error");
                }
            }
            _SetName?.Invoke();
            _SetRecord?.Invoke(0);
            _SetCoins?.Invoke(0);
            _CloseRegistory?.Invoke();
        }
    }

    private void OnDestroy()
    {
        RegistrationUI._Registrat -= Register;
        LoginUI._Login -= Login;
        _auth.StateChanged -= AuthStateChanged;
        _auth = null;
    }
}
