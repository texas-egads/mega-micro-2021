using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OneFinity {

    [System.Serializable]
    public class BGParticle {
        public float position;
        public float velocity;
        public BGParticle() {}
    }   

    public class BGCircleGrid : MonoBehaviour
    {
        static readonly float YRATIO = 1.07735026919f;

        public float dotSpacing;
        public Vector2 minCorner;
        public Vector2 maxCorner;
        public GameObject bgCirclePrefab;

        public float particlesPrewarmTime;
        public float particleRadius;
        public float particleMinSpeed;
        public float particleMaxSpeed;
        public float particleSpawnRate;
        public float particleSpawnVariance;

        public float normalDotSize;
        public float largeDotSize;
        public Color normalDotColor;
        public Color largeDotColor;

        [SerializeField] List<List<SpriteRenderer>> dots;
        [SerializeField] List<HashSet<BGParticle>> particles;

        private float timer;
        private float nextParticleTime;


        private void Awake() {
            dots = new List<List<SpriteRenderer>>();

            float xOffset = 0f;
            for (float y = minCorner.y; y < maxCorner.y; y += dotSpacing * YRATIO) {
                var dotRow = new List<SpriteRenderer>();
                for (float x = minCorner.x + xOffset; x < maxCorner.x; x += dotSpacing) {
                    dotRow.Add(Instantiate(bgCirclePrefab, new Vector2(x, y), Quaternion.identity).GetComponent<SpriteRenderer>());
                }
                dots.Add(dotRow);
                xOffset = dotSpacing / 2f - xOffset;
            }

            particles = dots.Select(row => new HashSet<BGParticle>()).ToList();
            for (float t = 0; t < particlesPrewarmTime; t += Time.fixedDeltaTime) {
                RunParticles(Time.fixedDeltaTime);
            }
        }

        private void Update() {
            for (int i = 0; i < dots.Count; i++) {
                var dotRow = dots[i];
                var particleRow = particles[i];

                foreach (SpriteRenderer dot in dotRow) {
                    float minDistance = particleRow.Count > 0 ? particleRow.Min(p => Mathf.Abs(dot.transform.position.x - p.position)) : particleRadius;
                    float frac = 1 - Mathf.Min(minDistance / particleRadius, 1);
                    dot.transform.localScale = Vector3.one * Mathf.Lerp(normalDotSize, largeDotSize, frac);
                    dot.color = Color.Lerp(normalDotColor, largeDotColor, frac);
                }
            }
        }

        private void FixedUpdate() {
            RunParticles(Time.deltaTime);
        }

        private void RunParticles(float time) {
            if (timer >= nextParticleTime) {
                timer -= nextParticleTime;

                int rowIndex = Random.Range(0, particles.Count);
                float speed = Random.Range(particleMinSpeed, particleMaxSpeed);

                BGParticle newParticle = new BGParticle();
                if (Random.Range(0.0f, 1.0f) < 0.5f) {
                    // moving right
                    newParticle.position = minCorner.x - particleRadius;
                    newParticle.velocity = speed;
                }
                else {
                    // moving left
                    newParticle.position = maxCorner.x + particleRadius;
                    newParticle.velocity = -speed;
                }

                particles[rowIndex].Add(newParticle);
                nextParticleTime = Random.Range(particleSpawnRate - particleSpawnVariance, particleSpawnRate + particleSpawnVariance);
            }
            timer += time;

            foreach (var particleRow in particles) {
                particleRow.RemoveWhere(particle => {
                    particle.position += particle.velocity * time;
                    return particle.position < minCorner.x - particleRadius || particle.position > maxCorner.x + particleRadius;
                });
            }
        }
    }
}