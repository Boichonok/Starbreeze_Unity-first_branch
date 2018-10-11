using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

/*
 * There are describe all require component for BaseShip, not for speciall sihp like ship1,ship2,ship3. 
*/
using Player;

namespace Ships
{
	public abstract class BaseShip : NetworkBehaviour
	{
		#region CommodSettings
		[SerializeField]
		private float attackValue;
		public float Attack_value {
			get {return attackValue;}
			set {this.attackValue = value;}
		}

		[SerializeField]
		private GameObject bulletPref;

		
		#endregion
		
		#region BaseShootingSettings
		public delegate void ShootAction (bool value);

		protected PlayerBattleController playerBattleController;

		[SerializeField]
		private ParticleSystem shootParticle;

		public ParticleSystem ShootParticle {
			set { shootParticle = value; }
			get { return shootParticle; }
		}

		[SerializeField]
		private AudioClip shootingSoundClip;

		public AudioClip ShootingSoundClip {
			set { shootingSoundClip = value; }
		}

		[SerializeField]
		private GameObject gunPosition;

		public GameObject GunPosition {
			get{ return gunPosition; }
		}


		[SerializeField]
		private GameObject shipGO;

		public GameObject ShipGO {
			get{ return shipGO; }
		}
		#endregion

		#region OnChangeHealthRegion
		[SerializeField]
		private GameObject hpBarGObject;
		public GameObject HpBarGObject {
			get {
				return hpBarGObject;
			}
		}

		[SerializeField]
		private Texture2D hpBarTexture;
		public Texture2D HpBarTexture {
			get { return hpBarTexture;}
		}

		[SyncVar (hook = "OnChangeHealth")]
		private float hp = 100;

		public float currentShipHp {
			get {
				return hp;
			}
		}

		[Command]
		public void CmdTakeDamage ()
		{
			if (!isServer)
				return;

			if (hp > 100) {
				hp = 100;
			}


			this.hp -= attackValue;


			if (hp < 0) {
			}
		}

		[Client]
		public void OnChangeHealth (float health)
		{
			this.hp = health;
			Debug.Log (" HpValuse: " + health + " And hp: " + this.hp);

		}

		#endregion

		#region OnArmorChanging
		[SerializeField]
		private GameObject armorBarGObject;
		public GameObject ArmorBarGObject {
			get {
				return armorBarGObject;
			}
		}

		[SerializeField]
		private Texture2D armorBarTexture;
		public Texture2D ArmorBarTexture {
			get { return armorBarTexture;}
		}

		[SyncVar (hook = "OnChangeArmor")]
		private float armor = 100;

		public float currentShipArmor {
			get {
				return armor;
			}
		}

		[Command]
		public void CmdTakeDamageArmor ()
		{
			if (!isServer)
				return;

			if (armor > 100) {
				armor = 100;
			}


			this.armor -= attackValue;


			if (armor < 0) {
			}
		}

		[Client]
		public void OnChangeArmor (float armor)
		{
			this.armor = armor;
			Debug.Log (" ArmorValuse: " + armor + " And armor: " + this.armor);

		}
		#endregion

		#region BaseAttackRegion

		protected void Attack (Vector3 target)
		{
			var shell = Instantiate (bulletPref, gunPosition.transform.position, gunPosition.transform.rotation) as GameObject;
			shell.GetComponent<Bullets> ().Target = target;
			shell.GetComponent<Bullets> ().WhoMadeShoot = gunPosition;

			NetworkServer.Spawn (shell);
		}

        #endregion

        protected ShipsType type;

        public abstract string GetInfoCommodAttackValue(Commods commods);


        protected abstract void Active_module1 (Vector3 target);

        protected abstract void Passive_module1 (Vector3 target);

		protected abstract void ShootingParticle (bool value);

		protected abstract void ShootingSound (bool value);

		protected void HpBarUI ()
		{
			Vector3 posScreen = Camera.main.WorldToScreenPoint (HpBarGObject.transform.position);
			GUI.Box (new Rect (posScreen.x - 1, Screen.height - posScreen.y, (100.0f / 5.0f) * 2.0f, 5), "");
			GUI.DrawTexture (new Rect (posScreen.x - 1, Screen.height - posScreen.y, (currentShipHp / 5.0f) * 2, 5), HpBarTexture);

		}

		protected void ArmorBarUI()
		{
			Vector3 posScreen = Camera.main.WorldToScreenPoint (ArmorBarGObject.transform.position);
			GUI.Box (new Rect(posScreen.x - 1, Screen.height - posScreen.y, (100.0f / 5.0f) * 2.0f, 5), "");
			GUI.DrawTexture (new Rect (posScreen.x - 1, Screen.height - posScreen.y, (currentShipArmor / 5.0f) * 2, 5), ArmorBarTexture);

		}

        public enum Commods {
            ACTIVE,
            PASSIVE,
            SPECIAL,
            NONE
        }
	}
}

