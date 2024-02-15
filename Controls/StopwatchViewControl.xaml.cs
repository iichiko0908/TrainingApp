using Microsoft.Maui.Dispatching;
using System.Diagnostics;

namespace TrainingApp.Controls;

public partial class StopwatchViewControl : ContentView
{
    /// <summary>
    /// タイマー
    /// </summary>
	private readonly Stopwatch stopwatch = new Stopwatch();
    /// <summary>
    /// タイマーイベント
    /// </summary>
    private readonly IDispatcherTimer timer = Application.Current.Dispatcher.CreateTimer();

    public StopwatchViewControl()
	{
		InitializeComponent();
        // 10ミリ単位にイベント発生
		timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
		timer.Tick += new EventHandler(TimerMethod);
	}
    /// <summary>
    /// Startボタンクリック
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
    /// Stopボタンクリック
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnStop_Click(object sender, EventArgs e)
    {
        btnStart.IsEnabled = true;  //スタートボタン有効
        btnStop.IsEnabled = true;  //リセットボタン有効
        btnStop.IsEnabled = false;  //ストップボタン無効

        timer.Stop();               //タイマー 測定停止
        stopwatch.Stop();           //ストップウォッチ停止
    }
    /// <summary>
    /// リセットボタンクリック
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnReset_Click(object sender, EventArgs e)
    {
        btnStart.IsEnabled = true;   //スタートボタン有効
        btnStop.IsEnabled = false;  //リセットボタン無効
        btnStop.IsEnabled = false;   //ストップボタン無効

        timer.Stop();                //タイマー 測定停止
        stopwatch.Reset();           //ストップウォッチ初期化

        lblTimer.Text = "00:00.00";       //分     初期化
        
    }
    private void TimerMethod(object sender, EventArgs e)
	{
		TimeSpan result = stopwatch.Elapsed;
		lblTimer.Text = result.ToString(@"mm\:ss\.ff");
	}
}