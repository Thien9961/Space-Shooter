using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn : MonoBehaviour
{
        private Button button;
        public delegate void Action();
        public Action action;
        private bool isButtonDown = false;

        protected virtual void Start()
        {
            button=GetComponent<Button>();      
        }

        public void OnButtonDown()
        {
            isButtonDown = true;
        }

        public void OnButtonUp()
        {
            isButtonDown = false;
        }

        protected virtual void Update()
        {
            if (isButtonDown)
                action();
        }
}
