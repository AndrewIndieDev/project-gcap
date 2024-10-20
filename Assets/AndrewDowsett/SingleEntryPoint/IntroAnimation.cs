using Cysharp.Threading.Tasks;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace AndrewDowsett.SingleEntryPoint
{
    public class IntroAnimation : MonoBehaviour
    {
        public MMF_Player introFeedbacks;

        public async UniTask Play()
        {
            await introFeedbacks.PlayFeedbacksTask();
        }
    }
}