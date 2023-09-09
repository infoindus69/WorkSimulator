using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;

namespace WorkSimulator
{
    public partial class FrmWorker : Form
    {
        CancellationTokenSource source;
        public FrmWorker()
        {
            InitializeComponent();
        }

        private void FrmWorker_Load(object sender, EventArgs e)
        {
            numInterval.Maximum = 600;
            numInterval.Minimum = 2;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int interval = (int)this.numInterval.Value;

            if (interval <= 0) interval = 5;

            var task = Task.Run(() =>
            {
                source = new CancellationTokenSource();
                btnStart.BackColor = Color.Green;
                do
                {
                    //int MaxWidth = SystemInformation.VirtualScreen.Width;
                    //int MaxHeight = SystemInformation.VirtualScreen.Height;

                    //int MaxWidth = SystemInformation.VirtualScreen.Width;
                    //int MaxHeight = SystemInformation.VirtualScreen.Height;

                    int MaxWidth = 65535;
                    int MaxHeight = 65535;

                    int H = new Random().Next(0, MaxHeight);
                    int L = new Random().Next(0, MaxWidth);
                    new InputSimulator().Mouse.MoveMouseToPositionOnVirtualDesktop(L, H);
                    //new InputSimulator().Mouse.MoveMouseTo(L, L);

                    var ret = Task.Delay(interval * 1000, source.Token);
                    try
                    {
                        ret.Wait();
                    }
                    catch (Exception ex)
                    {
                        //we get a normal error when task is Canceled
                        //MessageBox.Show(ex.Message);
                    }

                } while (!source.IsCancellationRequested);

                source.Dispose();

                //bStop = false;

                btnStart.BackColor = Button.DefaultBackColor;
            }
            );
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            source.Cancel();
        }
    }
}
