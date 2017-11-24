using System;

namespace DoctorAppointments
{
    public class SelectDoctorEventArgs : EventArgs
    {
        public int DoctorID { get; set; }
    }
}