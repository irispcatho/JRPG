using UnityEngine;

[ExecuteInEditMode]
public class SimpleBlit : MonoBehaviour
{
    public Material TransitionMaterial;
    public bool transitionIsActive = false;
    public bool fadeIn = true;
    public float transitionRate = 0.01f;

    public float cutoffVal;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (TransitionMaterial != null)
            Graphics.Blit(src, dst, TransitionMaterial);
    }

    private void FixedUpdate()
    {
        if (transitionIsActive)
        {
            if (fadeIn)
            {
                if (cutoffVal < 1f)
                {
                    cutoffVal += transitionRate;
                }
                else
                {
                    cutoffVal = 1f;
                    transitionIsActive = false;
                }
            }
            else
            {
                if (cutoffVal > 0f)
                {
                    cutoffVal -= transitionRate;
                }
                else
                {
                    cutoffVal = 0f;
                    transitionIsActive = false;
                }
            }
            TransitionMaterial.SetFloat("_Cutoff", cutoffVal);
        }
    }
}