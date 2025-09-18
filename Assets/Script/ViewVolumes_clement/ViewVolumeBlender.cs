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
}
