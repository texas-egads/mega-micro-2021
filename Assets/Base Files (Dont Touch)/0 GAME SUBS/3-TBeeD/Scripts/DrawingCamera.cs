using UnityEngine;
using UnityEngine.UI;

namespace TBeeD
{
    public class DrawingCamera : MonoBehaviour
    {
        [SerializeField] private RawImage overlayImage;
        private Camera drawingCamera;

        void Awake()
        {
            drawingCamera = GetComponent<Camera>();
        }

        void Start()
        {
            drawingCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            overlayImage.texture = drawingCamera.targetTexture;
        }
    }
}
