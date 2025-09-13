using UnityEngine;

public class OpenLinkButton : MonoBehaviour
{
    public string url = "https://unblip.com/tmp/privacy_and_terms/privacy.html";

    public void OpenLink()
    {
        Application.OpenURL(url);
    }
}
