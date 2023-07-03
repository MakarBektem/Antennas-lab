using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRay : MonoBehaviour
{
//объявление переменных для ВЫВОДА ПОДСКАЗОК
    public GameObject ruporON;      //текстовое поле, выводимое при наведении на облучатели
    public GameObject screenDist;   //текстовое поле, выводимое при наведении на передвигаемый блок
    public GameObject freqGen;      //текстовое поле, выводимое при наведении на генератор частоты
    public GameObject screenAngle;  ////текстовое поле, выводимое при наведении вращаемый блок

//объявление переменных для РУПОРНОЙ АНТЕННЫ (1 л.р.)
    public Transform[] ObjsS;   //объекты, двигаемые в случае установки малого рупора
    public Transform[] ObjsL;   //объекты, двигаемые в случае установки большого рупора
    private int installed;      //установлен ли рупор в 1 л.р.? 1 - установлен, 2 - нет
    private int SorL;       //какой рупор установлен? 0 - никакой, 1 - большой (L), 2 - маленький (S)

    public Text frequency;  //текстовое поле для вывода частоты генератора
    public Text distance;   //текстовое поле для вывода расстояния до экрана
    public Text KSV;        //текстовое поле для вывода коэффициента стоячей волны (КСВ)

    private float freq;  //частота генератора
    private float dist;  //расстояние до экрана
    
    //значения расстояния до экрана
    private float[] dists = new float[] {2f, 2.3f, 2.6f, 2.9f, 3.2f, 3.5f, 3.8f, 4.1f, 4.4f, 4.7f, 5f, 5.3f}; 
    //значения КСВ при установке большого рупора (L)
    private float[] KSVL = new float[] {1.4f, 1.36f, 1.2f, 1.12f, 1.18f, 1.32f, 1.4f, 1.35f, 1.19f, 1.13f, 1.21f, 1.34f};
    //значения КСВ при установке малого рупора (S)
    private float[] KSVS = new float[] {1.37f, 1.32f, 1.3f, 1.38f, 1.46f, 1.45f, 1.36f, 1.3f, 1.31f, 1.4f, 1.48f, 1.45f};

//объявление переменных для ЗЕРКАЛЬНОЙ АНТЕННЫ (2 л.р.)
    public Transform[] ObjsR;   //объекты, двигаемые в случае установки рупора
    public Transform[] ObjsV;   //объекты, двигаемые в случае установки волновода
    private int installed2;     //установлен ли облучатель в 2 л.р.? 1 - установлен, 2 - нет
    private int RorV;       //какой облучатель установлен? 0 - никакой, 1 - рупор (R), 2 - волновод (V)
    public GameObject axis; //предмет, вокруг которого будет вращаться тарелка

    public Text frequency1;    //текстовое поле для вывода частоты генератора
    public Text Angle;      //текстовое поле для вывода угла поворота антенны
    public Text DN;         //текстовое поле для вывода значений диаграммы направленности (ДН)
    
    private int angle;      //угол поворота антенны
    //углы поворота антенны
    private float[] angles = new float[] {-30f, -28f, -26f, -24f, -22f, -20f, -18f, -16f, -14f, -12f, -10f, -9f, -8f, -7f, -6f, -5f, -4f, -3f, -2f, -1f, 0f, 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f, 12f, 14f, 16f, 18f, 20f, 22f, 24f, 26f, 28f, 30f};
    //значения ДН, если установлен рупор
    private float[] DNr = new float[] {1.4f, 1.2f, 0.9f, 2.2f, 4.5f, 6.5f, 10f, 13f, 12f, 10.5f, 24f, 40f, 53f, 67f, 79f, 89f, 93f, 90f, 81f, 66f, 63f, 49f, 33f, 23f, 18f, 18f, 16f, 15f, 13.5f, 12f, 10f, 7f, 4.5f, 2.1f, 1.4f, 1.8f, 1.7f, 0.95f, 0.8f, 0.34f, 0.045f};
    //значения ДН, если установлен волновод
    private float[] DNv = new float[] {1.1f, 2.3f, 2f, 1.8f, 3.9f, 5.4f, 3.6f, 2.3f, 2.8f, 0.31f, 4.7f, 30f, 46f, 62f, 74f, 81f, 81f, 75f, 64f, 47f, 41f, 24f, 10f, 2.5f, 0.33f, 0.46f, 0.87f, 1.1f, 1.6f, 2.8f, 4.2f, 5f, 3f, 3f, 3.5f, 2.2f, 0.9f, 0.8f, 1f, 0.5f, 0.16f};

    private void Start() 
    {
//задание значений переменных для РУПОРНОЙ АНТЕННЫ (1 л.р.)
        installed = 0; //установлен ли рупор в 1 л.р.? 1 - установлен, 2 - нет
        freq = 1f;     //начальная частота
        SorL = 0;      //какой рупор установлен? 0 - никакой, 1 - большой (L), 2 - маленький (S)
        dist = 3.5f;   //начальное расстояние до экрана
//задание значений переменных для ЗЕРКАЛЬНОЙ АНТЕННЫ (2 л.р.)
        installed2 = 0; //установлен ли рупор в 2 л.р.? 1 - установлен, 2 - нет
        RorV = 0;       //какой излучатель установлен? 0 - никакой, 1 - рупор, 2 - волновод
        angle = 0;      //начальный угол поворота зеркальной антенны
        Debug.Log(angles.Length);
        Debug.Log(DNr.Length);
        Debug.Log(DNv.Length);
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);  //луч, соответствующий направлению взгляда игрока

        RaycastHit hit; //переменная, содержащая информацию объекте, на который направлен луч

        if (Physics.Raycast(ray, out hit))
        {
            string _name = hit.collider.gameObject.name;
//ПОДСКАЗКИ ПРИ НАВЕДЕНИИ НА ПРЕДМЕТЫ  
            if (_name == "ruporL" || _name == "ruporS" || _name == "rupor" ||  _name == "volnovod")
                {
                    ruporON.SetActive(true);
                }
            else 
                {
                    ruporON.SetActive(false);
                }
            if (_name == "BlockUp1")
                {
                    screenDist.SetActive(true);
                }
            else 
                {
                    screenDist.SetActive(false);
                }
            if (_name == "FrequencyGenerator1" || _name == "FrequencyGenerator2")
                {
                    freqGen.SetActive(true);
                }
            else 
                {
                    freqGen.SetActive(false);
                }
            if (_name == "BlockUp2")
                {
                    screenAngle.SetActive(true);
                }
            else 
                {
                    screenAngle.SetActive(false);
                }

// взаимодействия с РУПОРНОЙ АНТЕННОЙ (1 л.р.)
    //установка рупоров на новые координаты и изменение соответствующих переменных при наведении на них и нажатии ЛКМ
            if (installed == 0)     
            {
                if (_name == "ruporL" && Input.GetKeyUp(KeyCode.Mouse0))      
                {
                    hit.transform.position = new Vector3(2.85f, 1.565f, 27.441f);
                    installed = 1;
                    SorL = 1;
                }

                if (_name == "ruporS" && Input.GetKeyUp(KeyCode.Mouse0))
                {
                    hit.transform.position = new Vector3(2.83f, 1.381f, 27.39f);
                    installed = 1;
                    SorL = 2;
                }
            }

            else if (installed == 1)
            {
                if (_name == "ruporL" && Input.GetKeyUp(KeyCode.Mouse0))
                {
                    hit.transform.position = new Vector3(2.85f, 0.88f, 27.841f);
                    ObjsL[1].position = new Vector3(2.17f, 1.84f, 27.305f);
                    ObjsL[2].position = new Vector3(2.76f, 1.49f, 27.103f);

                    installed = 0;
                    SorL = 0;
                }

                if (_name == "ruporS" && Input.GetKeyUp(KeyCode.Mouse0))
                {
                    hit.transform.position = new Vector3(2.85f, 0.68f, 27.441f);
                    ObjsS[1].position = new Vector3(2.17f, 1.84f, 27.305f);
                    ObjsS[2].position = new Vector3(2.76f, 1.49f, 27.103f);

                    installed = 0;
                    SorL = 0;
                }
            } 
    //изменение частоты при наведении на генератор и вращении колесика мышки
           if (_name == "FrequencyGenerator1")
                {
                    float speed = Input.GetAxis("Mouse ScrollWheel");

                    if (speed > 0) 
                    {
                        freq += 1f;
                        frequency.text = freq + "GHz"; 
                    }		            
                    else if (speed < 0) 
                    {
                        freq -= 1f;
                        frequency.text = freq + "GHz";
                    }
                    else if (freq < 1f)
                    {
                        freq = 1f;
                    }
                    else if (freq > 15f)
                    {
                        freq = 15f;
                    }
                }
    //изменение положения рупоров и передвигаемого блока при наведении на этот блок и вращения колесика мышки
           if (_name == "BlockUp1")
                {
                    float speed = Input.GetAxis("Mouse ScrollWheel");
                    float speedDist = 0.3f;

                    if (SorL == 2)
                    {
                        if (speed > 0 && dist > 2f && dist < 6.3f) 
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            ObjsS[i].position = new Vector3(ObjsS[i].position.x, ObjsS[i].position.y, ObjsS[i].position.z + 0.01f);
                        }
                        dist -= speedDist;
                        distance.text = dist + "сm";  
                    }		            
                        else if (speed < 0 && dist > 1.7f && dist < 6f) 
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            ObjsS[i].position = new Vector3(ObjsS[i].position.x, ObjsS[i].position.y, ObjsS[i].position.z - 0.01f);
                        } 
                        dist += speedDist;
                        distance.text = dist + "сm";
                    }
                    }

                    if (SorL == 1)
                    {
                        if (speed > 0 && dist > 2f && dist < 6.3f)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            ObjsL[i].position = new Vector3(ObjsL[i].position.x, ObjsL[i].position.y, ObjsL[i].position.z + 0.01f);
                        }  
                        dist -= speedDist;
                        distance.text = dist + "сm";
                    }	
                        if (speed < 0 && dist > 1.7f && dist < 6f)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            ObjsL[i].position = new Vector3(ObjsL[i].position.x, ObjsL[i].position.y, ObjsL[i].position.z - 0.01f);
                        }  
                        dist += speedDist;
                        distance.text = dist + "сm";
                    }	            
                    }                  
                }
//взаимодействия с ЗЕРКАЛЬНОЙ АНТЕННОЙ
    //установка облучателей на новые координаты и изменение соответствующих переменных при наведении на них и нажатии ЛКМ
            if (installed2 == 0)
            {
                if (_name == "rupor" && Input.GetKeyUp(KeyCode.Mouse0))
                {
                    hit.transform.position = new Vector3(0.35f, 1.37f, 15.715f);
                    installed2 = 1;
                    RorV = 1;
                }

                if (_name == "volnovod" && Input.GetKeyUp(KeyCode.Mouse0))
                {
                    hit.transform.position = new Vector3(0.42f, 1.49f, 15.75f);
                    installed2 = 1;
                    RorV = 2;
                }
            }

            else if (installed2 == 1)
            {
                if (_name == "rupor" && Input.GetKeyUp(KeyCode.Mouse0))
                {
                    hit.transform.position = new Vector3(2f, 0.72f, 15.715f);
                    installed2 = 0;
                    RorV = 0;
                }

                if (_name == "volnovod" && Input.GetKeyUp(KeyCode.Mouse0))
                {
                    hit.transform.position = new Vector3(2f, 0.85f, 15.85f);
                    installed2 = 0;
                    RorV = 0;
                }
            } 
    //изменение угла вращения антенны при наведении на нее и вращения колесика мышки
           if (_name == "BlockUp2")
            {
                float speed = Input.GetAxis("Mouse ScrollWheel");

                if (speed > 0)
                {
                    if (RorV == 1)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            ObjsR[i].RotateAround(axis.transform.position, new Vector3(0, 1, 0), 100 * Time.deltaTime);
                        }
                        angle +=1;
                        Angle.text = angle + "grad";
                    }
                    if (RorV == 2)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            ObjsV[i].RotateAround(axis.transform.position, new Vector3(0, 1, 0), 100 * Time.deltaTime);
                        }
                        angle +=1;
                        Angle.text = angle + "grad";
                    }
                }
                if (speed < 0)
                {
                    if (RorV == 1)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            ObjsR[i].RotateAround(axis.transform.position, new Vector3(0, 1, 0), -100 * Time.deltaTime);
                        }
                        angle -=1;
                        Angle.text = angle + "grad";
                    }
                    if (RorV == 2)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            ObjsV[i].RotateAround(axis.transform.position, new Vector3(0, 1, 0), -100 * Time.deltaTime);
                        }
                        angle -=1;
                        Angle.text = angle + "grad";
                    }
                }   
            }
    //изменение частоты при наведении на генератор и вращении колесика мышки
            if (_name == "FrequencyGenerator2")
                {
                    float speed = Input.GetAxis("Mouse ScrollWheel");
                    if (speed > 0) 
                    {
                        freq += 1f;
                        frequency1.text = freq + "GHz"; 
                    }		            
                    else if (speed < 0) 
                    {
                        freq -= 1f;
                        frequency1.text = freq + "GHz";
                    }
                    else if (freq < 1f)
                    {
                        freq = 1f;
                    }
                    else if (freq > 15f)
                    {
                        freq = 15f;
                    }
                }
        }

//вывод результатов л.р. 1 РУПОРНАЯ АНТЕННА
        if (freq == 9f && SorL ==1)
        {
            for (int i = 0; i < 12; i++)
            {
                if (dist == dists[i])
                {
                    KSV.text = KSVL[i] +" ";
                }
            }
        } 
        else if (freq == 9f && SorL ==2)
        {
            for (int i = 0; i < 12; i++)
            {
                if (dist == dists[i])
                {
                    KSV.text = KSVS[i] +" ";
                }
            }
        }
        else if (freq != 9f || SorL ==0)
        {
            KSV.text = " ";
        } 
//вывод результатов л.р. 2 ЗЕРКАЛЬНАЯ АНТЕННА
        if (freq == 9f && RorV == 1)
        {
            for (int i = 0; i < 41; i++)
            {
                if (angle == angles[i])
                {
                    DN.text = DNr[i] + "mV";
                }
            }
        }
        else if (freq == 9f && RorV == 2)
        {
            for (int i = 0; i < 41; i++)
            {
                if (angle == angles[i])
                {
                    DN.text = DNv[i] + "mV";
                }
            }
        }
        else if (freq != 9f || RorV == 0)
        {
            DN.text = " ";
        } 
    }
}
