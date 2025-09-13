using UnityEngine;
using UnityEngine.UI;

public class MusicToggleButton : MonoBehaviour
{
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    private Button button;
    private Image buttonImage;
    private bool isMusicOn;

    private const string MUSIC_KEY = "MusicSetting";

    private AudioSource musicSource;

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        // tìm AudioSource ch?a nh?c n?n (ví d? g?n vào Camera/MainAudio)
        musicSource = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();

        isMusicOn = PlayerPrefs.GetInt(MUSIC_KEY, 1) == 1;

        ApplyMusicSetting();
        UpdateButtonImage();

        button.onClick.AddListener(ToggleMusic);
    }

    void ToggleMusic()
    {
        isMusicOn = !isMusicOn;

        PlayerPrefs.SetInt(MUSIC_KEY, isMusicOn ? 1 : 0);
        PlayerPrefs.Save();

        ApplyMusicSetting();
        UpdateButtonImage();
    }

    void ApplyMusicSetting()
    {
        if (musicSource != null)
            musicSource.mute = !isMusicOn;
    }

    void UpdateButtonImage()
    {
        buttonImage.sprite = isMusicOn ? musicOnSprite : musicOffSprite;
    }
}
