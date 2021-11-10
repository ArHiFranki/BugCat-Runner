using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private ParticleSystem _dieFX;
    [SerializeField] private SpriteRenderer _enemySprite;
    [SerializeField] private PolygonCollider2D _enemyCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.ApplyDamage(_damage);
            if (!player.IsDead)
            {
                StartCoroutine(DieCoroutine());
            }
        }
        else
        {
            Die();
        }
    }

    IEnumerator DieCoroutine()
    {
        _enemySprite.enabled = false;
        _enemyCollider.enabled = false;
        _dieFX.Play();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        _enemySprite.enabled = true;
        _enemyCollider.enabled = true;
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
