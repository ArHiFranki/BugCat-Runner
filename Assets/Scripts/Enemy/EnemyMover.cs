using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private SpeedManager _speedManager;

    private void Update()
    {
        transform.Translate(Vector3.left * _speedManager.CurrentSpeed * Time.deltaTime);
    }

    public void InitSpeedManager(SpeedManager speedManager)
    {
        _speedManager = speedManager;
    }
}
