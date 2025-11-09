using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshRedzone : MonoBehaviour
{
    public GameObject Redzone;
    
    private float fireInterval =3f;
    private float nextrezen = 0f;
    void Update()
    {
        if (Time.time > nextrezen)
        {
            nextrezen = Time.time + fireInterval;
            Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
            Instantiate(Redzone, position, Quaternion.identity);
        }
    }
}

