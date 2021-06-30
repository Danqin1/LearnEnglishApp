using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class SetsUI : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollView;
    [SerializeField] private SetLabel setLabelPrefab;
    // Start is called before the first frame update

    private List<SetLabel> setLabels = new List<SetLabel>();
    private SetsController setsController;
    private UIController uiController;

    private void Awake()
    {
        setsController = CoreContext.SetsController;
        uiController = CoreContext.UIController;
    }

    private void OnEnable()
    {
        ShowSets();
    }

    private void ShowSets()
    {
        var labels = scrollView.content.GetComponentsInChildren<Transform>().ToList();
        labels.Remove(scrollView.content.transform);

        for (int i = labels.Count - 1; i >= 0; i--)
        {
            Destroy(labels[i].gameObject);
        }
			
        setLabels.Clear();
			
        setsController.Sets.Keys.ToList().ForEach(x =>
        {
            var label = Instantiate(setLabelPrefab, scrollView.content.transform, false);
            label.Populate(x);
            label.onLabelRemove += OnLabelRemoved;
            label.onSetEnter += OnSetEnter;
            setLabels.Add(label);
        });
    }

    private void OnSetEnter(SetLabel set)
    {
        uiController.OnEnterSet(set.Name);
    }

    private void OnLabelRemoved(SetLabel label)
    {
        setLabels.Remove(label);
        setsController.RemoveSet(label.Name);
			
        ShowSets();
    }
}
