using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateAbilities : EditorWindow
{
    Ability yesAbility;
    string _abilityName;
    string _abilityDesc;
    
    int count = 0;
    public enum MODIFIERTYPE
    {
        damage,
        heal,
        applyBuff,
        applyDebuff
    }

    public enum TESTE
    {
        Create,
        Edit
    }
    public  TESTE teste;
    [MenuItem("Tools/AbilityMenu")]
 
      static void Init()
    {
        var window = GetWindow<CreateAbilities>();
        window.Show();
    }
    private void OnGUI()
    {
        _abilityName = (string)EditorGUILayout.TextField("Name: ", _abilityName);
        
        if (GUILayout.Button("Create ScriptableObj"))
        {
            Ability newAbility = ScriptableObject.CreateInstance<Ability>();
            string path = "Assets/_ScriptableObjects/" + _abilityName + ".asset";
            AssetDatabase.CreateAsset(newAbility, path);
            yesAbility = newAbility;
        }

        yesAbility = (Ability)EditorGUILayout.ObjectField("Scriptable Obj: ", yesAbility, typeof(Ability), false);
        _abilityDesc = (string)EditorGUILayout.TextField("Desc: ", _abilityDesc);

        if (yesAbility == null)
        {
            return;
        }
        if(GUILayout.Button("Add Damage Modifier"))
        {
            DamageModifier damage = new DamageModifier();
            yesAbility.Mods.Add(damage);
        }

        for (int i = 0; i < yesAbility.Mods.Count; i++)
        {
            Debug.Log(yesAbility.Mods[i].GetType());
            if(yesAbility.Mods[i] is DamageModifier)
            {
                yesAbility.Mods[i].Draw();
            }
        }

        if (yesAbility == null)
        {
            return;
        }
        if (GUILayout.Button("Add Healing Modifier"))
        {
            HealModifier healing = new HealModifier();
            yesAbility.Mods.Add(healing);
        }

        for (int i = 0; i < yesAbility.Mods.Count; i++)
        {
            Debug.Log(yesAbility.Mods[i].GetType());
            if (yesAbility.Mods[i] is HealModifier)
            {
                yesAbility.Mods[i].Draw();
            }
        }

        if (GUILayout.Button("Add Stun Modifier"))
        {
            StunModifier damage = new StunModifier();
            yesAbility.Mods.Add(damage);
        }


        for (int i = 0; i < yesAbility.Mods.Count; i++)
        {
            if (yesAbility.Mods[i] is StunModifier)
            {
                yesAbility.Mods[i].Draw();
            }
        }

        if(GUILayout.Button("Save"))
        {
            EditorUtility.SetDirty(yesAbility);
        }

        
        
        /*if (teste == TESTE.Create)
        {
            _lastCheckPoint = (Transform)EditorGUILayout.ObjectField("Previous Waypoint", _lastCheckPoint, typeof(Transform), true);
            if(_lastCheckPoint != null)
            {
                if (_lastCheckPoint.GetComponent<WayPoints>().NextWaypoint.Length==0)
                {

                     _possibleWays = EditorGUILayout.IntField("Possible ways",_possibleWays);
                     if (_possibleWays ==1)
                     {
                         if (GUILayout.Button("Create A new Waypoint"))
                         {
                             _newChekPoint = Instantiate(_lastCheckPoint);
                             _newChekPoint.SetParent(_lastCheckPoint.parent);
                            _newChekPoint.GetComponent<WayPoints>().SlowDown = false;
                            _newChekPoint.GetComponent<WayPoints>().HasATurn = false;
                            _newChekPoint.GetComponent<WayPoints>().Stop = false;
                             _newChekPoint.name = "WayPoint " + _lastCheckPoint.parent.childCount;
                             _lastCheckPoint.GetComponent<WayPoints>().NextWaypoint =new Transform[1] ;
                             _lastCheckPoint.GetComponent<WayPoints>().NextWaypoint[0] = _newChekPoint;
                             _lastCheckPoint = _newChekPoint;

                         }
                         if(_newChekPoint != null)
                        {
                            Selection.objects = new Object[]
                            {
                                 _newChekPoint.gameObject
                            };

                        }
                         
                     }
                     else
                     {
                         if (GUILayout.Button("Create A new Waypoint"))
                         {
                             _lastCheckPoint.GetComponent<WayPoints>().NextWaypoint = new Transform[_possibleWays];
                                                        List<GameObject> _templist2 = new List<GameObject>();

                             for (int i = 0; i < _possibleWays; i++)
                             {
                                  _newChekPoint = Instantiate(_lastCheckPoint);
                                  _newChekPoint.GetComponent<WayPoints>().NextWaypoint = new Transform[0];
                                  _newChekPoint.SetParent(_lastCheckPoint.parent);
                                _newChekPoint.GetComponent<WayPoints>().SlowDown = false;
                                _newChekPoint.GetComponent<WayPoints>().HasATurn = false;
                                _newChekPoint.GetComponent<WayPoints>().Stop = false;
                                _templist2.Add(_newChekPoint.gameObject);

                                _newChekPoint.name = "WayPoint " + _lastCheckPoint.parent.childCount;                      
                                 _lastCheckPoint.GetComponent<WayPoints>().NextWaypoint[i] = _newChekPoint;
        
                             }
                             _lastCheckPoint = _newChekPoint;

                            if (_newChekPoint != null)
                            {
                                GameObject[] arrayOfGameObjects = _templist2.ToArray();
                                Selection.objects = arrayOfGameObjects;


                            }

                        }
                     }
                }
                else
                {
                    _possibleWays = EditorGUILayout.IntField("Possible ways", _possibleWays);

                    if (_possibleWays == 1)
                    {
                        if (GUILayout.Button("Create A new Waypoint"))
                        {
                            _newChekPoint = Instantiate(_lastCheckPoint);
                            _newChekPoint.SetParent(_lastCheckPoint.parent);
                            _newChekPoint.name = "WayPoint " + _lastCheckPoint.parent.childCount;
                            _newChekPoint.GetComponent<WayPoints>().NextWaypoint = new Transform[0];
                            List<Transform> _templist = new List<Transform>();

                            int routes = _lastCheckPoint.GetComponent<WayPoints>().NextWaypoint.Length;                              ;
                            for (int i = 0; i < routes; i++)
                            {
                                _templist.Add( _lastCheckPoint.GetComponent<WayPoints>().NextWaypoint[i]);
                            }
                            _templist.Add(_newChekPoint);
                            
                            _lastCheckPoint.GetComponent<WayPoints>().NextWaypoint = new Transform[_templist.Count];

                            for (int i = 0; i < routes+_possibleWays; i++)
                            {
                                _lastCheckPoint.GetComponent<WayPoints>().NextWaypoint[i] = _templist[i];

                            }
                            
                            _lastCheckPoint = _newChekPoint;
                            if (_newChekPoint != null)
                            {
                                Selection.objects = new Object[]
                                {
                                 _newChekPoint.gameObject
                                };

                            }
                        }
                  


                    }
                    else
                    {
                        if (GUILayout.Button("Create A new Waypoint"))
                        {
                            int routes = _lastCheckPoint.GetComponent<WayPoints>().NextWaypoint.Length;
                            List<Transform> _templist = new List<Transform>();
                            List<GameObject> _templist2 = new List<GameObject>();
                            for (int i = 0; i < routes; i++)
                            {
                                _templist.Add( _lastCheckPoint.GetComponent<WayPoints>().NextWaypoint[i]);
                                
                            }
                                for (int i = 0; i < _possibleWays; i++)
                                {
                                    _newChekPoint = Instantiate(_lastCheckPoint);
                                    _templist.Add(_newChekPoint);
                                    _templist2.Add(_newChekPoint.gameObject);
                                    _newChekPoint.GetComponent<WayPoints>().NextWaypoint = new Transform[_lastCheckPoint.GetComponent<WayPoints>().NextWaypoint.Length + _possibleWays];
                                    _newChekPoint.SetParent(_lastCheckPoint.parent);
                                _newChekPoint.GetComponent<WayPoints>().SlowDown = false;
                                _newChekPoint.GetComponent<WayPoints>().HasATurn = false;
                                    _newChekPoint.GetComponent<WayPoints>().Stop = false;
                                    _newChekPoint.name = "WayPoint " + _lastCheckPoint.parent.childCount;

                                }
                         


                            _lastCheckPoint.GetComponent<WayPoints>().NextWaypoint = new Transform[_templist.Count];

                            for (int i = 0; i < routes+_possibleWays; i++)
                            {
                                _lastCheckPoint.GetComponent<WayPoints>().NextWaypoint[i] = _templist[i];

                            }
                                //if (newChekPoint != null)
                                //{
                                //    GameObject[] arrayOfGameObjects = _templist2.ToArray();
                                //    Selection.objects = arrayOfGameObjects;
                               



                                //}
                          
                            _lastCheckPoint = _newChekPoint;
                         
                        }
                    }
                }
            }
        } */ 
    }
}
