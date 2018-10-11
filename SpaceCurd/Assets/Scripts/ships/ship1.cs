using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Ships
{
    public class ship1 : BaseShip, IDamage_Ship
    {


        [SyncEvent]
        public event ShootAction EventOnShootShip1;

        [SyncVar(hook = "OnChangeCanShootShip1")]
        public bool isCanShootShip1 = true;

        [Command]
        public void CmdCanShootShip1(bool value)
        {
            if (!isServer)
                return;
            isCanShootShip1 = value;

        }

        [Client]
        public void OnChangeCanShootShip1(bool value)
        {
            this.isCanShootShip1 = value;

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

            type = ShipsType.Dammage;


        }



        void Update()
        {

        }



        [Command]
        public void CmdShip1Attack(Vector3 target)
        {
            EventOnShootShip1(false);
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
            GodnesGun(target);
        }

        protected override void Active_module1(Vector3 target)
        {
            EventOnShootShip1(false);
            Attack(target);
        }


        protected override void Passive_module1(Vector3 target)
        {
            EventOnShootShip1(false);

        }

        public void GodnesGun(Vector3 target)
        {
            EventOnShootShip1(false);
            Attack(target);
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
                EventOnShootShip1 += ShootingParticle;
                EventOnShootShip1 += ShootingSound;
                EventOnShootShip1 += CmdCanShootShip1;
            }
        }


        void OnDisable()
        {
            if (!NetworkClient.active)
            {
                EventOnShootShip1 -= ShootingParticle;
                EventOnShootShip1 -= ShootingSound;
                EventOnShootShip1 -= CmdCanShootShip1;
            }

        }

        void OnGUI()
        {
            HpBarUI();
            ArmorBarUI();
        }


    }
}
