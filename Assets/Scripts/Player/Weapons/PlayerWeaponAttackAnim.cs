using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponAttackAnim : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("IsAttacking");
        }
    }
}
