using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


    [Serializable]
    public class PatternAttack
    {
        public string name;
        public List<Vector2Int> position;
        public bool attackEnnemies;
        public bool attackAllies;
        public bool healAllies;


        //public PatternType type;       
        //public enum PatternType
        //{
        //    Normal,
        //    Special
        //}
    }

