using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class EnemySpawner : MonoBehaviour
{
    private GameObject Skeleton;
    private GameObject Golem;
    private GameObject Reptile;
    public GameObject Player;

    private GameObject skeletonInstantiated;
    private GameObject reptileInstantiated;

    private bool isSpawned;

    
    private void Start()
    {
        Skeleton = GameObject.Find("DungeonSkeleton_demo");
        //Golem = GameObject.Find("Golem");
        Reptile = GameObject.Find("Reptile");
        Player = GameObject.Find("First Person Controller Minimal");






    }

    private void Update()
    {
        if (!isSpawned)
        {
            if (Vector3.Distance(transform.position, Player.transform.position)<= 50)
            {
                bool isSkeletonSpawn = Random.value > 0.7;
                //bool isGolemSpawn = Random.value > 0.9;
                bool isReptileSpawn = Random.value > 0.9;
                if (isSkeletonSpawn)
                {
                    skeletonInstantiated = Instantiate(Skeleton, new Vector3(transform.position.x, transform.position.y + 50, transform.position.z), Quaternion.identity);
                }
                if (isReptileSpawn)
                {
                    reptileInstantiated = Instantiate(Reptile, new Vector3(transform.position.x + 10, transform.position.y + 50, transform.position.z + 10), Quaternion.identity);
                }
                isSpawned = true;
            }

        }

        if (Vector3.Distance(transform.position, Player.transform.position) >= 50)
        {
            if (Skeleton != null)
            {
                Destroy(skeletonInstantiated);
            }
            if (Reptile != null)
            {
                Destroy(reptileInstantiated);
            }
        }

    }
}
