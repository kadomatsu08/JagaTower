using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretSight : MonoBehaviour
{

    [SerializeField]
    private Turret turret;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        turret.CurrentTargetTransform= other.gameObject.transform;
    }
    
    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.name);
        turret.CurrentTargetTransform = null;
    }
}
