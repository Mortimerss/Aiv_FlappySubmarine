using UnityEngine;

public class DestroyOnBulletTrigger : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject spawnOnDestroy;
    [SerializeField][Range(1, 10)] private int maxInstances = 5;
    [SerializeField] private Vector3 randomDelta = Vector3.zero;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") || other.CompareTag("Player"))
        {
            if (_audioSource && _audioSource.clip)
            {
                // Create a temporary object for the sound
                GameObject tempAudio = new GameObject("TempAudio");
                AudioSource tempSource = tempAudio.AddComponent<AudioSource>();

                // Copy the settings of the original AudioSource
                tempSource.clip = _audioSource.clip;
                tempSource.volume = _audioSource.volume;
                tempSource.pitch = _audioSource.pitch;
                tempSource.Play();

                // Destroys the new object after the duration of the sound
                Destroy(tempAudio, _audioSource.clip.length);
            }

            DestroyWithSpawn(); // Destroys the object immediately
        }
    }

    private void DestroyWithSpawn()
    {
        if (spawnOnDestroy)
        {
            int realInstances = Random.Range(1, maxInstances);

            for (int i = 0; i < realInstances; i++)
            {
                GameObject go = Instantiate(spawnOnDestroy, transform.position +
                    randomDelta * Random.Range(-1f, 1f), spawnOnDestroy.transform.rotation);
                go.transform.localScale = Vector3.one * Random.Range(0.5f, 1.5f);
            }
        }

        Destroy(target);
    }
}
