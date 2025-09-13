using UnityEngine;

public class Gold : MonoBehaviour
{
    public int value = 1;
    public AudioClip pickupSfx;
    public float sfxVolume = 0.8f;
    public float lifeTime = 0.5f;

    void Start()
    {
        // c?ng v�ng v�o GS
        if (GS.Instance != null)
        {
            GS.Instance.AddCoin(value);
        }

        // �m thanh
        if (pickupSfx != null)
            AudioSource.PlayClipAtPoint(pickupSfx, transform.position, sfxVolume);

        // x�a prefab v�ng
        Destroy(gameObject, lifeTime);
    }
}
