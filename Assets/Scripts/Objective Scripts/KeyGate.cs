using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGate : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player One" && GameVariables.keyCount == 2)
        {
            GameVariables.keyCount--;

            Destroy(gameObject);
        }
    }
}
