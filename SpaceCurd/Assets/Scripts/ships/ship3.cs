using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Ships
{

    public class ship3 : BaseShip,ISupport_Ship
    {

        [SyncEvent]
        public event ShootAction EventOnShootShip3;

        [SyncVar(hook = "OnChangeCanShootShip3")]
        public bool isCanShootShip3 = true;

        [Command]
        public void CmdCanShootShip3(bool value)
        {
            if (!isServer)
                return;
            isCanShootShip3 = value;

        }

        [Client]
        public void OnChangeCanShootShip3(bool value)
        {
            this.isCanShootShip3 = value;


        }

        private bool isReady;

        public bool _isReady
        {
            get
            {
                return isReady;
            }
            set
            {
                isReady = value;
            }
        }



        // Use this for initialization
        void Start()
        {

            if (!isLocalPlayer)
                return;
            isReady = true;
            type = ShipsType.Supprot;


        }


        void Update()
        {

        }

        [Command]
        public void CmdShip3Attack(Vector3 target)
        {
            EventOnShootShip3(false);
            Attack(target);
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
            Ignite(target);
        }

        protected override void Active_module1(Vector3 target)
        {
            //Attack_value = 20.0f;
            EventOnShootShip3(false);
            Attack(target);
            //CommodIfo.text = "Commod: Active commod \n Attack: " + Attack_value + "\n";

        }


        protected override void Passive_module1(Vector3 target)
        {
            //Attack_value = 20.0f;
            EventOnShootShip3(false);
            //CommodIfo.text = "Commod: Pasive commod \n Attack: " + Attack_value + "\n";

        }


        public void Ignite(Vector3 target)
        {
            //Attack_value = 20.0f;
            EventOnShootShip3(false);
            Attack(target);
            //CommodIfo.text = "Commod: Ignite \n Attack: " + Attack_value + "\n";

        }

        protected override void ShootingParticle(bool value)
        {
            ShootParticle.Play();
            Debug.Log("Type Ship: " + type);
        }

        protected override void ShootingSound(bool value)
        {
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

        void OnEnable()
        {
            if (NetworkClient.active)
            {
                EventOnShootShip3 += ShootingParticle;
                EventOnShootShip3 += ShootingSound;
                EventOnShootShip3 += CmdCanShootShip3;

            }
        }


        void OnDisable()
        {
            if (!NetworkClient.active)
            {
                EventOnShootShip3 -= ShootingParticle;
                EventOnShootShip3 -= ShootingSound;
                EventOnShootShip3 -= CmdCanShootShip3;

            }
        }
        void OnGUI()
        {
            HpBarUI();
            ArmorBarUI();
        }
    }
}
