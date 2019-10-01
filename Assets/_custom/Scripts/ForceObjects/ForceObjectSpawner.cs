using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceObjectSpawner : MonoBehaviour
{
    public GameObject forceObject;
    public float radius;
    public int amount;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position + new Vector3(-radius, 0, 0), transform.position + new Vector3(radius, 0, 0));
        Gizmos.DrawLine(transform.position + new Vector3(0, -radius, 0), transform.position + new Vector3(0, radius, 0));
        Gizmos.DrawLine(transform.position + new Vector3(0, 0, -radius), transform.position + new Vector3(0, 0, radius));
    }

    void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            //get random position within a radius
            Vector3 point = transform.position + new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), Random.Range(-radius, radius));
            Quaternion rotation = Quaternion.Euler(Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f));
            //instantiate Force Object
            GameObject fo = Instantiate(forceObject, point, rotation);
            fo.transform.parent = transform;
        }
    }
}
