using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _coinValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.ChangeCoinCount(_coinValue);
        }

        Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
