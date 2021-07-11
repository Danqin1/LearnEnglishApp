using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class UIController : IDisposable
{
    private GameObject addSetPanel;
    private GameObject mySetsPanel;
    private GameObject mainPanel;
    private LearnUI learPanel;
    private Button addSet;
    private Button mySets;

    private LearnController learnController;

    public void Initialize(GameObject addSetPanel, GameObject mySetsPanel, GameObject mainPanel, Button addSet, Button mySets, LearnUI learPanel, LearnController learnController)
    {
        this.addSetPanel = addSetPanel;
        this.mySetsPanel = mySetsPanel;
        this.mainPanel = mainPanel;
        this.addSet = addSet;
        this.mySets = mySets;
        this.learPanel = learPanel;
        
        addSet.onClick.AddListener(ShowAddSetPanel);
        mySets.onClick.AddListener(ShowMySetsPanel);

        this.learnController = learnController;
        learnController.onSetDone += OnBack;
        
        ShowMainView();
    }

    public void Dispose()
    {
        addSet.onClick.RemoveListener(ShowAddSetPanel);
        mySets.onClick.RemoveListener(ShowMySetsPanel);
        
        learnController.onSetDone -= OnBack;
    }

    public void OnBack()
    {
        if (addSetPanel.activeSelf || mySetsPanel.activeSelf)
        {
            ShowMainView();
        }
        else if (learPanel.gameObject.activeSelf)
        {
            ShowMySetsPanel();
        }
        else
        {
            // if (Application.platform == RuntimePlatform.Android)
            // {
            //     AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            //     activity.Call<bool>("moveTaskToBack", true);
            // }
            // else
            // {
            //     Application.Quit();
            //}
            Application.Quit();
        }
    }

    private void ShowMySetsPanel()
    {
        mainPanel.SetActive(false);
        addSetPanel.SetActive(false);
        mySetsPanel.SetActive(true);
        learPanel.gameObject.SetActive(false);
    }

    private void ShowAddSetPanel()
    {
        mainPanel.SetActive(false);
        addSetPanel.SetActive(true);
        mySetsPanel.SetActive(false);
        learPanel.gameObject.SetActive(false);
    }

    private void ShowMainView()
    {
        mainPanel.SetActive(true);
        addSetPanel.SetActive(false);
        mySetsPanel.SetActive(false);
        learPanel.gameObject.SetActive(false);
    }

    public void OnStartLearn()
    {
        mainPanel.SetActive(false);
        addSetPanel.SetActive(false);
        mySetsPanel.SetActive(false);
        learPanel.gameObject.SetActive(true);
    }
}
