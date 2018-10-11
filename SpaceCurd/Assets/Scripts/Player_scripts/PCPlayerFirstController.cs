using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Ships;

namespace Player
{
    public class PCPlayerFirstController : NetworkBehaviour
    {

        private ship1 _ship1;
        private ship2 _ship2;
        private ship3 _ship3;

        private PlayerBattleController playerBattleController;

        private bool isReady;

        public bool IsReady
        {
            get { return isReady; }
            set { isReady = value; }
        }

        // Use this for initialization
        void Start()
        {
            if (!isLocalPlayer)
            {
                Destroy(this);
                return;
            }
            _ship1 = this.GetComponent<ship1>();
            _ship2 = this.GetComponent<ship2>();
            _ship3 = this.GetComponent<ship3>();

            playerBattleController = this.GetComponent<PlayerBattleController>();

            isReady = true;
        }


        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && _ship1.isCanShootShip1 && playerBattleController.IsPlayerCanGo)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject != null)
                    {
                        _ship1.CmdShip1Attack(hit.collider.transform.position);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && _ship2.isCanShootShip2 && playerBattleController.IsPlayerCanGo)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject != null)
                    {
                        _ship2.CmdShip2Attack(hit.collider.transform.position);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && _ship3.isCanShootShip3 && playerBattleController.IsPlayerCanGo)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject != null)
                    {
                        _ship3.CmdShip3Attack(hit.collider.transform.position);
                    }
                }
            }
        }
    }
}