using UnityEngine;
using System.Collections.Generic;

namespace TBeeD
{
    public class Brush : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private Transform bee;

        public int resolution = 512;
        private Texture2D whiteMap;
        public float brushSize;
        public Texture2D brushTexture;
        private Vector2 stored;
        public static Dictionary<Collider, RenderTexture> paintTextures = new Dictionary<Collider, RenderTexture>();

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            bee = FindObjectOfType<BeeController>().transform;
        }

        void Start()
        {
            CreateClearTexture();// clear white texture to draw on
        }

        void Update()
        {
            spriteRenderer.transform.position = bee.position;
        }

        void OnCollisionStay2D(Collision2D other)
        {
            spriteRenderer.enabled = true;
        }

        void DrawTexture(RenderTexture rt, float posX, float posY)
        {
            RenderTexture.active = rt; // activate rendertexture for drawtexture;
            GL.PushMatrix();                       // save matrixes
            GL.LoadPixelMatrix(0, resolution, resolution, 0);      // setup matrix for correct size

            // draw brushtexture
            Graphics.DrawTexture(new Rect(posX - brushTexture.width / brushSize, (rt.height - posY) - brushTexture.height / brushSize, brushTexture.width / (brushSize * 0.5f), brushTexture.height / (brushSize * 0.5f)), brushTexture);
            GL.PopMatrix();
            RenderTexture.active = null;// turn off rendertexture
        }

        RenderTexture GetWhiteRenderTexture()
        {
            RenderTexture rt = new RenderTexture(resolution, resolution, 32);
            Graphics.Blit(whiteMap, rt);
            return rt;
        }

        void CreateClearTexture()
        {
            whiteMap = new Texture2D(1, 1);
            whiteMap.SetPixel(0, 0, Color.white);
            whiteMap.Apply();
        }
    }
}
