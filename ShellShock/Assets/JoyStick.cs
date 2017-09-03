using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

	[SerializeField] private Image bgImg;
	[SerializeField] private Image stickImg;
	[SerializeField] private Vector3 inputVector;
	public float angle;
	public Transform shield;
	public Transform shieldShadow;
	public Mode mode;

	void Start () {
		bgImg = GetComponent<Image> ();
		stickImg = transform.GetChild (0).GetComponent<Image> ();
	}

	public virtual void OnDrag (PointerEventData ped) {
		Vector2 pos;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
			pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

			inputVector = new Vector3 (pos.x * 2, 0, pos.y * 2);
			inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

			stickImg.rectTransform.anchoredPosition = new Vector3 (inputVector.x * (bgImg.rectTransform.sizeDelta.x / 2), inputVector.z * (bgImg.rectTransform.sizeDelta.y / 2));
			angle = Mathf.Atan2 (inputVector.z, inputVector.x);
			if (!mode.eating) {
				shield.rotation = Quaternion.Euler (0, 0, angle * Mathf.Rad2Deg);
				shieldShadow.rotation = Quaternion.Euler (0, 0, angle * Mathf.Rad2Deg);
			} else {
				mode.beak.localPosition = new Vector3 (0.4f * Mathf.Cos (angle), 0.3f * Mathf.Sin (angle), 0);
			}
		}
	}

	public virtual void OnPointerDown (PointerEventData ped) {
		OnDrag (ped);
	}

	public virtual void OnPointerUp (PointerEventData ped) {
		inputVector = Vector3.zero;
		stickImg.rectTransform.anchoredPosition = Vector3.zero;
	}
}
