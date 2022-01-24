using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalads
{
    public class Tutorial : SingletonMonobehaviour<Tutorial>
    {
        
        [Header("Leg Target Joints")]
        //Indices: 0 = Middle Top, 1 = Middle Bottom, 2 = Right/Left Top, 3 Right/Left Bottom
        private List<TargetJoint2D> _leftClawTargetJoints;
        private List<TargetJoint2D> _rightClawTargetJoints;
        private TargetJoint2D _leftClawRestingJoint;
        private TargetJoint2D _rightClawRestingJoint;
        [SerializeField] private GameObject _tutorialUI;
        protected override void Awake()
        {
            base.Awake();
        }
        private void Start() 
        {
            
        }
        public void StartTutorial()
        {
            _tutorialUI.SetActive(true);
        }
        public void EndTutorial()
        {
            MinibossScript.Instance._passedTutorial = true;
            _tutorialUI.SetActive(false);
        }

        public void SetupTargetJoints(List<TargetJoint2D> leftClawTargetJoints, List<TargetJoint2D> rightClawTargetJoints, TargetJoint2D leftClawRestingJoint, TargetJoint2D rightClawRestingJoint)
        {
            _leftClawTargetJoints = leftClawTargetJoints;
            _rightClawTargetJoints = rightClawTargetJoints;
            _leftClawRestingJoint = leftClawRestingJoint;
            _rightClawRestingJoint = rightClawRestingJoint;
            _leftClawTargetJoints[3].enabled = true;
            _rightClawTargetJoints[3].enabled = true;
            _leftClawRestingJoint.enabled = false;
            _rightClawRestingJoint.enabled = false;
        }

        public void FreeClaw(bool freeLeftClaw)
        {
            if(freeLeftClaw)
            {
                _leftClawTargetJoints[3].enabled = false;
                _leftClawRestingJoint.enabled = true;
                BossHealth.Instance.TakeDamage();
            }
            else
            {
                _rightClawTargetJoints[3].enabled = false;
                _rightClawRestingJoint.enabled = true;
                BossHealth.Instance.TakeDamage();
            }
        }
    }
}
