using Syncfusion.SfSchedule.XForms;
using Xamarin.Forms;

namespace ScheduleXamarin
{
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
}


