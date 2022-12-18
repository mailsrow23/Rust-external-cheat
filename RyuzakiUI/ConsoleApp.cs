using System;
using System.Collections.Concurrent;
using System.Windows.Forms;
using System.Threading;
using Timer = System.Threading.Timer;

namespace RyuzakiUI
{
    public partial class ConsoleApp : Form
    {
        private readonly IntPtr _memHelper;
        private readonly ConcurrentQueue<string> _messageQueue;
        private readonly Timer _consoleTimer;
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
                // It's generally a good idea to log any exceptions that are caught
                // so that you can troubleshoot issues more easily.
                Console.WriteLine("Exception occurred while logging message: " + message);
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

            // It's generally a good idea to wrap this code in a check to make sure that
            // these objects have been initialized before trying to use them.
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
        /// Clear the
        
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
