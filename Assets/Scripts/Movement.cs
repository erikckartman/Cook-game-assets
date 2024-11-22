using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed; //Enter the speed for your character in inspector or right here
    private float moveFactor;

    private void Start()
    {
        moveFactor = speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //Get direction from input axis
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
              
        if (x != 0 || z != 0)
        {
            Vector3 moveDirection = new Vector3(x, 0, z).normalized; //Direction

            transform.position += moveDirection * moveFactor;;   //Move object

            transform.rotation = Quaternion.LookRotation(moveDirection);    //Rotate object in the direction
        }
    }
}
