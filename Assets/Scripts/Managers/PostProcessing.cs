using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class PostProcessing : MonoBehaviour
{
    public Volume globalVolume;
    private DepthOfField dof;
    public void BlurBG()
    {
        if (globalVolume != null && globalVolume.profile.TryGet(out dof))
            dof.active = true;
    }
}