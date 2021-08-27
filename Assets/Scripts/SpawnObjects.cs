using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField] GameObject mine, health, exp;
    [SerializeField] int mineCount, healthCount, expCount;
    [SerializeField] CharacterState character;
    [SerializeField] float spawnRadius = 100;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < mineCount; i++) {
            Respawn(Instantiate(mine).transform);          
        }
        for (int i = 0; i < expCount; i++)
        {
            Respawn(Instantiate(exp).transform);
        }
        for (int i = 0; i < healthCount; i++)
        {            
            Respawn(Instantiate(health).transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Respawn(Transform spawned)
    {
        spawned.position = character.transform.position + new Vector3(Random.value - 0.5f, 0, Random.value - 0.5f)*spawnRadius;
    }
}
