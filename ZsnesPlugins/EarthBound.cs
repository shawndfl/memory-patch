using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MemoryManager;

namespace ZsnesPlugins
{
    public partial class EarthBound : UserControl, IPlugin
    {
        #region Fields
        private PluginManager _manager;

        #endregion
        public EarthBound()
        {
            InitializeComponent();
        }

        #region IPlugin Members

        public new Control Control
        {
            get { return this; }
        }

        public void Init(PluginManager manager)
        {
            _manager = manager;            
        }

        #endregion
       
        private void SetToHpMax(string playerName)
        {
            string nessMaxHpStr = _manager.ReadValue(playerName, "MaxHP");
            //int nessMaxHp;
            //int.TryParse(nessMaxHpStr, out nessMaxHp);

            _manager.PokeValue(playerName, "In battle HP", nessMaxHpStr);
            _manager.PokeValue(playerName, "HP", nessMaxHpStr);
        }

        private void SetToPpMax(string playerName)
        {
            string nessMaxHpStr = _manager.ReadValue(playerName, "MaxPP");           

            _manager.PokeValue(playerName, "In battle PP", nessMaxHpStr);
            _manager.PokeValue(playerName, "PP", nessMaxHpStr);
        }

        private void SetPlayerStatus(string playerName)
        {            
            _manager.PokeValue(playerName, "Status", "0");
            _manager.PokeValue(playerName, "In battle Status", "0");            
        }

        private void SetEnemyHp(string enemyName)
        {
            _manager.PokeValue(enemyName, "HP", "0");
        }

        #region Control Events
        private void btnMaxHp_Click(object sender, EventArgs e)
        {
            try
            {
                SetToHpMax("Ness");
                SetToHpMax("Poo");
                SetToHpMax("Paula");
                SetToHpMax("Jeff");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnMaxPP_Click(object sender, EventArgs e)
        {
            try
            {
                SetToPpMax("Ness");
                SetToPpMax("Poo");
                SetToPpMax("Paula");
                SetToPpMax("Jeff");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnResetStatus_Click(object sender, EventArgs e)
        {
            try
            {
                SetPlayerStatus("Ness");
                SetPlayerStatus("Poo");
                SetPlayerStatus("Paula");
                SetPlayerStatus("Jeff");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void btnEnemy1Hp_Click(object sender, EventArgs e)
        {
             try
            {
                SetEnemyHp("Enemy");
                SetEnemyHp("Enemy 2");
                SetEnemyHp("Enemy 3");
                SetEnemyHp("Enemy 4");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      
    }
}
