using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Ships
{
	public class ship2 : BaseShip,ITank_Ship
	{
		[SyncEvent]
		public event ShootAction EventOnShootShip2;

		[SyncVar (hook = "OnChangeCanShootShip2")]
		public bool isCanShootShip2 = true;

		[Command]
		public void CmdCanShootShip2 (bool value)
		{
			if (!isServer)
				return;
			isCanShootShip2 = value;

		}

		[Client]
		public void OnChangeCanShootShip2 (bool value)
		{
			this.isCanShootShip2 = value;


		}


		private bool isReady;

		public bool _isReady {
			get {
				return isReady;
			}
			set {
				isReady = value;
			}
		}

		

		// Use this for initialization
		void Start ()
		{
		
			if (!isLocalPlayer)
				return;
			isReady = true;
			type = ShipsType.Tank;

		}


		void Update ()
		{
		
		}

		[Command]
		public void CmdShip2Attack (Vector3 target)
		{
			EventOnShootShip2 (false);
			Attack (target);
		}

        [Command]
        public void CmdCommod1(Vector3 target)
        {
            Active_module1(target);
        }

        [Command]
        public void CmdCommod2(Vector3 target)
        {
            Passive_module1(target);
        }

        [Command]
        public void CmdCommod3(Vector3 target)
        {
            Agro(target);
        }

        protected override void Active_module1 (Vector3 target)
		{
		//	Attack_value = 25.0f;
			EventOnShootShip2 (false);
            Attack(target);
            //CommodIfo.text = "Commod: Active commod \n Attack: " + Attack_value + "\n";

        }


        protected override void Passive_module1 (Vector3 target)
		{
			//Attack_value = 15.0f;
			EventOnShootShip2 (false);

			//CommodIfo.text = "Commod: Pasive commod \n Attack: " + Attack_value + "\n";

		}


        public void Agro(Vector3 target)
		{
			//Attack_value = 20.0f;
			EventOnShootShip2 (false);
            Attack(target);
            //CommodIfo.text = "Commod: Agro \n Attack: " + Attack_value + "\n";

        }

		protected override void ShootingParticle (bool value)
		{
		
			ShootParticle.Play ();
			Debug.Log ("Type Ship: " + type);
		}

		protected override void ShootingSound (bool value)
		{

			//Debug.Log ("Ship2 sound Boom!");
		}

        public override string GetInfoCommodAttackValue(Commods commods)
        {
            string infoCommod = "Commod ";
            switch (commods)
            {
                case Commods.ACTIVE:
                    {
                        Attack_value = 10.0f;
                        infoCommod += "Active";
                    }
                    break;
                case Commods.PASSIVE:
                    {
                        Attack_value = 5.0f;
                        infoCommod += "Passive";
                    }
                    break;
                case Commods.SPECIAL:
                    {
                        Attack_value = 20.0f;
                        infoCommod += "Special";
                    }
                    break;
            }
            return infoCommod + "\nAttack value: " + Attack_value.ToString();
        }

        void OnEnable ()
		{
			if (NetworkClient.active) {
				EventOnShootShip2 += ShootingParticle;
				EventOnShootShip2 += ShootingSound;
				EventOnShootShip2 += CmdCanShootShip2;
			}
		}


		void OnDisable ()
		{
			if (!NetworkClient.active) {
				EventOnShootShip2 -= ShootingParticle;
				EventOnShootShip2 -= ShootingSound;
				EventOnShootShip2 -= CmdCanShootShip2;
			}
		}
		void OnGUI ()
		{
			HpBarUI ();
			ArmorBarUI ();
		}

       
    }
}
