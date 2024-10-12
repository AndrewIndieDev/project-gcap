using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Camera menuCamera;

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Play()
    {
        GameManager.Instance.StartGame();
        menuCamera.gameObject.SetActive(false);
    }
}
