using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Ships;
using UnityEngine.UI;

namespace Player
{
    public class PlayerController : NetworkBehaviour
    {


        private Button active_commod;

        private Button special_commod;


        private Text shipInfo;

        private ship1 _ship1;
        private ship2 _ship2;
        private ship3 _ship3;

        private PlayerBattleController playerBattleController;

        private GameObject currentShip;

        private delegate void BeganTouchPhase(GameObject gameObject);
        private delegate void EndedTouchPhase(GameObject gameObject);

       
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

            shipInfo = GameObject.FindWithTag("CommodInfo").GetComponent<Text>();

            active_commod = GameObject.FindWithTag("ActiveCommodButton").GetComponent<Button>();
            active_commod.onClick.AddListener(() =>
            {
                if (playerBattleController.IsPlayerCanGo)
                    ActiveCommodListener();
            });

            special_commod = GameObject.FindWithTag("SpecialCommodButton").GetComponent<Button>();
            special_commod.onClick.AddListener(() =>
            {
                if (playerBattleController.IsPlayerCanGo)
                    SpecialCommodListener();
            });

            active_commod.enabled = false;
            special_commod.enabled = false;

        }


        // Update is called once per frame
        void Update()
        {
            TouchPhases((gameObject) =>
            {
                if (gameObject.GetComponent<BaseShip>().isLocalPlayer)
                {
                    currentShip = gameObject;
                }
            }, (gameObject) =>
            {
                if (gameObject.GetComponent<BaseShip>().isLocalPlayer)
                {
                    currentShip = gameObject;
                    shipInfo.text = updateCommodButtonsUI(currentShip);
                    Debug.Log("Chosen Ship: " + currentShip.tag);
                }
            });
        }

        private void ActiveCommodListener()
        {
            TouchPhases((gameObject) =>
            {
                //BeganTouchPhase

            }, (gameObject) =>
            {
                //EndedTouchPhase
                CurrentShipAttackActiveCommod(currentShip, gameObject);
            });
        }

        private void SpecialCommodListener()
        {
            TouchPhases((gameObject) =>
            {
                //BeganTouchPhase

            }, (gameObject) =>
            {
                //EndedTouchPhase
                CurrentShipAttackSpecialCommod(currentShip, gameObject);
            });
        }


        private void TouchPhases(BeganTouchPhase beganTouchPhase, EndedTouchPhase endedTouchPhase)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                switch (Input.GetTouch(i).phase)
                {
                    case TouchPhase.Began:
                        {
                            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                            RaycastHit hit;
                            if (Physics.Raycast(ray, out hit))
                                beganTouchPhase(hit.collider.gameObject);
                        }
                        break;
                    case TouchPhase.Ended:
                        {
                            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                            RaycastHit hit;
                            if (Physics.Raycast(ray, out hit))
                                endedTouchPhase(hit.collider.gameObject);
                        }
                        break;
                }
            }
         
        }

        private void CurrentShipAttackActiveCommod(GameObject currentShip, GameObject target)
        {
            switch (currentShip.tag)
            {
                case "ship1":
                    {
                        if (_ship1.isCanShootShip1)
                            _ship1.CmdCommod1(target.transform.position);
                    }
                    break;
                case "ship2":
                    {
                        if (_ship2.isCanShootShip2)
                            _ship2.CmdCommod1(target.transform.position);
                    }
                    break;
                case "ship3":
                    {
                        if (_ship3.isCanShootShip3)
                            _ship3.CmdCommod1(target.transform.position);
                    }
                    break;
            }
        }

        private void CurrentShipPasiveCommod(GameObject currentShip, GameObject target)
        {
            switch (currentShip.tag)
            {
                case "ship1":
                    {
                        if (_ship1.isCanShootShip1)
                            _ship1.CmdCommod2(target.transform.position);
                    }
                    break;
                case "ship2":
                    {
                        if (_ship2.isCanShootShip2)
                            _ship2.CmdCommod2(target.transform.position);
                    }
                    break;
                case "ship3":
                    {
                        if (_ship3.isCanShootShip3)
                            _ship3.CmdCommod2(target.transform.position);
                    }
                    break;
            }
        }

        private void CurrentShipAttackSpecialCommod(GameObject currentShip, GameObject target)
        {
            switch (currentShip.tag)
            {
                case "ship1":
                    {
                        if (_ship1.isCanShootShip1)
                            _ship1.CmdCommod3(target.transform.position);
                    }
                    break;
                case "ship2":
                    {
                        if (_ship2.isCanShootShip2)
                            _ship2.CmdCommod3(target.transform.position);
                    }
                    break;
                case "ship3":
                    {
                        if (_ship3.isCanShootShip3)
                            _ship3.CmdCommod3(target.transform.position);
                    }
                    break;
            }
        }

        private string updateCommodButtonsUI(GameObject currentShip)
        {
            string infoShipResult = "";
            switch (currentShip.tag)
            {
                case "ship1":
                    {
                        infoShipResult = "Ship: " + currentShip.tag + "\n Commod 1: " +
                                                               currentShip.GetComponentInParent<ship1>().GetInfoCommodAttackValue(BaseShip.Commods.ACTIVE)
                                                               + "\n Commod 2: " + currentShip.GetComponentInParent<ship1>().GetInfoCommodAttackValue(BaseShip.Commods.PASSIVE)
                                                               + "\n Commod 3: " + currentShip.GetComponentInParent<ship1>().GetInfoCommodAttackValue(BaseShip.Commods.SPECIAL);

                        active_commod.enabled = true;
                        //  passive_commod.enabled = true;
                        special_commod.enabled = true;
                    }
                    break;
                case "ship2":
                    {
                        infoShipResult = "Ship: " + currentShip.tag + "\n Commod 1: " +
                                                               currentShip.GetComponentInParent<ship2>().GetInfoCommodAttackValue(BaseShip.Commods.ACTIVE)
                                                               + "\n Commod 2: " + currentShip.GetComponentInParent<ship2>().GetInfoCommodAttackValue(BaseShip.Commods.PASSIVE)
                                                               + "\n Commod 3: " + currentShip.GetComponentInParent<ship2>().GetInfoCommodAttackValue(BaseShip.Commods.SPECIAL);

                        active_commod.enabled = true;
                        // passive_commod.enabled = true;
                        special_commod.enabled = true;
                    }
                    break;
                case "ship3":
                    {
                        infoShipResult = "Ship: " + currentShip.tag + "\n Commod 1: " +
                                                               currentShip.GetComponentInParent<ship3>().GetInfoCommodAttackValue(BaseShip.Commods.ACTIVE)
                                                               + "\n Commod 2: " + currentShip.GetComponentInParent<ship3>().GetInfoCommodAttackValue(BaseShip.Commods.PASSIVE)
                                                               + "\n Commod 3: " + currentShip.GetComponentInParent<ship3>().GetInfoCommodAttackValue(BaseShip.Commods.SPECIAL);

                        active_commod.enabled = true;
                        // passive_commod.enabled = true;
                        special_commod.enabled = true;
                    }
                    break;
            }
            return infoShipResult;
        }
    }
}
