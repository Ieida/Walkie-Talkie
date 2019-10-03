using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapSpawnController : MonoBehaviour
{
    [SerializeField] List<GameObject> scrap = new List<GameObject>();
    [SerializeField] float radius;
    [SerializeField] int amount;
    [SerializeField] float minSize;
    [SerializeField] float maxSize;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawLine(transform.position + new Vector3(-radius, 0, 0), transform.position + new Vector3(radius, 0, 0));
        Gizmos.DrawLine(transform.position + new Vector3(0, -radius, 0), transform.position + new Vector3(0, radius, 0));
        Gizmos.DrawLine(transform.position + new Vector3(0, 0, -radius), transform.position + new Vector3(0, 0, radius));
    }

    void Start()
    {
        if(scrap.Count != 0)
        {
            for (int i = 0; i < amount; i++)
            {
                int sp = Random.Range(0, scrap.Count);
                GameObject scrapPiece = scrap[sp];
                Vector3 position = transform.position + new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), Random.Range(-radius, radius));
                Quaternion rotation = Quaternion.Euler(Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f));
                GameObject scrapObj = Instantiate(scrapPiece, position, rotation, transform);
                scrapObj.transform.localScale = new Vector3(Random.Range(minSize, maxSize), Random.Range(minSize, maxSize), Random.Range(minSize, maxSize));
            }
        }
    }
}
