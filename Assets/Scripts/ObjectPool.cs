using System.Collections;
using System.Collections.Generic;
using System.Linq;                      // Необходимо для работы с пулом
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _enemyOneContainer;
    [SerializeField] private GameObject _enemyTwoContainer;
    [SerializeField] private GameObject _starContainer;
    [SerializeField] private GameObject _pickUpHeartContainer;
    [SerializeField] private GameObject _coinContainer;

    [SerializeField] private GameObject _enemyOnePrefab;
    [SerializeField] private GameObject _enemyTwoPrefab;
    [SerializeField] private GameObject _starPrefab;
    [SerializeField] private GameObject _pickUpHeartPrefab;
    [SerializeField] private GameObject _coinPrefab;

    [SerializeField] private int _enemyOnePoolCapacity;
    [SerializeField] private int _enemyTwoPoolCapacity;
    [SerializeField] private int _starPoolCapacity;
    [SerializeField] private int _pickUpHeartPoolCapacity;
    [SerializeField] private int _coinPoolCapacity;

    [SerializeField] protected SpeedManager speedManager;

    protected List<GameObject> enemyOnePool = new List<GameObject>();
    protected List<GameObject> enemyTwoPool = new List<GameObject>();
    protected List<GameObject> starPool = new List<GameObject>();
    protected List<GameObject> pickUpHeartPool = new List<GameObject>();
    protected List<GameObject> coinPool = new List<GameObject>();

    private void Initialize(GameObject prefab, List<GameObject> pool, GameObject container, int capacity)
    {
        for (int i = 0; i < capacity; i++)
        {
            GameObject spawned = Instantiate(prefab, container.transform);
            spawned.SetActive(false);
            spawned.GetComponent<EnemyMover>().InitSpeedManager(speedManager);
            //EnemyMover enemyMover = spawned.GetComponent<EnemyMover>();
            //enemyMover.InitSpeedManager(speedManager);

            pool.Add(spawned);
        }
    }

    protected void InitPools()
    {
        Initialize(_enemyOnePrefab, enemyOnePool, _enemyOneContainer, _enemyOnePoolCapacity);
        Initialize(_enemyTwoPrefab, enemyTwoPool, _enemyTwoContainer, _enemyTwoPoolCapacity);
        Initialize(_starPrefab, starPool, _starContainer, _starPoolCapacity);
        Initialize(_pickUpHeartPrefab, pickUpHeartPool, _pickUpHeartContainer, _pickUpHeartPoolCapacity);
        Initialize(_coinPrefab, coinPool, _coinContainer, _coinPoolCapacity);
    }

    protected bool TryGetObject(List<GameObject> pool, out GameObject result)
    {
        result = pool.FirstOrDefault(p => p.activeSelf == false);

        return result != null;
    }
}
