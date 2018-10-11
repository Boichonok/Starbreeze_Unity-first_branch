using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Ships
{

	public class Bullets : NetworkBehaviour
	{
		[SerializeField]
		private Sprite bulletView;

		private GameObject whoMadeShoot;

		public GameObject WhoMadeShoot {
			get { return whoMadeShoot; }
			set { this.whoMadeShoot = value; }
		}

		private Vector3 target;

		public Vector3 Target {
			get { return target; }
			set { this.target = value; }
		}
		// Use this for initialization
		void Start ()
		{
			this.GetComponent<SpriteRenderer> ().sprite = bulletView;
		}
	
		// Update is called once per frame
		[ServerCallback]
		void Update ()
		{
			if (whoMadeShoot != null) {
				target.z = whoMadeShoot.transform.position.z;
				transform.position = Vector3.MoveTowards (this.transform.position, target, 50.0f * Time.deltaTime);
			}
	
		}

		void OnTriggerEnter (Collider col)
		{
			if (!isServer)
				return;
			
			DamageToTarget (col);

			NetworkServer.Destroy (this.gameObject);
		
		}



		private void DamageToTarget (Collider col)
		{
			if (col.gameObject.tag == "ship1") {
				if (col.gameObject.GetComponentInParent<ship1> ().currentShipArmor > 0) {
					col.gameObject.GetComponentInParent<ship1> ().CmdTakeDamageArmor ();
				} else {
					col.gameObject.GetComponentInParent<ship1> ().CmdTakeDamage ();
				}
			} else if (col.gameObject.tag == "ship2") {
				if (col.gameObject.GetComponentInParent<ship2> ().currentShipArmor > 0) {
					col.gameObject.GetComponentInParent<ship2> ().CmdTakeDamageArmor ();
				} else {
					col.gameObject.GetComponentInParent<ship2> ().CmdTakeDamage ();
				}
			} else if (col.gameObject.tag == "ship3") {
				if (col.gameObject.GetComponentInParent<ship3> ().currentShipArmor > 0) {
					col.gameObject.GetComponentInParent<ship3> ().CmdTakeDamageArmor ();
				} else {
					col.gameObject.GetComponentInParent<ship3> ().CmdTakeDamage ();
				}
			}
		}

	}
}
