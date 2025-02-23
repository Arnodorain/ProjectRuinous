using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject[] prefabList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() => Instantiate(prefabList[Random.Range(0, prefabList.Length)], transform.position, transform.rotation);

// Update is called once per frame
void Update()
    {
        
    }
}
