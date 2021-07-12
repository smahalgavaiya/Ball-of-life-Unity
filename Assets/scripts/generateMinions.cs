using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateMinions : MonoBehaviour
{
    public GameObject minionsPrefab;
    public Transform parent;
    public int generateCount = 10;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < generateCount; ++i)
        {
            GameObject min=GameObject.Instantiate(minionsPrefab, parent);
            min.transform.position=new Vector2(parent.transform.position.x+i * 1f, parent.transform.position.y + i * 1f);
        }
    }
}
