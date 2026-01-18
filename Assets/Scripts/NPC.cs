using UnityEngine;

public class NPC : MonoBehaviour
{
    protected AudioSource audioSource;

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void OnCollision(Player player)
    {
        
    }

    public virtual void OnHurt()
    {

    }
}
