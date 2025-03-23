using UnityEngine;

public class DestroyItem : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 2f); // Distrugge l'oggetto dopo 2 secondi
    }
}
