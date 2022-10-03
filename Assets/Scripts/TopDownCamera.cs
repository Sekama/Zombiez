using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
     
   [Header("References")]
   public Transform direction;
   public Transform player;
   public Transform playerModel;
   public Rigidbody rb;

   public float rotationSpeed;

   public Camera cam;
   
   private Vector3 movement;
   private Vector3 mousePos;
   
   public GameObject topDownCam;
   
    private void Start() {
        Debug.Log("Press R to simulate reloading speed bonus, press E to turn it off");
    }

    private void Update()
    {
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        Plane p = new Plane(Vector3.up, direction.position);
        if (p.Raycast(mouseRay, out float hitDist))
        {
            Vector3 hitPoint = mouseRay.GetPoint(hitDist);
            direction.LookAt(hitPoint);
        }
    }
}
