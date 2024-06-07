using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class cloudspawn : MonoBehaviour
{
    private float timer = 0;
    public GameObject cloud;
    public float spawnRate;
    public float heightOffset;

    void Start()
    {
        clouds();    
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < spawnRate)
            timer += Time.deltaTime;
        else{
            clouds();
            timer = 0;
        }
    }

    void clouds(){
        
        float lowLimit = transform.position.y - heightOffset;
        float highLimit = transform.position.y + heightOffset;

        // Instantiate(pipe, transform.position, transform.rotation);
        Instantiate(cloud, new Vector3(transform.position.x, Random.Range(lowLimit, highLimit), 0), transform.rotation);

    }
}
