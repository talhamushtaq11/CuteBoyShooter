using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalexBg : MonoBehaviour 
	{	
		/// the relative speed of the object
		public float Speed = 0;
		public static ParalexBg CurrentParallaxOffset;

		protected Renderer _renderer;
		protected Vector2 _newOffset;

		protected float _position = 0;
		protected float yOffset;

		/// <summary>
		/// On start, we store the current offset
		/// </summary>
	    protected virtual void Start () 
		{
			CurrentParallaxOffset=this;
			if (GetComponent<Renderer>() != null)
			{
				_renderer = GetComponent<Renderer> ();
			}


		}

		/// <summary>
		/// On update, we apply the offset to the texture
		/// </summary>
	    protected virtual void Update()
		{
			

			if ((_renderer == null))
			{
				return;
			}
			// the new position is determined based on the level's speed and the object's speed
		
				_position += (Speed/300) * Time.deltaTime;
	      

			
			// position reset
			if (_position > 1.0f)
			{
				_position -= 1.0f;
			}

			// we apply the offset to the object's texture
			_newOffset.x = _position;
			_newOffset.y = yOffset;

			if (_renderer != null)
			{
				_renderer.material.mainTextureOffset = _newOffset;	
			}
			
		}
	}