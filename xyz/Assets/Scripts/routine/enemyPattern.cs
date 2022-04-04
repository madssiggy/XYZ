using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPattern : MonoBehaviour
{
    GameObject player;
    Vector3 pPos;
    public float SerchLength;
    bool canStateChange;
    enum state
    {
        idle=0,
        serch,
        chase,
        attack,
        ChaseAndAttack,
        STATE_MAX
    }
    int enemyState;

    public float moveSpeed;//
    public float attackLength;
    public float chaseTime;//�ǂ������鎞��
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("CubeCore");
        pPos = player.transform.position;
        enemyState = (int)state.idle;
        canStateChange = false;
        StartCoroutine("Act_Idle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Act_Idle()
    {
        Debug.Log("mode:" + enemyState);
        int time = 0;
        while (time < 3) {
            yield return new WaitForSeconds(1.0f);
            time++;
        }
        enemyState = (int)state.serch;
        StartCoroutine("Act_Serch");
        yield break;
    }
    IEnumerator Act_Serch()
    {
        Debug.Log("mode:" + enemyState);
        int time = 0;       //�J�E���g 
        Vector3 len;//�g���x�N�g���̐錾

        //�R�b�ԃT�[�`
        while (time < 3) {
            pPos = player.transform.position;
            len = pPos - transform.position;
            transform.LookAt(player.transform);
            if (Vector3.Magnitude(len) < SerchLength) {
                enemyState = (int)state.chase;
                StartCoroutine("Act_Chase");
                yield break;
            }
            yield return new WaitForSeconds(1.0f);
        }
        //�������Ȃ�������idle��
        enemyState = (int)state.idle;
        StartCoroutine("Act_Idle");
        yield break;
    }
    IEnumerator Act_Chase()
    {
        Debug.Log("mode:" + enemyState);
        int time = 0;       //�J�E���g 
        Vector3 len;//�g���x�N�g���̐錾

        //�ǂ�������
        while (time < chaseTime) {
            pPos = player.transform.position;
            len = pPos - transform.position;
            //��苗���ɂȂ�����
            if (Vector3.Magnitude(len) < attackLength) {
                //�~�܂��ċ���ł�
                enemyState = (int)state.attack;
                StartCoroutine("Act_Attack");
                yield break;
            }
            transform.LookAt(player.transform);
            transform.position += Vector3.Normalize(len)*moveSpeed;
            time++;
            yield return new WaitForSeconds(1.0f);
        }

        yield break;
    }
    IEnumerator Act_Attack()
    {
        StartCoroutine("Act_Idle");
        yield break;
    }
    IEnumerator Act_ChaseAndAttack()
    {
        yield break;
    }

    
}
