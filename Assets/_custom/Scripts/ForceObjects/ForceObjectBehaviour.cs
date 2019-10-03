﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceObjectBehaviour : MonoBehaviour
{
    Rigidbody rb;
    SphereCollider sc;
    public Transform model;
    public List<Rigidbody> objs = new List<Rigidbody>();
    public List<Rigidbody> fos = new List<Rigidbody>();
    [Space]
    public float minForce;
    public float maxForce;
    [System.NonSerialized] public float force;
    [Space]
    public float minSize;
    public float maxSize;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sc = GetComponent<SphereCollider>();
        rb.AddForce(new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * Random.Range(50.0f, 500.0f));
        force = Random.Range(minForce, maxForce);
        float size = Random.Range(minSize, maxSize);
        model.localScale = new Vector3(size, size, size);
    }

    void FixedUpdate()
    {
        if(rb.velocity.magnitude > 10.0f)
        {
            rb.AddForce(-rb.velocity.normalized * 5000.0f);
        }
        if(objs.Count < 1)
            return;

        foreach (var obj in objs)
        {
            Vector3 dist = transform.position - obj.position;
            float fDist = dist.magnitude;
            if(fDist < 0.2f)
            {
                obj.AddForce(-dist.normalized * (force*((obj.mass*rb.mass)/(fDist*fDist)))*4.0f);
            }
            obj.AddForce(dist.normalized * (force*((obj.mass*rb.mass)/(fDist*fDist))));
        }
        if(fos.Count < 1)
            return;

        foreach (var fo in fos)
        {
            if(fo == null)
            {
                fos.Remove(fo);
                return;
            }
            Vector3 dist = transform.position - fo.position;
            float fDist = dist.magnitude;
            if(fDist < 0.2f)
            {
                Rigidbody _fo = fo;
                sc.radius += _fo.GetComponent<SphereCollider>().radius;
                force += _fo.GetComponent<ForceObjectBehaviour>().force;
                rb.mass += _fo.mass;
                model.localScale += _fo.GetComponentInChildren<Transform>().localScale;
                fos.Remove(fo);
                Destroy(_fo.gameObject);
                return;
            }
            fo.AddForce(dist.normalized * (force*((fo.mass*rb.mass)/(fDist*fDist))));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent(out Rigidbody rb))
            return;

        if(objs.Contains(rb) || fos.Contains(rb))
            return;

        if(other.tag == "ForceObject")
        {
            fos.Add(rb);
            return;
        }
        objs.Add(rb);
    }

    void OnTriggerExit(Collider other)
    {
        if(!other.TryGetComponent(out Rigidbody rb))
            return;

        if(!objs.Contains(rb) && !fos.Contains(rb))
            return;
        
        if(fos.Contains(rb))
        {
            fos.Remove(rb);
            return;
        }
        objs.Remove(rb);
    }
}
