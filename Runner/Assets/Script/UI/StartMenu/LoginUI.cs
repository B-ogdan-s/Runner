using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _email;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private Button _loginButton;

    private string _emailText;
    private string _passwordText;


    public static System.Action<string, string> _Login;

    private void Start()
    {
        _email.onEndEdit.AddListener((string value) =>
        {
            _emailText = value;
        });
        _password.onEndEdit.AddListener((string value) =>
        {
            _passwordText = value;
        });
        _loginButton.onClick.AddListener(() =>
        {
            _Login?.Invoke(_emailText, _passwordText);
        });
    }
}
