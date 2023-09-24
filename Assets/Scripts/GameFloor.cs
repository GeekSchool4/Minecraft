using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFloor : MonoBehaviour
{
    public GameObject player;
    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x - 128, transform.position.y, player.transform.position.z - 128);
    }

}
