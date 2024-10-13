using UnityEngine;

public class URLButton : MonoBehaviour
{
    public string url = "";

    public void OpenURL()
    {
        if (url != "")
            Utilities.OpenLink(url);
    }
}
