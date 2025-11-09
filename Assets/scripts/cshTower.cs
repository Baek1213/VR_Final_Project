using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cshTower : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Transform SpawnPos;

    float timer = 0f;
    float delay = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        delay = UnityEngine.Random.Range(1, 35);
        timer += Time.deltaTime;
        if (timer >= delay)
        {
            Shoot();
            timer = 0f;
        }
    }
    void Shoot()
    {
            GameObject prefabBullet = Instantiate(bullet, SpawnPos.position, transform.rotation);
            prefabBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 400f);
    }
}
