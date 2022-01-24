using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Marmalads
{
    public enum ClawAttackStage
    {
        NotAttacking, Hovering, Attacking
    }
    public class MinibossScript : SingletonMonobehaviour<MinibossScript>
    {
        public Text UIText;
        public string startText;
        public string winText;
        [SerializeField][Range(0f,10f)] private float _phaseStartWaitTime;
        [SerializeField][Range(0,20)] private int _botsToKillInPhaseOne;
        [SerializeField][Range(0f,10f)] private float _phaseOneTimeBetweenBotSpawns;
        [SerializeField][Range(0f,10f)] private float _phaseTwoTimeBetweenBotSpawns;
        [SerializeField][Range(0f,10f)] private float _phaseTwoTimeBetweenLegAttacks;
        [SerializeField][Range(0f,10f)] private float _phaseTwoTimeLegTelegraphsAttacks;
        [SerializeField][Range(0f,10f)] private float _phaseTwoTimeLegRemainsOnGround;
        public ClawAttackStage _currentClawAttackStage = ClawAttackStage.NotAttacking;
        private Coroutine _enemySpawnCoroutine;
        private int _botsKilled;
        public bool _passedTutorial = false;
        private bool _passedPhaseOne = false;
        public bool _passedPhaseTwo = false;
        [SerializeField] private List<GameObject> _enemyPrefabs;
        [SerializeField] private List<Transform> _enemySpawnPoints;
        private HashSet<GroundEnemy> _spawnedGroundEnemies;
        [SerializeField] private List<BossClaw> _bossClaws;
        [Header("Screw Target Points")]
        //Indices: 0 = Top, 1 = Bottom
        [SerializeField] private List<Transform> _leftScrewTargetPoints;
        [SerializeField] private List<Transform> _middleScrewTargetPoints;
        [SerializeField] private List<Transform> _rightScrewTargetPoints;
        [Header("Leg Target Joints")]
        //Indices: 0 = Middle Top, 1 = Middle Bottom, 2 = Right/Left Top, 3 Right/Left Bottom
        [SerializeField] private List<TargetJoint2D> _leftClawTargetJoints;
        [SerializeField] private List<TargetJoint2D> _rightClawTargetJoints;
        [SerializeField] private TargetJoint2D _leftClawRestingJoint;
        [SerializeField] private TargetJoint2D _rightClawRestingJoint;
        [Header("Hover Indicators")]
        //Indicies: 0 = Left, 1 = Middle, 2 = Right
        [SerializeField] private List<GameObject> _hoverIndicators;
        protected override void Awake()
        {
            base.Awake();
        }
        private void Start()
        {
            // UIText.text = startText;
            MinigameManager.Instance.minigame.gameWin = true;
            _spawnedGroundEnemies = new HashSet<GroundEnemy>();
            SetupTargets();
            SetBossClawStates(false);
            Marmalads.Tutorial.Instance.SetupTargetJoints(_leftClawTargetJoints, _rightClawTargetJoints, _leftClawRestingJoint, _rightClawRestingJoint);

            StartCoroutine(Tutorial());
        }
        private void SetBossClawStates(bool active)
        {
            foreach(BossClaw claw in _bossClaws)
            {
                claw.SetColliderState(active);
            }
        }
        private void SetupTargets()
        {
            _rightClawTargetJoints[0].target = _leftClawTargetJoints[0].target = (Vector2)_middleScrewTargetPoints[0].position;
            _rightClawTargetJoints[1].target = _leftClawTargetJoints[1].target = (Vector2)_middleScrewTargetPoints[1].position;

            _leftClawTargetJoints[2].target = (Vector2)_leftScrewTargetPoints[0].position;
            _leftClawTargetJoints[3].target = (Vector2)_leftScrewTargetPoints[1].position;

            _rightClawTargetJoints[2].target = (Vector2)_rightScrewTargetPoints[0].position;
            _rightClawTargetJoints[3].target = (Vector2)_rightScrewTargetPoints[1].position;
        }
        private IEnumerator Tutorial()
        {
            yield return new WaitForSeconds(_phaseStartWaitTime);
            Marmalads.Tutorial.Instance.StartTutorial();
            while(!_passedTutorial)
            {
                yield return null;
            }
            StartCoroutine(PhaseOne());
        }
        private IEnumerator PhaseOne()
        {
            yield return new WaitForSeconds(_phaseStartWaitTime);
            _botsKilled = 0;
            _enemySpawnCoroutine = StartCoroutine(SpawnEnemies(_phaseOneTimeBetweenBotSpawns));
            while(!_passedPhaseOne)
            {
                if(_botsKilled >= _botsToKillInPhaseOne)
                {
                    _passedPhaseOne = true;
                }
                yield return null;
            }
            Debug.Log("------PHASE 1 PASSED------");
            StopCoroutine(_enemySpawnCoroutine);
            KillAllEnemies();
            StartCoroutine(PhaseTwo());
        }

        private IEnumerator PhaseTwo()
        {
            yield return new WaitForSeconds(_phaseStartWaitTime);
            SetBossClawStates(true);
            _botsKilled = 0;
            // _legAttackReady = true;
            _enemySpawnCoroutine = StartCoroutine(SpawnEnemies(_phaseTwoTimeBetweenBotSpawns));
            StartCoroutine(LegAttacks());
            while(!_passedPhaseTwo)
            {
                yield return null;
            }
            Debug.Log("------PHASE 2 PASSED------");
            StopCoroutine(_enemySpawnCoroutine);
            KillAllEnemies();
            Win();
        }

        private IEnumerator LegAttacks()
        {
            WaitForSeconds timeToWaitBetweenAttacks = new WaitForSeconds(_phaseTwoTimeBetweenLegAttacks);
            while(!_passedPhaseTwo)
            {
                ResetJoints();
                //One Leg Attack
                if(Random.value > .5f)
                {
                    // Debug.Log("1 Leg Attacking");
                    //Left Leg
                    if(Random.value > .5f)
                    {
                        _leftClawRestingJoint.enabled = false;
                        // Debug.Log("Left Leg Attacking");
                        // Left Screw
                        if(Random.value > .5f)
                        {
                            // Debug.Log("Left Screw");
                            yield return StartCoroutine(LegHover(_leftClawTargetJoints[2], _leftClawTargetJoints[3], _hoverIndicators[0]));
                        }
                        // Middle Screw
                        else
                        {
                            // Debug.Log("Middle Screw");
                            yield return StartCoroutine(LegHover(_leftClawTargetJoints[0], _leftClawTargetJoints[1], _hoverIndicators[1]));
                        }
                    }
                    //Right Leg
                    else
                    {
                        _rightClawRestingJoint.enabled = false;
                        // Debug.Log("Right Leg Attacking");
                        // Right Screw
                        if(Random.value > .5f)
                        {
                            // Debug.Log("Right Screw");
                            yield return StartCoroutine(LegHover(_rightClawTargetJoints[2], _rightClawTargetJoints[3], _hoverIndicators[2]));
                        }
                        // Middle Screw
                        else
                        {
                            // Debug.Log("Middle Screw");w
                            yield return StartCoroutine(LegHover(_rightClawTargetJoints[0], _rightClawTargetJoints[1], _hoverIndicators[1]));
                        }
                    }
                }
                //Both Legs Attack
                else
                {
                    bool middleTaken = false;
                    _leftClawRestingJoint.enabled = false;
                    _rightClawRestingJoint.enabled = false;
                    //Left Leg
                    // Left Screw
                    if(Random.value > .5f)
                    {
                        StartCoroutine(LegHover(_leftClawTargetJoints[2], _leftClawTargetJoints[3], _hoverIndicators[0]));
                    }
                    // Middle Screw
                    else
                    {
                        middleTaken = true;
                        StartCoroutine(LegHover(_leftClawTargetJoints[0], _leftClawTargetJoints[1], _hoverIndicators[1]));
                    }
                    //Right Leg
                    // Middle Screw
                    if(Random.value > .5f && !middleTaken)
                    {
                        yield return StartCoroutine(LegHover(_rightClawTargetJoints[0], _rightClawTargetJoints[1], _hoverIndicators[1]));
                    }
                    // Right Screw
                    else
                    {
                        yield return StartCoroutine(LegHover(_rightClawTargetJoints[2], _rightClawTargetJoints[3], _hoverIndicators[2]));
                    }
                }
                _leftClawRestingJoint.enabled = true;
                _rightClawRestingJoint.enabled = true;
                _currentClawAttackStage = ClawAttackStage.NotAttacking;
                yield return timeToWaitBetweenAttacks;
            }
            yield return null;
        }

        private IEnumerator LegHover(TargetJoint2D hoverTargetJoint, TargetJoint2D groundTargetJoint, GameObject indicator)
        {
            MinibossAudioManager.Instance.PlayBossWarningSFX();
            _currentClawAttackStage = ClawAttackStage.Hovering;
            indicator.SetActive(true);
            hoverTargetJoint.enabled = true;
            yield return new WaitForSeconds(_phaseTwoTimeLegTelegraphsAttacks);
            hoverTargetJoint.enabled = false;
            indicator.SetActive(false);
            yield return StartCoroutine(LegAttack(groundTargetJoint));
        }

        private IEnumerator LegAttack(TargetJoint2D targetJoint)
        {
            MinibossAudioManager.Instance.PlayBossAttackSFX();
            // _currentClawAttackStage = ClawAttackStage.Attacking;
            targetJoint.enabled = true;
            CameraShake.Instance.ShakeCamera(0.5f, 1f);
            yield return new WaitForSeconds(_phaseTwoTimeLegRemainsOnGround);
            targetJoint.enabled = false;
        }

        private void ResetJoints()
        {
            foreach(TargetJoint2D joint in _leftClawTargetJoints)
            {
                joint.enabled = false;
            }
            foreach(TargetJoint2D joint in _rightClawTargetJoints)
            {
                joint.enabled = false;
            }
            // _leftClawRestingJoint.enabled = false;
            // _rightClawRestingJoint.enabled = false;
        }
        private void ResetIndicators()
        {
            foreach(GameObject indicator in _hoverIndicators)
            {
                indicator.SetActive(false);
            }
        }
        public void SetClawAttackState(ClawAttackStage stage)
        {
            _currentClawAttackStage = stage;
        }

        private IEnumerator SpawnEnemies(float timeBetweenSpawns)
        {
            WaitForSeconds timebetweenSpawnsWait = new WaitForSeconds(timeBetweenSpawns);
            while(true)
            {
                int enemyPrefabIndex = Random.Range(0, _enemyPrefabs.Count);
                int spawnPositionIndex = Random.Range(0, _enemySpawnPoints.Count);
                GameObject newEnemy = Instantiate(_enemyPrefabs[enemyPrefabIndex], _enemySpawnPoints[spawnPositionIndex].position, Quaternion.identity);
                newEnemy.name = "GroundEnemy" + Time.time;
                //Left Spawn at index 0, Right Spawn at index 1
                newEnemy.GetComponentInChildren<GroundEnemy>()._walkRight = spawnPositionIndex == 0 ? true : false;
                if(spawnPositionIndex != 0)
                {
                    newEnemy.transform.rotation = new Quaternion(0,180,0,0);
                }
                _spawnedGroundEnemies.Add(newEnemy.GetComponentInChildren<GroundEnemy>());
                yield return timebetweenSpawnsWait;
            }
        }

        public void EnemyDie(GroundEnemy enemy)
        {
            
            if(_spawnedGroundEnemies.Remove(enemy))
            {
                _botsKilled++;
            }
        }

        private void KillAllEnemies()
        {
            foreach(GroundEnemy enemy in _spawnedGroundEnemies)
            {
                enemy.StartDieCoroutine();
            }
            _spawnedGroundEnemies.Clear();
        }
        
        public void Lose()
        {
            MinigameManager.Instance.minigame.gameWin = false;
            MinibossAudioManager.Instance.PlayLoseSFX();
            KillAllEnemies();
            StopAllCoroutines();
        }

        public void Win()
        {
            MinigameManager.Instance.minigame.gameWin = true;
            MinibossAudioManager.Instance.PlayWinSFX();
            KillAllEnemies();
            StopAllCoroutines();
            PlayerAttacks.Instance.Win();
        }
    }
}