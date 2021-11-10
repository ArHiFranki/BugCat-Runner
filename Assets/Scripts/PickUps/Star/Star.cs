using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.GetPowerUp();
        }

        Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
