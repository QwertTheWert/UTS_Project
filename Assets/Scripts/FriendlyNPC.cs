using System.Collections;
using UnityEngine;

public class FriendlyNPC : NPC
{

    public Animator emoteAnim;
    public string animalNoise;

    [Header("Audio")]
    public AudioClip positiveAudio;
    public AudioClip negativeAudio;

    public override void OnCollision(Player player)
    {
        base.OnCollision(player);
        if (animalNoise != null)
        {
            Debug.Log(animalNoise);
        }
        StopAllCoroutines();
        StartCoroutine(PlayAnimation(true));
    }

    public override void OnHurt(Player player)
    {
        base.OnHurt(player);
        StopAllCoroutines();
        StartCoroutine(PlayAnimation(false));
    }

    IEnumerator PlayAnimation(bool isPositive)
    {
        emoteAnim.SetTrigger(isPositive ? "toPositive" : "toNegative");
        
        audioSource.Stop();
        audioSource.resource = isPositive ? positiveAudio : negativeAudio;
        audioSource.pitch = Random.Range(.8f, 1.2f);
        audioSource.Play();

        yield return new WaitForSeconds(2f);
        emoteAnim.SetTrigger("toDefault");
    }
}