using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using Player;
using Ships;

public class BattleLogic : NetworkBehaviour
{
	// Use this for initialization

	static List<PlayerBattleController> players = new List<PlayerBattleController> ();


	private bool isBattleStarted = false;

	[SyncVar (hook = "OnChangeStroke")]
	public int countRound = 1;


	public static void AddPlayerToBattle (PlayerBattleController player)
	{
		players.Add (player);
	}



	[Command]
	public void CmdNextStroke ()
	{
		if (!isServer)
			return;
		countRound++;
		if (countRound >= 7) {
			Debug.Log ("Finished battle!");
		}
	}

	[Client]
	public void OnChangeStroke (int value)
	{
		this.countRound = value;
		Debug.Log ("Round: " + countRound);

	}



	public static void WhoIsNext (PlayerBattleController player)
	{
		
		if (player.netId.Value == players [0].netId.Value) {
			players [1].CmdSetIsPlayerCanGo (true);
			players [1].GetComponent<ship1> ().CmdCanShootShip1 (true);
			players [1].GetComponent<ship2> ().CmdCanShootShip2 (true);
			players [1].GetComponent<ship3> ().CmdCanShootShip3 (true);
		} else if (player.netId.Value == players [1].netId.Value) {
			players [0].CmdSetIsPlayerCanGo (true);
			players [0].GetComponent<ship1> ().CmdCanShootShip1 (true);
			players [0].GetComponent<ship2> ().CmdCanShootShip2 (true);
			players [0].GetComponent<ship3> ().CmdCanShootShip3 (true);
		}

	}

	void Start ()
	{
		players.Clear ();
	}

	void Update ()
	{
		if (!isServer)
			return;
	
		if (!isBattleStarted) {
			players [Random.Range (0, 2)].CmdSetIsPlayerCanGo (false);
			isBattleStarted = true;
		}




	}





}

