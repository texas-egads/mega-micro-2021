using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace Uhoh
{
    public class OceanManager : MonoBehaviour
    {
        public int fishCapacity = 5;
        public float fishTravelMin = 5;
        public float fishTravelMax = 20;
        public GameObject fishPrefab;

        public Sprite shortHookImage;

        public SpriteRenderer frogSpriteRenderer;

        public Sprite frogNormalSprite;
        public Sprite frogAboutCatchSprite;
        public Sprite frogCaughtSprite;

        public HookScript hookObject;
        public float hookYMin = -2.60f;
        public float hookYMax = -1.27f;
        public float hookYSpeed = 0.025f;
        public float hookTravel = 0.1f;
        
        private Bounds mBoundingBox;
        private GameObject mCapturedFish;
        private Bounds mFishBoundingBox;
        private List<GameObject> mFishes;
        private List<Vector3> mFishStartPositions;
        private List<float> mFishesTravel;
        private bool mGameOver;
        private Vector3 mHookInitialPosition;
        private SpriteRenderer mHookSpriteRenderer;
        private float mTimeElapsed = 0;

        private Vector3 GetRandomStartPosition(float fishTravel)
        {
            float randomX = Random.Range(mBoundingBox.min.x + mFishBoundingBox.size.x, 
                Math.Min(mBoundingBox.max.x - (fishTravel * 2),
                mBoundingBox.center.x));
            float randomY = Random.Range(mBoundingBox.min.y + mFishBoundingBox.size.y, 
                mBoundingBox.max.y - (mFishBoundingBox.size.y + 1));

            return new Vector3(randomX, randomY, 0);
        }
        
        void Start()
        {
            mBoundingBox = GetComponent<BoxCollider2D>().bounds;
            mFishBoundingBox = fishPrefab.GetComponent<BoxCollider2D>().bounds;
            mFishes = new List<GameObject>();
            mFishStartPositions = new List<Vector3>();
            mFishesTravel = new List<float>();
            mGameOver = false;
            mHookInitialPosition = hookObject.transform.position;
            mHookSpriteRenderer = hookObject.GetComponent<SpriteRenderer>();

            frogSpriteRenderer.sprite = frogNormalSprite;

            for (int i = 0; fishCapacity > i; ++i)
            {
                float fishTravel = Random.Range(fishTravelMin, fishTravelMax);
                GameObject newFish = Instantiate(fishPrefab);

                newFish.transform.position = GetRandomStartPosition(fishTravel);
                
                mFishes.Add(newFish);
                mFishStartPositions.Add(newFish.transform.position);
                mFishesTravel.Add(fishTravel);
            }

            MinigameManager.Instance.minigame.gameWin = false;
        }

        void Update()
        {
            //Assert(mFishes.Count == mFishStartPositions.Count && mFishStartPositions.Count == mFishesTravel.Count);
            
            float lerpPeriod = (float)Math.Sin(mTimeElapsed * 1.5f);
            
            for (int i = 0; mFishes.Count > i; ++i)
            {
                if (mFishes[i] == mCapturedFish)
                {
                    continue;
                }
                
                mFishes[i].transform.position = new Vector3(Mathf.Lerp(mFishStartPositions[i].x, 
                    mFishStartPositions[i].x + mFishesTravel[i], Math.Abs(lerpPeriod)), mFishStartPositions[i].y,
                    mFishStartPositions[i].z);
            }

            if (!mGameOver)
            {
                float hookYPosition = hookObject.transform.position.y;
                
                if (Input.GetAxisRaw("Vertical") < 0.0f)
                {
                    hookYPosition = Math.Max(hookYPosition - hookYSpeed, hookYMin);
                } else if (Input.GetAxisRaw("Vertical") > 0.0f)
                {
                    hookYPosition = Math.Min(hookYPosition + hookYSpeed, hookYMax);
                }

                if (Input.GetKeyDown(KeyCode.Space) && hookObject.is_colliding_with_fish())
                {
                    MinigameManager.Instance.PlaySound("FishCaught");
                    
                    hookObject.transform.position = mHookInitialPosition;
                    mHookSpriteRenderer.sprite = shortHookImage;

                    mCapturedFish = hookObject.get_colliding_game_object();
                    mCapturedFish.transform.position = mHookInitialPosition + new Vector3(0.0f, -0.32f);
                    
                    frogSpriteRenderer.sprite = frogCaughtSprite;
                    
                    MinigameManager.Instance.minigame.gameWin = true;
                    mGameOver = true;
                }
                else
                {
                    if (hookObject.is_colliding_with_fish())
                    {
                        frogSpriteRenderer.sprite = frogAboutCatchSprite;

                        MinigameManager.Instance.PlaySound("CatchingFish");
                    }
                    else
                    {
                        frogSpriteRenderer.sprite = frogNormalSprite;
                    }
                    
                    float lerpedHookXPosition;

                    if (lerpPeriod >= 0)
                    {
                        lerpedHookXPosition = Mathf.Lerp(mHookInitialPosition.x,
                            mHookInitialPosition.x + hookTravel, lerpPeriod);
                    }
                    else
                    {
                        lerpedHookXPosition = Mathf.Lerp(mHookInitialPosition.x, mHookInitialPosition.x - hookTravel, 
                            Math.Abs(lerpPeriod));
                    }
            
                    hookObject.transform.position = new Vector3(lerpedHookXPosition, hookYPosition, 
                                                                mHookInitialPosition.z); 
                }
            }

            mTimeElapsed += Time.deltaTime;
        }
    }
}