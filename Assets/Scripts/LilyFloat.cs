using UnityEngine;

public class LilyFloat : MonoBehaviour
{
    public float floatStrength = 0.1f; // bi�n ?? n?i
    public float floatSpeed = 2f;      // t?c ?? n?i

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Lily ch? n?i l�n xu?ng, kh�ng tr�i theo d�ng n??c
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatStrength;
        transform.position = startPos + new Vector3(0, newY, 0);
    }
}
