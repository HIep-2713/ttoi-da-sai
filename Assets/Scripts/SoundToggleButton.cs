using UnityEngine;
using UnityEngine.UI;

public class SoundToggleButton : MonoBehaviour
{
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private Button button;
    private Image buttonImage;
    private bool isSoundOn;

    private const string SOUND_KEY = "SoundSetting";

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        isSoundOn = PlayerPrefs.GetInt(SOUND_KEY, 1) == 1;

        ApplySoundSetting();
        UpdateButtonImage();

        button.onClick.AddListener(ToggleSound);
    }

    void ToggleSound()
    {
        isSoundOn = !isSoundOn;

        PlayerPrefs.SetInt(SOUND_KEY, isSoundOn ? 1 : 0);
        PlayerPrefs.Save();

        ApplySoundSetting();
        UpdateButtonImage();
    }

    void ApplySoundSetting()
    {
        // t?t t?t c? SFX (tr? nh?c n?n)
        AudioListener.volume = isSoundOn ? 1f : 0f;
    }

    void UpdateButtonImage()
    {
        buttonImage.sprite = isSoundOn ? soundOnSprite : soundOffSprite;
    }
}
