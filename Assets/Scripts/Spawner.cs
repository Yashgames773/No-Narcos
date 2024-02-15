using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    private Collider SpawnArea;

    public GameObject[] FruitsPrefabs;
    public GameObject BombPrefab;
    [Range(0f, 1f)]
    public float Bombchance = 0.1f;


    public float MinSpawnDelay = 0.25f;
    public float MaxSpawnDelay = 1f;

    public float MinAngle = 15f;
    public float MaxAngle = -15f;

    public float MinForce = 18f;
    public float MaxForce = 22f;

    public float MaxLifetime = 5f;


    private void Awake()
    {
        SpawnArea = GetComponent<Collider>();
    }

    private void OnEnable()
    {
       StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);

        while (enabled)
        {
            GameObject Prefabs = FruitsPrefabs[Random.Range(0, FruitsPrefabs.Length)];

            if (Random.value < Bombchance)
            {
                Prefabs = BombPrefab;
            }

            Vector3 position = new Vector3();

            position.x = Random.Range(SpawnArea.bounds.min.x, SpawnArea.bounds.max.x);
            position.y = Random.Range(SpawnArea.bounds.min.y, SpawnArea.bounds.max.y);
            position.z = Random.Range(SpawnArea.bounds.min.z, SpawnArea.bounds.max.z);

            Quaternion rotation = Quaternion.Euler(0f,0f, Random.Range(MinAngle, MaxAngle));

            GameObject Fruit = Instantiate(Prefabs, position, rotation);
            Destroy(Fruit,MaxLifetime);

            float force = Random.Range(MinForce, MaxForce);
            Fruit.GetComponent<Rigidbody>().AddForce(Fruit.transform.up * force, ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(MinSpawnDelay, MaxSpawnDelay));
        }
    }
}
