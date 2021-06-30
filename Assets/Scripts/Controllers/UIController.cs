using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : IDisposable
{
    private GameObject addSetPanel;
    private GameObject mySetsPanel;
    private GameObject mainView;
    private Button addSet;
    private Button mySets;

    public void Initialize(GameObject addSetPanel, GameObject mySetsPanel, GameObject mainView, Button addSet, Button mySets)
    {
        this.addSetPanel = addSetPanel;
        this.mySetsPanel = mySetsPanel;
        this.mainView = mainView;
        this.addSet = addSet;
        this.mySets = mySets;
        
        addSet.onClick.AddListener(ShowAddSetPanel);
        mySets.onClick.AddListener(ShowMySetsPanel);
        
        ShowMainView();
    }

    public void Dispose()
    {
        addSet.onClick.RemoveListener(ShowAddSetPanel);
        mySets.onClick.RemoveListener(ShowMySetsPanel);
    }

    public void OnBack()
    {
        if (addSetPanel || mySetsPanel)
        {
            ShowMainView();
        }

        if (mainView)
        {
            Application.Quit();
        }
    }

    private void ShowMySetsPanel()
    {
        mainView.SetActive(false);
        addSetPanel.SetActive(false);
        mySetsPanel.SetActive(true);
    }

    private void ShowAddSetPanel()
    {
        mainView.SetActive(false);
        addSetPanel.SetActive(true);
        mySetsPanel.SetActive(false);
    }

    private void ShowMainView()
    {
        mainView.SetActive(true);
        addSetPanel.SetActive(false);
        mySetsPanel.SetActive(false);
    }

    public void OnEnterSet(string setName)
    {
        throw new NotImplementedException();
    }
}
