using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

namespace LUKE_BEE {
	public class AllLogic : MonoBehaviour {

		public float moveCD = 0.1f;
		public float moveSpeed = 20f;
		public Tilemap groundTilemap;
		public Tilemap playerSpawnTilemap;
		public Tilemap foodLeftSpawnTilemap;
		public Tilemap foodRightSpawnTilemap;
		public Tilemap poopLeftSpawnTilemap;
		public Tilemap poopRightSpawnTilemap;

		public Vector2 poopRangeSmall;
		public Vector2 poopRangeLarge;

		public Sprite[] sprites;

		public GameObject food;
		public GameObject poop;
		public GameObject barry;

		private Dictionary<Vector3Int, GameObject> allFood = new Dictionary<Vector3Int, GameObject>();
		private Dictionary<Vector3Int, GameObject> allPoop = new Dictionary<Vector3Int, GameObject>();
		private bool canMove;
		private Vector3Int curPos;
		private Vector3 moveTo;

		private void Start() {
			canMove = true;
			curPos = GetRandomValidTilePos(playerSpawnTilemap);
			transform.position = groundTilemap.GetCellCenterWorld(curPos);
			moveTo = transform.position;

			Vector3Int food1Pos = GetRandomValidTilePos(foodLeftSpawnTilemap);
			Vector3Int food2Pos = GetRandomValidTilePos(foodRightSpawnTilemap);
			allFood.Add(food1Pos, Instantiate(food, groundTilemap.GetCellCenterWorld(food1Pos), Quaternion.identity));
			allFood.Add(food2Pos, Instantiate(food, groundTilemap.GetCellCenterWorld(food2Pos), Quaternion.identity));

			int poopSpawnCount = Mathf.FloorToInt(Random.Range(poopRangeSmall.x, poopRangeSmall.y));
			int poopLargeSpawnCount = Mathf.FloorToInt(Random.Range(poopRangeLarge.x, poopRangeLarge.y));
			bool largeRangeLeft = Random.value > 0.5f ? true : false;

			HashSet<Vector3Int> spawned = new HashSet<Vector3Int>();
			while(spawned.Count < poopSpawnCount) {
				Vector3Int newPos;
				if (!largeRangeLeft) newPos = GetRandomValidTilePos(poopLeftSpawnTilemap);
				else newPos = GetRandomValidTilePos(poopRightSpawnTilemap);

				if (spawned.Contains(newPos)) continue;
				spawned.Add(newPos);
				allPoop.Add(newPos, Instantiate(poop, groundTilemap.GetCellCenterWorld(newPos), Quaternion.identity));
			}

			spawned.Clear();
			while (spawned.Count < poopLargeSpawnCount) {
				Vector3Int newPos;
				if (largeRangeLeft) newPos = GetRandomValidTilePos(poopLeftSpawnTilemap);
				else newPos = GetRandomValidTilePos(poopRightSpawnTilemap);

				if (spawned.Contains(newPos)) continue;
				spawned.Add(newPos);
				allPoop.Add(newPos, Instantiate(poop, groundTilemap.GetCellCenterWorld(newPos), Quaternion.identity));
			}

			MinigameManager.Instance.minigame.gameWin = false;
		}

		Vector3Int GetRandomValidTilePos(Tilemap tm) {

			float rngX, rngY;
			rngX = Random.value;
			rngY = Random.value;

			BoundsInt bounds = tm.cellBounds;
			TileBase[] allTiles = tm.GetTilesBlock(bounds);
			int x = Mathf.FloorToInt(rngX * bounds.size.x);
			int y = Mathf.FloorToInt(rngY * bounds.size.y);

			Tile curTile = (Tile)allTiles[x + y * bounds.size.x];
			while (curTile == null) {
				rngX = Random.value;
				rngY = Random.value;
				x = Mathf.FloorToInt(rngX * bounds.size.x);
				y = Mathf.FloorToInt(rngY * bounds.size.y);
				curTile = (Tile)allTiles[x + y * bounds.size.x];
			}

			return new Vector3Int(x + bounds.position.x, y + bounds.position.y, 0);
		}

		private void StopAllAudio() {
			AudioSource[] allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
			foreach (AudioSource audioS in allAudioSources) {
				audioS.Stop();
			}
		}

		private void MoveLogic(Vector3Int dPos) {
			if (groundTilemap.HasTile(curPos + dPos)) {
				MinigameManager.Instance.PlaySound("move");
				GetComponent<Animator>().SetTrigger("Move");
				curPos += dPos;
				moveTo = groundTilemap.GetCellCenterWorld(curPos);
			}

			if (allFood.ContainsKey(curPos)) {
				allFood[curPos].SetActive(false);
				barry.GetComponent<SpriteRenderer>().sprite = sprites[0];
				MinigameManager.Instance.minigame.gameWin = true;
				StopAllAudio();
				MinigameManager.Instance.PlaySound("eat");
				canMove = false;
			} else if (allPoop.ContainsKey(curPos)) {
				allPoop[curPos].SetActive(false);
				barry.GetComponent<SpriteRenderer>().sprite = sprites[1];
				StopAllAudio();
				MinigameManager.Instance.PlaySound("die");
				canMove = false;
			} else {
				StartCoroutine(startMoveCD());
			}
		}

		private void Update() {

			transform.position = Vector3.MoveTowards(transform.position, moveTo, moveSpeed * Time.deltaTime);

			if (Vector3.Distance(transform.position, moveTo) <= 0.05f && canMove) {
				if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f) {
					MoveLogic(new Vector3Int((int)Input.GetAxisRaw("Horizontal"), 0, 0));
				} else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f) {
					MoveLogic(new Vector3Int(0, (int)Input.GetAxisRaw("Vertical"), 0));
				}
			}
		}

		private IEnumerator startMoveCD() {
			canMove = false;
			yield return new WaitForSeconds(moveCD);
			canMove = true;
		}
	}
}
