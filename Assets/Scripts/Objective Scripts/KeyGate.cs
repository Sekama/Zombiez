using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGate : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && GameVariables.keyCount == 2)
        {
            GameVariables.keyCount--;

            Destroy(gameObject);
        }
    }
}
