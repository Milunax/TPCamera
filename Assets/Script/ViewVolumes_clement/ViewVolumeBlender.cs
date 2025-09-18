using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ViewVolumeBlender : MonoBehaviour
{
    public static ViewVolumeBlender Instance;
    private List<AViewVolume> ActiveViewVolumes = new List<AViewVolume>();
    private Dictionary<AView, List<AViewVolume>> VolumesPerViews = new Dictionary<AView, List<AViewVolume>>();

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(this);
    }

    public void Update()
    {
        foreach(AView view in VolumesPerViews.Keys)
        {
            view.weight = 0.0f;
        }

        ActiveViewVolumes.Sort((volumeA, volumeB) =>
        {
            if(volumeA.priority < volumeB.priority) return -1;
            if(volumeA.priority > volumeB.priority) return 1;
            if (volumeA.priority == volumeB.priority)
            {
                if(volumeA.Uid <  volumeB.Uid) return -1;
                if(volumeA.Uid > volumeB.Uid) return 1;
            }
            return 0;
        });

        foreach(AViewVolume volume in ActiveViewVolumes)
        {
            float weight = Mathf.Clamp(volume.ComputeSelfWeight(), 0.0f, 1.0f);
            float remainingWeight = 1.0f - weight;
            foreach(AView view in VolumesPerViews.Keys)
            {
                view.weight *= remainingWeight;
                if (VolumesPerViews[view].Contains(volume))
                {
                    view.weight += weight;
                }
            }

            
        }
    }

    public void AddVolume(AViewVolume volume)
    {
        ActiveViewVolumes.Add(volume);
        if (!VolumesPerViews.ContainsKey(volume.view))
        {
            VolumesPerViews.Add(volume.view, new List<AViewVolume>());
            volume.view.SetActive(true);
        }
        VolumesPerViews[volume.view].Add(volume);
    }

    public void RemoveVolume(AViewVolume volume)
    {
        ActiveViewVolumes.Remove(volume);
        VolumesPerViews[volume.view].Remove(volume);
        if (VolumesPerViews[volume.view].Count <= 0)
        {
            VolumesPerViews.Remove(volume.view);
            volume.view.SetActive(false);
        }
    }

    private void OnGUI()
    {
        foreach(AViewVolume volume in ActiveViewVolumes)
        {
            GUILayout.Label(volume.name);
        }
    }
}
