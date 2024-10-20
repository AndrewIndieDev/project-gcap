using AndrewDowsett.CommonObservers;
using AndrewDowsett.IDisposables;
using AndrewDowsett.SceneLoading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AndrewDowsett.SingleEntryPoint
{
    public class GameInitiator : MonoBehaviour
    {
        [Header("Scene Post Initiation")]
        public string SceneToLoad;

        [Header("Prefabs to Instantiate")]
        public EntryScreen _entryScreen;
        public Camera _camera;
        public UpdateManager _updateManager;
        public FixedUpdateManager _fixedUpdateManager;
        public LateUpdateManager _lateUpdateManager;
        public SceneLoader _sceneLoader;
        public IntroAnimation _introAnimation;
        public GameManager _gameManager;
        public StatBarEnabler _statBarEnabler;
        public TickSystem _tickSystem;
        public TimeManager _timeManager;

        private async void Start()
        {
            BindObjects();

            // we can use an IDisposable here to simplify showing and hiding the loading screen:
            using (var loadingSceneDisposable = new DisposableShowEntryScreen(_entryScreen))
            {
                loadingSceneDisposable.SetLoadingText("Initializing...");
                loadingSceneDisposable.SetLoadingBarPercent(0.01f);
                await InitializeObjects();
                loadingSceneDisposable.SetLoadingText("Creating Objects...");
                loadingSceneDisposable.SetLoadingBarPercent(0.2f);
                await CreateObjects();
                loadingSceneDisposable.SetLoadingText("Preparing Game...");
                loadingSceneDisposable.SetLoadingBarPercent(0.4f);
                await PrepareGame();
                loadingSceneDisposable.SetLoadingText("Loading Scenes...");
                loadingSceneDisposable.SetLoadingBarPercent(0.5f);
                _sceneLoader.LoadScene(SceneToLoad);
                await UniTask.WaitUntil(() => !_sceneLoader.CurrentlyLoadingScene);
                loadingSceneDisposable.SetLoadingText("Done...");
                loadingSceneDisposable.SetLoadingBarPercent(1.0f);
            }
            await BeginGame();
        }

        private void BindObjects()
        {
            // Bind all objects
            _entryScreen = Instantiate(_entryScreen);
            _entryScreen.name = "EntryScreen";

            _camera = Instantiate(_camera);
            _camera.name = "MainCamera";

            _updateManager = Instantiate(_updateManager);
            _updateManager.name = "UpdateManager";

            _fixedUpdateManager = Instantiate(_fixedUpdateManager);
            _fixedUpdateManager.name = "FixedUpdateManager";

            _lateUpdateManager = Instantiate(_lateUpdateManager);
            _lateUpdateManager.name = "LateUpdateManager";

            _sceneLoader = Instantiate(_sceneLoader);
            _sceneLoader.name = "SceneLoader";

            _introAnimation = Instantiate(_introAnimation);
            _introAnimation.name = "IntroAnimation";

            _gameManager = Instantiate(_gameManager);
            _gameManager.name = "GameManager";

            _statBarEnabler = Instantiate(_statBarEnabler);
            _statBarEnabler.name = "StatBarEnabler";

            _tickSystem = Instantiate(_tickSystem);
            _tickSystem.name = "TickSystem";

            _timeManager = Instantiate(_timeManager);
            _timeManager.name = "TimeManager";
        }

        private async UniTask InitializeObjects()
        {
            // Perform initialization for analytics/steam etc.
            await UniTask.Delay(1000);
        }

        private async UniTask CreateObjects()
        {
            // Instantiate all objects into the scene
            await UniTask.Delay(1000);
        }

        private async UniTask PrepareGame()
        {
            // Prepare objects in the scene, if they need methods called before the game starts
            await UniTask.Delay(1000);
        }

        private async UniTask BeginGame()
        {
            // Wait for the intro animation
            await _introAnimation.Play();
            Debug.Log("Game has been loaded...");
            _sceneLoader.UnloadScene("EntryScene");
        }
    }
}