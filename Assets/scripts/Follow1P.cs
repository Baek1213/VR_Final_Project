using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow1P : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public Vector3 offset = new Vector3(0, 20, -20);
    public float smoothTime = 0.2f;
    Vector3 _vel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!target) target = GameObject.Find("Hero")?.transform;  // 새 Hero로 타깃 갱신
        // 위치만 부드럽게 따라옴
        Vector3 wanted = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, wanted, ref _vel, smoothTime);
    
    }
}
