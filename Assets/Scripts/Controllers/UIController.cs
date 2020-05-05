using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    public GameObject roadParent;
    public Slider levelProgress;
    public GameObject winContainer;
    public GameObject levelCompleted;
    public GameObject levelFailed;
    public Text levelText;
    private GameObject _pastCurrentRoad;

    private bool _click = false;
    private bool _successed = false;
    // Start is called before the first frame update
    void Awake()
    {
        levelCompleted.SetActive(false);
        winContainer.SetActive(false);
        levelFailed.SetActive(false);
        winContainer.SetActive(false);
        InitEvents();
    }

    void InitEvents()
    {
        Managers.EventManager.StartListening(Enum.Action.UpdateLoadProgress.ToString(), UpdateLoadProgress);
        Managers.EventManager.StartListening(Enum.Action.UpdateLevel.ToString(), UpdateLevel);
        Managers.EventManager.StartListening(Enum.Action.Success.ToString(), SuccessAnimation);
        Managers.EventManager.StartListening(Enum.Action.Failed.ToString(), Failed);
    }

    void UpdateLoadProgress(System.Object arg = null)
    {
        int count = (int)arg;
        levelProgress.DOValue((float)(count+3) / (float)roadParent.transform.childCount, .25f);
    }

    void UpdateLevel(System.Object arg = null)
    {
        int count = (int)arg;
        levelText.text = "LEVEL " + count.ToString();
        levelProgress.value = 0;
    }

    void Failed(System.Object arg = null)
    {
        levelFailed.SetActive(true);
        _successed = false;
        _click = true;
    }

    void SuccessAnimation(System.Object arg = null)
    {
        levelCompleted.SetActive(true);
        winContainer.SetActive(true);
        _successed = true;
        _click = true;
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && _click == true)
        {
            _click = false;
            levelCompleted.SetActive(false);
            winContainer.SetActive(false);
            levelFailed.SetActive(false);
            winContainer.SetActive(false);
            if (_successed)
            {
                Managers.EventManager.TriggerEvent(Enum.Action.NextGenereteStage.ToString());
            }
            else
            {
                Managers.EventManager.TriggerEvent(Enum.Action.Repeat.ToString());
            }
        }
    }
}
