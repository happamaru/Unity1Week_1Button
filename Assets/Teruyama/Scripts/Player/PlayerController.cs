using System.Collections;
using System.Collections.Generic;
using Assets.PixelHeroes.Scripts.CharacterScripts;
using UnityEngine;
using AnimationState = Assets.PixelHeroes.Scripts.CharacterScripts.AnimationState;
using UnityEngine.U2D.Animation;
using TMPro;
using System;
using DG.Tweening;

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] GroundCheck groundCheck;
        [SerializeField] WallCheck wallCheck;
        [SerializeField] SlopeCheck slopeCheck;
        public GameObject zanzou;
        public SpriteRenderer spriteRenderer;
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
        bool IsAttacked;//攻撃後の硬直で攻撃できないならtrue
        [NonSerialized] public int JumpCount;
        [SerializeField] int MaxJumpCount;//ジャンプ回数の最大値
        public float MaxSpeed;//スピードの最大値
        [SerializeField] float commandInterval;//コマンド後の硬直
        [SerializeField] ChracterDataBase chracterDataBase;//キャラクターのデータベース
        List<CharaDatas> charaData;
        [SerializeField] SpriteLibrary spriteLibrary;
        [SerializeField,ReadOnly] int NowCharaIndex;//現在のID
        string NowAnimation;
        ICommand NowCommand;

        [NonSerialized] public Action<float> OnChangeGauge;
        [NonSerialized] public Action OnResetGauge;
        [NonSerialized] public Action<int> OnHpChange;
        [NonSerialized] public Action<int> OnScoreChange;
        [NonSerialized] public Action<int> OnChangeSlot;
        public AttackManager attackManager;
        [SerializeField] GameObject ThiefField;

        public bool IsNoButton = false;
        public GameObject InitThief(){
            return Instantiate(ThiefField);
        }
         [SerializeField] GameObject block;
        public GameObject InitBlock(){
            return Instantiate(block);
        }

        const int MaxParty = 4;
        [SerializeField,ReadOnly] int[] PartyNums;

         enum DebugType{
            none,
            Team4,
            AllTeam,
        }
    [SerializeField] DebugType debugType;
    [SerializeField] int[] Debug_Party;
    [SerializeField,ReadOnly] bool IsNoDamage;

        /// <summary>
        /// キャラクターの変更、初期化処理
        /// </summary>
        void CharacterChange(int CharaIndex){
          spriteLibrary.spriteLibraryAsset = charaData[CharaIndex].Animation;  
          MaxSpeed = charaData[CharaIndex].speed;
          JumpSpeed = charaData[CharaIndex].jumpPower;
          MaxJumpCount = charaData[CharaIndex].JumpCount;
          rg2d.gravityScale = charaData[CharaIndex].gravity;
          commandInterval = charaData[CharaIndex].CommandInterval;
          NowAnimation = charaData[CharaIndex].animationType.ToString();
          NameText.text = charaData[CharaIndex].name;
          NowCommand = charaData[CharaIndex].Command;
        }
        IEnumerator ChangeEffect(int index){
            GameObject effect = Instantiate(charaData[index].changeEffect);
            effect.transform.position = this.transform.position;
            effect.transform.AddPosY(0.5f);
            yield return new WaitForSeconds(0.4f);
            Destroy(effect);
        }
        IEnumerator AttackInterval(){
            OnChangeGauge.Invoke(commandInterval);
            yield return new WaitForSeconds(commandInterval);
            IsAttacked = false;
        }
        IEnumerator Attack(){
            if(!IsAttacked){
                if(NowCommand != null){
                IsAttacked = true;
                Character.Animator.SetTrigger(NowAnimation);
                OnResetGauge();
                yield return NowCommand.Command(this);
                CharacterChange(PartyNums[NowCharaIndex]);
                StartCoroutine(AttackInterval());
                }
                }
        }
        /// <summary>
        /// 初期化
        /// </summary>
        void Start()
        {
            if(debugType == DebugType.none){
                for(int i =0;i<MaxParty;i++){
                PartyNums[i] = GameManager.team[i]; 
               }
            }
           else if(debugType == DebugType.Team4){
                GameManager.team = Debug_Party;
                for(int i =0;i<MaxParty;i++){
                PartyNums[i] = GameManager.team[i]; 
               }
            }else if(debugType == DebugType.AllTeam){
                PartyNums = new int[chracterDataBase.charaDatas.Count];
                for(int i=0;i<chracterDataBase.charaDatas.Count;i++){
                    PartyNums[i] = i;
                }
            }

           
            charaData = chracterDataBase.charaDatas;
            NowCharaIndex = 0;
            CharacterChange(PartyNums[NowCharaIndex]);
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
                if(!IsNoButton){
                StartCoroutine(Attack());
                } 
             }
              if (Input.GetKeyDown(KeyCode.LeftShift)){
                //if(!IsNoButton){
                NowCharaIndex++;
                if(NowCharaIndex >= PartyNums.Length){
                    NowCharaIndex = 0;
                }
                if(charaData[PartyNums[NowCharaIndex]].name == "守衛"){
                    IsNoDamage = true;
                }else{
                    IsNoDamage = false;
                }
                OnChangeSlot.Invoke(NowCharaIndex);
                CharacterChange(PartyNums[NowCharaIndex]);
               StartCoroutine(ChangeEffect(PartyNums[NowCharaIndex]));
                }
            // }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if(!IsNoButton){
                _inputX = -1;
                if(groundCheck.IsGround == true){
                Character.SetState(AnimationState.Running);
                }
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if(!IsNoButton){
                _inputX = 1;
                if(groundCheck.IsGround == true){   
                Character.SetState(AnimationState.Running);
                }
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                if(!IsNoButton){
                rg2d.velocity = new Vector2(0,rg2d.velocity.y);
                Character.SetState(AnimationState.Idle);
                }
            }
            
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if(!IsNoButton){

                if(JumpCount == 0){
                    if(groundCheck.IsGround == false) return;
                }
                if(JumpCount >= MaxJumpCount) return;
                JumpCount++;
                _inputY = 1;
                Character.SetState(AnimationState.Jumping);
                rg2d.velocity = new Vector2(rg2d.velocity.x,0);
                rg2d.AddForce(JumpSpeed * Vector2.up);
                //if(groundCheck.IsGround)
                {
                    JumpDust.Play(true);
                }
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
            if(rg2d.velocity.x > MaxSpeed) rg2d.velocity = new Vector2(MaxSpeed,rg2d.velocity.y);
            if(rg2d.velocity.x < -MaxSpeed) rg2d.velocity = new Vector2(-MaxSpeed,rg2d.velocity.y);
            
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
                 if(wallCheck.IsWall == false){
                 rg2d.AddForce(Vector3.right * RunSpeed * _inputX);
                 }
                 //else if(slopeCheck.IsSlope && wallCheck.IsWall == false){
                  //  this.gameObject.transform.Translate(0.1f*_inputX,0,0);
                 //}
                 
                }

            if (groundCheck.IsGround)
            {
                if (state == AnimationState.Jumping)
                {
                        Character.Animator.SetTrigger("Landed");
                        Character.SetState(AnimationState.Ready);
                        JumpDust.Play(true);
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
                if(groundCheck.IsGround == false){
                Character.SetState(AnimationState.Jumping);
                }else{
                    if(_inputX != 0){
                        Character.SetState(AnimationState.Running);   
                    }else{
                        Character.SetState(AnimationState.Idle);
                    }
                }
            }
        
            _inputX = _inputY = 0;

            if (groundCheck.IsGround && !Mathf.Approximately(rg2d.velocity.x, 0))
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

         private void OnCollisionEnter2D(Collision2D other) {
            
            var enemy = other.gameObject.GetComponent<IEnemy>();
            if(enemy != null){
                if(IsNoDamage){
                rg2d.AddForce(Vector2.left * this.transform.localScale.x * 800);
                Character.SetState(AnimationState.Blocking);
                return;
            }
                HitBlink();
                OnHpChange(enemy.AddDamage());
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            var item = other.gameObject.GetComponent<IScore>();
            if(item != null){
                OnScoreChange(item.AddScore());
            }
        }

        
        Sequence _seq;
     /// <summary> 点滅によるダメージ演出再生 </summary>
    private void HitBlink()
    {
        _seq?.Kill();
        _seq = DOTween.Sequence();
        _seq.AppendCallback(() => spriteRenderer.color = Vector4.zero);
        _seq.AppendInterval(0.05f);
        _seq.AppendCallback(() => spriteRenderer.color = Vector4.one);
        _seq.AppendInterval(0.05f);
        _seq.SetLoops(2);
        _seq.Play();
    }


     /*   private void OnTriggerStay2D(Collider2D other) {
            if(other.gameObject.tag == "ground"){
                if(!groundCheck.IsGround){
                rg2d.velocity = new Vector2(rg2d.velocity.x,0);
                groundCheck.IsGround = true;
                JumpCount = 0;
                }
            }
                
        }
       
        private void OnTriggerExit2D(Collider2D other) {
            if(other.gameObject.tag == "ground"){
                groundCheck.IsGround = false;
            }
        }
        */
    }
