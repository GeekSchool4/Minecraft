using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Quaternion startRotation;

    private void Start()
    {
        startRotation = transform.rotation;
    }

    void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation * startRotation;
    }
}
