using UnityEngine;

public class Bomb : MonoBehaviour 
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider>().enabled = false;
            
            // Play explosion sound
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayBombExplosion();
            }
            
            GameManager.Instance.Explode();
        }
    }
}