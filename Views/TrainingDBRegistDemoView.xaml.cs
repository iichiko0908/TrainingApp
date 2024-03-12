using TrainingApp.Models.DB;

namespace TrainingApp.Views;

public partial class TrainingDBRegistDemoView : ContentPage
{
	public TrainingDBRegistDemoView()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        using (TrainingDBContext context = new TrainingDBContext())
        {
            // トレーニングマスタ
            TrainingMaster trainingMaster = new TrainingMaster() { Part = "胸", EventName = "ベンチプレス", Order = 0 };
            trainingMaster.Insert(context);

            trainingMaster = new TrainingMaster() { Part = "胸", EventName = "インクラインベンチプレス", Order = 1 };
            trainingMaster.Insert(context);
            
            trainingMaster = new TrainingMaster() { Part = "胸", EventName = "ダンベルフライ", Order = 2 };
            trainingMaster.Insert(context);
            
            trainingMaster = new TrainingMaster() { Part = "胸", EventName = "ディップス", Order = 3 };
            trainingMaster.Insert(context);

            trainingMaster = new TrainingMaster() { Part = "背中", EventName = "チンニング", Order = 0 };
            trainingMaster.Insert(context);
            
            trainingMaster = new TrainingMaster() { Part = "背中", EventName = "ラットプルダウン", Order = 1 };
            trainingMaster.Insert(context);
            
            trainingMaster = new TrainingMaster() { Part = "背中", EventName = "プーリーロー", Order = 2 };
            trainingMaster.Insert(context);

            DateTime date = DateTime.Parse(DateTime.Now.AddDays(-21).ToShortDateString());
            TrainingRecordList trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 1, Weight = 60, NumberOfTimes = 10, Created = date };
            trainingRecordList.Insert(context);
            trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 2, Weight = 60, NumberOfTimes = 10, Created = date };
            trainingRecordList.Insert(context);
            trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 3, Weight = 60, NumberOfTimes = 10, Created = date };
            trainingRecordList.Insert(context);
            trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 4, Weight = 60, NumberOfTimes = 10, Created = date };
            trainingRecordList.Insert(context);
            trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 5, Weight = 60, NumberOfTimes = 8, Created = date };
            trainingRecordList.Insert(context);
            trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 6, Weight = 60, NumberOfTimes = 6, Created = date };
            trainingRecordList.Insert(context);

            date = DateTime.Parse(DateTime.Now.AddDays(-14).ToShortDateString());
            trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 1, Weight = 60, NumberOfTimes = 10, Created = date };
            trainingRecordList.Insert(context);
            trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 2, Weight = 60, NumberOfTimes = 10, Created = date };
            trainingRecordList.Insert(context);
            trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 3, Weight = 60, NumberOfTimes = 10, Created = date };
            trainingRecordList.Insert(context);
            trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 4, Weight = 65, NumberOfTimes = 8, Created = date };
            trainingRecordList.Insert(context);
            trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 5, Weight = 60, NumberOfTimes = 8, Created = date };
            trainingRecordList.Insert(context);
            trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 6, Weight = 60, NumberOfTimes = 6, Created = date };
            trainingRecordList.Insert(context);

            date = DateTime.Parse(DateTime.Now.AddDays(-7).ToShortDateString());
            trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 1, Weight = 60, NumberOfTimes = 10, Created = date };
            trainingRecordList.Insert(context);
            trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 2, Weight = 60, NumberOfTimes = 10, Created = date };
            trainingRecordList.Insert(context);
            trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 3, Weight = 70, NumberOfTimes = 10, Created = date };
            trainingRecordList.Insert(context);
            trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 4, Weight = 75, NumberOfTimes = 9, Created = date };
            trainingRecordList.Insert(context);
            trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 5, Weight = 70, NumberOfTimes = 8, Created = date };
            trainingRecordList.Insert(context);
            trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 6, Weight = 70, NumberOfTimes = 6, Created = date };
            trainingRecordList.Insert(context);

            date = DateTime.Parse(DateTime.Now.ToShortDateString());
            trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 1, Weight = 60, NumberOfTimes = 10, Created = date };
            trainingRecordList.Insert(context);
            trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 2, Weight = 60, NumberOfTimes = 10, Created = date };
            trainingRecordList.Insert(context);
            trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 3, Weight = 70, NumberOfTimes = 10, Created = date };
            trainingRecordList.Insert(context);
            trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 4, Weight = 70, NumberOfTimes = 10, Created = date };
            trainingRecordList.Insert(context);
            trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 5, Weight = 70, NumberOfTimes = 8, Created = date };
            trainingRecordList.Insert(context);
            trainingRecordList = new TrainingRecordList() { TrainingMasterId = 1, SetNumber = 6, Weight = 80, NumberOfTimes = 6, Created = date };
            trainingRecordList.Insert(context);
        }
    }
}