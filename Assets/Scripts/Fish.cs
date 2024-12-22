using UnityEngine;

public class Fish : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody fishRigidbody;
    private Collider fishCollider;
    private ParticleSystem splashEffect;

    public int points = 1;

    private void Awake()
    {
        fishRigidbody = GetComponent<Rigidbody>();
        fishCollider = GetComponent<Collider>();
        splashEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        // Play slice sound effect
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayFishSlice();
        }

        GameManager.Instance.IncreaseScore(points);

        // Disable the whole fish
        fishCollider.enabled = false;
        whole.SetActive(false);

        // Enable the sliced fish
        sliced.SetActive(true);
        splashEffect.Play();

        // Rotate based on the slice angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        // Add a force to each slice based on the blade direction
        foreach (Rigidbody slice in slices)
        {
            slice.velocity = fishRigidbody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            if (blade != null && blade.slicing)
            {
                Slice(blade.direction, blade.transform.position, blade.sliceForce);
            }
        }
    }
}