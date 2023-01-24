# How to get the tapped custom appointment details in Xamarin.Forms Schedule (SfSchedule)

You can get the tapped appointment details while tapping the inline appointment of a month by using the [MonthInlineAppointmentTapped](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfSchedule.XForms.SfSchedule.html#Syncfusion_SfSchedule_XForms_SfSchedule_MonthInlineAppointmentTapped) event in Xamarin [SfSchedule](https://www.syncfusion.com/xamarin-ui-controls/xamarin-scheduler).

**C#**

Create a custom class Meeting with mandatory fields From, To and EventName.
```
public class Meeting
{
    public string EventName { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public Color Color { get; set; }
}
```
**C#**

Create a ViewModel class and add the appointment details.
```
public class SchedulerViewModel : INotifyPropertyChanged
{
    private ObservableCollection<Meeting> meetings;
    private List<Color> colorCollection;
    private List<string> currentDayMeetings;
    public SchedulerViewModel()
    {
        this.Meetings = new ObservableCollection<Meeting>();
        this.AddAppointmentDetails();
        this.AddAppointments();
    }
    private void AddAppointmentDetails()
    {
        this.currentDayMeetings = new List<string>();
        this.currentDayMeetings.Add("General Meeting");
        this.currentDayMeetings.Add("Plan Execution");
        this.currentDayMeetings.Add("Project Plan");
        
        this.colorCollection = new List<Color>();
        this.colorCollection.Add(Color.FromHex("#FFA2C139"));
        this.colorCollection.Add(Color.FromHex("#FFD80073"));
        this.colorCollection.Add(Color.FromHex("#FF339933"));        
    }
    private void AddAppointments()
    {
        var today = DateTime.Now.Date;
        var random = new Random();
        for (int month = -1; month < 2; month++)
        {
            for (int day = -5; day < 5; day++)
            {
                for (int count = 0; count < 2; count++)
                {
                    var meeting = new Meeting();
                    meeting.From = today.AddMonths(month).AddDays(random.Next(1, 28)).AddHours(random.Next(9, 18));
                    meeting.To = meeting.From.AddHours(1);
                    meeting.EventName = this.currentDayMeetings[random.Next(7)];
                    meeting.Color = this.colorCollection[random.Next(14)];
                    this.Meetings.Add(meeting);
                }
            }
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;
    private void RaiseOnPropertyChanged(string propertyName)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```
**XAML**

Bind the appointments to a schedule by using the DataSource property. You can map the properties of Meeting class with SfSchedule by using the AppointmentMapping.
```
<ContentPage.Content>
    <Grid>
        <schedule:SfSchedule x:Name="Schedule"
                                DataSource="{Binding Meetings}"
                                ScheduleView="MonthView">
            <schedule:SfSchedule.AppointmentMapping>
                <schedule:ScheduleAppointmentMapping
                    ColorMapping="Color"
                    EndTimeMapping="To"
                    StartTimeMapping="From"
                    SubjectMapping="EventName"
                    />
            </schedule:SfSchedule.AppointmentMapping>
 
            <schedule:SfSchedule.BindingContext>
                <local:SchedulerViewModel/>
            </schedule:SfSchedule.BindingContext>
        </schedule:SfSchedule>
    </Grid>
</ContentPage.Content>
    <ContentPage.Behaviors>
        <local:SchedulerPageBehavior/>
    </ContentPage.Behaviors>
```
**C#**

In MonthInlineAppointmentTapped event, you need to cast the custom appointments to get the tapped appointment details.
```
public class SchedulerPageBehavior : Behavior<ContentPage>
{
    SfSchedule schedule;
    protected override void OnAttachedTo(ContentPage bindable)
    {
        base.OnAttachedTo(bindable);
        this.schedule = bindable.Content.FindByName<SfSchedule>("Schedule");
        this.schedule.MonthViewSettings.ShowAgendaView = true;
 
        this.WireEvents();
    }
    private void WireEvents()
    {
        this.schedule.MonthInlineAppointmentTapped += Schedule_MonthInlineAppointmentTapped;
    }
    private void Schedule_MonthInlineAppointmentTapped(object sender, MonthInlineAppointmentTappedEventArgs e)
    {
        if (e.Appointment != null)
        {
            var app = (e.Appointment as Meeting);
            App.Current.MainPage.DisplayAlert(app.EventName, app.From.ToString(), "OK");
 
        }
        else
        {
            App.Current.MainPage.DisplayAlert("", "No Events", "OK");
        }
    }
    protected override void OnDetachingFrom(ContentPage bindable)
    {
        base.OnDetachingFrom(bindable);
        this.UnWireEvents();
    }
    private void UnWireEvents()
    {
        this.schedule.MonthInlineAppointmentTapped += Schedule_MonthInlineAppointmentTapped;
    }
}
```

KB article - [How to get the tapped custom appointment details in Xamarin.Forms Schedule (SfSchedule)](https://www.syncfusion.com/kb/12201/how-to-get-the-tapped-custom-appointment-details-in-xamarin-forms-schedule-sfschedule)
