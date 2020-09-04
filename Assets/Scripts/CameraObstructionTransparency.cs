using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObstructionTransparency : MonoBehaviour
{
    private Shader oldShader = null;
    private Color oldColor = Color.black;
    private float transparency = 0.3f;
    private const float targetTransparency = 0.3f;
    private const float fallOff = 0.1f;
    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        oldShader = rend.material.shader;
        oldColor = rend.material.color;
        rend.material.shader = Shader.Find("Transparent/VertexLit");
    }

    public void MakeTransparent()
    {
        transparency = targetTransparency;
    }

    void Update()
    {
        if (transparency < 1.0f)
        {
            Color C = rend.material.color;
            C.a = transparency;
            rend.material.color = C;
        }
        else
        {
            // Reset shader
            rend.material.shader = oldShader;
            rend.material.color = oldColor;

            Destroy(this);
        }
        transparency += ((1.0f - targetTransparency) * Time.deltaTime) / fallOff;
    }
}
