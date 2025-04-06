using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBoard : MonoBehaviour
{
    public Material m1, m2;
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // 获取世界空间的包围盒尺寸
            Vector3 worldSize = renderer.bounds.size;
            Debug.Log("World Size (X): " + worldSize.x + ", (Z): " + worldSize.z);
        }
        else
        {
            Debug.LogError("Renderer component not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
