using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;

    // Update is called once per frame
	private void Update()
	{
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, -1), speed * Time.deltaTime);
    }
}
