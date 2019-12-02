using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_EnterSector : MonoBehaviour
{

    public Color lightColor;
    public Vector3 lightRotation;
    public float lightIntensity;
    public float lightIndirectMultiplier;

    private Color ogLightColor;
    private Quaternion ogLightRotation;
    private float ogLightIntensity;
    private float ogLightIndirectMultiplier;

    public Light masterLight;

    private Material curSkybox;

    public Texture newSkyFront;
    public Texture newSkyBack;
    public Texture newSkyLeft;
    public Texture newSkyRight;
    public Texture newSkyUp;
    public Texture newSkyDown;


    private bool blendingSkybox;
    private float blendSpeed;
    private bool doneBlend;

    private bool lerpLights;
    private float changingLightsFloat;

    public bool test;
    private float blendFloat;

    // Start is called before the first frame update
    void Start()
    {
        blendingSkybox = false;
        blendSpeed = 0.005f;
        blendFloat = 0.0f;
        curSkybox = RenderSettings.skybox;
        lerpLights = false;
        changingLightsFloat = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (blendingSkybox)
        {
            blendFloat += blendSpeed;
            if (blendFloat > 1.0f)
            {
                blendFloat = 1.0f;
                blendingSkybox = false;
                doneBlend = true;
            }

            curSkybox.SetFloat("_Blend", blendFloat);

        }


        if (doneBlend)
        {
            curSkybox.SetTexture("_FrontTex", newSkyFront);
            curSkybox.SetTexture("_BackTex", newSkyBack);
            curSkybox.SetTexture("_LeftTex", newSkyLeft);
            curSkybox.SetTexture("_RightTex", newSkyRight);
            curSkybox.SetTexture("_UpTex", newSkyUp);
            curSkybox.SetTexture("_DownTex", newSkyDown);
            curSkybox.SetFloat("_Blend", 0.0f);
            blendFloat = 0.0f;
            doneBlend = false;
        }

        if (lerpLights)
        {
            changingLightsFloat += blendSpeed;
            masterLight.color = Color.Lerp(ogLightColor, lightColor, changingLightsFloat);
            masterLight.intensity = Mathf.Lerp(ogLightIntensity, lightIntensity, changingLightsFloat);
            masterLight.bounceIntensity = Mathf.Lerp(ogLightIndirectMultiplier, lightIndirectMultiplier, changingLightsFloat);
            masterLight.transform.rotation = Quaternion.Lerp(ogLightRotation, Quaternion.Euler(lightRotation), changingLightsFloat);

            if (changingLightsFloat > 1.0f)
            {
                changingLightsFloat = 0.0f;
                lerpLights = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBody") {

            lerpLights = true;

            curSkybox.SetFloat("_Blend", 0.0f);

            blendingSkybox = true;
            curSkybox.SetTexture("_FrontTex2", newSkyFront);
            curSkybox.SetTexture("_BackTex2", newSkyBack);
            curSkybox.SetTexture("_LeftTex2", newSkyLeft);
            curSkybox.SetTexture("_RightTex2", newSkyRight);
            curSkybox.SetTexture("_UpTex2", newSkyUp);
            curSkybox.SetTexture("_DownTex2", newSkyDown);

            ogLightColor = masterLight.color;
            ogLightRotation = masterLight.transform.rotation;
            ogLightIntensity = masterLight.intensity;
            ogLightIndirectMultiplier = masterLight.bounceIntensity;
        }

    }

    private void OnTriggerExit(Collider other)
    {

    }
}
