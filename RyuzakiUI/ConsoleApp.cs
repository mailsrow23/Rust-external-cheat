using System;
using System.Collections.Concurrent;
using System.Windows.Forms;
using Timer = System.Threading.Timer;
using Overlay = Dissector.Overlay.Canvas;
using System.Threading;

namespace RyuzakiUI
{
    public partial class ConsoleApp : Form
    {
        private IntPtr _memHelper;
        private ConcurrentQueue<string> _messageQueue;
        private Timer _consoleTimer;
        private bool _isRunning;

        public bool IsRunning { get { return _isRunning; }  }

        /// <summary>
        /// Ctor
        /// </summary>
        public ConsoleApp(IntPtr memHelper)
        {
            InitializeComponent();
            InitializeConsoleLog();
            _memHelper = memHelper;
            _isRunning = true;
        }

        /// <summary>
        /// Enqueues a message to the console
        /// </summary>
        /// <param name="message"></param>
        private void LogActivity(string message)
        {
            try
            {
                _messageQueue.Enqueue(message);
            }
            catch (Exception)
            {

            }
        }

        protected override bool ShowWithoutActivation { get { return true; } }

        /// <summary>
        /// Initialize variables related to the console log
        /// </summary>
        private void InitializeConsoleLog()
        {
            _messageQueue = new ConcurrentQueue<string>();

            _consoleTimer = new Timer(new TimerCallback(_consoleTimer_Elapsed));

            _consoleTimer.Change(1000, 1000);
        }

        /// <summary>
        /// Helps control opening more than one form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConsoleApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            _isRunning = false;

            if (Overlay.GameObjectManager != null)
            {
                Overlay.GameObjectManager.StopDump();

                Overlay.UnsubscribeToLocalPlayerUpdates();

                Overlay.Stop();
            }
        }

        /// <summary>
        /// Starts dumping objects based on what we want to see (active or tagged objects)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _button_StartObjDump_Click(object sender, EventArgs e)
        {
            /* Get our GOM and collect the objects we want to track */
            Overlay.StartManagingGameObjects(_checkBox_EnableDebugging.Checked, _memHelper, LogActivity);

            /* Draw entities that we are managing/tracking */
            Overlay.RunDxForm();
        }

        /// <summary>
        /// Clear the console
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _button_Clear_Dump_Click(object sender, EventArgs e)
        {
            try
            {
                listBox_Console.Items.Clear();
            }
            catch (Exception)
            {
                
            }
        }

        /// <summary>
        /// Stop our dumps (gives us the ability to stop the dump and see something useful in the console if necessary)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _button_StopObjDump_Click(object sender, EventArgs e)
        {
            if (Overlay.GameObjectManager != null)
            {
                Overlay.GameObjectManager.StopDump();

                Overlay.UnsubscribeToLocalPlayerUpdates();

                Overlay.Stop();
            }
        }

        /// <summary>
        /// Dequeue messages from the message queue, write them into the listbox and then select the last inserted item
        /// This is signifcantly faster than writing text to a textbox.
        /// </summary>
        /// <param name="sender"></param>
        private void _consoleTimer_Elapsed(object sender)
        {
            try
            {
                var message = "";
                while (_messageQueue.TryDequeue(out message))
                {
                    listBox_Console.Invoke((MethodInvoker)(() => listBox_Console.Items.Add(message)));
                    listBox_Console.Invoke((MethodInvoker)(() => listBox_Console.SelectedIndex = listBox_Console.Items.Count - 1));
                    listBox_Console.Invoke((MethodInvoker)(() => listBox_Console.SelectedIndex = -1));
                }
            }
            catch (Exception)
            {

            }
        }

        #region Checkbox Toggles

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _checkBox_ToggleAnimals_CheckedChanged(object sender, EventArgs e)
        {
            if (Overlay.GameObjectManager != null)
            {
                if (_checkBox_ToggleAnimals.Checked == false)
                {
                    Overlay.GameObjectManager.StopOrStartCollectingAnimals(false);
                    Overlay.StopDrawingAnimals();
                }
                else
                {
                    Overlay.GameObjectManager.StopOrStartCollectingAnimals(true);
                    Overlay.DrawAnimals();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _checkBox_ToggleMilitaryCrates_CheckedChanged(object sender, EventArgs e)
        {
            if (Overlay.GameObjectManager != null)
            {
                if (_checkBox_ToggleMilitaryCrates.Checked == false)
                {
                    Overlay.GameObjectManager.StopOrStartCollectingMilitaryCrates(false);
                    Overlay.StopDrawingMilitaryCrates();
                }
                else
                {
                    Overlay.GameObjectManager.StopOrStartCollectingMilitaryCrates(true);
                    Overlay.DrawMilitaryCrates();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _checkBox_ToggleNormalCrate_CheckedChanged(object sender, EventArgs e)
        {
            if (Overlay.GameObjectManager != null)
            {
                if (_checkBox_ToggleNormalCrate.Checked == false)
                {
                    Overlay.GameObjectManager.StopOrStartCollectingWoodenLootCrates(false);
                    Overlay.StopDrawingWoodenLootCrates();
                }
                else
                {
                    Overlay.GameObjectManager.StopOrStartCollectingWoodenLootCrates(true);
                    Overlay.DrawWoodenLootCrates();
                }
            }
        }

        /// <summary>
        /// Enable or disable debugging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _checkBox_EnableDebugging_CheckedChanged(object sender, EventArgs e)
        {
            if (Overlay.GameObjectManager != null)
            {
                if (_checkBox_EnableDebugging.Checked)
                {
                    Overlay.GameObjectManager.ToggleDebugging(true);
                }
                else
                {
                    Overlay.GameObjectManager.ToggleDebugging(false);
                }
            }
        }

        /// <summary>
        /// Toggles the drawing for sulfur nodes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _checkBox_ToggleSulfur_CheckedChanged(object sender, EventArgs e)
        {
            if (Overlay.GameObjectManager != null)
            {
                if (_checkBox_ToggleSulfur.Checked == false)
                {
                    Overlay.GameObjectManager.StopOrStartCollectingSulfurNodes(false); /* stop */
                    Overlay.StopDrawingSulfurNodes();
                }
                else
                {
                    Overlay.GameObjectManager.StopOrStartCollectingSulfurNodes(true); /* start */
                    Overlay.DrawSulfurNodes();
                }
            }
        }

        /// <summary>
        /// Stop or start drawing players
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _checkBox_TogglePlayers_CheckedChanged(object sender, EventArgs e)
        {
            if (Overlay.GameObjectManager != null)
            {
                if (_checkBox_TogglePlayers.Checked == false)
                {
                    Overlay.GameObjectManager.StopOrStartCollectingPlayers(false);
                    Overlay.StopDrawingPlayers();
                }
                else
                {
                    Overlay.GameObjectManager.StopOrStartCollectingPlayers(true);
                    Overlay.DrawPlayers();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _checkBox_ToggleMetal_CheckedChanged(object sender, EventArgs e)
        {
            if (Overlay.GameObjectManager != null)
            {
                if (_checkBox_ToggleMetal.Checked == false)
                {
                    Overlay.GameObjectManager.StopOrStartCollectingMetalNodes(false);
                    Overlay.StopDrawingMetalNodes();
                }
                else
                {
                    Overlay.GameObjectManager.StopOrStartCollectingMetalNodes(true);
                    Overlay.DrawMetalNodes();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _checkBox_ToggleStone_CheckedChanged(object sender, EventArgs e)
        {
            if (Overlay.GameObjectManager != null)
            {
                if (_checkBox_ToggleStone.Checked == false)
                {
                    Overlay.GameObjectManager.StopOrStartCollectingStoneNodes(false);
                    Overlay.StopDrawingStoneNodes();
                }
                else
                {
                    Overlay.GameObjectManager.StopOrStartCollectingStoneNodes(true);
                    Overlay.DrawStoneNodes();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _checkBox_ToggleLargeBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Overlay.GameObjectManager != null)
            {
                if (_checkBox_ToggleLargeBox.Checked == false)
                {
                    Overlay.GameObjectManager.StopOrStartCollectingStorageContainers(false);
                    Overlay.StopDrawingStorageContainers();
                }
                else
                {
                    Overlay.GameObjectManager.StopOrStartCollectingStorageContainers(true);
                    Overlay.DrawStorageContainers();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _checkBox_ToggleToolCupboard_CheckedChanged(object sender, EventArgs e)
        {
            if (Overlay.GameObjectManager != null)
            {
                if (_checkBox_ToggleLargeBox.Checked == false)
                {
                    Overlay.GameObjectManager.StopOrStartCollectingToolCupboards(false);
                    Overlay.StopDrawingToolCupboards();
                }
                else
                {
                    Overlay.GameObjectManager.StopOrStartCollectingToolCupboards(true);
                    Overlay.DrawToolCupboards();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _checkBox_ToggleHemp_CheckedChanged(object sender, EventArgs e)
        {
            if (Overlay.GameObjectManager != null)
            {
                if (_checkBox_ToggleLargeBox.Checked == false)
                {
                    Overlay.GameObjectManager.StopOrStartCollectingHempNodes(false);
                    Overlay.StopDrawingHempNodes();
                }
                else
                {
                    Overlay.GameObjectManager.StopOrStartCollectingHempNodes(true);
                    Overlay.DrawHempNodes();
                }
            }
        }

        #endregion


    }
}