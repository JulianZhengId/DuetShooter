using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling instance;

    List<GameObject> spawnedGameObjects0 = new List<GameObject>();
    List<ParticleSystem> spawnedGameObjects1 = new List<ParticleSystem>();
    List<ParticleSystem> spawnedGameObjects2 = new List<ParticleSystem>();

    [SerializeField] private int maxAmount = 30;

    //dust
    [SerializeField] private GameObject gameObject0Prefab;
    
    //hit effect
    [SerializeField] private GameObject gameObject1Prefab;

    //kill effect
    [SerializeField] private GameObject gameObject2Prefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            instance = this;
        }
    }

    private void Start()
    {
        //spawn game object 0
        for (int i = 0; i < maxAmount; i++)
        {
            GameObject p = Instantiate(gameObject0Prefab, this.transform);
            p.SetActive(false);
            spawnedGameObjects0.Add(p);
        }

        //spawn game object 1
        for (int i = 0; i < maxAmount; i++)
        {
            GameObject p = Instantiate(gameObject1Prefab, this.transform);
            p.SetActive(false);
            spawnedGameObjects1.Add(p.GetComponent<ParticleSystem>());
        }

        //spawn game object 2
        for (int i = 0; i < maxAmount; i++)
        {
            GameObject p = Instantiate(gameObject2Prefab, this.transform);
            p.SetActive(false);
            spawnedGameObjects2.Add(p.GetComponent<ParticleSystem>());
        }
    }

    public GameObject GetObject0()
    {
        for (int i = 0; i < maxAmount; i++)
        {
            if (!spawnedGameObjects0[i].activeInHierarchy)
            {
                return spawnedGameObjects0[i];
            }
        }
        return null;
    }
    public ParticleSystem GetObject1()
    {
        for (int i = 0; i < maxAmount; i++)
        {
            if (!spawnedGameObjects1[i].gameObject.activeInHierarchy)
            {
                return spawnedGameObjects1[i];
            }
        }
        return null;
    }

    public ParticleSystem GetObject2()
    {
        for (int i = 0; i < maxAmount; i++)
        {
            if (!spawnedGameObjects2[i].gameObject.activeInHierarchy)
            {
                return spawnedGameObjects2[i];
            }
        }
        return null;
    }
}
