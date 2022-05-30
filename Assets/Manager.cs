using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    Vector3[] spawnPoints = new[] {new Vector3(-211.8504180908203f, 39.48400115966797f, 161.0645751953125f), new Vector3(-264.848541f, 40.49300003051758f, 276.570465f), new Vector3(193.70134f, 21.707000732421876f, 668.317566f), new Vector3(-396.31015f, 44.095001220703128f, 134.0728f), new Vector3(-302.368744f, 34.19300079345703f, 768.894775f), new Vector3(-29.9128876f, 31.34000015258789f, 645.249939f) };
    [SerializeField] GameObject[] items;
    public AudioSource source;
    public AudioClip clip;
    void Start()
    {

        List<int> randomList = new List<int>();
        int random = Random.Range(0, 6);
        for (int i = 0; i < spawnPoints.Length - 3; i++)
        {
            while (true)
            {
                if (randomList.Contains(random))
                {
                    random = Random.Range(0, 6);
                }
                else
                {
                    randomList.Add(random);
                    break;
                }
            }
            Instantiate(items[Random.Range(0, 2)], spawnPoints[random], Quaternion.identity, GameObject.Find("3D models").transform);
        }
        while (true)
        {
            if (randomList.Contains(random))
            {
                random = Random.Range(0, 6);
            }
            else
            {
                randomList.Add(random);
                break;
            }
        }
        Instantiate(items[2], spawnPoints[random], Quaternion.identity, GameObject.Find("3D models").transform);
    }

    void Update()
    {
        
    }
}
