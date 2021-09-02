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
        //Spawn cactus mines that damage the player
        for (int i = 0; i < mineCount; i++) {
            Respawn(Instantiate(mine).transform);          
        }

        //Spawn love balloons that give the player experience points
        for (int i = 0; i < expCount; i++)
        {
            Respawn(Instantiate(exp).transform);
        }

        //Spawn med packs that restore the players health
        for (int i = 0; i < healthCount; i++)
        {            
            Respawn(Instantiate(health).transform);
        }
    }
    void Respawn(Transform spawned)
    {
        spawned.position = character.transform.position + new Vector3(Random.value - 0.5f, 0, Random.value - 0.5f)*spawnRadius;
    }
}
