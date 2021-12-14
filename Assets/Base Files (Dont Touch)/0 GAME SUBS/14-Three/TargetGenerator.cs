using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Random = System.Random;

namespace Team14
{
    public class TargetGenerator : MonoBehaviour
    {
        public Transform[] TargetSpawns;
        
        private Target[] _targets;
        [SerializeField] private Target _targetPrefab;
        [SerializeField] private TargetConfig _targetConfig;
        [SerializeField] private Camera _wantedCamera;

        private void Start()
        {
            StartCoroutine(GenerateTargetsRoutine(0));
        }
        private IEnumerator GenerateTargetsRoutine(float time)
        {
            Random rand = new Random();
            var headgear = _targetConfig.headwear
                .OrderBy(x => rand.Next()).Take(TargetSpawns.Length).ToList();

            yield return new WaitForSeconds(time);

            if (_targets != null)
            {
                foreach (Target t in _targets)
                    Destroy(t.gameObject);
            }
            _targets = new Target[TargetSpawns.Length];

            for (int i = 0; i < TargetSpawns.Length; i++)
            {
                Transform spawn = TargetSpawns[i];
                _targets[i] = Instantiate(_targetPrefab, spawn);
                _targets[i].Generate(headgear[i]);
                _targets[i].Generator = this;
            }
            Target wanted = _targets[rand.Next(0, TargetSpawns.Length)];
            wanted.IsWanted = true;
            SetLayer(wanted.gameObject, LayerMask.NameToLayer("Object 1"));

            _wantedCamera.transform.position = new Vector3(wanted.transform.position.x, wanted.transform.position.y, -10);
            _wantedCamera.transform.parent = wanted.transform;
            PieHunterManager.Instance.Pie.Reset();
        }
        public void GenerateTargets()
        {
            StartCoroutine(GenerateTargetsRoutine(1));
        }

        private void SetLayer(GameObject g, int layer)
        {
            g.layer = layer;
            foreach (Transform child in g.transform)
                SetLayer(child.gameObject, layer);
        }
    }
}
