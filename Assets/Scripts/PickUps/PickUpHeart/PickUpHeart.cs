using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHeart : MonoBehaviour
{
    [SerializeField] private int _hpValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.HealthUp(_hpValue);
        }

        Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
