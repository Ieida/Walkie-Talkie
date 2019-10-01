using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;

[System.Serializable]
public class WalkieTalkieReferences
{
    [System.NonSerialized] public Rigidbody rb;
    public Transform player;
    public float pickUpDistance;
}

public class WalkieTalkieBehaviour : MonoBehaviour
{
    public WalkieTalkieReferences wr;
    ParentConstraint pc;

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, wr.pickUpDistance);
    }

    void Start()
    {
        wr.rb = GetComponent<Rigidbody>();
        pc = GetComponent<ParentConstraint>();
    }

    void Update()
    {
        float distanceSq = (wr.player.position - transform.position).sqrMagnitude;
        if(Input.GetMouseButtonUp(1))
        {
            pc.translationAtRest = transform.position;
            pc.rotationAtRest = transform.eulerAngles;
            pc.weight = 0.0f;
            pc.constraintActive = false;
        }

        if(distanceSq > wr.pickUpDistance*wr.pickUpDistance)
            return;

        if(Input.GetMouseButtonDown(1))
        {
            wr.rb.velocity = Vector3.zero;
            pc.translationAtRest = transform.position;
            pc.rotationAtRest = transform.eulerAngles;
            pc.constraintActive = true;
        }
        else if(Input.GetMouseButton(1))
        {
            pc.weight = Mathf.Lerp(pc.weight, 1.0f, Time.deltaTime * 5.0f);
        }
    }
}
