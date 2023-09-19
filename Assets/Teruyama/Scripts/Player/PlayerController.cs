using System.Collections;
using System.Collections.Generic;
using Assets.PixelHeroes.Scripts.CharacterScripts;
using UnityEngine;
using AnimationState = Assets.PixelHeroes.Scripts.CharacterScripts.AnimationState;
using UnityEngine.U2D.Animation;
using TMPro;
using System;

    public class PlayerController : MonoBehaviour
    {
        public Character Character;
        public float RunSpeed;
        public float JumpSpeed;
        public float CrawlSpeed = 0.25f;
        public ParticleSystem MoveDust;
        public ParticleSystem JumpDust;

        private Vector3 _motion = Vector3.zero;
        [System.NonSerialized] public int _inputX;
        private int _inputY;

        public Rigidbody2D rg2d;
        [SerializeField] TextMeshProUGUI NameText; 
        bool IsGround;//地面に接地しているか
        bool IsAttacked;//攻撃後の硬直で攻撃できないならtrue
        int JumpCount;
        [SerializeField] int MaxJumpCount;//ジャンプ回数の最大値
        public float MaxSpeed;//スピードの最大値
        [SerializeField] float commandInterval;//コマンド後の硬直
        [SerializeField] ChracterDataBase chracterDataBase;//キャラクターのデータベース
        List<CharaDatas> charaData;
        [SerializeField] SpriteLibrary spriteLibrary;
        int NowCharaIndex;//現在のキャラクターのID
        string NowAnimation;
        ICommand NowCommand;

        [NonSerialized] public Action<float> OnChangeGauge;
        [NonSerialized] public Action OnResetGauge;
        public AttackCollider attackCollider;

        /// <summary>
        /// キャラクターの変更、初期化処理
        /// </summary>
        void CharacterChange(int CharaIndex){
          rg2d.gravityScale = 3;
          spriteLibrary.spriteLibraryAsset = charaData[CharaIndex].Animation;  
          MaxSpeed = charaData[CharaIndex].speed;
          JumpSpeed = charaData[CharaIndex].jumpPower;
          MaxJumpCount = charaData[CharaIndex].JumpCount;
          commandInterval = charaData[CharaIndex].CommandInterval;
          NowAnimation = charaData[CharaIndex].animationType.ToString();
          NameText.text = charaData[CharaIndex].name;
          NowCommand = charaData[CharaIndex].Command;
        }
        IEnumerator AttackInterval(){
            OnChangeGauge.Invoke(commandInterval);
            yield return new WaitForSeconds(commandInterval);
            IsAttacked = false;
        }
        IEnumerator Attack(){
            if(!IsAttacked){
                OnResetGauge();
                IsAttacked = true;
                Character.Animator.SetTrigger(NowAnimation);
                if(NowCommand != null){
                yield return NowCommand.Command(this);
                }
                CharacterChange(NowCharaIndex);
                StartCoroutine(AttackInterval());
                }
        }
        /// <summary>
        /// 初期化
        /// </summary>
        void Start()
        {
            charaData = chracterDataBase.charaDatas;
            NowCharaIndex = 0;
            CharacterChange(NowCharaIndex);
            Character.SetState(AnimationState.Idle);
        }

        void Update()
        {
          //  if (Input.GetKeyDown(KeyCode.A)) Character.Animator.SetTrigger("Attack");
          //  else if (Input.GetKeyDown(KeyCode.J)) Character.Animator.SetTrigger("Jab");
          //  else if (Input.GetKeyDown(KeyCode.P)) Character.Animator.SetTrigger("Push");
          //  else if (Input.GetKeyDown(KeyCode.H)) Character.Animator.SetTrigger("Hit");
          //  else if (Input.GetKeyDown(KeyCode.I)) { Character.SetState(AnimationState.Idle); _activityTime = 0; }
          //  else if (Input.GetKeyDown(KeyCode.R)) { Character.SetState(AnimationState.Ready); _activityTime = Time.time; }
         //   else if (Input.GetKeyDown(KeyCode.B)) Character.SetState(AnimationState.Blocking);
         //   else if (Input.GetKeyUp(KeyCode.B)) Character.SetState(AnimationState.Ready);
          //  else if (Input.GetKeyDown(KeyCode.D)) Character.SetState(AnimationState.Dead);

            // Builder characters only.
          //  else if (Input.GetKeyDown(KeyCode.O)) Character.Animator.SetTrigger("Shot");
          //  else if (Input.GetKeyDown(KeyCode.F)) Character.Animator.SetTrigger("Fire1H");
          //  else if (Input.GetKeyDown(KeyCode.E)) Character.Animator.SetTrigger("Fire2H");
          //  else if (Input.GetKeyDown(KeyCode.C)) Character.SetState(AnimationState.Climbing);
          //  else if (Input.GetKeyUp(KeyCode.C)) Character.SetState(AnimationState.Ready);
          //  else if (Input.GetKeyUp(KeyCode.L)) Character.Blink();

            

             if (Input.GetKeyDown(KeyCode.Space)){
                StartCoroutine(Attack());
             }
              if (Input.GetKeyDown(KeyCode.LeftShift)){
                NowCharaIndex++;
                if(NowCharaIndex >= charaData.Count){
                    NowCharaIndex = 0;
                }
                CharacterChange(NowCharaIndex);
             }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _inputX = -1;
                if(IsGround == true){
                Character.SetState(AnimationState.Running);
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                _inputX = 1;
                if(IsGround == true){   
                Character.SetState(AnimationState.Running);
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                rg2d.velocity = new Vector2(0,rg2d.velocity.y);
                Character.SetState(AnimationState.Idle);
            }
            
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if(JumpCount >= MaxJumpCount) return;
                JumpCount++;
                _inputY = 1;
                Character.SetState(AnimationState.Jumping);
                rg2d.AddForce(JumpSpeed * Vector2.up);
                //if(IsGround)
                {
                    JumpDust.Play(true);
                }
            }
        }

        public void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            var state = Character.GetState();

            if (state == AnimationState.Dead)
            {
                if (_inputX == 0) return;

                Character.SetState(AnimationState.Running);
            }

            if (_inputX != 0)
            {
                Turn(_inputX);
            }

            if(state == AnimationState.Running || state == AnimationState.Jumping||state == AnimationState.Idle){
                 rg2d.AddForce(Vector3.right * RunSpeed * _inputX);
                 if(rg2d.velocity.x > MaxSpeed) rg2d.velocity = new Vector2(MaxSpeed,rg2d.velocity.y);
                 if(rg2d.velocity.x < -MaxSpeed) rg2d.velocity = new Vector2(-MaxSpeed,rg2d.velocity.y);
                }

            if (IsGround)
            {
                if (state == AnimationState.Jumping)
                {
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        GetDown();
                    }
                    else
                    {
                        Character.Animator.SetTrigger("Landed");
                        Character.SetState(AnimationState.Ready);
                        JumpDust.Play(true);
                    }
                }


                if(state == AnimationState.Crawling){
                 rg2d.AddForce(Vector3.right * CrawlSpeed * _inputX);
                }
                else if(state == AnimationState.Running || state == AnimationState.Jumping){
                 rg2d.AddForce(Vector3.right * RunSpeed * _inputX);
                 if(rg2d.velocity.x > MaxSpeed) rg2d.velocity = new Vector2(MaxSpeed,rg2d.velocity.y);
                 if(rg2d.velocity.x < -MaxSpeed) rg2d.velocity = new Vector2(-MaxSpeed,rg2d.velocity.y);
                }
            }
            else
            {
                _motion = new Vector3(RunSpeed * _inputX, _motion.y);
                Character.SetState(AnimationState.Jumping);
            }
        
            _inputX = _inputY = 0;

            if (IsGround && !Mathf.Approximately(rg2d.velocity.x, 0))
            {
                var velocity = MoveDust.velocityOverLifetime;

                velocity.xMultiplier = 0.2f * -Mathf.Sign(rg2d.velocity.x);

                if (!MoveDust.isPlaying)
                {
                    MoveDust.Play();
                }
            }
            else
            {
                MoveDust.Stop();
            }
            
        }
                            

        private void Turn(int direction)
        {
            var scale = Character.transform.localScale;

            scale.x = Mathf.Sign(direction) * Mathf.Abs(scale.x);

            Character.transform.localScale = scale;
        }

        private void GetDown()
        {
            Character.Animator.SetTrigger("GetDown");
            Character.CharacterController.center = new Vector3(0, 0.05f) * Character.transform.localScale.x;
            Character.CharacterController.height = 0.08f * Character.transform.localScale.x;
        }

        private void GetUp()
        {
            Character.Animator.SetTrigger("GetUp");
            Character.CharacterController.center = new Vector3(0, 0.08f) * Character.transform.localScale.x;
            Character.CharacterController.height = 0.16f * Character.transform.localScale.x;
        }

        private void OnTriggerStay2D(Collider2D other) {
        IsGround = true;
        JumpCount = 0;
        }
        private void OnTriggerExit2D(Collider2D other) {
            IsGround = false;
        }
    }
