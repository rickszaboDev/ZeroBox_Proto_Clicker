using UnityEngine;

public  class ParticleSpawner : MonoBehaviour
{
    public static ParticleSpawner instance;

    public GameObject death;

    private void Awake()
    {
        instance = this;
    }
    

}
