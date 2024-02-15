using TrainingApp.Views;

namespace TrainingApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(TrainingDateTimeSelectView), typeof(TrainingDateTimeSelectView));
            Routing.RegisterRoute(nameof(TrainingRecordListView), typeof(TrainingRecordListView));
            Routing.RegisterRoute(nameof(TrainingMasterView), typeof(TrainingMasterView));
            Routing.RegisterRoute(nameof(TrainingMasterAddView), typeof(TrainingMasterAddView));
            Routing.RegisterRoute(nameof(TrainingRecordAddView), typeof(TrainingRecordAddView));

            this.gridHeader.Add(new TrainingApp.Controls.StopwatchViewControl(), 1);
        }
        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);
            pageTitle.Text = Current.CurrentPage.Title;
        }
    }
}
