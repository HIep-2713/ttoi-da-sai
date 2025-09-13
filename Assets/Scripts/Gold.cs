using UnityEngine;

public class Gold : MonoBehaviour
{
    public int value = 1;
    public AudioClip pickupSfx;
    public float sfxVolume = 0.8f;
    public float lifeTime = 0.5f;

    void Start()
    {
        // c?ng vàng vào GS
        if (GS.Instance != null)
        {
            GS.Instance.AddCoin(value);
        }

        // âm thanh
        if (pickupSfx != null)
            AudioSource.PlayClipAtPoint(pickupSfx, transform.position, sfxVolume);

        // xóa prefab vàng
        Destroy(gameObject, lifeTime);
    }
}
