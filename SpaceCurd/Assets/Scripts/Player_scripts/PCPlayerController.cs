using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Ships;
using UnityEngine.UI;

namespace Player
{
    enum PCPlayerContorllerState
    {
        SELECTING_SHIP,
        SELECTING_COMMOD,
        SELECTING_TARGET
    }
    public class PCPlayerController : NetworkBehaviour
    {
        private PCPlayerContorllerState currentState;

        private PlayerBattleController playerBattleController;

        private Button active_commod;

        private Button special_commod;

        private Text shipInfo;

        private ship1 selectedShip1;
        private ship2 selectedShip2;
        private ship3 selectedShip3;

        private BaseShip.Commods selectedCommod;
        // Use this for initialization
        void Start()
        {
            if (!isLocalPlayer)
            {
                Destroy(this);
                return;
            }
            currentState = PCPlayerContorllerState.SELECTING_SHIP;
            playerBattleController = this.GetComponent<PlayerBattleController>();
            active_commod = GameObject.FindWithTag("ActiveCommodButton").GetComponent<Button>();
            special_commod = GameObject.FindWithTag("SpecialCommodButton").GetComponent<Button>();
            shipInfo = GameObject.FindWithTag("CommodInfo").GetComponent<Text>();

            selectedShip1 = null;
            selectedShip2 = null;
            selectedShip3 = null;
            selectedCommod = BaseShip.Commods.NONE;

        }

        // Update is called once per frame
        void Update()
        {
            switch (currentState)
            {
                case PCPlayerContorllerState.SELECTING_SHIP:
                    {
                        if (Input.GetKey(KeyCode.Mouse0))
                        {
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit hit;
                            if (Physics.Raycast(ray, out hit))
                            {
                                if (hit.collider.GetComponentInParent<BaseShip>().isLocalPlayer)
                                {
                                    switch (hit.collider.tag)
                                    {
                                        case "ship1":
                                            {
                                                if (selectedShip1 == null)
                                                {
                                                    selectedShip1 = hit.collider.GetComponentInParent<ship1>();
                                                    currentState = PCPlayerContorllerState.SELECTING_COMMOD;
                                                    Debug.Log("selectedShip1");
                                                }
                                            }
                                            break;
                                        case "ship2":
                                            {
                                                if (selectedShip2 == null)
                                                {
                                                    selectedShip2 = hit.collider.GetComponentInParent<ship2>();
                                                    currentState = PCPlayerContorllerState.SELECTING_COMMOD;
                                                    Debug.Log("selectedShip2");
                                                }
                                            }
                                            break;
                                        case "ship3":
                                            {
                                                if (selectedShip3 == null)
                                                {
                                                    selectedShip3 = hit.collider.GetComponentInParent<ship3>();
                                                    currentState = PCPlayerContorllerState.SELECTING_COMMOD;
                                                    Debug.Log("selectedShip3");
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case PCPlayerContorllerState.SELECTING_COMMOD:
                    {
                        if (playerBattleController.IsPlayerCanGo)
                        {
                            active_commod.onClick.AddListener(() =>
                            {
                                if (selectedShip1 != null)
                                {
                                    shipInfo.text = selectedShip1.GetInfoCommodAttackValue(BaseShip.Commods.ACTIVE);
                                }
                                else if (selectedShip2 != null)
                                {
                                    shipInfo.text = selectedShip2.GetInfoCommodAttackValue(BaseShip.Commods.ACTIVE);
                                }
                                else if (selectedShip3 != null)
                                {
                                    shipInfo.text = selectedShip3.GetInfoCommodAttackValue(BaseShip.Commods.ACTIVE);
                                }
                                selectedCommod = BaseShip.Commods.ACTIVE;
                                currentState = PCPlayerContorllerState.SELECTING_TARGET;
                            });

                            special_commod.onClick.AddListener(() =>
                            {
                                if (selectedShip1 != null)
                                {
                                    shipInfo.text = selectedShip1.GetInfoCommodAttackValue(BaseShip.Commods.SPECIAL);
                                }
                                else if (selectedShip2 != null)
                                {
                                    shipInfo.text = selectedShip2.GetInfoCommodAttackValue(BaseShip.Commods.SPECIAL);
                                }
                                else if (selectedShip3 != null)
                                {
                                    shipInfo.text = selectedShip3.GetInfoCommodAttackValue(BaseShip.Commods.SPECIAL);
                                }
                                selectedCommod = BaseShip.Commods.SPECIAL;
                                currentState = PCPlayerContorllerState.SELECTING_TARGET;
                            });
                        }
                    }
                    break;
                case PCPlayerContorllerState.SELECTING_TARGET:
                    {
                        if (Input.GetKey(KeyCode.Mouse0))
                        {
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit hit;
                            if (Physics.Raycast(ray, out hit))
                            {
                                if (selectedShip1 != null && selectedShip1.isCanShootShip1)
                                {
                                    switch (selectedCommod)
                                    {
                                        case BaseShip.Commods.ACTIVE:
                                            {
                                                selectedShip1.CmdCommod1(hit.transform.position);
                                            }
                                            break;
                                        case BaseShip.Commods.SPECIAL:
                                            {
                                                selectedShip1.CmdCommod3(hit.transform.position);
                                            }
                                            break;
                                        case BaseShip.Commods.PASSIVE:
                                            {
                                                selectedShip1.CmdCommod2(hit.transform.position);
                                            }
                                            break;
                                    }
                                }
                                else if (selectedShip2 != null && selectedShip2.isCanShootShip2)
                                {
                                    switch (selectedCommod)
                                    {
                                        case BaseShip.Commods.ACTIVE:
                                            {
                                                selectedShip2.CmdCommod1(hit.transform.position);
                                            }
                                            break;
                                        case BaseShip.Commods.SPECIAL:
                                            {
                                                selectedShip2.CmdCommod3(hit.transform.position);
                                            }
                                            break;
                                        case BaseShip.Commods.PASSIVE:
                                            {
                                                selectedShip2.CmdCommod2(hit.transform.position);
                                            }
                                            break;
                                    }
                                }
                                else if (selectedShip3 != null && selectedShip3.isCanShootShip3)
                                {
                                    switch (selectedCommod)
                                    {
                                        case BaseShip.Commods.ACTIVE:
                                            {
                                                selectedShip3.CmdCommod1(hit.transform.position);
                                            }
                                            break;
                                        case BaseShip.Commods.SPECIAL:
                                            {
                                                selectedShip3.CmdCommod3(hit.transform.position);
                                            }
                                            break;
                                        case BaseShip.Commods.PASSIVE:
                                            {
                                                selectedShip3.CmdCommod2(hit.transform.position);
                                            }
                                            break;
                                    }
                                }
                                currentState = PCPlayerContorllerState.SELECTING_SHIP;
                                selectedCommod = BaseShip.Commods.NONE;
                                selectedShip1 = null;
                                selectedShip2 = null;
                                selectedShip3 = null;

                            }
                        }
                    }
                    break;
            }
        }


    }
}
