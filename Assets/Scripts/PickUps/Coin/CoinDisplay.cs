using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinDisplay : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _coinCountText;

    private void Awake()
    {
        _coinCountText.text = "" + 0;
    }

    private void OnEnable()
    {
        _player.CoinChanged += OnCoinChanged;
        OnCoinChanged(_player.CoinCount);
    }

    private void OnDisable()
    {
        _player.CoinChanged -= OnCoinChanged;
    }

    private void OnCoinChanged(int coinCount)
    {
        _coinCountText.text = coinCount.ToString();
    }
}
