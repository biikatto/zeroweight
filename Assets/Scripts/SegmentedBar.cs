using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SegmentedBar : MonoBehaviour{
	private List<Transform> segments;
	public void Start(){
		segments = new List<Transform>();
		foreach(Transform child in transform){
			segments.Add(child);
		}
		segments.Reverse();
	}

	public void Phase(float phase){
		int activeSegments = (int)(segments.Count*phase);
		Debug.Log(activeSegments);
		for(int i=0;i<segments.Count;i++){
			if(i+1<=activeSegments){
				segments[i].gameObject.SetActive(true);
			}else{
				segments[i].gameObject.SetActive(false);
			}
		}
	}
}
