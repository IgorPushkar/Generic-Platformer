using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour {

	public int segments;
	public float posRange;
	public Color color = Color.cyan;
	public float fadeSpeed;

	private LineRenderer line;

	void Start(){
		line = GetComponent<LineRenderer> ();
		line.SetVertexCount (segments);

		float startX = Random.Range (-1f, 1f);
		Vector3 startPoint = new Vector3 (startX, transform.position.y, transform.position.z);
		float endX = Random.Range (-1f, 1f);
		Vector3 endPoint = new Vector3 (endX, startPoint.y, startPoint.z + 10f);
		Vector3 vector = endPoint - startPoint;
		posRange = vector.z / segments * 0.25f;

		for (int i = 0; i < segments; ++i) {
			//				float z = ((vector / segments) * i).z;
			//				float x = (Mathf.Sin (90f * z / segments - 1)) * midPoint.x + vector.x;
			//				float y = (Mathf.Sin (90f * z / segments - 1)) * midPoint.y + vector.y;

			//				line.SetPosition (i, new Vector3 (x, y, z) + player.transform.position);
			Vector3 point = vector / segments * i + startPoint + Random.onUnitSphere * posRange;
			line.SetPosition (i, point);
		}

		line.SetPosition (0, startPoint);
		line.SetPosition (segments - 1, endPoint);
	}

	void Update(){
		color.a -= fadeSpeed * Time.deltaTime;

		line.SetColors (color, color);

		if (color.a <= 0f) {
			Destroy (this.gameObject);
		}
	}
}