using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;

    //Rango de terreno donde se va a generar los enemigos.
    public float spawnRange = 5;
    public int howManyEnem;

    private int enemyIndex;

    // Start is called before the first frame update
    void Start()
    {
        //enemyIndex = Random.Range(0, enemyPrefab.Length);
        SpawnEnemyWave(howManyEnem);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <sumary>Generate a random position</summary>
    /// <returns>Returns a vector with a random position </returns>
    private Vector3 GenerateRandomPos()
    {
        float posX = Random.Range(-spawnRange, spawnRange);
        float posZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(posX, this.transform.position.y, posZ);

        return spawnPosition;
    }

    /// <sumary>
    /// Spawn an amount of random enemies
    /// <param name= numberEnem>NUmber of enemies to spawn</param>
    /// </summary>
    private void SpawnEnemyWave(int numberEnem)
    {
        for (int i = 0; i < numberEnem; i++)
        {
            int typeEnem = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[typeEnem], GenerateRandomPos(), enemyPrefab[typeEnem].transform.rotation);   
        }
    }

}
