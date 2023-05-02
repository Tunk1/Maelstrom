using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnrate = 3f;

    [SerializeField] private GameObject[] enemyPrefab;
    private void Start()
    {
        StartCoroutine(Spawner());
        IEnumerator Spawner()
        {
            WaitForSeconds wait = new WaitForSeconds(spawnrate);


            while (true)
            {
                yield return wait;
                int rand = Random.Range(0, enemyPrefab.Length);
                GameObject enemyToSpawn = enemyPrefab[rand];
                Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            }
        }
    }
}
