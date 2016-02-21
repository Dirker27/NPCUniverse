using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public TradeOracle oracle { get; private set; }

	// Use this for initialization
	void Start () {
        this.oracle = new TradeOracle();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
