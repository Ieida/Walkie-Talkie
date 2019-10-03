using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapPieceBehaviour : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * Random.Range(5.0f, 15.0f));
    }
}
