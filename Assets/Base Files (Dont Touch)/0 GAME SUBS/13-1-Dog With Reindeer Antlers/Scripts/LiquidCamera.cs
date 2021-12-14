using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DogWithReindeerAntlers
{
    public class LiquidCamera : MonoBehaviour
    {
        private RenderTexture colorRenderTexture;

        private Camera cam;

        public SpriteRenderer liquidRenderer;
        [HideInInspector]
        public List<Material> droplets = new List<Material>();

        void Start()
        {
            colorRenderTexture = new RenderTexture(Camera.main.pixelWidth, Camera.main.pixelHeight, 24, RenderTextureFormat.Default);
            colorRenderTexture.Create();

            cam = GetComponent<Camera>();
            cam.CopyFrom(Camera.main);
            cam.clearFlags = CameraClearFlags.SolidColor;

            cam.backgroundColor = Color.clear;

            cam.targetTexture = colorRenderTexture;

            liquidRenderer.material.SetTexture("_Blurs", colorRenderTexture);
        }

        void LateUpdate()
        {
            transform.position = Camera.main.transform.position;

            cam.cullingMask = 1 << LayerMask.NameToLayer("Object 4");
            cam.targetTexture = colorRenderTexture;
            cam.Render();
        }
    }
}