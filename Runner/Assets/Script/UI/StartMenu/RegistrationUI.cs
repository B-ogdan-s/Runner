using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RegistrationUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _email;
    [SerializeField] private TMP_InputField _name;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private TMP_InputField _password2;
    [SerializeField] private Button _registrationButton;

    private string _emailText;
    private string _nameText;
    private string _passwordText;
    private string _password2Text;

    public static System.Action<string, string, string> _Registrat;

    private void Start()
    {
        _email.onEndEdit.AddListener((string value) =>
        {
            _emailText = value;
        });
        _name.onEndEdit.AddListener((string value) =>
        {
            if (value.Length < 20)
            {
                _nameText = value;
            }
            else
            {
                _name.text = "";
                _nameText = "";
            }
        });
        _password.onEndEdit.AddListener((string value) =>
        {
            if (value.Length >= 6 && value.Length <= 20)
            {
                _passwordText = value;
            }
            else
            {
                _password.text = "";
                _passwordText = "";
            }
        });
        _password.onEndEdit.AddListener((string value) =>
        {
            if (_passwordText == value)
            {
                _password2Text = value;
            }
            else
            {
                _password2.text = "";
                _password2Text = "";

            }
        });
        _registrationButton.onClick.AddListener(() =>
        {
            _Registrat?.Invoke(_emailText, _password2Text, _nameText);
        });
    }

}
