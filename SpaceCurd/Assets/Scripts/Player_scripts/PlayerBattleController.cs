using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Ships;

namespace Player
{
	public class PlayerBattleController : NetworkBehaviour
	{
		[SerializeField]
		private BattleLogic battleLogic;

		[SyncVar (hook = "OnChangeIsPlayerCanGo")]
		private bool isPlayerCanGo = true;


		public bool IsPlayerCanGo {
			get { return isPlayerCanGo; }
			set { isPlayerCanGo = value; }
		}

		[Command]
		public void CmdSetIsPlayerCanGo (bool value)
		{
			if (!isServer)
				return;
			isPlayerCanGo = value;

		}

		[Client]
		void OnChangeIsPlayerCanGo (bool value)
		{
			this.isPlayerCanGo = value;
			Debug.Log ("IsPlayerCanGo:: " + isPlayerCanGo + " PlayerID:: " + netId.Value + " IsLocal Player:: " + isLocalPlayer);
		}

		private int countBlokedShips = 0;

		public int CountBlockedShips {
			get { return countBlokedShips; }
			set { countBlokedShips = value; }
		}

		// Use this for initialization
		void Start ()
		{
			if (isServer) {
				BattleLogic.AddPlayerToBattle (this);
			}
			battleLogic = FindObjectOfType<BattleLogic> ();
		}


		
		void Update ()
		{
			if (!GetComponent<ship1> ().isCanShootShip1 &&
			    !GetComponent<ship2> ().isCanShootShip2 &&
			    !GetComponent<ship3> ().isCanShootShip3 &&
			    isPlayerCanGo == true) {

				battleLogic.CmdNextStroke ();
				CmdSetIsPlayerCanGo (false);
				BattleLogic.WhoIsNext (this);
			}

		}
	}
}
