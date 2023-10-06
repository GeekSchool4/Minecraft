using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
public class TestNavMesh : MonoBehaviour
{
    public NavMeshSurface surface;

    void Update()
    {
        surface.BuildNavMesh();
    }
}
