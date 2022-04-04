using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreScript : CubeScript
{
    enum colorName
    {
        red=0,
        blue,
        green,
        white,
        black,
        COLOR_MAX
    }
    int color;//�������Ă���F�B�ǂ����ŏ���������K�v����

    enum CreateWay
    {
        width=0,
        height,
        depth,
        WAY_MAX
    }
    int way;//�������B������=0

    public Vector3 tokenPos;//�u���b�N�����ʒu�B���x�Ɂ{����Ă����B

    Rigidbody rb;
    public float moveSpeed;//�ړ����x
    // Start is called before the first frame update
    void Start()
    {
        tokenPos = new Vector3(1.0f, 1.0f, 1.0f);
        way = 0;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
//�u���b�N�����֌W~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        if(
            (Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
            ||(Input.GetKey(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.RightArrow)) 
            ) {
            way = (int)CreateWay.width;
            CreateCommand();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            way = (int)CreateWay.height;
            CreateCommand();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            way = (int)CreateWay.depth;
            CreateCommand();
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //�L��������֌W~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        KeyControl();
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    }
    void KeyControl()
    {
        Vector3 way;
        way = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
            way += Vector3.left;

        if (Input.GetKey(KeyCode.W))
            way += Vector3.forward;
        if (Input.GetKey(KeyCode.S))
            way += Vector3.back;
        if (Input.GetKey(KeyCode.D))
            way += Vector3.right;
        Vector3.Normalize(way);
        transform.LookAt(transform.position + way);
        rb.MovePosition(transform.position + way* Time.deltaTime*moveSpeed);
    }
    void Action()
    {
        //space���͂Ŋe�u���b�N�̃A�N�V����
    }
    void CreateCommand()
    {
        Debug.Log("Done:CreateCommand");
        switch (way) {
            case (int)CreateWay.width:
                CreateCube_Width();
                break;
            case (int)CreateWay.height:
                CreateCube_Height();
                break;
            default:
            case (int)CreateWay.depth:
                CreateCube_Depth();
                break;
        }
    }
    void CreateCube_Width()
    {
        Debug.Log("Done:CreateCube_Width");
        GameObject obj = SelectCube();
        // �v���n�u�����ɃI�u�W�F�N�g�𐶐�����
        for(float y = tokenPos.y;y>0.0f; y-=1.0f) {
            for (float z = tokenPos.z;z>0.0f; z-=1.0f) {
                GameObject instance = (GameObject)Instantiate(obj,
                                                     new Vector3(tokenPos.x, y-1.0f,z-1.0f),
                                                     Quaternion.identity);
                instance = (GameObject)Instantiate(obj,
                                                             new Vector3(-tokenPos.x, y-1.0f, z-1.0f),
                                                             Quaternion.identity);
            }
        }
       
        tokenPos.x += 1.0f;
    }
    void CreateCube_Height()
    {
        Debug.Log("Done:CreateCube_Height");
        GameObject obj = SelectCube();
        // �v���n�u�����ɃI�u�W�F�N�g�𐶐�����

        GameObject instance = (GameObject)Instantiate(obj,
                                                      new Vector3(0.0f, tokenPos.y, 0.0f),
                                                      Quaternion.identity);

        if (tokenPos.x > 1.0f) {
            for (float x = tokenPos.x; x > 0.0f; x -= 1.0f) {
                for (float z = tokenPos.z; z > 0.0f; z -= 1.0f) {
                    instance = (GameObject)Instantiate(obj,
                                                         new Vector3(x - 1.0f, tokenPos.y, z - 1.0f),
                                                         Quaternion.identity);
                    instance = (GameObject)Instantiate(obj,
                                                         new Vector3((-1.0f * x) + 1.0f, tokenPos.y, z - 1.0f),
                                                         Quaternion.identity);
                }
            }
        }
        tokenPos.y += 1.0f;
    }
    void CreateCube_Depth()
    {
        Debug.Log("Done:CreateCube_Height");
        GameObject obj = SelectCube();
        // �v���n�u�����ɃI�u�W�F�N�g�𐶐�����
        GameObject instance = (GameObject)Instantiate(obj,
                                                      new Vector3(0.0f,  0.0f, tokenPos.z),
                                                      Quaternion.identity);
        tokenPos.z += 1.0f;
    }
    GameObject SelectCube()
    {
        GameObject target = null; 

        switch (color) {
            case (int)colorName.black:
                target= (GameObject)Resources.Load("Prefab/CubeBlack");
                break;
            case (int)colorName.blue:
                target = (GameObject)Resources.Load("Prefab/CubeBlue");
                break;
            case (int)colorName.white:
                target = (GameObject)Resources.Load("Prefab/CubeWhite");
                break;
            case (int)colorName.green:
                target = (GameObject)Resources.Load("Prefab/CubeGreen");
                break;
            case (int)colorName.red:
                Debug.Log("done:SELECTRED");
                target = (GameObject)Resources.Load("Prefab/CubeRed");
                break;
            default:
                break;

        }
        return target;
    }
    void ChangeColor()
    {
        if (Input.GetKeyDown("0")) {
            color = 0;
        }
        if (Input.GetKeyDown("1")) {
            color = 1;
        }
        if (Input.GetKeyDown("2")) {
            color = 2;
        }
        if (Input.GetKeyDown("3")) {
            color = 3;
        }
        if (Input.GetKeyDown("4")) {
            color = 4;
        }
    }
}
