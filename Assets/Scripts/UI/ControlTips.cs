using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTips : MonoBehaviour
{
    [SerializeField] private SpeedManager _speedManager;

    private void Update()
    {
        transform.Translate(Vector2.left * _speedManager.CurrentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Player player))
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
