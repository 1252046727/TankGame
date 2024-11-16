using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //º”‘ÿ‘§…Ë
    public static GameObject LoadPrefab(string path)
    {
        return Resources.Load<GameObject>(path);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
