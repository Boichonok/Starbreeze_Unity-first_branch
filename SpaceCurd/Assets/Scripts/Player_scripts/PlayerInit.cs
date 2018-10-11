using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Ships;

namespace Player
{
	public class PlayerInit : NetworkBehaviour
	{
		uint playerId;

		public uint _playerId {
			set {
				playerId = value;
			}
			get {
				return playerId;
			}
		}


		private ship1 _ship1;
		private ship2 _ship2;
		private ship3 _ship3;

		[SerializeField]
		private Sprite ship1View;

		public Sprite _ship1View {
			get {
				return ship1View;
			}
			set {
				this.ship1View = value;
			}
		}

		[SerializeField]
		private Sprite ship2View;

		public Sprite _ship2View {
			get {
				return ship2View;
			}
			set {
				this.ship2View = value;
			}
		}

		[SerializeField]
		private Sprite ship3View;

		public Sprite _ship3View {
			get {
				return ship3View;
			}
			set {
				this.ship3View = value;
			}
		}

		private bool isViewsUpdate = false;

		void Start ()
		{
			if (!isLocalPlayer) {
				return;
			}
			_ship1 = GetComponent <ship1> ();
			_ship2 = GetComponent <ship2> ();
			_ship3 = GetComponent <ship3> ();

			playerId = this.netId.Value;

		}

		[Command]
		private void CmdInitShipsView ()
		{
			RpcUpdateShipsView ();
		}

		[ClientRpc]
		private void RpcUpdateShipsView ()
		{
			_ship1.ShipGO.GetComponent<SpriteRenderer> ().sprite = ship1View;
			_ship2.ShipGO.GetComponent<SpriteRenderer> ().sprite = ship2View;
			_ship3.ShipGO.GetComponent<SpriteRenderer> ().sprite = ship3View;
		}

		void Awake ()
		{
			_ship1 = GetComponent<ship1> ();
			_ship2 = GetComponent<ship2> ();
			_ship3 = GetComponent<ship3> ();

		}


		void Update ()
		{
			if (_ship1._isReady && _ship2._isReady && _ship3._isReady && isViewsUpdate == false) {
				CmdInitShipsView ();
			} 
		}
	}
}
