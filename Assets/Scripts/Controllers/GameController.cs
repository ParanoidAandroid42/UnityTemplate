using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Controller
{
    public class GameController : MonoBehaviour
    {
        public const float WIDTH = 1080;
        public const float HEIGHT = 1920;

        private int _currentStage;

        public GameObject playerContainer;

        private int levelCount;

        void Start()
        {
            if (PlayerPrefs.GetInt("LevelCount") != 0)
            {
                levelCount = PlayerPrefs.GetInt("LevelCount");
            }
            else
            {
                levelCount = 1;
            }
            //GameAnalyticsSDK.GameAnalytics.Initialize();
            InitProperties();
        }

        void SaveLevel()
        {
            PlayerPrefs.SetInt("LevelSave", 1);
            PlayerPrefs.SetInt("LevelCount", PlayerPrefs.GetInt("LevelCount") + 1);
        }

        void GetLevel()
        {
            if(PlayerPrefs.GetInt("LevelSave") == 1)
            {
                _currentStage = PlayerPrefs.GetInt("LevelCount");
            }
            else
            {
                _currentStage = 0;
            }
        }

        void InitProperties()
        {
            GetLevel();
            InitEvents();
            Managers.EventManager.TriggerEvent(Enum.Action.NextGenereteStage.ToString());
        }

        void InitEvents()
        {
            Managers.EventManager.StartListening(Enum.Action.NextGenereteStage.ToString(), NextGenereteStage);
            Managers.EventManager.StartListening(Enum.Action.Success.ToString(), Success);
            Managers.EventManager.StartListening(Enum.Action.Failed.ToString(), Failed);
            Managers.EventManager.StartListening(Enum.Action.Repeat.ToString(), Repeat);
        }

          void Failed(System.Object arg = null)
        {
            levelCount = PlayerPrefs.GetInt("LevelCount");
           // ElephantSDK.Elephant.LevelFailed(levelCount);
        }

        void Success(System.Object arg = null)
        {
            levelCount = PlayerPrefs.GetInt("LevelCount");
            //GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Complete, levelCount.ToString());
            //ElephantSDK.Elephant.LevelCompleted(levelCount);
            SaveLevel();
        }

        void Repeat(System.Object arg = null)
        {
            CreateInit();
        }

        private void DestroyPreviousObjecs()
        {

        }

        void CreateInit()
        {
            DestroyPreviousObjecs();
            CreateStage();
            UpdateLevel();
        }

        void UpdateLevel()
        {
            if (PlayerPrefs.GetInt("LevelCount") != 0)
            {
                Managers.EventManager.TriggerEvent(Enum.Action.UpdateLevel.ToString(), PlayerPrefs.GetInt("LevelCount") + 1);
            }
            else
            {
                Managers.EventManager.TriggerEvent(Enum.Action.UpdateLevel.ToString(), 1);
            }
        }

        void NextGenereteStage(System.Object arg = null)
        {
            _currentStage++;
            CreateInit();
        }

        void CreateStage()
        {
            //_currentStageConfig = Resources.Load<Stage>("ScriptablesObject/Stages/Stage" + _currentStage);
            //if (_currentStageConfig != null)
            //{
            //    _currentStageObject = Instantiate(_currentStageConfig.stagePrefab, _currentStageConfig.stagePrefab.transform.position, Quaternion.identity);
            //    GameObject player = Instantiate(_currentStageConfig.playerWithBall, new Vector3(0,0,0), Quaternion.identity, playerContainer.transform);
            //    player.transform.localPosition = _currentStageConfig.playerWithBall.transform.position;
            //    Camera.main.gameObject.GetComponent<CameraController>().player = player;
            //}
            //else
            //{
            //    _currentStage = Random.Range(1, 20);
            //    CreateInit();
            //}
            
            //GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start, levelCount.ToString());
            //ElephantSDK.Elephant.LevelStarted(levelCount);
        }
    }
}
