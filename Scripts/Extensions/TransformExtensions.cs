using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{

	public static void LerpMove (this Transform transform, Vector3 targetPos, float speed)
	{
		var direction = (targetPos - transform.position).normalized;
		transform.LookAt (new Vector3 (targetPos.x, transform.position.y, targetPos.z));
		transform.Translate (direction * speed);
	}
}