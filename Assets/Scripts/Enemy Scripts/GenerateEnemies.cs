using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{


    public GameObject theEnemy;
    public float xPos;
    public float zPos;
    public int enemyCount;
    public int maxEnemies;
   
    void Start()
    {
        StartCoroutine(EnemyDrop());
    }
    IEnumerator EnemyDrop()
    {
        while (enemyCount < maxEnemies)
        {
            xPos = gameObject.transform.position.x + Random.Range(1, 12);
            zPos = gameObject.transform.position.z + Random.Range(16, 1);
            Instantiate(theEnemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;
        }
    }

    
}
