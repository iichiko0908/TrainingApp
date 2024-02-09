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

        }
    }
}
