using Microsoft.Maui.Dispatching;
using System.Diagnostics;

namespace TrainingApp.Controls;

public partial class StopwatchViewControl : ContentView
{
    /// <summary>
    /// �^�C�}�[
    /// </summary>
	private readonly Stopwatch stopwatch = new Stopwatch();
    /// <summary>
    /// �^�C�}�[�C�x���g
    /// </summary>
    private readonly IDispatcherTimer timer = Application.Current.Dispatcher.CreateTimer();

    public StopwatchViewControl()
	{
		InitializeComponent();
        // 10�~���P�ʂɃC�x���g����
		timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
		timer.Tick += new EventHandler(TimerMethod);
	}
    /// <summary>
    /// Start�{�^���N���b�N
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	private void btnStart_Click(object sender, EventArgs e)
	{
		this.btnStart.IsEnabled = false;
		this.btnStop.IsEnabled = true;
		this.btnClear.IsEnabled = true;

		stopwatch.Start();
		timer.Start();
	}
    /// <summary>
    /// Stop�{�^���N���b�N
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnStop_Click(object sender, EventArgs e)
    {
        btnStart.IsEnabled = true;  //�X�^�[�g�{�^���L��
        btnStop.IsEnabled = true;  //���Z�b�g�{�^���L��
        btnStop.IsEnabled = false;  //�X�g�b�v�{�^������

        timer.Stop();               //�^�C�}�[ �����~
        stopwatch.Stop();           //�X�g�b�v�E�H�b�`��~
    }
    /// <summary>
    /// ���Z�b�g�{�^���N���b�N
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnReset_Click(object sender, EventArgs e)
    {
        btnStart.IsEnabled = true;   //�X�^�[�g�{�^���L��
        btnStop.IsEnabled = false;  //���Z�b�g�{�^������
        btnStop.IsEnabled = false;   //�X�g�b�v�{�^������

        timer.Stop();                //�^�C�}�[ �����~
        stopwatch.Reset();           //�X�g�b�v�E�H�b�`������

        lblTimer.Text = "00:00.00";       //��     ������
        
    }
    private void TimerMethod(object sender, EventArgs e)
	{
		TimeSpan result = stopwatch.Elapsed;
		lblTimer.Text = result.ToString(@"mm\:ss\.ff");
	}
}