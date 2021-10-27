using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandomGen : MonoBehaviour
{

int juicySauce;
public GameObject straightTrack;
public GameObject fourfiveLTrack;
public GameObject fourfiveRTrack;
public GameObject ninetyLTrack;
public GameObject ninetyRTrack;
public GameObject RampTrack;
public GameObject car;
public float x;
public float y;
public float z;
public string prev;
public float rot;
private bool primed;
public bool newSeed = false;
public bool server = false;
public bool first = true;
public bool seedSet = false;

//public static float defaultContactOffset;
//landing on railing clips thru stage bc teleport

    // Start is called before the first frame update
    void Start()
    {
        //if (server) {
            y = -2;
            z = 20;
            x = 0;
            rot = 0;
            //Debug.Log(newSeed);
            newSeed = true;
            //Debug.Log(newSeed);
        //}
    }

    // Update is called once per frame
    void Update() {
        if (first && seedSet) {
                generate();
                first = false;
            }
    }
    void LateUpdate()
    {
        //if (server) {
            
            if (z-car.transform.position.z >= 300) {
                primed = true;
                //Debug.Log("Primed");
            }
            if (primed && z-car.transform.position.z < 300) {
                primed = false;
                Debug.Log("Generating");
                generate();
                newSeed = true;
            }
        //}
    }

    //public void setServer() {
    //    server = true;
    //}

    public int getSeed() {
        //Debug.Log("in getseed");
        //if (newSeed) {
            //Debug.Log("RandomGen prev seed: " + prev);
            //newSeed = false;
            //return prev;
        //} else {
            //Debug.Log("newSeed false");
            //return "";
        //}
        int initialSeed = UnityEngine.Random.Range(1, 1000000);
        UnityEngine.Random.InitState(initialSeed);
        seedSet = true;
        return initialSeed;
    }

    public void setSeed(){
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
    }

    public void seedSetTrue() {
        seedSet = true;
    }
    //public void setSeedFalse() {
    //    newSeed = false;
    //}

    public void generate() {
        prev = "";
        for (int i = 0; i < 40; i++) {
            juicySauce = UnityEngine.Random.Range(1, 60);
            prev = prev + juicySauce.ToString() + ",";
            //Debug.Log(juicySauce);
            if (juicySauce < 56) {
                GameObject.Instantiate(straightTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
                if (rot==0) {
                    z += 20;
                } else if (rot==90) {
                    x += 20;
                } else if (rot==-90) {
                    x -= 20;
                } else if (rot==45) {
                    x += (float)14.142;
                    z += (float)14.142;
                } else if (rot==-45) {
                    x -= (float)14.142;
                    z += (float)14.142;
                }



            } else if (juicySauce == 59 && rot != 90 && rot != 135) {

                GameObject.Instantiate(fourfiveRTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
                if (rot == 0) {
                    x += (float)2.3431;
                    z += (float)5.6569;
                } else if (rot==90) {
                    x += (float)(2.3431);
                    z += (rot/90 * -1) * (float)(5.6569);
                } else if (rot==-90) {
                    x += (rot/90 * 1) * (float)(5.6569);
                    z += (rot/90 * -1) * (float)(2.3431);
                
                } else if (rot==45) {
                    x += (rot/45 * 1) * (float)5.6569;
                    z += (rot/45 * 1) * (float)2.3431;
                }else if (rot==-45) {
                    x += (rot/45 * 1) * (float)2.3431;
                    z += (rot/45 * -1) * (float)5.6569;
                }
                //Debug.Log("Rotation: " + rot + ".......xyz" + x + "'''''''" + y + "''''''''" + z);
                rot += 45;

            } else if (juicySauce == 58 && rot != -90 && rot != -135) {
                
                GameObject.Instantiate(fourfiveLTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
                if (rot == 0) {
                    x -= (float)2.3431;
                    z += (float)5.6569;
                } else if (rot==90) {
                    x += (float)5.6569;
                    z -= (rot/90 * -1) * (float)(2.3431);
                } else if (rot==-90) {
                    x -= (float)2.3431;
                    z += (rot/90 * 1) * (float)(5.6569);
                } else if (rot==45) {
                    x -= (rot/45 * -1) * (float)2.3431;
                    z += (rot/45 * 1) * (float)5.6569;
                } else if (rot==-45) {
                    x -= (rot/45 * -1) * (float)5.6569;
                    z += (rot/45 * -1) * (float)2.3431;
                }
                //Debug.Log("Rotation: " + rot + ".......xyz" + x + "'''''''" + y + "''''''''" + z);
                rot -= 45;

            } else if (juicySauce == 57 && rot != 90 && rot != 45) {
                
                GameObject.Instantiate(ninetyRTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
                if (rot == 0) {
                    x += 8;
                    z += 8;
                } else if (rot==-45) {
                    z += (float)(11.3137);
                } else if (rot==45) {
                    z += (float)(11.3137);
                } else {
                    x += (rot/90 * 1) * 8;
                    z += (rot/90 * -1) * 8;
                }
                
                rot += 90;

            } else if (juicySauce == 56 && rot != -90 && rot != -45) {

                GameObject.Instantiate(ninetyLTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
                if (rot == 0) {
                    x -= 8;
                    z += 8;
                } else if (rot==-45) {
                    z += (float)(11.3137);
                } else if (rot==45) {
                    z += (float)(11.3137);
                } else {
                    z += (rot/90 * 1) * 8;
                    x -= (rot/90 * -1) * 8;
                }
                
                rot -= 90;

            } else {
                i -= 1;
            }
        }



        if (rot == 90) {
            GameObject.Instantiate(ninetyLTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
            if (rot == 0) {
                x -= 8;
                z += 8;
            } else if (rot==-45) {
                z += (float)(11.3137);
            } else if (rot==45) {
                z += (float)(11.3137);
            } else {
                z += (rot/90 * 1) * 8;
                x -= (rot/90 * -1) * 8;
            }
            rot -= 90;
        } else if (rot == -90) {
            GameObject.Instantiate(ninetyRTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
            if (rot == 0) {
                x += 8;
                z += 8;
            } else if (rot==-45) {
                z += (float)(11.3137);
            } else if (rot==45) {
                z += (float)(11.3137);
            } else {
                x += (rot/90 * 1) * 8;
                z += (rot/90 * -1) * 8;
            }
            rot += 90;
        } else if (rot == 45) {
            GameObject.Instantiate(fourfiveLTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
            if (rot == 0) {
                x -= (float)2.3431;
                z += (float)5.6569;
            } else if (rot==90) {
                x += (float)5.6569;
                z -= (rot/90 * -1) * (float)(2.3431);
            } else if (rot==-90) {
                x -= (float)2.3431;
                z += (rot/90 * 1) * (float)(5.6569);
            } else if (rot==45) {
                x -= (rot/45 * -1) * (float)2.3431;
                z += (rot/45 * 1) * (float)5.6569;
            } else if (rot==-45) {
                x -= (rot/45 * -1) * (float)5.6569;
                z += (rot/45 * -1) * (float)2.3431;
            }
            //Debug.Log("Rotation: " + rot + ".......xyz" + x + "'''''''" + y + "''''''''" + z);
            rot -= 45;
        } else if (rot == -45) {
            GameObject.Instantiate(fourfiveRTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
            if (rot == 0) {
                x += (float)2.3431;
                z += (float)5.6569;
            } else if (rot==90) {
                x += (float)(2.3431);
                z += (rot/90 * -1) * (float)(5.6569);
            } else if (rot==-90) {
                x += (rot/90 * 1) * (float)(5.6569);
                z += (rot/90 * -1) * (float)(2.3431);
            
            } else if (rot==45) {
                x += (rot/45 * 1) * (float)5.6569;
                z += (rot/45 * 1) * (float)2.3431;
            }else if (rot==-45) {
                x += (rot/45 * 1) * (float)2.3431;
                z += (rot/45 * -1) * (float)5.6569;
            }
            //Debug.Log("Rotation: " + rot + ".......xyz" + x + "'''''''" + y + "''''''''" + z);
            rot += 45;
        }
        GameObject.Instantiate(straightTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
        z += 20;
        GameObject.Instantiate(straightTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
        z += 20;
        GameObject.Instantiate(straightTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
        z += 20;
        GameObject.Instantiate(straightTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
        z += 20;
        GameObject.Instantiate(straightTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
        z += 20;
        GameObject.Instantiate(RampTrack, new Vector3(x,y,z), Quaternion.identity);
        z += 150;
        
    }

/*
    public void generate(string seed) {
        foreach (char c in seed) {
            //Debug.Log(juicySauce);
            if (c < 56) {
                GameObject.Instantiate(straightTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
                if (rot==0) {
                    z += 20;
                } else if (rot==90) {
                    x += 20;
                } else if (rot==-90) {
                    x -= 20;
                } else if (rot==45) {
                    x += (float)14.142;
                    z += (float)14.142;
                } else if (rot==-45) {
                    x -= (float)14.142;
                    z += (float)14.142;
                }



            } else if (c == 59 && rot != 90 && rot != 135) {

                GameObject.Instantiate(fourfiveRTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
                if (rot == 0) {
                    x += (float)2.3431;
                    z += (float)5.6569;
                } else if (rot==90) {
                    x += (float)(2.3431);
                    z += (rot/90 * -1) * (float)(5.6569);
                } else if (rot==-90) {
                    x += (rot/90 * 1) * (float)(5.6569);
                    z += (rot/90 * -1) * (float)(2.3431);
                
                } else if (rot==45) {
                    x += (rot/45 * 1) * (float)5.6569;
                    z += (rot/45 * 1) * (float)2.3431;
                }else if (rot==-45) {
                    x += (rot/45 * 1) * (float)2.3431;
                    z += (rot/45 * -1) * (float)5.6569;
                }
                //Debug.Log("Rotation: " + rot + ".......xyz" + x + "'''''''" + y + "''''''''" + z);
                rot += 45;

            } else if (c == 58 && rot != -90 && rot != -135) {
                
                GameObject.Instantiate(fourfiveLTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
                if (rot == 0) {
                    x -= (float)2.3431;
                    z += (float)5.6569;
                } else if (rot==90) {
                    x += (float)5.6569;
                    z -= (rot/90 * -1) * (float)(2.3431);
                } else if (rot==-90) {
                    x -= (float)2.3431;
                    z += (rot/90 * 1) * (float)(5.6569);
                } else if (rot==45) {
                    x -= (rot/45 * -1) * (float)2.3431;
                    z += (rot/45 * 1) * (float)5.6569;
                } else if (rot==-45) {
                    x -= (rot/45 * -1) * (float)5.6569;
                    z += (rot/45 * -1) * (float)2.3431;
                }
                //Debug.Log("Rotation: " + rot + ".......xyz" + x + "'''''''" + y + "''''''''" + z);
                rot -= 45;

            } else if (c == 57 && rot != 90 && rot != 45) {
                
                GameObject.Instantiate(ninetyRTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
                if (rot == 0) {
                    x += 8;
                    z += 8;
                } else if (rot==-45) {
                    z += (float)(11.3137);
                } else if (rot==45) {
                    z += (float)(11.3137);
                } else {
                    x += (rot/90 * 1) * 8;
                    z += (rot/90 * -1) * 8;
                }
                
                rot += 90;

            } else if (c == 56 && rot != -90 && rot != -45) {

                GameObject.Instantiate(ninetyLTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
                if (rot == 0) {
                    x -= 8;
                    z += 8;
                } else if (rot==-45) {
                    z += (float)(11.3137);
                } else if (rot==45) {
                    z += (float)(11.3137);
                } else {
                    z += (rot/90 * 1) * 8;
                    x -= (rot/90 * -1) * 8;
                }
                
                rot -= 90;

            } else {
                //no need to skip gen bc everything should land, might need to try catch these statements tho or switch to lists
                //i -= 1;
            }
        }



        if (rot == 90) {
            GameObject.Instantiate(ninetyLTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
            if (rot == 0) {
                x -= 8;
                z += 8;
            } else if (rot==-45) {
                z += (float)(11.3137);
            } else if (rot==45) {
                z += (float)(11.3137);
            } else {
                z += (rot/90 * 1) * 8;
                x -= (rot/90 * -1) * 8;
            }
            rot -= 90;
        } else if (rot == -90) {
            GameObject.Instantiate(ninetyRTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
            if (rot == 0) {
                x += 8;
                z += 8;
            } else if (rot==-45) {
                z += (float)(11.3137);
            } else if (rot==45) {
                z += (float)(11.3137);
            } else {
                x += (rot/90 * 1) * 8;
                z += (rot/90 * -1) * 8;
            }
            rot += 90;
        } else if (rot == 45) {
            GameObject.Instantiate(fourfiveLTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
            if (rot == 0) {
                x -= (float)2.3431;
                z += (float)5.6569;
            } else if (rot==90) {
                x += (float)5.6569;
                z -= (rot/90 * -1) * (float)(2.3431);
            } else if (rot==-90) {
                x -= (float)2.3431;
                z += (rot/90 * 1) * (float)(5.6569);
            } else if (rot==45) {
                x -= (rot/45 * -1) * (float)2.3431;
                z += (rot/45 * 1) * (float)5.6569;
            } else if (rot==-45) {
                x -= (rot/45 * -1) * (float)5.6569;
                z += (rot/45 * -1) * (float)2.3431;
            }
            //Debug.Log("Rotation: " + rot + ".......xyz" + x + "'''''''" + y + "''''''''" + z);
            rot -= 45;
        } else if (rot == -45) {
            GameObject.Instantiate(fourfiveRTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
            if (rot == 0) {
                x += (float)2.3431;
                z += (float)5.6569;
            } else if (rot==90) {
                x += (float)(2.3431);
                z += (rot/90 * -1) * (float)(5.6569);
            } else if (rot==-90) {
                x += (rot/90 * 1) * (float)(5.6569);
                z += (rot/90 * -1) * (float)(2.3431);
            
            } else if (rot==45) {
                x += (rot/45 * 1) * (float)5.6569;
                z += (rot/45 * 1) * (float)2.3431;
            }else if (rot==-45) {
                x += (rot/45 * 1) * (float)2.3431;
                z += (rot/45 * -1) * (float)5.6569;
            }
            //Debug.Log("Rotation: " + rot + ".......xyz" + x + "'''''''" + y + "''''''''" + z);
            rot += 45;
        }
        GameObject.Instantiate(straightTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
        z += 20;
        GameObject.Instantiate(straightTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
        z += 20;
        GameObject.Instantiate(straightTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
        z += 20;
        GameObject.Instantiate(straightTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
        z += 20;
        GameObject.Instantiate(straightTrack, new Vector3(x,y,z), Quaternion.identity * Quaternion.Euler(0, rot, 0));
        z += 20;
        GameObject.Instantiate(RampTrack, new Vector3(x,y,z), Quaternion.identity);
        z += 150;
        
    }
    */
}
