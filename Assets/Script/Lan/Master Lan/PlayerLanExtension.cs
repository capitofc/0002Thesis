using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using CMF;

public class PlayerLanExtension : NetworkBehaviour
{
    [Header("Player Cam")]
    [SerializeField] GameObject playerCam;
    SyncList<GameObject> players;
    public int posIndex = -1;

    [SerializeField] public GameObject ballSpointPoint;
    Skills skillFX;
    public GameObject[] particles;


    Animator anim;
    Mover mover;
    float animSpeed;
    float movementSpeed;


    private void Start()
    {
        if (isLocalPlayer)
        {
            if (isServer && isClient) 
            {
                Debug.Log("Is Server");
                CmdAddReadyPlayer();
                CmdAddPlayerTotal();
            }
            if (isClient && !isServer)
            {
                Debug.Log("Is Client");
                CmdAddPlayerTotal();
            }
            GameObject buttonHandler = GameObject.Find("Button Handler");
            buttonHandler.GetComponent<ButtonsHandler>().player = gameObject;
            buttonHandler.GetComponent<ButtonsHandler>().cast = gameObject.GetComponent<SkillControls>();
            playerCam.SetActive(true);
            anim = GetComponent<Animator>();
            mover = GetComponent<Mover>();
            CmdAddToPlayerList(gameObject);
            StageLocalPlayerReference();
            skillFX = gameObject.GetComponentInChildren<Skills>();
            particles = skillFX.particles;
        }
    }

    private void Update()
    {
        if(isLocalPlayer && Input.GetKeyDown(KeyCode.Alpha0))
        {

        }
    }


    void FindThrower()
    {
        GameObject.Find("NetworkStorage").GetComponent<LanThrower>().player = gameObject;
    }

    void StageLocalPlayerReference()
    {
        if (GameObject.Find("PlayerPositionHandler"))
        {
            GameObject.Find("PlayerPositionHandler").GetComponent<PositionHandler>().myPlayer = gameObject;
            GameObject.Find("Stage1Handler").GetComponent<LanStage1Handler>().Player = gameObject;
            GameObject.Find("Stage1Handler").GetComponent<LanStage1Handler>().GetAllText();
            GameObject.Find("Stage1Handler").GetComponent<LanStage1Handler>().GetAllPlatforms();
            GameObject.Find("Stage1Handler").GetComponent<LanStage1Handler>().CmdToAddAlivePlayer();
            GameObject.Find("Stage1Handler").GetComponent<LanStage1Handler>().GetAllPowerUps();
            posIndex = GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().PositionPos;
            CmdAddPositionPos();
        }
    }

    [Command]
    void CmdAddPositionPos()
    {
        RpcAddPositionPos();
    }

    [ClientRpc]
    void RpcAddPositionPos()
    {
        GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().PositionPos++;
    }

    [Command]
    void CmdAddPlayerTotal()
    {
        GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().PlayerTotal++;
    }

    [Command]
    void CmdAddReadyPlayer()
    {
        GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().PlayerReady++;
    }

    [Command]
    void CmdAddToPlayerList(GameObject me)
    {
        RpcAddToPlayerList(me);
        Debug.Log("Added player to Storage!");
    }

    [ClientRpc]
    void RpcAddToPlayerList(GameObject me)
    {
        GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().playerLan.Add(me);
    }

    [ClientRpc]
    public void SetMyPosition(GameObject pos)
    {
        gameObject.transform.position = pos.transform.position;
    }

    [Command]
    public void ResetMyPosition(GameObject pos)
    {
        gameObject.transform.position = pos.transform.position;
    }

    [Command]
    public void CmdSpeedUp()
    {
        Debug.Log("I am Called");
        RpcSpeedUp();
    }

    [TargetRpc]
    public void RpcSpeedUp()
    {
        animSpeed = anim.speed;
        movementSpeed = GetComponent<AdvancedWalkerController>().getMovementSpeed();
        GetComponent<AdvancedWalkerController>().setMovementSpeed(10f);
        anim.speed = 2.5f;
        StartCoroutine(ResetSpeed());
    }

    [Command]
    public void CmdResetSpeed()
    {
        RpcResetSpeed();
    }

    [TargetRpc]
    public void RpcResetSpeed()
    {
        GetComponent<AdvancedWalkerController>().setMovementSpeed(movementSpeed);
        anim.speed = animSpeed;
    }

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(3f);
        CmdResetSpeed();
    }

    IEnumerator skillTime(float time, GameObject target, int choice, float animSpeed)
    {
        yield return new WaitForSeconds(time);

        if (choice == 1)
        {
            target.GetComponent<AdvancedWalkerController>().setMovementSpeed(7f);
            target.GetComponent<AdvancedWalkerController>().setJumpSpeed(6f);
            target.GetComponent<SoundManager>().adSrc.pitch = 1f;
        }
        else if (choice == 2)
        {
            //target.GetComponent<AdvancedWalkerController>().setGravity(47f);
            target.GetComponent<AdvancedWalkerController>().setJumpSpeed(6f);
        }
        else if (choice == 3)
        {
            target.GetComponent<AdvancedWalkerController>().setMovementSpeed(7f);
            target.GetComponent<AdvancedWalkerController>().setJumpSpeed(6f);
            target.GetComponent<SoundManager>().adSrc.pitch = 1f;
            Animator anim = target.GetComponent<Animator>();
            anim.speed = animSpeed;
        }
        else if (choice == 4)
        {
            target.GetComponent<AdvancedWalkerController>().setMovementSpeed(7f);
            target.GetComponent<AdvancedWalkerController>().setJumpSpeed(6f);
            Animator anim = target.GetComponent<Animator>();
            anim.speed = animSpeed;
        }
    }

    IEnumerator disableParticle(int i, float time, Skills skillFX)
    {
        yield return new WaitForSeconds(time);

        if (skillFX.particles[i].GetComponent<DemoReactivator>() != null)
            skillFX.particles[i].GetComponent<DemoReactivator>().CancelInvoke();

        skillFX.particles[i].gameObject.SetActive(false);
    }

    [Command]
    public void CmdResetCdSkill()
    {
        RpcResetSkill();
    }

    [TargetRpc]
    public void RpcResetSkill()
    {
        GetComponent<SkillControls>().resetCD();
    }

    [Command]
    public void CmdUltiPoint()
    {
        RpcUltiPoint();
    }

    [TargetRpc]
    public void RpcUltiPoint()
    {
        GetComponent<SkillControls>().setUltiPoint(true);
    }

    [Command]
    public void CmdTakeTrixSkill()
    {
        RpcTakeTrixSkill();
    }

    [TargetRpc]
    public void RpcTakeTrixSkill()
    {
        GameObject target = gameObject;
        AdvancedWalkerController simp = GetComponent<AdvancedWalkerController>();
        simp = target.GetComponent<AdvancedWalkerController>();
        TuxAnimations anim = target.GetComponent<TuxAnimations>();
        SoundManager sound = target.GetComponent<SoundManager>();
        sound.adSrc.pitch = .2f;
        float speed = simp.getMovementSpeed();
        simp.setMovementSpeed(0f);
        GetComponent<LanSkillAnimation>().SpawnEffect(1, true);

        StartCoroutine(skillTime(3f, gameObject, 1, 0));
    }

    [Command]
    public void CmdEffectParticle(int pos)
    {
        
    }

    [TargetRpc]
    public void RpcEffectParticle(int pos)
    {
        skillFX.particles[pos].SetActive(true);
    }

    [Command]
    public void CmdTakeZilchSkill()
    {
        RpcTakeZilchSkill();
    }

    [TargetRpc]
    public void RpcTakeZilchSkill()
    {
        GetComponent<Skills>().psychosis(gameObject);
    }

    [Command]
    public void CmdTakeMazeSkill()
    {
        RpcTakeMazeSkill();
    }

    [TargetRpc]
    public void RpcTakeMazeSkill()
    {
        GetComponent<Skills>().slow(gameObject);
    }

    #region Collider

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("sbTrix"))
        {
            Debug.Log("Hit sbTrix");
            CmdTakeTrixSkill();

        }
        else if (other.gameObject.tag.Equals("sbZilch"))
        {
            Debug.Log("Hit");
            //CmdTakeZilchSkill();
            
        }
        else if (other.gameObject.tag.Equals("sbMaze"))
        {
            Debug.Log("Hit");
            //CmdTakeMazeSkill();
           
        }
        
    }


    #endregion
}
