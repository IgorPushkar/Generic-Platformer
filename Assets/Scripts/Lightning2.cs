using UnityEngine;
using System.Collections;

public class Lightning2 : MonoBehaviour {

	public Color color = Color.cyan;
	public float fadeSpeed;

	private int segments;
	private LineRenderer line;
	private Vector3 startPoint;
	private Vector3 endPoint;
	private float range;

	void Start(){
		line = GetComponent<LineRenderer> ();
		line.SetVertexCount (segments);
		Vector3 vector = endPoint - startPoint;

		for (int i = 0; i < segments; ++i) {
			Vector3 point = vector / segments * i + startPoint + Random.onUnitSphere * range;
			line.SetPosition (i, point);
		}

		line.SetPosition (0, startPoint);
		line.SetPosition (segments - 1, endPoint);
	}

	public void SetPoints(Vector3 start, Vector3 end, int segments, float pointRange){
		startPoint = start;
		endPoint = end;
		this.segments = segments;
		range = pointRange;

	}

	void Update(){
		color.a -= fadeSpeed * Time.deltaTime;

		line.SetColors (color, color);

		if (color.a <= 0f) {
			Destroy (this.gameObject);
		}
	}
}