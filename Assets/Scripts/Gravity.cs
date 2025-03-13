using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.0066743f;
    public static List<Gravity> gravityObjectList;
    [SerializeField] bool planet = false;
    [SerializeField] int orbitSpeed = 1000;


    void Awake(){
        rb = GetComponent<Rigidbody>();
        if(gravityObjectList == null){
            gravityObjectList = new List<Gravity>();
        } 
        gravityObjectList.Add(this);
        
        if(!planet){
            rb.AddForce(Vector3.left * orbitSpeed);
        }
    }
    void FixedUpdate()
    {
        foreach(var obj in gravityObjectList){
            if(obj != this)
            Attract(obj);
        }
    }
    void Attract(Gravity other){
        Rigidbody OtherRb = other.rb;
        Vector3 direction = rb.position - OtherRb.position;
        float distance = direction.magnitude;
        float foceMagnitude = G * (rb.mass * OtherRb.mass / Mathf.Pow(distance, 2));
        Vector3 gavityFore = foceMagnitude * direction.normalized;

        OtherRb.AddForce(gavityFore);
    }
}
