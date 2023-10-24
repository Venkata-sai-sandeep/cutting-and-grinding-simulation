using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectCuttingController : MonoBehaviour
{
    public GameObject cuttableObject;
    [SerializeField]
    int collision_cnt = 0;
    
    private void Start()
    {
        cuttableObject = transform.gameObject;
        //Cut(transform.position);
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag.Equals("knife"))
    //    {



    //        //if(collision_cnt == 0)
    //        //{
    //        //    //Cut(transform.position);
    //        //    collision_cnt++;
    //        //}

    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("knife"))
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                Vector3 collisionPoint = contact.point;
                Vector3 localCollisionPoint = transform.InverseTransformPoint(collisionPoint);
                Vector3 localScale = transform.localScale;
                Vector3 localCollisionScale = new Vector3(
                    localCollisionPoint.x / localScale.x,
                    localCollisionPoint.y / localScale.y,
                    localCollisionPoint.z / localScale.z
                );
                Collider collider = transform.gameObject.GetComponent<Collider>();
                GameObject temp1 = cuttableObject;
                temp1.transform.position =cuttableObject.transform.position;
                temp1.transform.eulerAngles = cuttableObject.transform.eulerAngles;
                if(localCollisionScale.y > 0)
                {

                }
                else
                {

                }

                //Instantiate()
                Debug.Log("Collision happened at local position: " + localCollisionScale);
            }
            //ContactPoint contact = collision.contacts[0];
            //Vector3 hitDirection = contact.normal;
            //collision_cnt++;
            //if (Vector3.Dot(hitDirection, transform.forward) > 0)
            //{
            //    // Collision occurred on the front side of the object.
            //    Debug.Log("Collision on the front side.");
            //}
            //else
            //{
            //    // Collision occurred on the back side of the object.
            //    Debug.Log("Collision on the back side.");
            //}
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("knife"))
            collision_cnt--;
    }
    public void Cut(Vector3 cutPosition)
    {
       
    }
}
