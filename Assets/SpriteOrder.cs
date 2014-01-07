using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SpriteOrder : MonoBehaviour
{

		public string sortingLayer;

		// Use this for initialization
		void Start ()
		{
				this.renderer.sortingLayerName = sortingLayer;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
