//#define DEBUG_RENDER

using UnityEngine;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;

namespace UB.Simple2dWeatherEffects.Standard
{
    
    public class D2FogsPE : EffectBase
    {
        public Transform CamTransform;
        private Vector3 _firstPosition;
        private Vector3 _difference;
        public float CameraSpeedMultiplier = 1f;

        public Color Color = new Color(1f, 1f, 1f, 1f);
        public float Size = 1f;
        public float HorizontalSpeed = 0.4f;
        public float VerticalSpeed = 0f;
        [Range(0.0f, 5)]
        public float density;

        public Shader Shader;
        private Material _material;

        public float Density
        {
            get => density;
            set
            {
                density = value;
                if (density > 5)
                {
                    density = 5;
                }
                if (density < 0)
                {
                    density = 0;
                }
               
            }
        }

        private void Awake()
        {
            _firstPosition = CamTransform.position;
            density = 5;
        }

        private void Update()
        {
            _difference = CamTransform.position - _firstPosition;
            
        }

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (_material)
            {
                DestroyImmediate(_material);
                _material = null;
            }
            if (Shader)
            {
                _material = new Material(Shader);
                _material.hideFlags = HideFlags.HideAndDontSave;

                if (_material.HasProperty("_Color"))
                {
                    _material.SetColor("_Color", Color);
                }
                if (_material.HasProperty("_Size"))
                {
                    _material.SetFloat("_Size", Size);
                }
                if (_material.HasProperty("_Speed"))
                {
                    _material.SetFloat("_Speed", HorizontalSpeed);
                }
                if (_material.HasProperty("_VSpeed"))
                {
                    _material.SetFloat("_VSpeed", VerticalSpeed);
                }
                if (_material.HasProperty("_VSpeed"))
                {
                    _material.SetFloat("_VSpeed", VerticalSpeed);
                }                
                if (_material.HasProperty("_Density"))
                {
                    _material.SetFloat("_Density", density);
                }
                if (_material.HasProperty("_CameraSpeedMultiplier"))
                {
                    _material.SetFloat("_CameraSpeedMultiplier", CameraSpeedMultiplier);
                }
                if (_material.HasProperty("_UVChangeX"))
                {
                    _material.SetFloat("_UVChangeX", _difference.x);
                }
                if (_material.HasProperty("_UVChangeY"))
                {
                    _material.SetFloat("_UVChangeY", _difference.y);
                }
            }

            if (Shader != null && _material != null)
            {
                Graphics.Blit(source, destination, _material);
            }
        }

        public void StartSmoke()
        {                    
            density -= Time.deltaTime;
            
        }
        public void EndSmoke()
        {
            while(density < 5)
            {            
                density += Time.deltaTime;
            }
        }
    }
}