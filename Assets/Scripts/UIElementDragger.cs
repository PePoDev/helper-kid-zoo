﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIElementDragger : MonoBehaviour {
	public const string DRAGGABLE_TAG = "UIDraggable";
	public GameObject A1, A2, A3;

	private bool dragging = false;
	private Vector2 originalPosition;
	private Transform objectToDrag;
	private Image objectToDragImage;
	private List<RaycastResult> hitObjects = new List<RaycastResult>();

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			objectToDrag = GetDraggableTransformUnderMouse();
			if (objectToDrag != null) {
				dragging = true;

				originalPosition = objectToDrag.position;
				objectToDragImage = objectToDrag.GetComponent<Image>();
			}
			A1.SetActive(false);
			A2.SetActive(false);
			A3.SetActive(false);
		}
		if (dragging) {
			objectToDrag.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			objectToDrag.localPosition = new Vector3(objectToDrag.localPosition.x, objectToDrag.localPosition.y, -10f);
		}
		if (Input.GetMouseButtonUp(0)) {
			if (objectToDrag != null) {
				var objectToReplace = GetDraggableTransformUnderMouseA();

				if (objectToReplace != null) {
					if (objectToDrag.name.Equals("Input 1"))
						gameObject.SendMessage("ClickInput", 2);
					else if (objectToDrag.name.Equals("Input 2"))
						gameObject.SendMessage("ClickInput", 3);
					else if (objectToDrag.name.Equals("Input 3"))
						gameObject.SendMessage("ClickInput", 1);
				}
				objectToDrag.position = originalPosition;

				objectToDrag = null;
			}

			dragging = false;
		}
	}
	private GameObject GetObjectUnderMouse() {
		var pointer = new PointerEventData(EventSystem.current);
		pointer.position = Input.mousePosition;
		EventSystem.current.RaycastAll(pointer, hitObjects);
		if (hitObjects.Count <= 0) return null;
		return hitObjects.First().gameObject;
	}
	private Transform GetDraggableTransformUnderMouse() {
		var clickedObject = GetObjectUnderMouse();
		// get top level object hit
		if (clickedObject != null && clickedObject.tag == DRAGGABLE_TAG) {
			return clickedObject.transform;
		}
		return null;
	}
	private Transform GetDraggableTransformUnderMouseA() {
		var clickedObject = GetObjectUnderMouse();
		// get top level object hit
		if (clickedObject != null && clickedObject.tag == "AAA") {
			return clickedObject.transform;
		}
		return null;
	}
}
