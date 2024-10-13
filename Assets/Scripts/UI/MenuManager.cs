using MoreMountains.Feedbacks;
using System.Threading.Tasks;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public MMF_Player menuFeedbacks;

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public async void Play()
    {
        menuFeedbacks.PlayFeedbacks();

        await Task.Delay(600);

        GameManager.Instance.StartGame();
    }
}
