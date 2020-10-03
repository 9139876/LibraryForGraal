using System;
using System.Threading;
using System.Windows.Forms;

namespace Graal.Library.Common.GUI
{
    public partial class ProgressBarWindow : Form
    {
        CancellationTokenSource cancelTokenSource;
        CancellationToken token;
        WorkerForProgressBar action;

        public delegate void WorkerForProgressBar(ref int max, ref int cur, ref string text);

        public ProgressBarWindow(WorkerForProgressBar _action, string name = "Progress")
        {
            InitializeComponent();
            action = _action;
            this.Text = name;

            Shown += new EventHandler((object sender, EventArgs e) => new Thread(Start).Start());
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            cancelTokenSource.Cancel();
        }

        private void Start()
        {
            cancelTokenSource = new CancellationTokenSource();
            token = cancelTokenSource.Token;

            int max = 0, cur = 0;
            string text = string.Empty;

            Thread work = new Thread(() => action(ref max, ref cur, ref text));
            work.Start();

            while (work.ThreadState != ThreadState.Aborted && work.ThreadState != ThreadState.Stopped)
            {
                if (token.IsCancellationRequested)
                    work.Abort();
                else
                {
                    if (PBar.Maximum != max)
                        PBar.Invoke(new Action(() => PBar.Maximum = max));
                    if (PBar.Value != cur && cur <= PBar.Maximum)
                        PBar.Invoke(new Action(() => PBar.Value = Math.Min(max, cur)));


                    Lbl_Counter.Invoke(new Action(() => Lbl_Counter.Text = $"Готово {cur} из {max}"));
                    Lbl_Text.Invoke(new Action(() => Lbl_Text.Text = text));
                }
                Thread.Sleep(100);
            }

            this.Invoke(new Action(Close));
        }
    }
}
