using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float comerValue;
    public float rise;
    public GameObject goalkeeper;
    public Rigidbody rb;
    public bool direction=false;
    private bool speed=false, final=false;

public float a;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
      a=Random.Range(0,3);
    }

    // Update is called once per frame
    void Update()
    {
         if (direction)
              Movement();
    }

    public void Movement()
    {
      //Vector3 direction = (goalkeeper.transform.position - this.gameObject.transform.position).normalized;
       //  Vector3 firstPos = transform.position;      
     //   rb.AddForce(direction * comerValue,ForceMode.Force);
   
       
      transform.position =  Vector3.Lerp(transform.position,goalkeeper.transform.position, 1* Time.deltaTime); 
    //   transform.position= Vector3.MoveTowards(transform.localPosition,goalkeeper.transform.localPosition,comerValue*Time.deltaTime);
        if(this.gameObject.transform.position.z <goalkeeper.transform.localPosition.z/2){   
          this.gameObject.transform.position += new Vector3 ( 0, rise,  transform.localPosition.z)*Time.deltaTime;
           //  this.gameObject.transform.localPosition += new Vector3 (transform.localPosition.x, rise, 0)*Time.deltaTime;
          }
         else if(this.gameObject.transform.position.z >=goalkeeper.transform.localPosition.z/2) {
             this.gameObject.transform.localPosition += new Vector3 (0, a,  transform.localPosition.z)*Time.deltaTime;
           //  transform.position= Vector3.Lerp( transform.position,goalkeeper.transform.position,Time.deltaTime*rise);
          }

    }
      private void OnTriggerEnter(Collider other)
    { 
       if(other.CompareTag("goalkeeper"))
         {
          final=true;
          direction=false;
          speed=false;
         } 
     }
}
