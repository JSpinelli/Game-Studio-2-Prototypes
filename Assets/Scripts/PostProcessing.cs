using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PostProcessing : MonoBehaviour
{
    public Material[] postProcessingMats;
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        for (int i = 0; i < postProcessingMats.Length -1 ; i++)
        {
            Graphics.Blit(src,src,postProcessingMats[i]);
        }
        Graphics.Blit(src,null,postProcessingMats[postProcessingMats.Length-1]);
    }
}
