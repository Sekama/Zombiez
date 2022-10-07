using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndRoomTrigger : MonoBehaviour
{
    [SerializeField] private Collider endRoomTrigger;
    [SerializeField] private GameObject victoryScreen;
    private void OnTriggerEnter(Collider endRoomTrigger)
    {
        Time.timeScale = 0;
        victoryScreen.SetActive(true);
    }
}
        
        

        
    
