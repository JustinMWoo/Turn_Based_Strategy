using Stats.CharacterStats;
using UnityEngine;


// updates stat values to the UI
public class StatPanel : MonoBehaviour
{
    [SerializeField] StatDisplay[] statDisplays;
    [SerializeField] string[] statNames;

    private CharacterStats[] stats;

    private void OnValidate()
    {
        statDisplays = GetComponentsInChildren<StatDisplay>();
        UpdateStatNames();
    }

    public void SetStats(params CharacterStats[] charStats)
    {
        stats = charStats;

        // throw error if too many stats are trying to be set
        if(stats.Length > statDisplays.Length)
        {
            Debug.LogError("Not Enough Stat Displays");
            return;
        }

        // if we have more stat displays than stats, then disable to extra stat displays
        for (int i = 0; i < statDisplays.Length; i++)
        {
            statDisplays[i].gameObject.SetActive(i < stats.Length);

        }
    }

    //updates the stat values in UI. iterates through stats and updates the values to the current value. Will be called in character class when we need to update stats
    public void UpdateStatValues()
    {
        for(int i=0; i < stats.Length; i++)
        {
            statDisplays[i].ValueText.text = stats[i].Value.ToString();
        }
    }

    // same as above but for stat names
    public void UpdateStatNames()
    {
        for (int i = 0; i < statNames.Length; i++)
        {
            statDisplays[i].NameText.text = statNames[i];
        }
    }

}
