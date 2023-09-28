using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRayAttack : MonoBehaviour
{
    [SerializeField] int weaponDamage;
    [SerializeField] float weaponRange;
    [SerializeField] Transform rayOrigin;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask playerLayer;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Hit()
    {
        RaycastHit HitInfo;
        if (gameObject.tag == "PlayerWeapon")
        {
            if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out HitInfo, weaponRange, enemyLayer))
            {
                if (HitInfo.transform.gameObject.GetComponent<Health>())
                    HitInfo.transform.gameObject.GetComponent<Health>().GetDamage(weaponDamage);
            }

        }

        else if (gameObject.tag == "enemy")
        {
            if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out HitInfo, weaponRange, playerLayer))
            {

                if (HitInfo.transform.gameObject.GetComponent<Health>())
                    HitInfo.transform.gameObject.GetComponent<Health>().GetDamage(weaponDamage);
            }
        }

    }
}
