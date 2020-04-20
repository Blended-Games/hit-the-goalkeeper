using System;
using System.Security.Cryptography;
using Data;
using UnityEngine;

namespace GUI2
{
    public class UpgradePanel : MonoBehaviour
    {
        #region Singleton

        public static UpgradePanel main;

        private void Awake()
        {
            if (main != null && main != this)
            {
                Destroy(gameObject);
                return;
            }

            main = this;
        }
        #endregion


        public UpgradeObjectsBehaviour damageUpgrade, healthUpgrade;
    }
}