using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

using Photon.Realtime;

namespace XReal.XTown.Yacht
{
    public enum GameState
    {
        initializing,
        ready,
        shaking,
        pouring,
        rolling,
        selecting,
        waiting,
        finish
    }

    public class GameManager : MonoBehaviour, IPunTurnManagerCallbacks
    {
        public static GameManager instance;
        public static Quaternion[] rotArray = new Quaternion[6];
        public static int turnCount = 1;
        public static bool rollTrigger = false;
        public static GameState currentGameState = GameState.initializing;


        public TakeTurns turnManager;

        /* events: set callbacks in inspector */ 
        public UnityEvent onInitialize;
        public UnityEvent onReadyStart;
        public UnityEvent onReadyToSelect;
        public UnityEvent onShakingStart;
        public UnityEvent onPouringStart;
        public UnityEvent onRollingStart;
        public UnityEvent onRollingFinish;
        public UnityEvent onFinish;

        private bool initializeTrigger = false;
        private bool readyTrigger = false;
        private float posX = 1.4f;
        private float posY = 7.0f; 


        /* Monobehaviour callbacks */


        void Awake()
        {
            /*
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (instance != this)
                {
                    Destroy(this.gameObject);
                }
            }
            */
            rotArray[0] = Quaternion.Euler(90f, 0f, 0f);
            rotArray[1] = Quaternion.Euler(0f, 0f, 0f);
            rotArray[2] = Quaternion.Euler(0f, 90f, 90f);
            rotArray[3] = Quaternion.Euler(0f, 0f, -90f);
            rotArray[4] = Quaternion.Euler(180f, 0f, 0f);
            rotArray[5] = Quaternion.Euler(-90f, 90f, 0f);
        }

        // Start is called before the first frame update
        void Start()
        {
            initializeTrigger = true;

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.X)) // a special event
            {
                int rand = UnityEngine.Random.Range(0, 100);
                Debug.Log("sending " + rand + " final");
                turnManager.SendMove(rand, true);
            }
            if (Input.GetKeyDown(KeyCode.E)) // an ordinary event
            {
                int rand = UnityEngine.Random.Range(0, 100);
                Debug.Log("sending " + rand + " not final");
                turnManager.SendMove(rand, false);
            }
        }
        /* Update is called once per frame
        void Update()
        {

            // �ֻ��� �� ��Ÿ ���� �ʱ�ȭ �� ready�� �̵�
            if (currentGameState == GameState.initializing)
            {
                SetGameState(GameState.ready);
                turnCount = 1;
                onInitialize.Invoke();
                initializeTrigger = false;
                readyTrigger = true;
                // ShownSlot �ʱ�ȭ�� onInitialize �̺�Ʈ�� �߰��ؾ� ��
            }

            // ready
            if (currentGameState == GameState.ready && readyTrigger)
            {
                onReadyStart.Invoke();
                readyTrigger = false;
            }

            // ready���� X ������ selecting���� ��ȯ. �̰� ù ��° �ֻ��� ���� ���� �Ұ���
            if (Input.GetKey(KeyCode.X) && currentGameState == GameState.ready && turnCount > 1 && CupManager.playingAnim == false)
            {
                CupManager.playingAnim = true;
                SetGameState(GameState.selecting);
                onReadyToSelect.Invoke();
                Debug.Log("ready to selecting");
            }
            

            // ready���� �����̽��� ������ shaking���� ��ȯ.
            if (Input.GetKeyDown(KeyCode.Space) && currentGameState == GameState.ready && CupManager.playingAnim == false)
            {
                bool moreThanOne = DiceScript.diceInfoList.Any(x => x.keeping == false);

                if (moreThanOne)
                {
                    SetGameState(GameState.shaking);
                    onShakingStart.Invoke();
                }

            }
            

            //shaking���� �����̽��� ���� pouring���� ��ȯ.
            if (Input.GetKeyUp(KeyCode.Space) && currentGameState == GameState.shaking)
            {
                SetGameState(GameState.pouring);
                onPouringStart.Invoke();
            }
            
            // rolling���� �ٲ�� ����
            if (currentGameState == GameState.rolling && rollTrigger == true)
            {
                rollTrigger = false;
                onRollingStart.Invoke();
            }

            // ��� �ֻ����� rolling�� ������ selecting���� ��ȯ
            bool rollingFinished = !DiceScript.diceInfoList.Any(x => x.diceNumber == 0);

            if (currentGameState == GameState.rolling && rollingFinished)
            {
                SetGameState(GameState.selecting);
                onRollingFinish.Invoke();
                turnCount += 1;
            }

            // 3�� �� ������ �� �Ŀ��� selecting���� finish�� ��ȯ
            if (currentGameState == GameState.selecting && turnCount > 3)
            {
                SetGameState(GameState.finish);
                onFinish.Invoke();
            }

            // selecting �ܰ迡�� X ������ ready �ܰ�� ��ȯ. �̰� �ֻ��� �� �� �� ������ �Ұ���
            if (Input.GetKey(KeyCode.X) && currentGameState == GameState.selecting && turnCount <= 3 && CupManager.playingAnim == false)
            {
                bool moreThanOne = DiceScript.diceInfoList.Any(x => x.keeping == false);

                if (moreThanOne)
                {
                    SetGameState(GameState.ready);
                    readyTrigger = true;
                }
            }
        }
        */

        /* public methods */
        public static void SetGameState(GameState newGameState)
        {
            if (Enum.IsDefined(typeof(GameState), newGameState))
            {
                currentGameState = newGameState;
                Debug.Log("state: " + newGameState);
            }
        }

        /* Turn Listener callbacks */
        public void OnTurnBegins(int turn)
        {
            Debug.Log("Turn " + turn + "begins!");
        }

        public void OnTurnCompleted(int turn)
        {
            Debug.Log("Turn " + turn + "ends");
        }

        public void OnPlayerMove(Player player, int turn, object move)
        {
            int mv = (int)move;
            Debug.Log("player" + player.ActorNumber + "has made a move " + mv);
        }

        // When a player finishes a turn (includes the move of that player)
        public void OnPlayerFinished(Player player, int turn, object move)
        {
            int mv = (int)move;
            Debug.Log("player" + player.ActorNumber + "'s turn has ended!" + mv);
        }

    }
}

