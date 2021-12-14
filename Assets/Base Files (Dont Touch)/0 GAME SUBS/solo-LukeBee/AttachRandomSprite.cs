using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LUKE_BEE {
	public class AttachRandomSprite : MonoBehaviour {

		public Sprite[] sprites;

		void Start() {
			GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
		}
	}
}
