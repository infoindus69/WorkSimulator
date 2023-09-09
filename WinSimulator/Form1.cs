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

namespace WinSimulator
{
    public partial class Form1 : Form
    {
        //static bool bStop = false;
        static bool bInProgress = false;
        CancellationTokenSource source;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new InputSimulator().Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_E);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new InputSimulator().Keyboard
            .ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_R)
            .Sleep(1000)
            .TextEntry("notepad")
            .Sleep(1000)
            .KeyPress(VirtualKeyCode.RETURN)
            .Sleep(1000)
            .TextEntry("Ceci est un message temporaire ....")
            .TextEntry("Ce message s'auto détruira dans 5 secondes.")
            .Sleep(5000)
            .ModifiedKeyStroke(VirtualKeyCode.MENU, VirtualKeyCode.F4)
            .KeyPress(VirtualKeyCode.VK_N);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new InputSimulator().Keyboard
            .ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_R)
            .Sleep(1000)
            .TextEntry("mspaint")
            .Sleep(1000)
            .KeyPress(VirtualKeyCode.RETURN)
            .Sleep(1000)
            .Mouse
            .LeftButtonDown()
            .MoveMouseToPositionOnVirtualDesktop(65535 / 2, 65535 / 2)
            .LeftButtonUp();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
             var task = Task.Run(() =>
            {
                source = new CancellationTokenSource();
                button5.BackColor = Color.Green;
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

                    var ret =  Task.Delay(5000, source.Token);
                    try
                    {
                        ret.Wait();
                    }
                    catch(Exception ex)
                    {
                        //we get a normal error when task is Canceled
                        //MessageBox.Show(ex.Message);
                    }
                        
                } while (!source.IsCancellationRequested);

                source.Dispose();

                //bStop = false;

                button5.BackColor = Button.DefaultBackColor;
            }
            );

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //bStop = true;
            source.Cancel();
        }
    }
}
