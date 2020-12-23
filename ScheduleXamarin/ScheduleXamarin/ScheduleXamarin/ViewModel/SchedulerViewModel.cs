using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace ScheduleXamarin
{
    public class SchedulerViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Meeting> meetings;
        private List<string> currentDayMeetings;
        public SchedulerViewModel()
        {
            this.Meetings = new ObservableCollection<Meeting>();
            this.AddAppointmentDetails();
            this.AddAppointments();
        }
        public ObservableCollection<Meeting> Meetings
        {
            get
            {
                return this.meetings;
            }
            set
            {
                this.meetings = value;
                this.RaiseOnPropertyChanged("Meetings");
            }
        }
        private void AddAppointmentDetails()
        {
            this.currentDayMeetings = new List<string>();
            this.currentDayMeetings.Add("General Meeting");
            this.currentDayMeetings.Add("Plan Execution");
            this.currentDayMeetings.Add("Project Plan");
            this.currentDayMeetings.Add("Consulting");
            this.currentDayMeetings.Add("Support");
            this.currentDayMeetings.Add("Development Meeting");
            this.currentDayMeetings.Add("Scrum");
            this.currentDayMeetings.Add("Project Completion");
            this.currentDayMeetings.Add("Release updates");
            this.currentDayMeetings.Add("Performance Check");
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
                        meeting.Color = Color.FromHex("#889e81");
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
}

  
