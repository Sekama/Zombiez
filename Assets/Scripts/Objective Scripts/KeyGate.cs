using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGate : MonoBehaviour
{
 
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && KeyCount.keyAmount == 3)
        {
            KeyCount.keyAmount--;

            Destroy(gameObject);
        }
    }
}
