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
            // ��ȡ����ռ�İ�Χ�гߴ�
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
