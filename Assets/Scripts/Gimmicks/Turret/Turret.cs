using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform thisTransform;
    public Transform CurrentTargetTransform { get; set; }
    RaycastHit hit;
    LayerMask playerLayerMask;
    
    // Start is called before the first frame update
    void Awake()
    {
        thisTransform = transform;
        playerLayerMask += LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        if (CurrentTargetTransform)
        {
            var position = transform.position;
            var isHit = Physics.Raycast(position
                , CurrentTargetTransform.position - position
                , out hit
                , 10f
                ,playerLayerMask);
            if (isHit)
            {
                transform.LookAt(CurrentTargetTransform);
            }
            
#if UNITY_EDITOR
            Debug.DrawRay(position, CurrentTargetTransform.position - position, Color.red);
#endif
        }
    }
}
