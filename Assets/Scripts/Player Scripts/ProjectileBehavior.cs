using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
    }

    void Update()
    {
        Destroy(gameObject, 1);
    }
}
