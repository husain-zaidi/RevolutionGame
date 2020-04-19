using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Propagator : MonoBehaviour
{
    public List<GameObject> oldies;
    public GameObject shard;
    // Start is called before the first frame update
    void Start()
    {
        oldies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Propagate()
    {
        List<GameObject> nearby = oldies.Distinct().ToList();
        foreach(GameObject obj in nearby)
        {
            Vector3 direction = obj.transform.position - transform.position;
            Instantiate(shard, transform.position, Quaternion.FromToRotation(Vector3.up,direction));

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Oldie"))
        {
            oldies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Oldie"))
        {
            oldies.Remove(other.gameObject);
        }
    }
}
