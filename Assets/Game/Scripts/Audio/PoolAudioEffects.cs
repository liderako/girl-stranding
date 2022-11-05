using System;
using UnityEngine;
using UnityEngine.Pool;

public class PoolAudioEffects : MonoBehaviour
{
    public PoolType poolType;
    public bool collectionChecks = true;
    public int maxPoolSize = 0x19;
    private IObjectPool<AudioSource> m_Pool;
    public enum PoolType
    {
        Stack,
        LinkedList
    }

    private AudioSource CreatePooledItem()
    {
        GameObject obj1 = new GameObject("Pooled Audio");
        obj1.transform.parent = base.transform;
        return obj1.AddComponent<AudioSource>();
    }

    private void OnDestroyPoolObject(AudioSource system)
    {
        Destroy(system.gameObject);
    }

    private void OnReturnedToPool(AudioSource system)
    {
        system.clip = null;
        system.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(AudioSource system)
    {
        system.gameObject.SetActive(true);
    }

    public IObjectPool<AudioSource> Pool
    {
        get
        {
            this.m_Pool ??= ((this.poolType != PoolType.Stack) ? ((IObjectPool<AudioSource>) new LinkedPool<AudioSource>(new Func<AudioSource>(this.CreatePooledItem), new Action<AudioSource>(this.OnTakeFromPool), new Action<AudioSource>(this.OnReturnedToPool), new Action<AudioSource>(this.OnDestroyPoolObject), this.collectionChecks, this.maxPoolSize)) : ((IObjectPool<AudioSource>) new ObjectPool<AudioSource>(new Func<AudioSource>(this.CreatePooledItem), new Action<AudioSource>(this.OnTakeFromPool), new Action<AudioSource>(this.OnReturnedToPool), new Action<AudioSource>(this.OnDestroyPoolObject), this.collectionChecks, 10, this.maxPoolSize)));
            return this.m_Pool;
        }
    }
}