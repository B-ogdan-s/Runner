using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;

public class Coins : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _coinsTextRunn;

    private int _coins;
    private int _addCoins;

    public static System.Action<int> _NewCoins;

    private async void Start()
    {
        DeathingState._Enter += AddCoins;
        Coin._AddCoin += AddCoin;
        var s = Database.ReadCoins();
        await Task.WhenAll(s);

        int.TryParse(s.Result, out _coins);

        _coinsText.text = _coins.ToString();

    }

    private void AddCoin()
    {
        _addCoins++;
        _coinsTextRunn.text = (_addCoins + _coins).ToString();
    }

    private void AddCoins()
    {
        _coins += _addCoins;
        _NewCoins?.Invoke(_coins);
        _coinsText.text = _coins.ToString();
        _addCoins = 0;
    }

    private void OnDestroy()
    {
        Coin._AddCoin -= AddCoin;
        StartingState._Enter -= AddCoins;
    }

}
